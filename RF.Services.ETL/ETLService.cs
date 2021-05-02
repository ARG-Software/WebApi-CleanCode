using RF.Domain.Entities;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Parser;
using RF.Domain.Interfaces.ValueObjects;
using RF.Services.ETL.Common;
using RF.Services.ETL.Common.Exceptions;
using RF.Services.Interfaces.ETL;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Server;
using RF.Application.Core.Notifications.Logging;
using RF.Application.Core.UseCases.Episode.Queries.GetEpisodeIdByAlias;
using RF.Application.Core.UseCases.Isrc.Queries.GetISRCBySongId;
using RF.Application.Core.UseCases.Label.Queries.GetLabelIdByAlias;
using RF.Application.Core.UseCases.PlatformTier.Queries.GetPlatformTierAndPlatformTypeIdByAlias;
using RF.Application.Core.UseCases.ProductionTitle.Queries.GetProductionTitleAndTypeIdByAlias;
using RF.Application.Core.UseCases.Publisher.Queries.GetPublisherIdByAlias;
using RF.Application.Core.UseCases.Society.Queries.GetSocietyIdByAlias;
using RF.Application.Core.UseCases.Song.Queries.GetSongIdBySourceSongCode;
using RF.Application.Core.UseCases.Template.Queries.GetSourceIdByTemplateId;
using RF.Application.Core.UseCases.Template.Queries.GetTemplateDefinitionObjectById;
using RF.Domain.Dto;
using RF.Domain.ValueObjects;
using RF.Application.Core.UseCases.RoyaltyType.Queries.GetRoyaltyTypeIdAndRoyaltyTypeGroupIdByAliasAndSourceId;
using RF.Application.Core.UseCases.Song.Queries.GetSongIdByIsrcOrIswcId;
using RF.Application.Core.UseCases.Territory.Queries.GetTerritoryAndRegionIdByAlias;

namespace RF.Services.ETL
{
    public class ETLService : CommonService, IETLService
    {
        public IParser<ParsedStatement> Parser { private get; set; }
        public IStatementService StatementService { get; set; }
        public IRoyaltiesService RoyaltiesService { private get; set; }
        public IETLContextService ContextService { private get; set; }

        public IStatementErrors StatementProcessingErrors { private get; set; }

        public async Task<IEnumerable<IParsedStatement>> ParseStatementFile(int templateId, Stream statementFile)
        {
            if (statementFile == null)
            {
                throw new ETLException("File is null, please correct it and try again");
            }

            var templateDefinitionObject = await Bus.Send(new GetTemplateDefinitionObjectByIdQuery() { Id = templateId });

            var parsedListWithStatementsForRequestedSource =
                await Parser.ConvertStreamFileToObjectList(statementFile, templateDefinitionObject);
            if (parsedListWithStatementsForRequestedSource == null)
            {
                throw new ETLException("Couldn't parse statement file, please correct it and try again");
            }

            return parsedListWithStatementsForRequestedSource;
        }

        public async Task<string> ProcessFileInBackground(string jobName, RoyaltyManagerProcessmentDto royaltiesManagerDto, PerformContext backgroundJob)
        {
            ContextService.SetBackgroundTaskContext(backgroundJob);
            ContextService.SetBackgroundTaskContextTags(royaltiesManagerDto.Tags);
            ContextService.SetPaymentForETLProcessment(royaltiesManagerDto.PaymentId);

            var sourceIdForProcessment = await Bus.Send(new GetSourceIdByTemplateIdQuery { Id = royaltiesManagerDto.TemplateId });

            var statementList = royaltiesManagerDto.StatementList;

            foreach (var statement in statementList)
            {
                await BuildStatementDetail(statement, sourceIdForProcessment);
            }

            if (!StatementProcessingErrors.HasAny())
            {
                return FinalizeStatementDigestion(royaltiesManagerDto.TemplateId);
            }

            await Bus.Publish(new LogStatementErrorsNotification { BackgroundJobContext = ContextService.GetBackgroundTaskContext() });
            throw new ETLException("File not processed successfully. List of errors:" + StatementProcessingErrors.ToString());
        }

        /// <summary>
        /// Finalizes the statement process by taking the last steps to conclude the statement ingestion process.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns></returns>
        private string FinalizeStatementDigestion(int templateId)
        {
            var statementHeader = ProcessStatements(templateId);

            ProcessRoyalties();

            if (ContextService.CheckIfPaymentCanBeReconciled(statementHeader))
            {
                ContextService.MarkPaymentAsReconciled();
            }

            if (!CommitDatabasePendingChanges())
            {
                throw new ETLException("Couldn't commit statements and royalties to database.");
            }

            return "File processed with success";
        }

        /// <summary>
        /// Prepares the statements for database insertion.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        private StatementHeader ProcessStatements(int templateId)
        {
            var statementHeader = StatementService.CreateStatementHeader(templateId);

            StatementService.InsertStatementHeader(statementHeader);

            StatementService.InsertStatementDetails(statementHeader);

            return statementHeader;
        }

