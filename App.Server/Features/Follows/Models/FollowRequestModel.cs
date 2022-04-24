using System.ComponentModel.DataAnnotations;

namespace App.Server.Features.Follows.Models
{
    public class FollowRequestModel
    {
        [Required]
        public string UserId { get; set; }
    }
}