using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class Currency : RFBaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}