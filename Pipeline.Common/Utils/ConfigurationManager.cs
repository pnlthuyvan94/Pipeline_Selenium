namespace Pipeline.Common.Utils
{
    public static class ConfigurationManager
    {
        public static string GetValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key]; ;
        }
    }
    
}
