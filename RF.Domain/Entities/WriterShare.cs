using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class WriterShare : RFBaseEntity
    {
        public int SongId { get; set; }

        [ForeignKey("SongId")]
        public virtual Song Song { get; set; }

        public int WriterId { get; set; }

        [ForeignKey("WriterId")]
        public virtual Writer Writer { get; set; }

        public int WriterRoleCodeId { get; set; }

        [ForeignKey("WriterRoleCodeId")]
        public virtual WriterRoleCode WriterRoleCode { get; set; }

        public int DistributionAgreementId { get; set; }

        [ForeignKey("DistributionAgreementId")]
        public virtual DistributionAgreement DistributionAgreement { get; set; }

        public double Share { get; set; }
    }
}