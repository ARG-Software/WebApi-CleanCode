using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class DistributionAgreement : RFBaseEntity
    {
        public string Name { get; set; }

        public int DistributionAgreementFilterId { get; set; }

        [ForeignKey("DistributionAgreementFilterId")]
        public virtual DistributionAgreementFilter DistributionAgreementFilter { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}