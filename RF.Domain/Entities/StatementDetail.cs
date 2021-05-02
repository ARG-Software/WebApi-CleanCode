using RF.Domain.Common;
using RF.Domain.Entities.SongCodeIdentifier;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Interfaces.Entities;

namespace RF.Domain.Entities
{
    public class StatementDetail : RFBaseEntity, IFilterProperties
    {
        public double RoyaltyNetUsd { get; set; }
        public double RoyaltyNetForeign { get; set; }

        public double? UnitRate { get; set; }
        public double? BonusAmount { get; set; }
        public int StatementHeaderId { get; set; }
        public int? RoyaltyTypeId { get; set; }
        public int? TerritoryId { get; set; }
        public int? PublisherId { get; set; }
        public int? EpisodeId { get; set; }
        public int? PlatformTierId { get; set; }
        public int? ProductionTitleId { get; set; }

        public int? LabelId { get; set; }
        public int? AlbumId { get; set; }
        public int? SocietyId { get; set; }
        public int? ISRCId { get; set; }
        public int? ISWCId { get; set; }
        public int SongId { get; set; }

        public DateTime? PerformanceDate { get; set; }
        public int Quantity { get; set; }

        #region NotMappedProperties

        [NotMapped]
        public int? RoyaltyTypeGroupId { get; set; }

        [NotMapped]
        public int? PlatformTypeId { get; set; }

        [NotMapped]
        public int? ProductionTypeId { get; set; }

        [NotMapped]
        public int? RegionId { get; set; }

        [NotMapped]
        public int? SourceId { get; set; }

        #endregion NotMappedProperties

        #region NavigationProperties

        [ForeignKey("ISRCId")]
        public virtual ISRC ISRC { get; set; }

        [ForeignKey("ISWCId")]
        public virtual ISWC ISWC { get; set; }

        [ForeignKey("SongId")]
        public virtual Song Song { get; set; }

        [ForeignKey("SocietyId")]
        public virtual Society Society { get; set; }

        [ForeignKey("RoyaltyTypeId")]
        public virtual RoyaltyType RoyaltyType { get; set; }

        [ForeignKey("TerritoryId")]
        public virtual Territory Territory { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }

        [ForeignKey("PlatformTierId")]
        public virtual PlatformTier PlatformTier { get; set; }

        [ForeignKey("EpisodeId")]
        public virtual Episode Episode { get; set; }

        [ForeignKey("ProductionTitleId")]
        public virtual ProductionTitle ProductionTitle { get; set; }

        [ForeignKey("LabelId")]
        public virtual Label Label { get; set; }

        [ForeignKey("StatementHeaderId")]
        public virtual StatementHeader StatementHeader { get; set; }

        #endregion NavigationProperties
    }
}