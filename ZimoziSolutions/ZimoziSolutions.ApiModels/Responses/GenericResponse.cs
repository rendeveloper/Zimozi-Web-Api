using System.Net;

namespace ZimoziSolutions.ApiModels.Responses
{
    public class GenericResponse
    {
        public bool Successful { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public object Data { get; set; }
    }
}
