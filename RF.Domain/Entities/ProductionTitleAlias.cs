using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class ProductionTitleAlias : RFBaseEntity
    {
        public string Name { get; set; }
        public int ProductionTitleId { get; set; }

        [ForeignKey("ProductionTitleId")]
        public virtual ProductionTitle ProductionTitle { get; set; }
    }
}