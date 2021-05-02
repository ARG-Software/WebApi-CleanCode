using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class Writer : RFBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        public string WriterLN { get; set; }
        public string WriterFN { get; set; }
        public string WriterPRAff { get; set; }
        public string WriterMRAff { get; set; }
        public int WriterIPINameNumber { get; set; }
        public int? WriterIPIBaseNumber { get; set; }
        public bool Controlled { get; set; }
    }
}