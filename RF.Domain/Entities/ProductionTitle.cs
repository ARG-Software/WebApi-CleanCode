using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Entities.Core;

namespace RF.Domain.Entities
{
    public class ProductionTitle : RFBaseEntity
    {
        public string Title { get; set; }
        public int ProductionTypeId { get; set; }

        [ForeignKey("ProductionTypeId")]
        public virtual ProductionType ProductionType { get; set; }
    }
}