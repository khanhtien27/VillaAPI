﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace VillaAPI.Models.DTO
{
    public class VillaCreateDTO
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public string? Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string? ImageUrl { get; set; }
        public string? Amenity { get; set; }
    }
}
