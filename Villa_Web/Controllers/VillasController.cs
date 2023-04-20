using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Web.Models;
using Villa_Web.Models.DTO;
using Villa_Web.Service.IServices;

namespace Villa_Web.Controllers
{
    public class VillasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVillaService _service;
        public VillasController(IMapper mapper, IVillaService service)
        {
            _mapper = mapper;
            _service = service;
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> list = new();
            var respone = await _service.GetAll<APIResponse>();
            if(respone != null && respone.IsSuccess) 
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(respone.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO villaCreateDTO)
        {
            if(ModelState.IsValid)
            {
                var respone = await _service.Create<APIResponse>(villaCreateDTO);
                if(respone != null && respone.IsSuccess) 
                {
                    TempData["success"] = "Villa Created successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered";

            return View(villaCreateDTO);
        }

        

        public async Task<IActionResult> UpdateVilla(int villaID)
        {
            var respone = await _service.Get<APIResponse>(villaID);
            if(respone != null && respone.IsSuccess)
            {
                
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(respone.Result));
                return View(_mapper.Map<VillaUpdateDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO villaUpdateDTO)
        {
            if(ModelState.IsValid)
            {
                TempData["success"] = "Villa Updated successfully";
                var respone = await _service.Update<APIResponse>(villaUpdateDTO);
                if(respone != null && respone.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered";

            return View(villaUpdateDTO);
        }


        public async Task<IActionResult> DeleteVilla(int villaID)
        {
            var respone = await _service.Get<APIResponse>(villaID);
            if (respone != null && respone.IsSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(respone.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO villaDTO)
        {
          
                var respone = await _service.Delete<APIResponse>(villaDTO.Id);
                if (respone != null && respone.IsSuccess)
                {
                TempData["success"] = "Villa Deleted successfully";

                return RedirectToAction(nameof(IndexVilla));
                }
            TempData["error"] = "Error encountered";
            return View(villaDTO);
        }
    }
}
