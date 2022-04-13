using App.Server.Data.Models.Base;

namespace App.Server.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Playlist : DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<PlaylistSong> PlaylistSong { get; set; } = new HashSet<PlaylistSong>();

        public ActivePlayingSong ActivePlayingSong { get; set; }
    }
}
