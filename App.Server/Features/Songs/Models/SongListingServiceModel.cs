namespace App.Server.Features.Songs.Models
{
    using System;

    public class SongListingServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int Duration { get; set; }

        public string TotalTime { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
