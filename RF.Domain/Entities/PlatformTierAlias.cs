using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class PlatformTierAlias : RFBaseEntity
    {
        public string Name { get; set; }
        public int PlatformTierId { get; set; }

        [ForeignKey("PlatformTierId")]
        public virtual PlatformTier PlatformTier { get; set; }
    }
}