using FluentValidation;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request
{
    public class ValidationDeleteRequest : AbstractValidator<int>
    {
        public ValidationDeleteRequest()
        {
            RuleFor(id => id)
                .NotNull().WithMessage(ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.IdNotNull))
                .GreaterThan(0).WithMessage(ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.IdGreaterThanName));
        }
    }
}
