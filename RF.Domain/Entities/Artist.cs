using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class Artist : RFBaseEntity
    {
        public string Name { get; set; }
        public int? LabelId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PayeeName { get; set; }
        public string TaxId { get; set; }
        public string Isni { get; set; }
        public string IPI { get; set; }

        [ForeignKey("LabelId")]
        public virtual Label Label { get; set; }
    }
}