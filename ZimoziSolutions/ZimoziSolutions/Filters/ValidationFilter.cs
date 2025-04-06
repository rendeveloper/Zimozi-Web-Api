using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Validations.Result;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Controllers;
using ZimoziSolutions.Validations.Model;
using ZimoziSolutions.Validations.Request;
using ZimoziSolutions.ApiModels.Tasks;

namespace ZimoziSolutions.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerActionDescriptor descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            CustomValidationResult customValidationResult = ExecuteValidator($"{descriptor.ControllerName}{Constants.GenericContoller}", descriptor.ActionName, context.ActionArguments);

            if (context.ModelState.IsValid && customValidationResult.IsValid)
                await next();
            else
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Successful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ConcatMessages(ApplicationContext.Texts.GetValue(nameof(Texts.Shared), Constants.ModelErrorName), customValidationResult),
                    ErrorCode = HttpStatusCode.BadRequest
                });
            }
        }

        private CustomValidationResult ExecuteValidator(string controllerName, string actionName, IDictionary<string, object> arguments)
        {
            try
            {
                return GetInstance(controllerName, actionName, arguments);
            }
            catch (Exception)
            {
                return new CustomValidationResult()
                {
                    IsValid = false
                };
            }
        }

        public CustomValidationResult GetInstance(string controllerName, string actionName, IDictionary<string, object> arguments)
        {
            return controllerName switch
            {
                nameof(TasksController) => TasksActions(actionName, arguments),
                _ => throw new NotImplementedException()
            };
        }

        public CustomValidationResult TasksActions(string actionName, IDictionary<string, object> arguments) =>
            actionName switch
            {
                Constants.TaskGet => new ValidatorModel<PagerData>().Validate(new ValidationPagerDataRequest(), arguments),
                Constants.TaskPost => new ValidatorModel<TaskModel>().Validate(new ValidationTaskAddRequest(), arguments),
                _ => throw new NotImplementedException()
            };

        private string ConcatMessages(string message, CustomValidationResult customValidationResult)
        {
            StringBuilder messages = new StringBuilder();

            if (customValidationResult.Errors.Count > 0)
            {
                messages.Append(message);
                messages.Append(" ");
                messages.AppendJoin(" ", customValidationResult.Errors);
            }

            return messages.ToString();
        }
    }
}
