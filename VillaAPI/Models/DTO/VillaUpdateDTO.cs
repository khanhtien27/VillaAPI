using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace VillaAPI.Models.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string? Name { get; set; }
        public string? Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        public string? Amenity { get; set; }
    }
}
