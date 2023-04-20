using VillaAPI.Models;

namespace VillaAPI.VillaRespository.IVillaRespository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task Update(VillaNumber entity);
    }
}
