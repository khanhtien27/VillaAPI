using System.Linq.Expressions;
using VillaAPI.Models;

namespace VillaAPI.VillaRespository.IVillaRespository
{
    public interface IVillaRespository : IRepository<Villa>
    {    
        Task Update(Villa entity);
    }
}
