using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.Models.DTO;
using VillaAPI.VillaRespository.IVillaRespository;

namespace VillaAPI.VillaRespository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string secret;
        public UserRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            secret = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUserUnique(string userName)
        {
            var user = _context.LocalUsers.FirstOrDefault(x => x.UserName == userName);
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponeDTO> Login(LoginRequiredDTO loginRequired)
        {
            var user = _context.LocalUsers.FirstOrDefault(x => x.PassWord == loginRequired.Password 
            && x.UserName.ToLower() == loginRequired.UserName.ToLower());

            if(user == null)
            {
                return new LoginResponeDTO()
                {
                    Token = "",
                    localUser = null
                };
            }
            //if user was found generate JWT token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF32.GetBytes(secret);

            var tokeDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                //Ngay het han: 7 ngay
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokeDescriptor);
            LoginResponeDTO loginRequiredDTO = new LoginResponeDTO()
            {
                Token = tokenHandler.WriteToken(token),
                localUser = user,
            };
            return loginRequiredDTO;
        }

        public async Task<LocalUser> Register(RegisterationRequiredDTO registerationRequired)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registerationRequired.UserName,
                Name = registerationRequired.Name,
                PassWord = registerationRequired.PassWord,
                Role = registerationRequired.Role,
            };
            await _context.LocalUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            //it make sense to empty the password begore we return
            user.PassWord = "";
            return user;
        }
    }
}
