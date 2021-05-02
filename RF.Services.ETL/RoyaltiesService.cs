using System;
using System.Collections.Generic;
using System.Linq;
using RF.Domain.Entities;
using RF.Domain.Interfaces.Builders;
using RF.Domain.Interfaces.Filters;
using RF.Domain.Interfaces.ValueObjects;
using RF.Services.ETL.Common;
using RF.Services.ETL.Common.Exceptions;
using RF.Services.Interfaces.ETL;

namespace RF.Services.ETL
{
    public sealed class RoyaltiesService : CommonService, IRoyaltiesService
    {
        public IDistributionAgreementFilterBuilder<DistributionAgreementDetail> QueryBuilderForDistributionAgreementDetail { private get; set; }

        public IDistributionAgreementRankScore<StatementDetail, DistributionAgreementDetail> DistributionAgreementRankScore { private get; set; }

        public void InsertRoyaltiesDistributions(IEnumerable<RoyaltyDistribution> royaltiesList)
        {
            try
            {
                RoyaltyDistributionRepository.InsertRFEntity(royaltiesList);
            }
            catch (Exception)
            {
                throw new ETLException("Couldn't insert royalty distributions on database");
            }
        }

        public IEnumerable<RoyaltyDistribution> RoyaltiesDistributionCalculation(IEnumerable<StatementDetail> statementList)
        {
            var royaltiesDistributionsToInsert = new List<RoyaltyDistribution>();
            foreach (var statement in statementList)
            {
                var royaltiesDistribution = this.RoyaltiesForStatementDetail(statement);
                royaltiesDistributionsToInsert.AddRange(royaltiesDistribution);
            }

            return royaltiesDistributionsToInsert;
        }

        /// <summary>
        /// Royalties validation and calculation for single statement detail.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        /// <exception cref="ETLException">
        /// The statement with the Id " + statement.Id + " doesn't have a ISRC associated
        /// or
        /// The song for the ISRC " + statementIsrcId + " doesn't exist
        /// or
        /// The song with the Id " + songIdThatMatchISRC + " doesn't have a writer share associated
        /// </exception>
        private IEnumerable<RoyaltyDistribution> RoyaltiesForStatementDetail(StatementDetail statement)
        {
            var statementIsrcId = statement.ISRCId;
            //If a statement has a ISRC id that is unknown return null
            if (!statementIsrcId.HasValue)
            {
                throw new ETLException("The statement with the Song Id " + statement.SongId + " doesn't have a ISRC associated");
            }

            // Get SongId from the corresponding ISRC
            // A single ISRC corresponds only to a single song
            var songIdThatMatchIsrc = IsrcRepository.Single(x => x.Id == statementIsrcId)?.SongId;

            if (!songIdThatMatchIsrc.HasValue)
            {
                throw new ETLException("The song for the ISRC " + statementIsrcId + " doesn't exist");
            }

            var controlledWriterSharesForSong = CalculateControlledWriterSharesForSong(songIdThatMatchIsrc);

            var controlledShareTotalPercentage = controlledWriterSharesForSong.Sum(x => x.Share);

            var royaltyDistributionForStatement = WriterPublisherDistribution(statement, controlledWriterSharesForSong, controlledShareTotalPercentage);

            return royaltyDistributionForStatement;
        }

        /// <summary>
        /// Calculates the controlled writer shares for song.
        /// </summary>
        /// <param name="songIdThatMatchISRC">The song identifier that match isrc.</param>
        /// <returns></returns>
        /// <exception cref="ETLException">The song with the Id " + songIdThatMatchISRC +
        ///                                              " doesn't have a writer share associated</exception>
        private List<WriterShare> CalculateControlledWriterSharesForSong(int? songIdThatMatchISRC)
        {
            //Get all records in WriterShare that match the song ID
            var controlledWriterSharesForSong = WriterShareRepository
                .GetAll(x => x.SongId == songIdThatMatchISRC && x.Writer.Controlled, null, true, i => i.Writer).ToList();

            if (!controlledWriterSharesForSong.Any())
            {
                throw new ETLException("The song with the Id " + songIdThatMatchISRC +
                                       " doesn't have a writer share associated");
            }

            return controlledWriterSharesForSong;
        }

