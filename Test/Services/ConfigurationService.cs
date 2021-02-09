namespace Test.Services
{
    using System.Collections.Specialized;
    using System.Configuration;
    using Extensions;

    public class ConfigurationService : IConfigurationService
    {
        public NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        public ConnectionStringSettingsCollection ConnectionStrings => ConfigurationManager.ConnectionStrings;

        public string ConnectionString(string name)
        {
            return ConnectionStrings[name]?.ConnectionString;
        }

        public T GetSection<T>(string sectionName)
        {
            return (T) ConfigurationManager.GetSection(sectionName);
        }

        public T GetAppSettingAs<T>(string key)
        {
            return AppSettings[key].As<T>();
        }

        public T GetAppSettingAsOrDefault<T>(string key)
        {
            return AppSettings[key].AsOrDefault<T>();
        }
    }
}