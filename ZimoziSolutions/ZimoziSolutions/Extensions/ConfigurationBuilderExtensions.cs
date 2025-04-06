using ZimoziSolutions.Common.Constants;
using ZimoziSolutions.Common.Context;
using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Common.Parameter;
using ZimoziSolutions.Exceptions.Api;

namespace ZimoziSolutions.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static void AddApplicationContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            var outPutDirectory = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var appsettingsFilePath = Path.Combine(outPutDirectory, Constants.AppsettingsFilePath);
            var parametersFilePath = Path.Combine(outPutDirectory, Constants.ParametersFilePath);
            var textsFilePath = Path.Combine(outPutDirectory, Constants.TextsFilePath);

            configuration.AddJsonFile(appsettingsFilePath, true, true);
            ApplicationContext.AppSetting = configuration.GetSection(nameof(AppSetting)).Get<AppSetting>();
            configuration.AddJsonFile(parametersFilePath, true, true);
            ApplicationContext.Parameters = configuration.GetSection(nameof(Parameters)).Get<Parameters>();
            configuration.AddJsonFile(textsFilePath, true, true);
            ApplicationContext.Texts = configuration.GetSection(nameof(Texts)).Get<Texts>();

            if (ApplicationContext.AppSetting == null)
                throw new ApiException(Constants.WrongDBConnection + outPutDirectory + appsettingsFilePath);

            if (ApplicationContext.Parameters == null)
                throw new ApiException(Constants.ParameterFileNotNound + outPutDirectory + parametersFilePath);

            if (ApplicationContext.Texts == null)
                throw new ApiException(Constants.TextsFileNotFound + outPutDirectory + textsFilePath);
        }
    }
}
