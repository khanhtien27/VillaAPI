using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models
{
    public class LocalUser
    {
        public int Id { get; set; }
        
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
