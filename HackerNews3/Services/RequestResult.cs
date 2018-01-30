using System.Net;

namespace HackerNews3.Services
{
    public class RequestResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}