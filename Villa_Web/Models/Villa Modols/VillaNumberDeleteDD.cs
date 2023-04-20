using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_Web.Models.DTO;

namespace Villa_Web.Models.Villa_Modols
{
    public class VillaNumberDeleteDD
    {
        public VillaNumberDTO VillaNumber { get; set; }
        public VillaNumberDeleteDD() {
            VillaNumber = new VillaNumberDTO();        
        }
        [ValidateNever] // we never want to validate this
        public IEnumerable<SelectListItem> VillaList { get; set; }


    }
}
