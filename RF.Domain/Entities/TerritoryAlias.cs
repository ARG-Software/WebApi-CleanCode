using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class TerritoryAlias : RFBaseEntity
    {
        public string Name { get; set; }
        public int TerritoryId { get; set; }

        [ForeignKey("TerritoryId")]
        public virtual Territory Territory { get; set; }
    }
}