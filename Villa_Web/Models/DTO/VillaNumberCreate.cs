using System.ComponentModel.DataAnnotations;

namespace Villa_Web.Models.DTO
{
    public class VillaNumberCreate
    {
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string? SpecialDetail { get; set; }
    }
}
