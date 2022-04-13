using App.Server.Data.Models.Base;

namespace App.Server.Data.Models
{
    public class PlaylistSong : Entity
    {
        public int PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public int SongId { get; set; }

        public Song Song { get; set; }

        public int SongIndex { get; set; }
    }
}
