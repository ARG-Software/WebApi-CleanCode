using System.Collections.Generic;
using RF.Domain.Interfaces.Common;

namespace RF.Domain.Interfaces.Entities
{
    public interface ISource: IRFBaseEntity
    {
        string Name { get; set; }

        ICollection<ITemplate> Templates { get; set; }
    }
}