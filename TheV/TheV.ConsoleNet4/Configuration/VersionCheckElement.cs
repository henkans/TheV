using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheV.ConsoleNet4.Configuration
{

    public class VersionCheckElement : ConfigurationElement
    {
        [ConfigurationProperty("title", IsRequired = true)]
        public string Title => this["title"] as string;

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name => this["name"] as string;

        [ConfigurationProperty("filename", IsRequired = true)]
        public string Filename => this["filename"] as string;

        [ConfigurationProperty("arguments", IsRequired = true)]
        public string Arguments => this["arguments"] as string;
    }
}
