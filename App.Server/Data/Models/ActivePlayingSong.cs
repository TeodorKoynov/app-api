namespace App.Server.Data.Models
{

    public class ActivePlayingSong
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int? PlaylistId { get; set; }

        public Playlist Playlist { get; set; }

        public int SongId { get; set; }

        public Song Song {get; set;}
    }
}
