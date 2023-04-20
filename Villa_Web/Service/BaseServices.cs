using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;
using Villa_Utinity;
using Villa_Web.Models;
using Villa_Web.Service.IServices;

namespace Villa_Web.Service
{
    public class BaseServices : IBaseService
    {
        public APIResponse responeModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseServices(IHttpClientFactory httpClient)
        {
            this.responeModel = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("Khanhtien");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url); //https:localhost:7002/API/Villa....///
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        // chuyen tu josn qua dinh dang c# understand <-> va` ngc lai
                        // //SerializeObject chuyen tu dang c# ->  dang json
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.apiType)
                {
                    case SD.APIType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.APIType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.APIType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    APIResponse APIResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if(apiResponse.StatusCode == HttpStatusCode.BadRequest || apiResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        APIResponse.StatusCode = HttpStatusCode.BadRequest;
                        APIResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(APIResponse);
                        var returnRes = JsonConvert.DeserializeObject<T>(res);
                        return returnRes;
                    }
                }
                catch(Exception e)
                {
                    var exrespone = JsonConvert.DeserializeObject<T>(apiContent);
                    return exrespone;
                }
                var APIRespone = JsonConvert.DeserializeObject<T>(apiContent);
                return APIRespone;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorsMessge = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
