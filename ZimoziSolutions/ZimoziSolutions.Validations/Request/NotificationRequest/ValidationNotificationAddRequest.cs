using FluentValidation;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request.NotificationRequest
{
    public class ValidationNotificationAddRequest : AbstractValidator<NotificationsModel>
    {
        public ValidationNotificationAddRequest()
        {
            RuleFor(e => e.TaskUpdates)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));
        }
    }
}
