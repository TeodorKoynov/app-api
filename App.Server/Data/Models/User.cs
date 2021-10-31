namespace App.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public IEnumerable<Song> Songs { get; } = new HashSet<Song>();

        public ICollection<Playlist> Playlists { get; } = new HashSet<Playlist>();

        public ActivePlayingSong ActivePlayingSong { get; set; }
    }
}
