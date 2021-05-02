using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class SongAlias : RFBaseEntity
    {
        public string Name { get; set; }

        public int SongId { get; set; }

        [ForeignKey("SongId")]
        public virtual Song Song { get; set; }
    }
}