using System.Net;

namespace ZimoziSolutions.Exceptions.Business
{
    [Serializable]
    public class BusinessSolutionException : Exception
    {
        private readonly bool _successful;
        private readonly HttpStatusCode _statusCode;
        private readonly string _message;
        private readonly string _errorCode;

        public bool Successful { get { return _successful; } }
        public HttpStatusCode StatusCode { get { return _statusCode; } }
        public string MessageError { get { return _message; } }
        public string ErrorCode { get { return _errorCode; } }

        public BusinessSolutionException(HttpStatusCode statusCode, string message) : base(message)
        {
            _successful = false;
            _statusCode = statusCode;
            _message = message;
            _errorCode = statusCode.GetHashCode().ToString();
        }

        public BusinessSolutionException(bool successful, HttpStatusCode statusCode, string message, string errorCode) : base(message)
        {
            _successful = successful;
            _statusCode = statusCode;
            _message = message;
            _errorCode = errorCode;
        }
    }
}
