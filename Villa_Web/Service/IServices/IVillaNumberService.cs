using Villa_Web.Models.DTO;

namespace Villa_Web.Service.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAll<T>();
        Task<T> Get<T>(int id);
        Task<T> Create<T>(VillaNumberCreate villaCreateDTO);
        Task<T> Update<T>(VillaNumberUpdate villaUpdateDTO);
        Task<T> Delete<T>(int id);
    }
}
