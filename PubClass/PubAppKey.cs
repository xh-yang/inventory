using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PublicClass
{
    public class PubAppKey
    {
        public static string GetSetting(string key)
        {
            return ConfigurationSettings.AppSettings.Get(key);
        }
    }
}
