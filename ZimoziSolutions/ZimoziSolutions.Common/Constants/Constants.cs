
namespace ZimoziSolutions.Common.Constants
{
    public static class Constants
    {
        #region Application
        public const string GenericContoller = "Controller";
        public const string TaskGet = "Get";
        public const string TaskPost = "Post";
        public const string TaskPut = "Put";
        public const string AssemblyNameCore = "ZimoziSolutions.Core";
        public const string TaskColumnTypeName = "varchar(100)";
        public const string TaskColumnTypeNameEmail = "varchar(50)";
        public const string SharedName = "Shared";
        public const string CustomValidationName = "CustomValidation";
        public const string PagerValidationName = "PagerValidation";
        public const string TaskValidationName = "TaskValidation";

        //For Custom
        public const string CustomGet = "Get";
        public const string CustomGetId = "GetId";
        public const string CustomPost = "Post";
        public const string CustomPut = "Put";
        public const string CustomDel = "Delete";
        public const string CustomAuthName = "Auth";


        //Localhost
#if DEBUG
        public const string AppsettingsFilePath = @"ZimoziSolutions\appsettings.json";
        public const string ParametersFilePath = @"ZimoziSolutions\SolutionItems\parameters.json";
        public const string TextsFilePath = @"ZimoziSolutions\SolutionItems\texts.json";
#else
        //dotnet publish
        //public const string AppsettingsFilePath = @"ZimoziSolutions\out\appsettings.Development.json";
        //public const string ParametersFilePath = @"ZimoziSolutions\out\SolutionItems\parameters.json";
        //public const string TextsFilePath = @"ZimoziSolutions\out\SolutionItems\texts.json";
        //Docker image
        public const string AppsettingsFilePath = @"ZimoziSolutions/appsettings.Development.json";
        public const string ParametersFilePath = @"ZimoziSolutions/SolutionItems/parameters.json";
        public const string TextsFilePath = @"ZimoziSolutions/SolutionItems/texts.json";
#endif
        #endregion Application

        #region Messages
        public const string MaxDifferenceInMinutesFirstName = "MaxDifferenceInMinutesFirst";
        public const string MaxDifferenceInMinutesLastName = "MaxDifferenceInMinutesLast";
        public const string MissingDataName = "MissingData";
        public const string ModelErrorName = "ModelError";
        public const string NoContentName = "NoContent";
        public const string AlreadyExistsName = "AlreadyExists";
        public const string NameNotEmptyName = "NameNotEmpty";
        public const string NameLengthName = "NameLength";
        public const string EmailNotEmpty = "EmailNotEmpty";
        public const string EmailLengthName = "EmailLength";
        public const string IdGreaterThanName = "IdGreaterThan";
        public const string PageNumberName = "PageNumber";
        public const string PageSizeName = "PageSize";
        public const string WrongDBConnection = "Wrong DB connection string.";
        public const string ParameterFileNotNound = "Parameter file not found.";
        public const string TextsFileNotFound = "Texts file not found.";
        public const string AlreadyExistsUsername = "UserAlreadyExists";
        public const string InvalidUserAndPass = "InvalidUserAndPass";
        #endregion Messages
    }
}
