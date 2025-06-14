using AituConnectApi.Models.Redis;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AituConnectApi.Models
{
    public class User : IdentityUser, ICachable
    {
        [Required]
        public University University { get; set; }

        [Required]
        public string UniversityId { get; set; }

        [Required]
        public Major Major { get; set; }

        [Required]
        public string MajorId { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }
    }
}
