using App.Server.Data.Models.Base;

namespace App.Server.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Validation.Song;

    public class Song : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public AudioFile AudioFile { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<PlaylistSong> PlaylistSong { get; set; } = new HashSet<PlaylistSong>();

        public ActivePlayingSong ActivePlayingSong { get; set; }

    }
}
