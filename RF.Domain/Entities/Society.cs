using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;
using RF.Domain.Entities.Core;

namespace RF.Domain.Entities
{
    public class Society : RFBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        public string Name { get; set; }
        public string Acronym { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int Affiliates { get; set; }
        public int TerritoryId { get; set; }

        [ForeignKey("TerritoryId")]
        public virtual Territory Territory { get; set; }
    }
}