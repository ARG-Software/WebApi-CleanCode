using System.Collections.Generic;
using Newtonsoft.Json;

namespace RF.Domain.Interfaces.ValueObjects
{
    public interface ITemplateDefinition
    {
        [JsonProperty(PropertyName = "label", Required = Required.Always)]
        string Label { get; }

        [JsonProperty(PropertyName = "startingLine", Required = Required.Always)]
        int StartingLine { get; }

        IDictionary<string, ITemplateFieldDefinition> Fields { get; }

        int? WorkSheetNumber { get; }

        ITemplateDefinition CreateTemplateDefinition(string definition);

        bool IsDefinitionValid(string definition);


    }
}