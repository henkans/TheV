using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheV.ConsoleNet4.Configuration
{


    public class VersionCheckElementCollection : ConfigurationElementCollection
    {
        public VersionCheckElement this[int index]
        {
            get => BaseGet(index) as VersionCheckElement;
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new VersionCheckElement this[string responseString]
        {
            get => (VersionCheckElement)BaseGet(responseString);
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new VersionCheckElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((VersionCheckElement)element).Name;
        }
    }
}
