namespace App.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Validation.User;

    public class Profile
    {
        [MaxLength(MaxBiographyLenght)]
        public string Biography { get; set; }
        
        public string MainPhotoUrl { get; set; }
    }
}