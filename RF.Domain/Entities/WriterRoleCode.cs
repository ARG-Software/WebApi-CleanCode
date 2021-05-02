using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class WriterRoleCode : RFBaseEntity
    {
        public string Role { get; set; }
        public string Code { get; set; }
    }
}