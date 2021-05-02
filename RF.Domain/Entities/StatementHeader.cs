using RF.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RF.Domain.Entities
{
    public class StatementHeader : RFBaseEntity
    {
        public double TotalLocal { get; set; }
        public double TotalForeign { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }

        public int TemplateId { get; set; }

        [ForeignKey("TemplateId")]
        public Template Template { get; set; }

        public int? PaymentReceivedId { get; set; }

        [ForeignKey("PaymentReceivedId")]
        public PaymentReceived PaymentReceived { get; set; }
    }
}