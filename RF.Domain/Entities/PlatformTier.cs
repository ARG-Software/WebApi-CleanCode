using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class PlatformTier : RFBaseEntity
    {
        public string Name { get; set; }
        public int PlatformId { get; set; }

        [ForeignKey("PlatformId")]
        public virtual Platform Platform { get; set; }
    }
}