using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;
using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;

namespace ZimoziSolutions.Validations.Custom
{
    public static class CustomValidation
    {
        public static IRuleBuilderOptions<T, string> JustLetters<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder
                .Must(x => (string.IsNullOrEmpty(x) || Regex.IsMatch(x, ApplicationContext.Parameters.RegexJustLetters)))
                .WithMessage(x => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, nameof(JustLetters))} {x.GetType().Name}.");

        public static IRuleBuilderOptions<T, string> JustNumbers<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder
                .Must(x => (string.IsNullOrEmpty(x) || Regex.IsMatch(x, ApplicationContext.Parameters.RegexJustNumbers)))
                .WithMessage(x => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, nameof(JustNumbers))} {x.GetType().Name}.");

        public static IRuleBuilderOptions<T, string> BeAnEmail<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder
                .Must(x => (string.IsNullOrEmpty(x) || Regex.IsMatch(x, ApplicationContext.Parameters.RegexBeAnEmail)))
                .WithMessage(x => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, nameof(BeAnEmail))} {x.GetType().Name}.");

        public static IRuleBuilderOptions<T, string> BeAPhone<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder
                .Must(x => (string.IsNullOrEmpty(x) || Regex.IsMatch(x, ApplicationContext.Parameters.RegexBeAPhone)))
                .WithMessage(x => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, nameof(BeAPhone))} {x.GetType().Name}.");

        public static IRuleBuilderOptions<T, string> BeADate<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder
                .Must(x => x.ValidateDateFormat())
                .WithMessage(x => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, nameof(BeADate))} {x.GetType().Name}.");

        public static IRuleBuilderOptions<T, string> MaxDifferenceInMinutes<T>(this IRuleBuilder<T, string> ruleBuilder, int maxMinutes) =>
            ruleBuilder
                .Must(x => ValidateDurationDate(x, maxMinutes))
                .WithMessage(x => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.MaxDifferenceInMinutesFirstName)} " +
                $"{maxMinutes} {ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.MaxDifferenceInMinutesLastName)} {x.GetType().Name}.");

        public static IRuleBuilderOptions<T, DateTime> MaxDifferenceInMinutes<T>(this IRuleBuilder<T, DateTime> ruleBuilder, int maxMinutes) =>
             ruleBuilder
                 .Must(x => ValidateDurationDate(x.ToString(ApplicationContext.Parameters.DateFormat), maxMinutes))
                 .WithMessage((x, v) => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.MaxDifferenceInMinutesFirstName)} " +
                 $"{maxMinutes} {ApplicationContext.Texts.GetValue(Constants.CustomValidationName, Constants.MaxDifferenceInMinutesLastName)} {v.GetType().Name}.");

        public static IRuleBuilderOptions<T, string> BeAShortDate<T>(this IRuleBuilder<T, string> ruleBuilder) =>
             ruleBuilder
                 .Must(x => !x.ValidateExactFormatDate(ApplicationContext.Parameters.DateFormat))
                 .WithMessage((x, v) => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, nameof(BeAShortDate))} {v.GetType().Name}.");

        public static IRuleBuilderOptions<T, string> BeLessThanToDate<T>(this IRuleBuilder<T, string> ruleBuilder) =>
             ruleBuilder
                 .Must(x => DateTime.UtcNow > Convert.ToDateTime(x))
                 .WithMessage((x, v) => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, nameof(BeLessThanToDate))} {v.GetType().Name}.");

        public static IRuleBuilderOptions<T, string> IsNotEmptyThenLenght<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max) =>
             ruleBuilder
                 .Must(x => (string.IsNullOrEmpty(x) || (x.Length >= min && x.Length <= max)))
                 .WithMessage((x, v) => $"{ApplicationContext.Texts.GetValue(Constants.CustomValidationName, nameof(IsNotEmptyThenLenght))} {v.GetType().Name}.");

        public static bool ValidateExactFormatDate(this string value, string format)
        {
            return DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public static bool ValidateDateFormat(this string value)
        {
            return DateTime.TryParse(value, out _) && DateTime.TryParseExact(value, ApplicationContext.Parameters.DateFormat, null, DateTimeStyles.None, out _);
        }

        public static bool ValidateDurationDate(string value, int maxMinutes)
        {
            if (!value.ValidateDateFormat())
                return false;

            string dateAcceptTerms = value.TrimStart().TrimEnd();
            string currentDate = DateTime.UtcNow.ToString(ApplicationContext.Parameters.DateFormat);
            TimeSpan diff = Convert.ToDateTime(currentDate) - Convert.ToDateTime(dateAcceptTerms);

            if (diff.TotalMinutes >= -10 && diff.TotalMinutes <= maxMinutes)
                return true;

            return false;
        }

        public static bool ValidateMinDiffBetweenTwoDates(DateTime initialDate, DateTime finalDate, int minYearsDiff, int minDayDiff = 0)
        {
            DateTime pivotDate = finalDate.AddDays(-minDayDiff).AddYears(-minYearsDiff);

            if (pivotDate < initialDate)
                return false;

            return true;
        }

        public static int GetAgeFromBirthDate(DateTime birthDate)
        {
            int age = DateTime.UtcNow.Year - birthDate.Year;

            if (birthDate > DateTime.UtcNow.AddYears(-age))
                age--;

            return age;
        }
    }
}
