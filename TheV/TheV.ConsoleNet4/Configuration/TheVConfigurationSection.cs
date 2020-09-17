using System.Configuration;

namespace TheV.ConsoleNet4.Configuration
{
    public class TheVConfigurationSection : ConfigurationSection
    {
        public static TheVConfigurationSection GetConfig()
        {
            return (TheVConfigurationSection)ConfigurationManager.GetSection("TheV");
        }

        [ConfigurationProperty("VersionChecks")]
        [ConfigurationCollection(typeof(VersionCheckElement), AddItemName = "VersionCheck")]
        public VersionCheckElementCollection VersionChecks
        {
            get
            {
                object o = this["VersionChecks"];
                return o as VersionCheckElementCollection;
            }
        }
    }
}
