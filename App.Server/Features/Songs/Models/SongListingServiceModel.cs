namespace App.Server.Features.Songs.Models
{
    using App.Server.Data.Models;
    using System;

    public class SongListingServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string AudioFile { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
