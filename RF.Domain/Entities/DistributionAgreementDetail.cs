using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Entities.Core;
using RF.Domain.Entities.SongCodeIdentifier;
using RF.Domain.Interfaces.Entities;

namespace RF.Domain.Entities
{
    public class DistributionAgreementDetail : RFBaseEntity, IFilterProperties
    {
        public int? IswcId { get; set; }

        [ForeignKey("IswcId")]
        public virtual ISWC Iswc { get; set; }

        public int? IsrcId { get; set; }

        [ForeignKey("IsrcId")]
        public virtual ISRC Isrc { get; set; }

        public int? RoyaltyTypeId { get; set; }

        [ForeignKey("RoyaltyTypeId")]
        public virtual RoyaltyType RoyaltyType { get; set; }

        public int? TerritoryId { get; set; }

        [ForeignKey("TerritoryId")]
        public virtual Territory Territory { get; set; }

        public int? PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }

        public int RecipientId { get; set; }

        [ForeignKey("RecipientId")]
        public virtual Recipient Recipient { get; set; }

        public int? EpisodeId { get; set; }

        [ForeignKey("EpisodeId")]
        public virtual Episode Episode { get; set; }

        public int? PlatformTierId { get; set; }

        [ForeignKey("PlatformTierId")]
        public virtual PlatformTier PlatformTier { get; set; }

        public int? ProductionTitleId { get; set; }

        [ForeignKey("ProductionTitleId")]
        public virtual ProductionTitle ProductionTitle { get; set; }

        public int? LabelId { get; set; }

        [ForeignKey("LabelId")]
        public virtual Label Label { get; set; }

        public int? SocietyId { get; set; }

        [ForeignKey("SocietyId")]
        public virtual Society Society { get; set; }

        public int? DistributionAgreementId { get; set; }

        [ForeignKey("DistributionAgreementId")]
        public virtual DistributionAgreement DistributionAgreement { get; set; }

        public int? ProductionTypeId { get; set; }

        [ForeignKey("ProductionTypeId")]
        public virtual ProductionType ProductionType { get; set; }

        public int? PlatformTypeId { get; set; }

        [ForeignKey("PlatformTypeId")]
        public virtual PlatformType PlatformType { get; set; }

        public int? SourceId { get; set; }

        [ForeignKey("SourceId")]
        public virtual Source Source { get; set; }

        public int? RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }

        public int? RoyaltyTypeGroupId { get; set; }

        [ForeignKey("RoyaltyTypeGroupId")]
        public virtual RoyaltyTypeGroup RoyaltyTypeGroup { get; set; }

        public double Share { get; set; }

        public double Rate { get; set; }

        public int AgreementGroup { get; set; }
    }
}