using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Entities.Core;

namespace RF.Domain.Entities
{
    public class RoyaltyDistribution : RFBaseEntity
    {
        public double Amount { get; set; }

        public int RecipientId { get; set; }

        public int StatementDetailId { get; set; }

        public int WriterId { get; set; }

        [ForeignKey("RecipientId")]
        public virtual Recipient Recipient { get; set; }

        [ForeignKey("WriterId")]
        public virtual Writer Writer { get; set; }

        [ForeignKey("StatementDetailId")]
        public virtual StatementDetail StatementDetail { get; set; }

        public double Rate { get; set; }
    }
}