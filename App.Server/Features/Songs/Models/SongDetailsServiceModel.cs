namespace App.Server.Features.Songs.Models
{
    public class SongDetailsServiceModel : SongListingServiceModel
    {
        public string Description { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
