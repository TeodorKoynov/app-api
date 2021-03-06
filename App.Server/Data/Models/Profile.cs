namespace App.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Validation.User;

    public class Profile
    {
        [Key]
        [Required]
        public string UserId { get; set; }
        
        [MaxLength(MaxNameLenght)]
        public string Name { get; set; }

        [MaxLength(MaxBiographyLenght)]
        public string Biography { get; set; }
        
        public string MainPhotoUrl { get; set; }
    }
}