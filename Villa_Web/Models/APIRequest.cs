using Microsoft.AspNetCore.Mvc;
using static Villa_Utinity.SD;

namespace Villa_Web.Models
{
    public class APIRequest
    {
        public APIType apiType { get; set; } = APIType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
