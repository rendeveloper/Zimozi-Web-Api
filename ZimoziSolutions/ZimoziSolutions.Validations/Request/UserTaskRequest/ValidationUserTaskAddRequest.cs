using FluentValidation;
using ZimoziSolutions.ApiModels.UserTask;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request.UserTaskRequest
{
    public class ValidationUserTaskAddRequest : AbstractValidator<UserTasksModel>
    {
        public ValidationUserTaskAddRequest()
        {
            RuleFor(e => e.UserId)
                .NotNull().WithMessage(ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.IdNotNull));

            RuleFor(e => e.TaskId)
                .NotNull().WithMessage(ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.IdNotNull));
        }
    }
}
