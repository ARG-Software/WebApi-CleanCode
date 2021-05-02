using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class Payee : RFBaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string SwiftCode { get; set; }
        public string IBAN { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string MailingCountry { get; set; }
        public string PaypalAddress { get; set; }
        public string TaxId { get; set; }
    }
}