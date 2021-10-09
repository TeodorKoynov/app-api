namespace App.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public IEnumerable<Song> Songs { get; } = new HashSet<Song>();
    }
}
