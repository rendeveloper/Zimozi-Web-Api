using ZimoziSolutions.Common.Languages;
using ZimoziSolutions.Common.Parameter;

namespace ZimoziSolutions.Common.Context
{
    public class ApplicationContext
    {
        public static AppSetting? AppSetting { get; set; }
        public static Parameters Parameters { get; set; } = new Parameters();
        public static Texts Texts { get; set; } = new Texts();
    }
}
