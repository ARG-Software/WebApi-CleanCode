using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class LabelAlias : RFBaseEntity
    {
        public string Name { get; set; }
        public int LabelId { get; set; }

        [ForeignKey("LabelId")]
        public virtual Label Label { get; set; }
    }
}