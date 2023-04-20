using System.Net;

namespace VillaAPI.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorsMessge = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorsMessge { get; set; }
        public object Result { get; set; }
    }
}
