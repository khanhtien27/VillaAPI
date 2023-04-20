using Villa_Web.Models.DTO;

namespace Villa_Web.Service.IServices
{
    public interface IVillaService
    {
        Task<T> GetAll<T>();
        Task<T> Get<T>(int id);
        Task<T> Create<T>(VillaCreateDTO villaCreateDTO);
        Task<T> Update<T>(VillaUpdateDTO villaUpdateDTO);
        Task<T> Delete<T>(int id);
    }
}
