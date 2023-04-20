using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_Web.Models.DTO;

namespace Villa_Web.Models.Villa_Modols
{
    public class VillaNumberDropDown
    {
        public VillaNumberCreate VillaNumber { get; set; }
        public VillaNumberDropDown() {
            VillaNumber = new VillaNumberCreate();        
        }
        [ValidateNever] // we never want to validate this
        public IEnumerable<SelectListItem> VillaList { get; set; }


    }
}
