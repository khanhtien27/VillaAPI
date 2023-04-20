using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.VillaRespository.IVillaRespository;

namespace VillaAPI.Controllers
{
    [Route("API/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _APIRespone;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._APIRespone = new();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequiredDTO loginRequiredDTO)
        {
            var Loginrespon = await _userRepository.Login(loginRequiredDTO);
            if(Loginrespon.localUser == null || string.IsNullOrEmpty(Loginrespon.Token) )
            {
                _APIRespone.StatusCode = HttpStatusCode.BadRequest;
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge.Add("UseName or password is incorrect");
                return BadRequest(_APIRespone);
            }
            
            return Ok(Loginrespon);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegisterationRequiredDTO registerationRequiredDTO)
        {
            bool ifUserNameUnique = _userRepository.IsUserUnique(registerationRequiredDTO.UserName);
            if(!ifUserNameUnique)
            {
                _APIRespone.StatusCode = HttpStatusCode.BadRequest;
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge.Add("UseName already exits");
                return BadRequest(_APIRespone);
            }
            var use = await _userRepository.Register(registerationRequiredDTO);
            if(use == null)
            {
                _APIRespone.StatusCode = HttpStatusCode.BadRequest;
                _APIRespone.IsSuccess = false;
                _APIRespone.ErrorsMessge.Add("Erros while registering");
                return BadRequest(_APIRespone);
            }
            return Ok(_APIRespone);
        }
    }
}
