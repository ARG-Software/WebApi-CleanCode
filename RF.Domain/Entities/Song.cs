using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class Song : RFBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public string SongTitle { get; set; }

        public int? FirstReleaseCatalogNumber { get; set; }

        public string IntendedPurpose { get; set; }

        public string ProductionTitle { get; set; }

        public string Library { get; set; }

        public string CdIdentifier { get; set; }

        public string WorkTitleCDCut { get; set; }

        public bool? PublicDomain { get; set; }

        public string PDTitle { get; set; }
    }
}