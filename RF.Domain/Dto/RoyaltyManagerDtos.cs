using System.Collections.Generic;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.Domain.Dto
{
    public class RoyaltyManagerProcessmentDto
    {
        public int TemplateId { get; set; }
        public int PaymentId { get; set; }

        public string[] Tags { get; set; }

        public IEnumerable<IParsedStatement> StatementList { get; set; }
    }
}