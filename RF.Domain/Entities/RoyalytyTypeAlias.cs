using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Entities.Core;

namespace RF.Domain.Entities
{
    public class RoyaltyTypeAlias : RFBaseEntity
    {
        public string Name { get; set; }
        public int RoyaltyTypeId { get; set; }
        public int SourceId { get; set; }

        [ForeignKey("RoyaltyTypeId")]
        public virtual RoyaltyType RoyaltyType { get; set; }

        [ForeignKey("SourceId")]
        public virtual Source Source { get; set; }
    }
}