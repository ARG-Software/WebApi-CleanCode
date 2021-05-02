using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using RF.Domain.Entities;
using RF.Domain.Entities.Core;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Infrastructure.Data.Adapters.EntityFrameworkCore.Contexts;

[assembly:InternalsVisibleTo("RF.UnitTests")]

namespace RF.Infrastructure.Data.Adapters.EntityFrameworkCore
{
    
    public class EFCoreContext : EFBaseContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EFCoreContext"/> class.
        /// Do not use, for test only
        /// </summary>
        public EFCoreContext()
        {

        }
        
        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {
        }

        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CWRFile> CWRFile { get; set; }
        public virtual DbSet<DistributionAgreement> DistributionAgreement { get; set; }
        public virtual DbSet<DistributionAgreementDetail> DistributionAgreementDetail { get; set; }
        public virtual DbSet<DistributionAgreementFilter> DistributionAgreementFilter { get; set; }
        public virtual DbSet<EAN> EAN { get; set; }
        public virtual DbSet<Episode> Episode { get; set; }
        public virtual DbSet<EpisodeAlias> EpisodeAlias { get; set; }
        public virtual DbSet<ISRC> ISRC { get; set; }
        public virtual DbSet<ISWC> ISWC { get; set; }
        public virtual DbSet<Label> Label { get; set; }
        public virtual DbSet<LabelAlias> LabelAlias { get; set; }
        public virtual DbSet<MechanicalLicense> MechanicalLicense { get; set; }
        public virtual DbSet<Payee> Payee { get; set; }
        public virtual DbSet<PaymentReceived> PaymentReceived { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<PlatformTier> PlatformTier { get; set; }
        public virtual DbSet<PlatformTierAlias> PlatformTierAlias { get; set; }
        public virtual DbSet<PlatformType> PlatformType { get; set; }
        public virtual DbSet<ProductionTitle> ProductionTitle { get; set; }
        public virtual DbSet<ProductionTitleAlias> ProductionTitleAlias { get; set; }
        public virtual DbSet<ProductionType> ProductionType { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<PublisherAlias> PublisherAlias { get; set; }
        public virtual DbSet<PublisherType> PublisherType { get; set; }
        public virtual DbSet<Recipient> Recipient { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<RoyaltyDistribution> RoyaltyDistribution { get; set; }
        public virtual DbSet<RoyaltyType> RoyaltyType { get; set; }
        public virtual DbSet<RoyaltyTypeAlias> RoyaltyTypeAlias { get; set; }
        public virtual DbSet<Society> Society { get; set; }
        public virtual DbSet<SocietyAlias> SocietyAlias { get; set; }
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<SongAlias> SongAlias { get; set; }
        public virtual DbSet<Source> Source { get; set; }
        public virtual DbSet<SourceSongCode> SourceSongCode { get; set; }
        public virtual DbSet<StatementDetail> StatementDetail { get; set; }
        public virtual DbSet<StatementHeader> StatementHeader { get; set; }
        public virtual DbSet<Territory> Territory { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<TerritoryAlias> TerritoryAlias { get; set; }
        public virtual DbSet<UPC> UPC { get; set; }
        public virtual DbSet<Writer> Writer { get; set; }
        public virtual DbSet<WriterRoleCode> WriterRoleCode { get; set; }
        public virtual DbSet<WriterShare> WriterShare { get; set; }
    }
}