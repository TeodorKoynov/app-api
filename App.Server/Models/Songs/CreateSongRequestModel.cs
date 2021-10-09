namespace App.Server.Models.Songs
{
    using System.ComponentModel.DataAnnotations;
    using static Data.Validation.Song;

    public class CreateSongRequestModel
    {
        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string AudioUrl { get; set; }
    }
}
