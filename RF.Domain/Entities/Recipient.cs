using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class Recipient : RFBaseEntity
    {
        public string Name { get; set; }
        public int PayeeId { get; set; }

        [ForeignKey("PayeeId")]
        public virtual Payee Payee { get; set; }
    }
}