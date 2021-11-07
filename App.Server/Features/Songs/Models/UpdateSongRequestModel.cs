namespace App.Server.Features.Songs.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.Validation.Song;

    public class UpdateSongRequestModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
