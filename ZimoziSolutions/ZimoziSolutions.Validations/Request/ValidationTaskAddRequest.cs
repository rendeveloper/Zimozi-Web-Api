using FluentValidation;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request
{
    public class ValidationTaskAddRequest : AbstractValidator<TaskModel>
    {
        public ValidationTaskAddRequest()
        {
            /*RuleFor(e => e.TaskName)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.TaskValidationName, Constants.NameNotEmptyName))
                .Length(3, 100).WithMessage(ApplicationContext.Texts.GetValue(Constants.TaskValidationName, Constants.NameLengthName))
                .JustLetters();

            RuleFor(e => e.TaskEmail)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.TaskValidationName, Constants.EmailNotEmpty))
                .Length(3, 100).WithMessage(ApplicationContext.Texts.GetValue(Constants.TaskValidationName, Constants.EmailLengthName))
                .BeAnEmail();*/
        }
    }
}
