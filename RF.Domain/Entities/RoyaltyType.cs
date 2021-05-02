using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class RoyaltyType : RFBaseEntity
    {
        public string Title { get; set; }

        public int? RoyaltyTypeGroupId { get; set; }

        [ForeignKey("RoyaltyTypeGroupId")]
        public virtual RoyaltyTypeGroup RoyaltyTypeGroup { get; set; }
    }
}