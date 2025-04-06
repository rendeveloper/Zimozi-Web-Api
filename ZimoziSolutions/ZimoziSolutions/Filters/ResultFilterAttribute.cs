using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Constants;

namespace ZimoziSolutions.Filters
{
    public class ResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is null)
            {
                context.Result = new ObjectResult(new GenericResponse
                {
                    Successful = AssignSuccess(context),
                    StatusCode = (HttpStatusCode)AssignStatusCode(context),
                    Message = AssignMessage(context),
                    Data = AssignResult(context),
                    ErrorCode = ""
                });
            }
        }

        private static dynamic AssignSuccess(ActionExecutedContext context)
        {
            return context.Result is not null && (context.Result as ObjectResult).StatusCode is 200;
        }

        private static dynamic AssignResult(ActionExecutedContext context)
        {
            return context.Result is null ? null : (context.Result as ObjectResult).Value;
        }

        private static dynamic AssignStatusCode(ActionExecutedContext context)
        {
            return context.Result is null ? null : (context.Result as ObjectResult).StatusCode;
        }

        private static dynamic AssignMessage(ActionExecutedContext context)
        {
            return context.Result is null ? ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.NoContentName) : "";
        }
    }
}
