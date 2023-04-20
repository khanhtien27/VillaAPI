using System.Linq.Expressions;
using VillaAPI.Models;

namespace VillaAPI.VillaRespository.IVillaRespository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filler = null, string? includeProperties =null);
        Task<T> Get(Expression<Func<T, bool>> fillter = null, bool track = true, string? includeProperties = null);
        Task Remove(T entity);
        Task Create(T entity);
        Task Save();
    }
}
