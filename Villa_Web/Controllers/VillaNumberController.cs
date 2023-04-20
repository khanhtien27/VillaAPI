using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Linq;
using Villa_Web.Models;
using Villa_Web.Models.DTO;
using Villa_Web.Models.Villa_Modols;
using Villa_Web.Service.IServices;

namespace Villa_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVillaNumberService _service;
        private readonly IVillaService _villaService;
        public VillaNumberController(IMapper mapper, IVillaNumberService service, IVillaService villaService)
        {
            _mapper = mapper;
            _service = service;
            _villaService = villaService;
        }
        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new();
            var respone = await _service.GetAll<APIResponse>();
            if (respone != null && respone.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(respone.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberDropDown villaNumberDropDown = new VillaNumberDropDown();
            var respone = await _villaService.GetAll<APIResponse>();
            if (respone != null && respone.IsSuccess )
            {
                villaNumberDropDown.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(respone.Result)).Select(i => new SelectListItem
                    {
                            Text = i.Name,
                            Value = i.Id.ToString(),
                    });
            }
            return View(villaNumberDropDown);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateVillaNumber(VillaNumberDropDown villaNumberCreate)
        {
            if (ModelState.IsValid)
            {
                var respone = await _service.Create<APIResponse>(villaNumberCreate.VillaNumber);
                if (respone != null && respone.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if(respone.ErrorsMessge.Count != 0)
                    {
                        ModelState.AddModelError("ErrorsMessge", respone.ErrorsMessge.FirstOrDefault());
                    }
                }

            }
            var rsp = await _villaService.GetAll<APIResponse>();
            if(rsp != null && rsp.IsSuccess)
            {
                villaNumberCreate.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(rsp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }
            return View(villaNumberCreate);
        }

        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            VillaNumberUpdateDD villaNumberUpdateDD = new VillaNumberUpdateDD();
            var respone = await _service.Get<APIResponse>(villaNo);
            if (respone != null && respone.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(respone.Result));
                villaNumberUpdateDD.VillaNumber =  _mapper.Map<VillaNumberUpdate>(model);
            }

            //Drop Down menu
            respone = await _villaService.GetAll<APIResponse>();
            if(respone != null && respone.IsSuccess)
            {
                villaNumberUpdateDD.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(respone.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberUpdateDD);
            }

            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateDD villaNumberUpdate)
        {
            if (ModelState.IsValid)
            {
                TempData["hihi"] = "love you";
                var respone = await _service.Update<APIResponse>(villaNumberUpdate.VillaNumber);
                if (respone != null && respone.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (respone.ErrorsMessge.Count != 0)
                    {
                        ModelState.AddModelError("ErrorsMessge", respone.ErrorsMessge.FirstOrDefault());
                    }
                }

            }
            var rsp = await _villaService.GetAll<APIResponse>();
            if (rsp != null && rsp.IsSuccess)
            {
                villaNumberUpdate.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(rsp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString(),
                    });
            }
            return View(villaNumberUpdate);
        }

        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            VillaNumberDeleteDD villaNumberDeleteDD = new VillaNumberDeleteDD();
            var respone = await _service.Get<APIResponse>(villaNo);
            if (respone != null && respone.IsSuccess)
            {
                villaNumberDeleteDD.VillaNumber = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(respone.Result));
            }

            //Drop Down menu
            respone = await _villaService.GetAll<APIResponse>();
            if (respone != null && respone.IsSuccess)
            {
                villaNumberDeleteDD.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(respone.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberDeleteDD);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteDD villaNumberDTO)
        {

            var respone = await _service.Delete<APIResponse>(villaNumberDTO.VillaNumber.VillaNo);
            if (respone != null && respone.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            return View(villaNumberDTO);
        }

    }
}
