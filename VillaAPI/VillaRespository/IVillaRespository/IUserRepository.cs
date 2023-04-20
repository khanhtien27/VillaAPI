using VillaAPI.Models;
using VillaAPI.Models.DTO;

namespace VillaAPI.VillaRespository.IVillaRespository
{
    public interface IUserRepository
    {
        public bool IsUserUnique(string userName);
        Task<LoginResponeDTO> Login (LoginRequiredDTO loginRequired);
        Task <LocalUser> Register (RegisterationRequiredDTO registerationRequired);
    }
}
