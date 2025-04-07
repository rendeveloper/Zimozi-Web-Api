using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request
{
    public class ValidationTaskUpdateRequest : AbstractValidator<TaskModel>
    {
        public ValidationTaskUpdateRequest()
        {
            RuleFor(e => e.TaskId)
                .GreaterThan(0).WithMessage(ApplicationContext.Texts.GetValue(Constants.TaskValidationName, Constants.IdGreaterThanName)); ;

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));

            RuleFor(e => e.Status)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));
        }
    }
}
