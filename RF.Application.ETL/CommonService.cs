using System;
using System.Runtime.CompilerServices;
using RF.Application.Interfaces.Exceptions;
using RF.Domain.Entities;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Repositories.Generic;
using RF.Domain.Interfaces.UnitOfWork;

[assembly: InternalsVisibleTo("RF.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace RF.Application.ETL
{
    public abstract class CommonService
    {
        public IUnitOfWork UnitOfWork { protected get; set; }

        public IReadWriteRepository<EpisodeAlias> EpisodeAliasRepository { protected get; set; }
        public IReadWriteRepository<ISRC> IsrcRepository { protected get; set; }
        public IReadWriteRepository<ISWC> IswcRepository { protected get; set; }
        public IReadWriteRepository<LabelAlias> LabelAliasRepository { protected get; set; }
        public IReadWriteRepository<PaymentReceived> PaymentReceivedRepository { protected get; set; }
        public IReadWriteRepository<PlatformTierAlias> PlatformAliasRepository { protected get; set; }
        public IReadWriteRepository<ProductionTitleAlias> ProductionTitleAliasRepository { protected get; set; }
        public IReadWriteRepository<PublisherAlias> PublisherAliasRepository { protected get; set; }
        public IReadWriteRepository<RoyaltyTypeAlias> RoyaltyTypeAliasRepository { protected get; set; }
        public IReadWriteRepository<SocietyAlias> SocietyAliasRepository { protected get; set; }
        public IReadWriteRepository<SongAlias> SongAliasRepository { protected get; set; }
        public IReadWriteRepository<Source> SourceRepository { protected get; set; }
        public IReadWriteRepository<SourceSongCode> SourceSongCodeRepository { protected get; set; }
        public IReadWriteRepository<StatementDetail> StatementDetailRepository { protected get; set; }
        public IReadWriteRepository<StatementHeader> StatementHeaderRepository { protected get; set; }
        public IReadWriteRepository<Template> TemplateRepository { protected get; set; }
        public IReadWriteRepository<TerritoryAlias> TerritoryAliasRepository { protected get; set; }
        public IReadWriteRepository<WriterShare> WriterShareRepository { protected get; set; } 
        public IReadWriteRepository<RoyaltyDistribution> RoyaltyDistributionRepository { protected get; set; }
        public IReadWriteRepository<DistributionAgreementDetail> DistributionAgreementDetailRepository { protected get; set; }
        public IReadWriteRepository<DistributionAgreement> DistributionAgreementRepository { protected get; set; }

        /// <summary>
        /// Commits the database pending changes.
        /// </summary>
        /// <returns></returns>
        internal virtual bool CommitDatabasePendingChanges()
        {
            try
            {
                return UnitOfWork.Commit();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the type of the repository by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="RFServiceException">Repository not found for the requested type " + typeof(T).Name</exception>
        internal virtual IReadWriteRepository<T> GetRepositoryByType<T>() where T : class
        {
            if (typeof(T) == typeof(EpisodeAlias)) return (IReadWriteRepository<T>)EpisodeAliasRepository;
            if (typeof(T) == typeof(ISRC)) return (IReadWriteRepository<T>)IsrcRepository;
            if (typeof(T) == typeof(ISWC)) return (IReadWriteRepository<T>)IswcRepository;
            if (typeof(T) == typeof(LabelAlias)) return (IReadWriteRepository<T>)LabelAliasRepository;
            if (typeof(T) == typeof(PaymentReceived)) return (IReadWriteRepository<T>)PaymentReceivedRepository;
            if (typeof(T) == typeof(PlatformTierAlias)) return (IReadWriteRepository<T>)PlatformAliasRepository;
            if (typeof(T) == typeof(ProductionTitleAlias)) return (IReadWriteRepository<T>)ProductionTitleAliasRepository;
            if (typeof(T) == typeof(PublisherAlias)) return (IReadWriteRepository<T>)PublisherAliasRepository;
            if (typeof(T) == typeof(RoyaltyTypeAlias)) return (IReadWriteRepository<T>)RoyaltyTypeAliasRepository;
            if (typeof(T) == typeof(SocietyAlias)) return (IReadWriteRepository<T>)SocietyAliasRepository;
            if (typeof(T) == typeof(SongAlias)) return (IReadWriteRepository<T>)SongAliasRepository;
            if (typeof(T) == typeof(Source)) return (IReadWriteRepository<T>)SourceRepository;
            if (typeof(T) == typeof(StatementHeader)) return (IReadWriteRepository<T>)StatementHeaderRepository;
            if (typeof(T) == typeof(TerritoryAlias)) return (IReadWriteRepository<T>)TerritoryAliasRepository;
            if (typeof(T) == typeof(Template)) return (IReadWriteRepository<T>)TemplateRepository;

            throw new RFServiceException("Repository not found for the requested type " + typeof(T).Name);
        }
    }
}