namespace App.Server.Features.Playlist.Models
{
    using App.Server.Features.Songs.Models;

    public class SongFromPlaylistResponseModel : SongListingServiceModel
    {
        public int Index { get; set; }
    }
}
