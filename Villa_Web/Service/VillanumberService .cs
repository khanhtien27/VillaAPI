using Villa_Utinity;
using Villa_Web.Models;
using Villa_Web.Models.DTO;
using Villa_Web.Service.IServices;

namespace Villa_Web.Service
{
    public class VillaNumberService : BaseServices, IVillaNumberService
    {
        private readonly IHttpClientFactory httpClient;
        private string VillaURL;
        public VillaNumberService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            this.httpClient = httpClient;
            VillaURL = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> Create<T>(VillaNumberCreate villaCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType=SD.APIType.POST,
                Data = villaCreateDTO, 
                Url = VillaURL+ "/API/VillaNumber"
            });
        }

        public Task<T> Delete<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.APIType.DELETE,
                Url = VillaURL + "/API/VillaNumber?villaNo=" + id
            });
        }

        public Task<T> Get<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.APIType.GET,
                Url = VillaURL + "/API/VillaNumber/VillaNo?villaNo=" + id
            }); 
        }

        public Task<T> GetAll<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.APIType.GET,
                Url = VillaURL + "/API/VillaNumber"
            });
        }

        public Task<T> Update<T>(VillaNumberUpdate villaUpdateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                apiType = SD.APIType.PUT,
                Data = villaUpdateDTO,
                Url = VillaURL + "/API/VillaNumber/VillaNo?villaNo=" + villaUpdateDTO.VillaNo
            });
        }
    }
}
