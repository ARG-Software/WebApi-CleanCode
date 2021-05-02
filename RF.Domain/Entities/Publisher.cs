using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Entities.Core;

namespace RF.Domain.Entities
{
    public class Publisher : RFBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        public string Name { get; set; }
        public string MailAddress { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxId { get; set; }
        public string IPINameNumber { get; set; }
        public string IPIBaseNumber { get; set; }

        public int PublisherTypeId { get; set; }
        public bool Controlled { get; set; }

        public int PRAffiliationId { get; set; }

        public int MRAffiliationId { get; set; }

        public int SRAffiliationId { get; set; }

        [ForeignKey("PublisherTypeId")]
        public virtual PublisherType PublisherType { get; set; }

        [ForeignKey("PRAffiliationId")]
        public virtual Society PRAAffiliation { get; set; }

        [ForeignKey("MRAffiliationId")]
        public virtual Society MRAAffiliation { get; set; }

        [ForeignKey("SRAffiliationId")]
        public virtual Society SRAAffiliation { get; set; }
    }
}