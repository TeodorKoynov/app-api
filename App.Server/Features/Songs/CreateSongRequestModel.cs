namespace App.Server.Features.Songs
{
    using System.ComponentModel.DataAnnotations;
    using static Data.Validation.Song;

    public class CreateSongRequestModel
    {
        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string AudioUrl { get; set; }
    }
}
