using Villa_Web.Models;

namespace Villa_Web.Service.IServices
{
    public interface IBaseService
    {
        APIResponse responeModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
