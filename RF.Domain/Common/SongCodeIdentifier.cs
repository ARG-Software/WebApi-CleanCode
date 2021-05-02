using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Entities;

namespace RF.Domain.Common
{
    public abstract class SongCodeIdentifierBase : RFBaseEntity
    {
        public int SongId { get; set; }

        [ForeignKey("SongId")]
        public virtual Song Song { get; set; }

        public string Code { get; set; }

        public bool Default { get; set; }
    }
}