        /// <summary>
        /// Writers the publisher distribution.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <param name="controlledWriterSharesForSong">The controlled writer shares for song.</param>
        /// <param name="controlledShareTotalPercentage">The controlled share total percentage.</param>
        /// <returns></returns>
        private IEnumerable<RoyaltyDistribution> WriterPublisherDistribution(StatementDetail statement, IEnumerable<WriterShare> controlledWriterSharesForSong,
            double controlledShareTotalPercentage)
        {
            var royaltyDistributionForStatement = new List<RoyaltyDistribution>();
            var statementId = statement.Id;
            foreach (var writer in controlledWriterSharesForSong)
            {
                var writerId = writer.WriterId;
                var writerControlledShare = writer.Share / controlledShareTotalPercentage;
                var writerDistributionAgreementId = writer.DistributionAgreementId;
                var writerIncome = writerControlledShare * statement.RoyaltyNetUsd;

                var distributionForWriterAgreement =
                    BuildDistributionForWriterAgreement(writerDistributionAgreementId, statement);

                royaltyDistributionForStatement.AddRange(
                    BuildRoyaltiesDistributionForDifferentPublishers(statementId, writerId, writerIncome, distributionForWriterAgreement));
            }

            return royaltyDistributionForStatement;
        }

        /// <summary>
        /// Builds the distribution for writer agreement.
        /// </summary>
        /// <param name="writerDistributionAgreementId">The writer distribution agreement identifier.</param>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        /// <exception cref="ETLException">
        /// No Agreement Details found for the Agreement Id " +
        ///                                              writerDistributionAgreementId
        /// or
        /// The agreement id " + writerDistributionAgreementId + " doesn't have the Shares correct, the sum doesn't match to 100%
        /// </exception>
        private IEnumerable<DistributionAgreementDetail> BuildDistributionForWriterAgreement(int writerDistributionAgreementId, StatementDetail statement)
        {
            var filterForDistributionAgreementDetail = GetFilterForDistributionAgreement(writerDistributionAgreementId);

            var predicateToSearchTheDistributionAgreementDetail =
                BuildDistributionAgreementPredicate(writerDistributionAgreementId, statement,
                    filterForDistributionAgreementDetail);

            var distributionAgreementDetailMatchingResults =
                DistributionAgreementDetailRepository.GetAll(predicateToSearchTheDistributionAgreementDetail.ExpressionCriteria).ToList();

            if (!distributionAgreementDetailMatchingResults.Any())
            {
                throw new ETLException("No Agreement Details found for the Agreement Id " +
                                       writerDistributionAgreementId);
            }

            var distributionAgreementDetailBestMatchGroup = GetMostRankedDistributionAgreementDetailGroup(
                distributionAgreementDetailMatchingResults, statement).ToList();

            var distributionAgreementForWriterTotalPercentage =
                distributionAgreementDetailBestMatchGroup.Sum(x => x.Share);

            if (Math.Abs(distributionAgreementForWriterTotalPercentage - 1) <= 0.0)
            {
                return distributionAgreementDetailBestMatchGroup;
            }

            throw new ETLException("The agreement id " + writerDistributionAgreementId + " doesn't have the Shares correct, the sum doesn't match to 100%");
        }

        /// <summary>
        /// Gets the filter for distribution agreement.
        /// </summary>
        /// <param name="distributionAgreementId">The distribution agreement identifier.</param>
        /// <returns></returns>
        /// <exception cref="ETLException">Couldn't find distribution filter for the distribution agreement with the Id " +
        ///                        distributionAgreementId</exception>
        private DistributionAgreementFilter GetFilterForDistributionAgreement(int distributionAgreementId)
        {
            var distributionAgreementFilter = DistributionAgreementRepository
                    .Single(x => x.Id == distributionAgreementId, null, true,
                        x => x.DistributionAgreementFilter)
                    ?.DistributionAgreementFilter;

            if (distributionAgreementFilter != null)
            {
                return distributionAgreementFilter;
            }

            throw new ETLException(
                "Couldn't find distribution filter for the distribution agreement with the Id " +
                distributionAgreementId);
        }

