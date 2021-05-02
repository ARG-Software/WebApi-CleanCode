using RF.Domain.Common;

namespace RF.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PaymentReceived : RFBaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }

        public double ExchangeRate { get; set; }
        public double GrossAmountLocal { get; set; }
        public double NetAmountLocal { get; set; }
        public double GrossAmountForeign { get; set; }
        public double NetAmountForeign { get; set; }

        public int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public bool Reconciled { get; set; } = false;

        public int SourceId { get; set; }

        [ForeignKey("SourceId")]
        public Source Source { get; set; }
    }
}