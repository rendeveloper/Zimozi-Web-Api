using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request.NotificationRequest
{
    public class ValidationNotificationUpdateRequest : AbstractValidator<NotificationsModel>
    {
        public ValidationNotificationUpdateRequest()
        {
            RuleFor(e => e.Id)
                .GreaterThan(0).WithMessage(ApplicationContext.Texts.GetValue(Constants.TaskValidationName, Constants.IdGreaterThanName)); ;

            RuleFor(e => e.TaskUpdates)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));
        }
    }
}
