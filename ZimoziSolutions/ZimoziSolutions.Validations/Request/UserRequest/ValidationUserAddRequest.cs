using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request.UserRequest
{
    public class ValidationUserAddRequest : AbstractValidator<UserCustomModel>
    {
        public ValidationUserAddRequest()
        {
            RuleFor(e => e.Username)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));
        }
    }
}