        /// <summary>
        /// Prepares the royalties for database insertion.
        /// </summary>
        /// <returns></returns>
        private void ProcessRoyalties()
        {
            var statementDetails = ContextService.GetStatementDetailsToBeInserted();
            var royaltiesDistribution = RoyaltiesService.RoyaltiesDistributionCalculation(statementDetails).ToList();

            if (!royaltiesDistribution.Any())
            {
                throw new ETLException("Couldn't calculate any royalties for current file");
            }

            RoyaltiesService.InsertRoyaltiesDistributions(royaltiesDistribution);
        }

        /// <summary>
        /// Builds the statement detail.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <param name="sourceId"></param>
        private async Task BuildStatementDetail(IParsedStatement statement, int sourceId)
        {
            var newStatement = await FillStatementDetailValues(statement, sourceId);

            ContextService.AddStatementDetailToInsert(newStatement);
        }

        /// <summary>
        /// Fills the in statement detail values.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        private async Task<StatementDetail> FillStatementDetailValues(IParsedStatement statement, int sourceId)
        {
            var paymentReceived = ContextService.GetPaymentForETLProcessment();

            var newStatement = new StatementDetail()
            {
                SourceId = sourceId
            };

            //Mandatory
            var royaltyTypeIdAndRoyaltyTypeGroupId = await Bus.Send(new GetRoyaltyTypeAndRoyaltyTypeGroupIdByAliasAndSourceIdQuery()
            { RoyaltyTypeAlias = statement.RoyaltyType, SourceId = sourceId });
            (newStatement.RoyaltyTypeId, newStatement.RoyaltyTypeGroupId) = (royaltyTypeIdAndRoyaltyTypeGroupId.RoyaltyTypeId, royaltyTypeIdAndRoyaltyTypeGroupId.RoyaltyTypeGroupId);

            //Mandatory
            var territoryAndRegionId = await Bus.Send(new GetTerritoryAndRegionIdByTerritoryAliasQuery() { TerritoryAlias = statement.Territory });
            (newStatement.TerritoryId, newStatement.RegionId) = (territoryAndRegionId.TerritoryId, territoryAndRegionId.RegionId);

            // Mandatory
            newStatement.SongId = await Bus.Send(new GetSongIdBySourceSongCodeOrAliasQuery() { Song = statement.Song, SourceSongCode = statement.SourceSongCode });

            newStatement.SocietyId = await Bus.Send(new GetSocietyIdByAliasQuery() { SocietyAlias = statement.Society });

            newStatement.PublisherId = await Bus.Send(new GetPublisherIdByAliasQuery() { PublisherAlias = statement.Publisher });

            var platformTypeAndPlatformTierId = await Bus.Send(new GetPlatformTierAndPlatformTypeIdByAliasQuery() { PlatformTierAlias = statement.Platform });

            (newStatement.PlatformTierId, newStatement.PlatformTypeId) = (platformTypeAndPlatformTierId.PlatformTierId, platformTypeAndPlatformTierId.PlatformTypeId);

            newStatement.EpisodeId = await Bus.Send(new GetEpisodeIdByAliasQuery() { EpisodeAlias = statement.Episode });

            newStatement.LabelId = await Bus.Send(new GetLabelIdByAliasQuery() { LabelAlias = statement.Label });

            var productionTitleAndTypeId = await Bus.Send(new GetProductionTitleAndTypeIdByAliasQuery() { ProductionTitleAlias = statement.ProductionTitle });

            (newStatement.ProductionTitleId, newStatement.ProductionTypeId) = (productionTitleAndTypeId.ProductionTitleId, productionTitleAndTypeId.ProductionTitleTypeId);

            newStatement.ISRCId = StatementService.FindCode<ISRC>(statement.ISRC, newStatement.SongId);
            newStatement.ISWCId = StatementService.FindCode<ISWC>(statement.ISWC, newStatement.SongId);

            newStatement.PerformanceDate = statement.GetPerformanceDate();
            newStatement.BonusAmount = statement.GetBonusAmount();
            newStatement.RoyaltyNetUsd = statement.GetRoyaltyNetUsdWithExchangeRate(paymentReceived.ExchangeRate);
            newStatement.RoyaltyNetForeign = statement.GetRoyaltyNetUsd();
            newStatement.Quantity = statement.GetQuantity();
            newStatement.UnitRate = statement.GetUnitRate();

            await RetrySongIdFetch(statement, newStatement);

            await RetryIsrcFetch(newStatement);

            return newStatement;
        }

        private async Task RetryIsrcFetch(StatementDetail newStatement)
        {
            if (newStatement.SongId != 0 && newStatement.ISRCId == null)
            {
                newStatement.ISRCId =
                    await Bus.Send(new GetIsrcBySongIdQuery() { SongId = newStatement.SongId });
            }
        }

        private async Task RetrySongIdFetch(IParsedStatement statement, StatementDetail newStatement)
        {
            if (newStatement.SongId == 0)
            {
                newStatement.SongId = await Bus.Send(new GetSongIdByIsrcOrIswcIdQuery()
                {
                    IsrcId = newStatement.ISRCId,
                    IswcId = newStatement.ISWCId,
                    Song = statement.Song,
                    SourceSongCode = statement.SourceSongCode
                });
            }
        }
    }
}