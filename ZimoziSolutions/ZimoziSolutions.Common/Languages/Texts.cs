
namespace ZimoziSolutions.Common.Languages
{
    public class Texts
    {
        public IDictionary<string, string> Shared { get; set; }
        public IDictionary<string, string> CustomValidation { get; set; }
        public IDictionary<string, string> PagerValidation { get; set; }
        public IDictionary<string, string> TaskValidation { get; set; }

        public string GetValue(string methodName, string key)
        {
            string value = methodName switch
            {
                nameof(Shared) => Shared[key],
                nameof(CustomValidation) => CustomValidation[key],
                nameof(PagerValidation) => PagerValidation[key],
                nameof(TaskValidation) => TaskValidation[key],
                _ => String.Empty,
            };

            return value;
        }
    }
}
