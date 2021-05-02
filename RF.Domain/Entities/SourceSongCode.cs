using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities
{
    public class SourceSongCode : RFBaseEntity
    {
        public string Code { get; set; }

        public int SongId { get; set; }

        [ForeignKey("SongId")]
        public virtual Song Song { get; set; }

        public int SourceId { get; set; }

        [ForeignKey("SourceId")]
        public virtual Source Source { get; set; }
    }
}