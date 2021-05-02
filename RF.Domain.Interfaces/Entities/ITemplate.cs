using RF.Domain.Interfaces.Common;
using RF.Domain.Interfaces.ValueObjects;

namespace RF.Domain.Interfaces.Entities
{
    public interface ITemplate: IRFBaseEntity
    {
        
        string Name { get; set; }

        string Definition { get; set; }

        ISource Source { get; set; }

        ITemplateDefinition GetTemplateDefinitionObject();

        void SetTemplateDefinition(string templateDefinitionString);
    }
}