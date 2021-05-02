using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Interfaces.Entities;

namespace RF.Domain.Entities
{
    public class Source : RFBaseEntity, ISource
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Template> Templates { get; set; }

        [NotMapped]
        ICollection<ITemplate> ISource.Templates
        {
            get => (ICollection<ITemplate>)Templates;
            set => Templates = (ICollection<Template>)value;
        }
    }
}