using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Entities.Core;

namespace RF.Domain.Entities
{
    public class Platform : RFBaseEntity
    {
        public string Name { get; set; }
        public int PlatformTypeId { get; set; }

        [ForeignKey("PlatformTypeId")]
        public virtual PlatformType PlatformType { get; set; }
    }
}