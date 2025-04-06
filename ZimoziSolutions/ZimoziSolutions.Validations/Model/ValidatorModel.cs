using FluentValidation;
using FluentValidation.Results;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Validations.Result;

namespace ZimoziSolutions.Validations.Model
{
    public class ValidatorModel<T>
    {
        public CustomValidationResult Validate(IValidator validator, IDictionary<string, object> arguments)
        {
            T entity = ValidatorModel<T>.GetObject(arguments);
            IValidator<T> realValidator = (IValidator<T>)validator;
            ValidationResult result = realValidator.Validate(entity);

            return new CustomValidationResult(result);
        }

        public CustomValidationResult Validate(IValidator[] validators, IDictionary<string, object> arguments)
        {
            int count = 0;
            if (validators.Length == arguments.Count)
            {
                CustomValidationResult[] results = new CustomValidationResult[validators.Length];
                foreach (KeyValuePair<string, object> argument in arguments)
                {
                    ValidationContext<object> context = new(argument.Value);
                    ValidationResult result = validators[count].Validate(context);
                    results[count] = new CustomValidationResult(result);
                    count++;
                }
                CustomValidationResult validationRsults = new(results);
                return validationRsults.Validate();
            }
            CustomValidationResult error = new();
            error.Errors.Add(ApplicationContext.Texts.GetValue(Constants.SharedName, Constants.MissingDataName));
            return error;
        }

        private static T GetObject(IDictionary<string, object> arguments)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);
            if (arguments.Any())
                obj = (T)(arguments.First().Value);

            return (T)obj;
        }
    }
}
