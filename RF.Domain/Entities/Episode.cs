using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Entities.Core;

namespace RF.Domain.Entities
{
    public class Episode : RFBaseEntity
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int? ProductionTitleId { get; set; }

        [ForeignKey("ProductionTitleId")]
        public virtual ProductionTitle ProductionTitle { get; set; }
    }
}