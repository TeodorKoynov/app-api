namespace App.Server.Data.Models
{
    public class AudioFile
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int SongId { get; set; }

        public Song Song { get; set; } 
    }
}
