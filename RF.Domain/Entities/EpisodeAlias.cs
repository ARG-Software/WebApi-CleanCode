using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class EpisodeAlias : RFBaseEntity
    {
        public string Name { get; set; }
        public int EpisodeId { get; set; }

        [ForeignKey("EpisodeId")]
        public virtual Episode Episode { get; set; }
    }
}