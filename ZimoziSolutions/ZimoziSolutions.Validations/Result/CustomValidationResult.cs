using FluentValidation.Results;

namespace ZimoziSolutions.Validations.Result
{
    public class CustomValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
        public dynamic Value { get; set; }
        private List<CustomValidationResult> Validations { get; set; }

        public CustomValidationResult()
        {
            Errors = new List<string>();
            IsValid = false;
        }

        public CustomValidationResult(dynamic value)
        {
            Errors = new List<string>();
            IsValid = false;
            Value = value;
        }

        public CustomValidationResult(CustomValidationResult[] validations)
        {
            Errors = validations.Where(x => !x.IsValid).SelectMany(x => x.Errors).ToList();
            IsValid = validations.All(v => v.IsValid);
            Validations = validations.ToList();
        }

        public CustomValidationResult(ValidationResult result)
        {
            Errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            IsValid = result.IsValid;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public CustomValidationResult Validate()
        {
            if (Validations is null)
                return this;

            if (Validations != null && Validations.All(v => v.IsValid))
            {
                Validations = null;
                IsValid = true;
                return this;
            }
            else
            {
                IEnumerable<List<string>> errors = Validations.Where(v => !v.IsValid).Select(v => v.Errors);
                Errors = CreateErrorList(errors).ToList();
                IsValid = false;
                return this;
            }
        }

        private static IEnumerable<string> CreateErrorList(IEnumerable<List<string>> errors)
        {
            foreach (List<string> errorList in errors)
                foreach (string error in errorList)
                    yield return error;
        }
    }
}
