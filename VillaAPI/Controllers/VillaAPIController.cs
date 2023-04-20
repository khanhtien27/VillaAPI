using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VillaAPI.Data;
using VillaAPI.Logging;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.VillaRespository.IVillaRespository;

namespace VillaAPI.Controllers
{
    [Route("/API/Villa")]
    [ApiController] //Chỉ ra các models [Antribut] 
    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaRespository _dbVilla;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;
        public VillaAPIController(IVillaRespository dbVilla, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _mapper = mapper;
            this._response = new();
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villalist = await _dbVilla.GetAll();
                _response.Result = _mapper.Map<List<VillaDTO>>(villalist);
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
            }
            return _response;
        }


        [HttpGet("Id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _dbVilla.Get(i => i.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                {
                    _response.IsSuccess = false;
                    _response.ErrorsMessge = new List<string>()
                {
                    ex.ToString()
                };
                }
                return _response;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO villaCreateDTO)
        {
            try
            {
                if (await _dbVilla.Get(n => n.Name.ToLower() == villaCreateDTO.Name.ToLower()) != null)
                {
                    return BadRequest(ModelState);
                }

                if (villaCreateDTO == null)
                {
                    return BadRequest(villaCreateDTO);
                }
                /*//if (villaCreateDTO.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}
                //if(villaDTO.Id == 0)
                //{
                //    villaDTO.Id = 1;
                //}

                //Villa model = new()
                //{
                //    Amenity = villaCreateDTO.Amenity,
                //    Name = villaCreateDTO.Name,
                //    Details = villaCreateDTO.Details,
                //    Sqft = villaCreateDTO.Sqft,
                //    ImageUrl = villaCreateDTO.ImageUrl,
                //    Occupancy = villaCreateDTO.Occupancy,
                //    Rate = villaCreateDTO.Rate,

                //};
                */

                Villa model = _mapper.Map<Villa>(villaCreateDTO);
                await _dbVilla.Create(model);
                _response.Result = _mapper.Map<Villa>(villaCreateDTO);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { Id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessge = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }
        // if you use ActioneResult here, you can define what the type return
        // with IActionResult, you dont need define what the type return

        [HttpDelete("Id", Name = "DeleteVilla")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id) //"IActionResult": dont need return any data
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var villa = await _dbVilla.Get(i => i.Id == id);
                if (villa == null) return NotFound();
                else
                {
                    await _dbVilla.Remove(villa);
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessge = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpPut("Id", Name = "Update")]
        public async Task<ActionResult<APIResponse>> Update(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            try
            {
                if (villaUpdateDTO == null || id != villaUpdateDTO.Id)
                {
                    return BadRequest();
                }
                //var villa = VillaStore.VillaList.FirstOrDefault(a => a.Id == id);
                //villa.Name = villaDTO.Name;
                //villa.Occupancy = villaDTO.Occupancy;
                //villa.Sqft = villaDTO.Sqft;
                //Villa model = new()
                //{
                //    Amenity = villaDTO.Amenity,
                //    Name = villaDTO.Name,
                //    Details = villaDTO.Details,
                //    Sqft = villaDTO.Sqft,
                //    ImageUrl = villaDTO.ImageUrl,
                //    Occupancy = villaDTO.Occupancy,
                //    Rate = villaDTO.Rate,
                //    Id = villaDTO.Id,
                //};

                var model = _mapper.Map<Villa>(villaUpdateDTO);
                await _dbVilla.Update(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessge = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpPatch("Id", Name = "UpdatePartialVilla")]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVilla.Get(i => i.Id == id, track: false);
            var villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(villa);

            //VillaUpdateDTO villaUpdateDTO = new()
            //{
            //    Amenity = villa.Amenity,
            //    Name = villa.Name,
            //    Details = villa.Details,
            //    Sqft = villa.Sqft,
            //    ImageUrl = villa.ImageUrl,
            //    Occupancy = villa.Occupancy,
            //    Rate = villa.Rate,
            //    Id = villa.Id,
            //};

            if (villa == null) return BadRequest();
            patchDTO.ApplyTo(villaUpdateDTO, ModelState);
            var model = _mapper.Map<Villa>(villaUpdateDTO);

            //Villa model = new()
            //{
            //    Amenity = villaUpdateDTO.Amenity,
            //    Name = villaUpdateDTO.Name,
            //    Details = villaUpdateDTO.Details,
            //    Sqft = villaUpdateDTO.Sqft,
            //    ImageUrl = villaUpdateDTO.ImageUrl,
            //    Occupancy = villaUpdateDTO.Occupancy,
            //    Rate = villaUpdateDTO.Rate,
            //    Id = villaUpdateDTO.Id,
            //};

            await _dbVilla.Update(model);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}


