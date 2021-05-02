using System;
using System.Linq;
using System.Linq.Expressions;
using RF.Application.Interfaces.ETL;
using RF.Application.Interfaces.Exceptions;
using RF.Domain.Common;
using RF.Domain.Entities;
using RF.Domain.Enum;

namespace RF.Application.ETL
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
                throw new RFServiceException("Couldn't insert statement header.");
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
                throw new RFServiceException("Couldn't insert statements details.");
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

        public int? FindSongIdBySourceSongCode(string sourceSongCode)
        {
            if (string.IsNullOrEmpty(sourceSongCode))
            {
                return null;
            }
            var songId = SourceSongCodeRepository.Single(x => string.Equals(x.Code, sourceSongCode, StringComparison.CurrentCultureIgnoreCase))?.SongId;
            return songId;
        }

        public int? FindSongIdByAlias(string songName)
        {
            if (string.IsNullOrEmpty(songName))
            {
                return null;
            }

            var songId = SongAliasRepository.Single(x => string.Equals(x.Name, songName, StringComparison.CurrentCultureIgnoreCase))?.SongId;
            return songId;
        }

        public int FindSongIdByAlternativeWay(int? isrcId, int? iswcId, string song)
        {
            int? songId = null;
            if (isrcId.HasValue)
            {
                songId = IsrcRepository.Single(x => x.Id == isrcId.Value)?.SongId;
            }

            if (iswcId.HasValue && songId == null)
            {
                songId = IswcRepository.Single(x => x.Id == iswcId.Value)?.SongId;
            }

            if (songId != null)
            {
                return songId.Value;
            }

            ContextService.AddErrorToETLParsingErrors(ETLErrorEnum.Song, "Couldn't get Song Id for " + song);
            return 0;
        }

        public T FindStatementDetailFieldsByAlias<T>(string alias, ETLErrorEnum errorKey, bool isMandatoryField, Expression<Func<T, bool>> predicate) where T : class
        {
            if (string.IsNullOrEmpty(alias))
            {
                if (isMandatoryField)
                {
                    ContextService.AddErrorToETLParsingErrors(errorKey, "Field Empty. Mandatory, must be filled.");
                }
                return null;
            }

            var entity = GetRepositoryByType<T>().Single(predicate);

            if (entity != null)
            {
                return entity;
            }

            ContextService.AddErrorToETLParsingErrors(errorKey, "Could not find " + alias);
            return null;
        }

        public int? FindCode<T>(string code, int songId) where T : SongCodeIdentifierBase, new()
        {
            int? codeId = null;

            var repository = GetRepositoryByType<T>();

            if (!string.IsNullOrEmpty(code))
            {
                //If code is passed and doesn't exist on database, then we try to create a new code entry on the database and get its id
                codeId = repository.Single(x => string.Equals(x.Code, code, StringComparison.CurrentCultureIgnoreCase))?.Id;
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
                throw new RFServiceException("Couldn't Add new Entry for with  code value " + code + " to Database");
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
                throw new RFServiceException(
                    "Couldn't update code value " + code + " to Database");
            }

            int? codeId = entity.Id;
            return codeId;
        }
    }
}