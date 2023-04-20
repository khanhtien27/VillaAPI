using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.DTO
{
    public class VillaNumberUpdate
    {
        public int VillaNo { get; set; }

        [Required]
        public int VillaID { get; set; }

        public string? SpecialDetail { get; set; }
    }
}
