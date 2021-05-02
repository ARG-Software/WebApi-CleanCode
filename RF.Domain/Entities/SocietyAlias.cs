using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class SocietyAlias : RFBaseEntity
    {
        public string Name { get; set; }
        public int SocietyId { get; set; }

        [ForeignKey("SocietyId")]
        public virtual Society Society { get; set; }
    }
}