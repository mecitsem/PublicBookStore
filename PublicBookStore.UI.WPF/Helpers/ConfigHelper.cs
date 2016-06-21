using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicBookStore.UI.WPF.Classes;

namespace PublicBookStore.UI.WPF.Helpers
{
    public static class ConfigHelper
    {
        public static string ApiUri => ConfigurationManager.AppSettings[Constants.AppSettings.ApiUri];
    }
}
