using System;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Interfaces.Common;

namespace RF.Domain.Common
{
    public abstract class RFBaseEntity : IRFBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string CreatedByUserLogin { get; set; }

        public string ModifiedByUserLogin { get; set; }

        public string Description { get; set; }
    }
}