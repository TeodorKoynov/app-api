﻿namespace App.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Validation.Song;

    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string AudioUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
