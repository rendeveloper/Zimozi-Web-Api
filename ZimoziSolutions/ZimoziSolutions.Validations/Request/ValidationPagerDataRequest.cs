using FluentValidation;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request
{
    public class ValidationPagerDataRequest : AbstractValidator<PagerData>
    {
        public ValidationPagerDataRequest()
        {
            RuleFor(e => e.PageNumber)
                .GreaterThan(0).WithMessage(ApplicationContext.Texts.GetValue(Constants.PagerValidationName, Constants.PageNumberName));

            RuleFor(e => e.PageSize)
                .GreaterThan(0).WithMessage(ApplicationContext.Texts.GetValue(Constants.PagerValidationName, Constants.PageSizeName));

        }
    }
}
