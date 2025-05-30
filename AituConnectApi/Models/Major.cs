using System.ComponentModel.DataAnnotations;

namespace AituConnectApi.Models
{
    public class Major
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
