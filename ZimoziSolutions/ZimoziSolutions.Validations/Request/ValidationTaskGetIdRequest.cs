using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request
{
    public class ValidationTaskGetIdRequest : AbstractValidator<int>
    {
        public ValidationTaskGetIdRequest()
        {
            RuleFor(id => id)
                .NotNull().WithMessage(ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.IdNotNull))
                .GreaterThan(0).WithMessage(ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.IdGreaterThanName));
        }
    }
}
