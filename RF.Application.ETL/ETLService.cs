using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RF.Application.Interfaces.ETL;
using RF.Application.Interfaces.Exceptions;
using RF.Application.Services;
using RF.Domain.Entities;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Enum;
using RF.Domain.Interfaces.Parser;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.Application.ETL
{
    public class ETLService : CommonService, IETLService
    {
        public IParser<IParsedStatement> Parser { private get; set; }
        public IStatementService StatementService { get; set; }
        public IRoyaltiesService RoyaltiesService { private get; set; }
        public ITemplateService TemplateService { private get; set; }
        public IETLContextService ContextService { private get; set; }

        /// <summary>
        /// Parses the statement file.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="statementFile">The statement file.</param>
        public async Task<IEnumerable<IParsedStatement>> ParseStatementFile(int templateId, Stream statementFile)
        {
            if (statementFile == null)
            {
                throw new RFServiceException("File is null, please correct it and try again");
            }

            var convertedTemplate = TemplateService.ConvertTemplateToTemplateStructure(templateId);

            var parsedListWithStatementsForRequestedSource =
                await Parser.ConvertStreamFileToObjectList(statementFile, convertedTemplate);
            if (parsedListWithStatementsForRequestedSource == null)
            {
                throw new RFServiceException("Couldn't parse statement file, please correct it and try again");
            }

            return parsedListWithStatementsForRequestedSource;
        }

        /// <summary>
        /// Builds the statements details according to business rules
        /// </summary>
        /// <param name="statementList"></param>
        /// <param name="templateId"></param>
        /// <param name="paymentId"></param>
        public IEnumerable ProcessFile(IEnumerable<IParsedStatement> statementList, int templateId, int paymentId)
        {
            SetContextVariablesForProcessment(templateId, paymentId);

            foreach (var statement in statementList)
            {
                BuildStatementDetail(templateId, statement);
            }

            var errorsDuringETLParsing = ContextService.GetETLParsingErrors();
            return errorsDuringETLParsing.Any()
                ? errorsDuringETLParsing
                : FinalizeStatementDigestion(templateId);
        }

        /// <summary>
        /// Sets the context variables for processment.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <param name="paymentId">The payment identifier.</param>
        private void SetContextVariablesForProcessment(int templateId, int paymentId)
        {
            ContextService.SetPaymentForETLProcessment(paymentId);
            ContextService.SetSourceIdForETLProcessment(templateId);
        }

        /// <summary>
        /// Finalizes the statement process by taking the last steps to conclude the statement ingestion process.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns></returns>
        private IEnumerable FinalizeStatementDigestion(int templateId)
        {
            ProcessStatements(templateId);

            ProcessRoyalties();

            if (ContextService.CheckIfPaymentCanBeReconciled())
            {
                ContextService.MarkPaymentAsReconciled();
            }

            if (!CommitDatabasePendingChanges())
            {
                throw new RFServiceException("Couldn't commit statements and royalties to database.");
            }

            return "File processed with success";
        }

        /// <summary>
        /// Prepares the statements for database insertion.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        private void ProcessStatements(int templateId)
        {
            var statementHeader = StatementService.CreateStatementHeader(templateId);

            StatementService.InsertStatementHeader(statementHeader);

            StatementService.InsertStatementDetails(statementHeader);
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
                throw new RFServiceException("Couldn't calculate any royalties for current file");
            }

            RoyaltiesService.InsertRoyaltiesDistributions(royaltiesDistribution);
        }

        /// <summary>
        /// Builds the statement detail.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="statement">The statement.</param>
        private void BuildStatementDetail(int sourceId, IParsedStatement statement)
        {
            var newStatement = FillStatementDetailValues(sourceId, statement);

            ContextService.AddStatementDetailToInsert(newStatement);
        }

        /// <summary>
        /// Fills the in statement detail values.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="statement">The statement.</param>
        /// <returns></returns>
        private StatementDetail FillStatementDetailValues(int sourceId, IParsedStatement statement)
        {
            var paymentReceived = ContextService.GetPaymentForETLProcessment();

            var newStatement = new StatementDetail
            {
                //Mandatory
                RoyaltyTypeId = StatementService.FindStatementDetailFieldsByAlias<RoyaltyTypeAlias>(statement.RoyaltyType,
                                    ETLErrorEnum.RoyaltyType, true,
                                    (x => string.Equals(x.Name, statement.RoyaltyType,
                                              StringComparison.CurrentCultureIgnoreCase) && x.SourceId == sourceId)
                                )?.RoyaltyTypeId ?? 0,
                //Mandatory
                TerritoryId = StatementService.FindStatementDetailFieldsByAlias<TerritoryAlias>(statement.Territory,
                                  ETLErrorEnum.Territory, true,
                                  (x => string.Equals(x.Name, statement.Territory,
                                      StringComparison.CurrentCultureIgnoreCase))
                              )?.TerritoryId ?? 0,
                // Mandatory
                SongId = StatementService.FindSongIdBySourceSongCode(statement.SourceSongCode) ?? StatementService.FindSongIdByAlias(statement.Song) ?? 0,

                SocietyId = StatementService.FindStatementDetailFieldsByAlias<SocietyAlias>(statement.Society,
                        ETLErrorEnum.Society, false,
                        (x => string.Equals(x.Name, statement.Society, StringComparison.CurrentCultureIgnoreCase)))
                    ?.SocietyId,
                PublisherId = StatementService.FindStatementDetailFieldsByAlias<PublisherAlias>(statement.Publisher,
                        ETLErrorEnum.Publisher, false,
                        (x => string.Equals(x.Name, statement.Publisher, StringComparison.CurrentCultureIgnoreCase)))
                    ?.PublisherId,
                PlatformTierId = StatementService.FindStatementDetailFieldsByAlias<PlatformTierAlias>(statement.Platform,
                        ETLErrorEnum.Platform, false,
                        (x => string.Equals(x.Name, statement.Platform, StringComparison.CurrentCultureIgnoreCase)))
                    ?.PlatformTierId,
                EpisodeId = StatementService.FindStatementDetailFieldsByAlias<EpisodeAlias>(statement.Episode,
                        ETLErrorEnum.Episode, false,
                        (x => string.Equals(x.Name, statement.Episode, StringComparison.CurrentCultureIgnoreCase)))
                    ?.EpisodeId,
                LabelId = StatementService.FindStatementDetailFieldsByAlias<LabelAlias>(statement.Label, ETLErrorEnum.Label,
                        false,
                        (x => string.Equals(x.Name, statement.Label, StringComparison.CurrentCultureIgnoreCase)))
                    ?.LabelId,
                ProductionTitleId = StatementService.FindStatementDetailFieldsByAlias<ProductionTitleAlias>(
                        statement.ProductionTitle, ETLErrorEnum.ProductionTitle, false,
                        (x => string.Equals(x.Name, statement.ProductionTitle,
                            StringComparison.CurrentCultureIgnoreCase)))
                    ?.ProductionTitleId,

                PerformanceDate = statement.GetPerformanceDate(),
                BonusAmount = statement.GetBonusAmount(),
                RoyaltyNetUsd = statement.GetRoyaltyNetUsdWithExchangeRate(paymentReceived.ExchangeRate),
                RoyaltyNetForeign = statement.GetRoyaltyNetUsd(),
                Quantity = statement.GetQuantity(),
                UnitRate = statement.GetUnitRate(),
            };

            newStatement.ISRCId = StatementService.FindCode<ISRC>(statement.ISRC, newStatement.SongId);
            newStatement.ISWCId = StatementService.FindCode<ISWC>(statement.ISWC, newStatement.SongId);

            if (newStatement.SongId == 0)
            {
                newStatement.SongId = StatementService.FindSongIdByAlternativeWay(newStatement.ISRCId, newStatement.ISWCId, statement.Song);
            }

            return newStatement;
        }
    }
}