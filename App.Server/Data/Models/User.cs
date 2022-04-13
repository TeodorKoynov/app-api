using System;
using System.ComponentModel.DataAnnotations;
using App.Server.Data.Models.Base;

namespace App.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    
    public class User : IdentityUser, IEntity
    {
        public Profile Profile { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? ModifiedOn { get; set; }
        
        public string ModifiedBy { get; set; }
        
        public IEnumerable<Song> Songs { get; } = new HashSet<Song>();

        public ICollection<Playlist> Playlists { get; } = new HashSet<Playlist>();

        public ActivePlayingSong ActivePlayingSong { get; set; }
    }
}
