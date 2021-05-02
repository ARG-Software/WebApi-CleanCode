using Newtonsoft.Json;

namespace RF.Domain.Interfaces.ValueObjects
{
    public interface ITemplateFieldDefinition
    {
        [JsonProperty(PropertyName = "type", Required = Required.Default)]
        string Type { get; set; }

        [JsonProperty(PropertyName = "value", Required = Required.AllowNull)]
        string Value { get; set; }
    }
}