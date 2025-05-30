using System.ComponentModel.DataAnnotations;

namespace AituConnectApi.Models
{
    public class University
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<User> Students { get; set; }

        public List<Post> RelatedPosts { get; set; }
    }
}
