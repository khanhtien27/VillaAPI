﻿using System.ComponentModel.DataAnnotations;

namespace Villa_Web.Models.DTO
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string? SpecialDetail { get; set; }
        public VillaDTO Villa { get; set; }
    }
}
