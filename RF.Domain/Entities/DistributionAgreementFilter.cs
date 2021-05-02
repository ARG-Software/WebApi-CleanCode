using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class DistributionAgreementFilter : RFBaseEntity
    {
        public bool RoyaltyType { get; set; }

        public bool Territory { get; set; }

        public bool Publisher { get; set; }

        public bool Episode { get; set; }

        public bool PlatformTier { get; set; }

        public bool ProductionTitle { get; set; }

        public bool Label { get; set; }

        public bool Album { get; set; }

        public bool Society { get; set; }

        public bool Source { get; set; }

        public bool Region { get; set; }

        public bool PlatformType { get; set; }

        public bool ProductionType { get; set; }

        public bool RoyaltyTypeGroup { get; set; }
    }
}