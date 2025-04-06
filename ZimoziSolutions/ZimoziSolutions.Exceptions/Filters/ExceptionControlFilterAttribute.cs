using ZimoziSolutions.Exceptions.Api;
using ZimoziSolutions.Exceptions.Business;
using ZimoziSolutions.ApiModels.Responses;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ZimoziSolutions.Exceptions.Filters
{
    public class ExceptionControlFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                ApiException e => SetExceptionData(false,
                                      HttpStatusCode.BadRequest,
                                      e.Message,
                                      HttpStatusCode.InternalServerError.GetHashCode().ToString()),
                BusinessSolutionException e => SetExceptionData(false,
                                                  e.StatusCode,
                                                  e.Message,
                                                  e.ErrorCode ?? HttpStatusCode.InternalServerError.GetHashCode().ToString()),
                _ => SetExceptionData(false,
                                      HttpStatusCode.InternalServerError,
                                      context.Exception.Message,
                                      HttpStatusCode.InternalServerError.GetHashCode().ToString()),// unhandled error
            };
        }

        private static ObjectResult SetExceptionData(bool successful, HttpStatusCode statusCode, string message, string errorCode)
        {
            return new ObjectResult(new GenericResponse
            {
                Successful = successful,
                StatusCode = statusCode,
                Message = message,
                ErrorCode = errorCode
            });
        }
    }
}
