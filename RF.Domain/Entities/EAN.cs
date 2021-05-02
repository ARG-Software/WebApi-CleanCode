using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class EAN : RFBaseEntity
    {
        public int LabelId { get; set; }

        [ForeignKey("LabelId")]
        public virtual Label Label { get; set; }

        public int DistributingLabelId { get; set; }

        [ForeignKey("DistributingLabelId")]
        public virtual Label DistributingLabel { get; set; }

        public string Name { get; set; }

        public string Configuration { get; set; }

        public bool Default { get; set; }
    }
}