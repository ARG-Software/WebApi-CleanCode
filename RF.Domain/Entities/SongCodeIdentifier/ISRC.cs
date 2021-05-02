using System;
using System.ComponentModel.DataAnnotations.Schema;
using RF.Domain.Common;

namespace RF.Domain.Entities.SongCodeIdentifier
{
    public class ISRC : SongCodeIdentifierBase
    {
        public string RecordingName { get; set; }

        public TimeSpan? RecordingDuration { get; set; }

        public DateTime? FirstReleaseDate { get; set; }

        public int? ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public virtual Artist Artist { get; set; }
    }
}