using System;
using System.Linq;
using RF.Domain.Common;
using RF.Domain.Entities;
using RF.Services.ETL.Common;
using RF.Services.ETL.Common.Exceptions;
using RF.Services.Interfaces.ETL;

namespace RF.Services.ETL
{
    public class StatementService : CommonService, IStatementService
    {
        public IETLContextService ContextService { protected get; set; }

        public bool InsertStatementHeader(StatementHeader statementHeader)
        {
            try
            {
                StatementHeaderRepository.InsertRFEntity(statementHeader);
            }
            catch (Exception)
            {
                throw new ETLException("Couldn't insert statement header.");
            }
            return true;
        }

        public bool InsertStatementDetails(StatementHeader statementHeader)
        {
            try
            {
                var statementDetailsToInsert = ContextService.GetStatementDetailsToBeInserted().ToList();
                statementDetailsToInsert.ForEach(e => e.StatementHeaderId = statementHeader.Id);
                StatementDetailRepository.InsertRFEntity(statementDetailsToInsert);
            }
            catch (Exception)
            {
                throw new ETLException("Couldn't insert statements details.");
            }

            return true;
        }

        public StatementHeader CreateStatementHeader(int templateId)
        {
            var statementDetails = ContextService.GetStatementDetailsToBeInserted().ToList();

            var totalRoyaltyNetUsd = statementDetails.Sum(e => e.RoyaltyNetUsd);
            var totalRoyaltyNetForeign = statementDetails.Sum(e => e.RoyaltyNetForeign);
            var paymentId = ContextService.GetPaymentForETLProcessment().Id;

            var statementHeaderToInsert = new StatementHeader
            {
                TemplateId = templateId,
                TotalLocal = totalRoyaltyNetUsd,
                TotalForeign = totalRoyaltyNetForeign,
                PaymentReceivedId = paymentId
            };
            return statementHeaderToInsert;
        }

        public int? FindCode<T>(string code, int songId) where T : SongCodeIdentifierBase, new()
        {
            int? codeId = null;

            var repository = GetRepositoryByType<T>();

            if (!string.IsNullOrEmpty(code))
            {
                //If code is passed and doesn't exist on database, then we try to create a new code entry on the database and get its id
                codeId = repository.Single(x => string.Equals(x.Code.ToLower(), code.ToLower()))?.Id;
                if (codeId.HasValue)
                {
                    return codeId;
                }
            }

            if (songId != 0 && !string.IsNullOrEmpty(code))
            {
                var entity = repository.Single((x => x.Default && x.SongId == songId));
                if (entity != null && string.IsNullOrEmpty(entity.Code))
                {
                    codeId = UpdateCodeEntity(code, entity);
                }
                else
                {
                    codeId = InsertCodeEntity<T>(code, songId);
                }
            }
            else
            {
                if (songId != 0)
                {
                    codeId = repository.Single(x => x.SongId == songId)?.Id;
                }
            }

            return codeId;
        }

        public int? InsertCodeEntity<T>(string code, int songId) where T : SongCodeIdentifierBase, new()
        {
            var entityToInsert = new T() { Code = code, SongId = songId };
            GetRepositoryByType<T>().InsertRFEntity(entityToInsert);
            if (!CommitDatabasePendingChanges())
            {
                throw new ETLException("Couldn't Add new Entry for with  code value " + code + " to Database");
            }

            int? codeId = entityToInsert.Id;
            return codeId;
        }

        public int? UpdateCodeEntity<T>(string code, T entity) where T : SongCodeIdentifierBase, new()
        {
            entity.Code = code;
            GetRepositoryByType<T>().UpdateRFEntity(entity);
            if (!CommitDatabasePendingChanges())
            {
                throw new ETLException(
                   "Couldn't update code value " + code + " to Database");
            }

            int? codeId = entity.Id;
            return codeId;
        }
    }
}