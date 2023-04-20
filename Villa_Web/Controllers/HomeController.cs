using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Villa_Web.Models;
using Villa_Web.Models.DTO;
using Villa_Web.Service.IServices;

namespace Villa_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVillaService _service;
        public HomeController(IMapper mapper, IVillaService service)
        {
            _mapper = mapper;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = new();
            var respone = await _service.GetAll<APIResponse>();
            if (respone != null && respone.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(respone.Result));
            }
            return View(list);
        }
    }
}