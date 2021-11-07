namespace App.Server.Features.Playlist.Models
{
    using App.Server.Features.Songs.Models;
    using System.Collections.Generic;

    public class PlaylistDetailsServiceModel : PlaylistListingServiceModel
    {
        public PlaylistDetailsServiceModel()
        {
            this.Songs = new List<SongListingServiceModel>();
        }

        public List<SongListingServiceModel> Songs { get; set; }
     
        public int SongCount { get; set; }
    }
}
