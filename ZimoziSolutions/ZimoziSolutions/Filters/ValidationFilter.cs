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
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Validations.Request.UserRequest;
using ZimoziSolutions.Validations.Request.CommentRequest;
using ZimoziSolutions.Validations.Request.NotificationRequest;

namespace ZimoziSolutions.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerActionDescriptor descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            if (descriptor.ControllerName != Constants.CustomAuthName)
            {
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
            else
            {
                await next();
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
                nameof(UserController) => UserActions(actionName, arguments),
                nameof(CommentController) => CommentActions(actionName, arguments),
                nameof(NotificationController) => NotificationActions(actionName, arguments),
                _ => throw new NotImplementedException()
            };
        }

        public CustomValidationResult TasksActions(string actionName, IDictionary<string, object> arguments) =>
            actionName switch
            {
                Constants.TaskGet => new ValidatorModel<PagerData>().Validate(new ValidationPagerDataRequest(), arguments),
                Constants.CustomGetId => new ValidatorModel<int>().Validate(new ValidationTaskGetIdRequest(), arguments),
                Constants.TaskPost => new ValidatorModel<TaskModel>().Validate(new ValidationTaskAddRequest(), arguments),
                Constants.TaskPut => new ValidatorModel<TaskModel>().Validate(new ValidationTaskUpdateRequest(), arguments),
                Constants.CustomDel => new ValidatorModel<int>().Validate(new ValidationDeleteRequest(), arguments),
                _ => throw new NotImplementedException()
            };

        public CustomValidationResult UserActions(string actionName, IDictionary<string, object> arguments) =>
            actionName switch
            {
                Constants.TaskGet => new ValidatorModel<PagerData>().Validate(new ValidationPagerDataRequest(), arguments),
                Constants.CustomGetId => new ValidatorModel<int>().Validate(new ValidationTaskGetIdRequest(), arguments),
                Constants.CustomPost => new ValidatorModel<UserCustomModel>().Validate(new ValidationUserAddRequest(), arguments),
                Constants.CustomPut => new ValidatorModel<UserCustomModel>().Validate(new ValidationUserUpdateRequest(), arguments),
                Constants.CustomDel => new ValidatorModel<int>().Validate(new ValidationDeleteRequest(), arguments),
                _ => throw new NotImplementedException()
            };

        public CustomValidationResult CommentActions(string actionName, IDictionary<string, object> arguments) =>
            actionName switch
            {
                Constants.CustomGet => new ValidatorModel<PagerData>().Validate(new ValidationPagerDataRequest(), arguments),
                Constants.CustomGetId => new ValidatorModel<int>().Validate(new ValidationTaskGetIdRequest(), arguments),
                Constants.CustomPost => new ValidatorModel<TaskCommentsModel>().Validate(new ValidationCommentAddRequest(), arguments),
                Constants.CustomPut => new ValidatorModel<TaskCommentsModel>().Validate(new ValidationCommentUpdateRequest(), arguments),
                Constants.CustomDel => new ValidatorModel<int>().Validate(new ValidationDeleteRequest(), arguments),
                _ => throw new NotImplementedException()
            };

        public CustomValidationResult NotificationActions(string actionName, IDictionary<string, object> arguments) =>
            actionName switch
            {
                Constants.CustomGet => new ValidatorModel<PagerData>().Validate(new ValidationPagerDataRequest(), arguments),
                Constants.CustomGetId => new ValidatorModel<int>().Validate(new ValidationTaskGetIdRequest(), arguments),
                Constants.CustomPost => new ValidatorModel<NotificationsModel>().Validate(new ValidationNotificationAddRequest(), arguments),
                Constants.CustomPut => new ValidatorModel<NotificationsModel>().Validate(new ValidationNotificationUpdateRequest(), arguments),
                Constants.CustomDel => new ValidatorModel<int>().Validate(new ValidationDeleteRequest(), arguments),
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
