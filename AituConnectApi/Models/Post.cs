using System.ComponentModel.DataAnnotations;

namespace AituConnectApi.Models
{
    public class Post : IHaveOwner
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public User User { get; set; }  // Navigation property

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public University University { get; set; }

        [Required]
        public string UniversityId { get; set; }

        [Required]
        public List<Subject> Subjects { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
