using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.DTO
{
    public class RegisterationRequiredDTO
    {
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
