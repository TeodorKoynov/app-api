namespace App.Server.Features.Playlist.Models
{
    using System.Collections.Generic;

    public class PlaylistDetailsServiceModel : PlaylistListingServiceModel
    {
        public PlaylistDetailsServiceModel()
        {
            this.Songs = new List<SongFromPlaylistResponseModel>();
        }

        public List<SongFromPlaylistResponseModel> Songs { get; set; }
     
        public int SongCount { get; set; }
    }
}
