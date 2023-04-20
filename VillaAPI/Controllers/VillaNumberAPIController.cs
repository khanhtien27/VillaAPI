using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.VillaRespository.IVillaRespository;

namespace VillaAPI.Controllers
{
    [Route("/API/VillaNumber")]
    [ApiController] //Chỉ ra các models [Antribut] 
    public class VillaNumberAPIController :ControllerBase
    {
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;
        private readonly IVillaRespository _Dbvilla;
        public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRespository Dbvilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _Dbvilla = Dbvilla;
            this._response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                IEnumerable<VillaNumber> villanumber = await _dbVillaNumber.GetAll(includeProperties: "villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villanumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessge = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }
        }

        [HttpGet("VillaNo", Name = "GetVillaNo")]
        public async Task<ActionResult<APIResponse>> GetVillaNo(int villaNo)
        {
            try
            {
                if (villaNo == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await _dbVillaNumber.Get(i => i.VillaNo == villaNo, includeProperties: "villa");
                if (villaNumber == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessge = new List<string>()
                {
                    ex.ToString()
                };
                return _response;
            }

        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreatVillaNumber([FromBody] VillaNumberCreate villaNumberCreate)
        {
            if (await _dbVillaNumber.Get(i => i.VillaNo == villaNumberCreate.VillaNo) != null)
            {
                ModelState.AddModelError("ErrorsMessge", "VillaNo is already exits, Choose diferent ViilaNumber");
                return BadRequest(ModelState);
            }
            if (await _Dbvilla.Get(i => i.Id == villaNumberCreate.VillaID) == null)
            {
                ModelState.AddModelError("ErrorsMessge", "Villa is not already exits");
                return BadRequest(ModelState);
            }
            if (villaNumberCreate == null) return BadRequest();
            var villanumber = _mapper.Map<VillaNumber>(villaNumberCreate);
            await _dbVillaNumber.Create(villanumber);
            _response.Result = _mapper.Map<VillaNumber>(villaNumberCreate);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetVillaNo", new { VillaNo = villanumber.VillaNo }, _response);
        }

        [HttpDelete]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int villaNo)
        {
            if(villaNo == 0) return BadRequest();
            var villa = await _dbVillaNumber.Get(i => i.VillaNo == villaNo);
            if ( villa == null)
            {
                ModelState.AddModelError("ErrorsMessge", "VillaNumber not Exits");
                return BadRequest(ModelState);
            }
            else
            {
                await _dbVillaNumber.Remove(villa);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }

        [HttpPut("VillaNo", Name = "UpdateVillaNo")]
        public async Task<ActionResult<APIResponse>> UpdateVillaNo(int villaNo ,[FromBody] VillaNumberUpdate villaNumberUpdate)
        {
            if(villaNumberUpdate == null || villaNo != villaNumberUpdate.VillaNo) return BadRequest();
            if(await _Dbvilla.Get(i => i.Id == villaNumberUpdate.VillaID) == null)
            {
                ModelState.AddModelError("ErrorsMessge", "Not exits");
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<VillaNumber>(villaNumberUpdate);
            await _dbVillaNumber.Update(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
