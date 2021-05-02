using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Exceptions;
using RF.Domain.Interfaces.Entities;
using RF.Domain.Interfaces.ValueObjects;
using RF.Domain.ValueObjects;

namespace RF.Domain.Entities
{
    public class Template : RFBaseEntity, ITemplate
    {
        [Required]
        public string Name { get; set; }

        public string Definition { get; set; }
        
        public virtual Source Source { get; set; }
      

        [NotMapped]
        ISource ITemplate.Source
        {
            get => (ISource)Source;
            set => Source = (Source)value;
        }

        public ITemplateDefinition GetTemplateDefinitionObject()
        {
            if (string.IsNullOrEmpty(this.Definition))
            {
                return null;
            }

            try
            {
                ITemplateDefinition templateDefinitionObject = new TemplateDefinition();
                return templateDefinitionObject.CreateTemplateDefinition(this.Definition);
            }
            catch (InvalidOperationException e)
            {
                throw new RFDomainException(e.Message);
            }
        }

        public void SetTemplateDefinition(string templateDefinitionString)
        {
            ITemplateDefinition templateDefinition = new TemplateDefinition();
            if (templateDefinition.IsDefinitionValid(templateDefinitionString))
            {
                this.Definition = templateDefinitionString;
            }
        }
    }
}