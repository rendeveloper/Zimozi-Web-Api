using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Request.CommentRequest
{
    public class ValidationCommentAddRequest : AbstractValidator<TaskCommentsModel>
    {
        public ValidationCommentAddRequest()
        {
            RuleFor(e => e.Comments)
                .NotEmpty().WithMessage(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));
        }
    }
}