        /// <summary>
        /// Builds the distribution agreement predicate.
        /// </summary>
        /// <param name="distributionAgreementId">The distribution agreement identifier.</param>
        /// <param name="statement">The statement.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        private ICreteria<DistributionAgreementDetail> BuildDistributionAgreementPredicate(int distributionAgreementId,
            StatementDetail statement, DistributionAgreementFilter filter)
        {
            var predicateForQuery = QueryBuilderForDistributionAgreementDetail
                .Start()
                .WithDistributionAgreement(distributionAgreementId)
                .WithEpisode(filter.Episode, statement.EpisodeId)
                .WithLabel(filter.Label, statement.LabelId)
                .WithPlatformTier(filter.PlatformTier, statement.PlatformTierId)
                .WithPublisher(filter.Publisher, statement.PublisherId)
                .WithRoyaltyType(filter.RoyaltyType, statement.RoyaltyTypeId)
                .WithSociety(filter.Society, statement.SocietyId)
                .WithRoyaltyTypeGroup(filter.RoyaltyTypeGroup, statement.RoyaltyTypeGroupId)
                .WithRegion(filter.Region, statement.RegionId)
                .WithSource(filter.Source, statement.SourceId)
                .WithProductionType(filter.ProductionType, statement.ProductionTypeId)
                .WithTerritory(filter.Territory, statement.TerritoryId)
                .WithPlatformType(filter.PlatformType, statement.PlatformTypeId)
                .WithTerritory(filter.Territory, statement.TerritoryId)
                .WithSource(filter.Source, statement.SourceId)
                .Build();

            return predicateForQuery;
        }

        /// <summary>
        /// Gets the most ranked distribution agreement detail group.
        /// </summary>
        /// <param name="filteredResults">The filtered results.</param>
        /// <param name="statementToCompareWith">The statement to compare with.</param>
        /// <returns></returns>
        /// <exception cref="ETLException">Couldn't calculate the most valuable filter</exception>
        private IEnumerable<DistributionAgreementDetail> GetMostRankedDistributionAgreementDetailGroup(
            IEnumerable<DistributionAgreementDetail> filteredResults, StatementDetail statementToCompareWith)
        {
            var filteredProperties = QueryBuilderForDistributionAgreementDetail.GetFilteredKeys();

            var distributionAgreementDetails = filteredResults.ToList();

            var mostRankedRow = DistributionAgreementRankScore.CalculateMostRankedFilterGroup(distributionAgreementDetails, statementToCompareWith, filteredProperties);

            return distributionAgreementDetails.Where(x => x.AgreementGroup == mostRankedRow.AgreementGroup);
        }

        /// <summary>
        /// Builds the royalties distribution for different publishers.
        /// </summary>
        /// <param name="statementId">The statement identifier.</param>
        /// <param name="writerId">The writer identifier.</param>
        /// <param name="writerIncome">The writer income.</param>
        /// <param name="distributionForWriterAgreement">The distribution for writer agreement.</param>
        /// <returns></returns>
        private IEnumerable<RoyaltyDistribution> BuildRoyaltiesDistributionForDifferentPublishers(int statementId,
            int writerId, double writerIncome, IEnumerable<DistributionAgreementDetail> distributionForWriterAgreement)
        {
            foreach (var distribution in distributionForWriterAgreement)
            {
                yield return new RoyaltyDistribution()
                {
                    StatementDetailId = statementId,
                    RecipientId = distribution.RecipientId,
                    WriterId = writerId,
                    Amount = writerIncome * distribution.Share,
                    Rate = distribution.Rate
                };
            }
        }
    }
}