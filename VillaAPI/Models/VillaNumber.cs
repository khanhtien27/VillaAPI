using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaAPI.Models
{
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int VillaNo { get; set; }
        [ForeignKey("villa")]
        public int VillaID { get; set; }
        public Villa villa { get; set; }
        public string? SpecialDetail { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreaded { get; set; }
    }
}
