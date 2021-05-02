using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using RF.Domain.Interfaces.ValueObjects;
using RF.Library.IO;
[assembly: InternalsVisibleTo("RF.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace RF.Domain.ValueObjects
{
    internal class TemplateFieldDefinition : ITemplateFieldDefinition
    {
        public string Type { get; set; }

        public string Value { get; set; }
    }

    public class TemplateDefinition : ITemplateDefinition
    {
        public TemplateDefinition()
        {
            this.Fields = new Dictionary<string, ITemplateFieldDefinition>();
        }

        public string Label { get; set; }

        public int StartingLine { get; set; }

        [JsonProperty(ItemConverterType = typeof(JsonAbstractTypeConverter<TemplateFieldDefinition, ITemplateFieldDefinition>))]
        public IDictionary<string, ITemplateFieldDefinition> Fields { get; set; }

        public int? WorkSheetNumber { get; set; } = 0;
        

        public ITemplateDefinition CreateTemplateDefinition(string definition)
        {
           this.Deserialize(definition);
           return this;
        }

        public bool IsDefinitionValid(string definition)
        {
            try
            {
                this.Deserialize(definition);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private ITemplateDefinition Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new  InvalidOperationException("Definition string is null or empty");
            }

            try
            {
                JsonConvert.DeserializeObject<TemplateDefinition>(json, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                });
                return this;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error parsing template definition, incorrect or invalid one");
            }
        }
    }

    
}