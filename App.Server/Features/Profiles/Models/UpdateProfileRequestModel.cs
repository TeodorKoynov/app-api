using App.Server.Data;

namespace App.Server.Features.Profiles.Models
{
    using System.ComponentModel.DataAnnotations;
    
    using static Validation.User;
    
    public class UpdateProfileRequestModel
    {
        public string UserName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        [MaxLength(MaxNameLenght)]
        public string Name { get; set;}

        [MaxLength(MaxBiographyLenght)]
        public string Biography { get; set; }
        
        public string MainPhotoUrl { get; set; }
    }
}