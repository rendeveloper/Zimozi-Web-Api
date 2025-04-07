using FluentValidation;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request.UserRequest
{
    public class ValidationUserUpdateRequest : AbstractValidator<UserCustomModel>
    {
        public ValidationUserUpdateRequest()
        {

            RuleFor(e => e.Id)
                .GreaterThan(0).WithMessage(ApplicationContext.Texts.GetValue(Constants.TaskValidationName, Constants.IdGreaterThanName)); ;

            RuleFor(e => e.Username)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));
        }
    }
}
