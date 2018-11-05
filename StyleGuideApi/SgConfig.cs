using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StyleGuide
{
    class SgConfig
    {
        public static string DbConString()
        {
            return Decrypt(System.Web.Configuration.WebConfigurationManager.AppSettings["StyleGuideDbConString"].ToString());
        }

        private static string Decrypt(string str)
        {
            string ret="";
            try
            {
                ret = Library2.Sys.Security.Rijndael.Decrypt(str, "AjeSg");
            }
            catch (Exception)
            {
                ret = str;
            }
            return ret;
        }

        public static Library2.Db3.oDbSettings AxConDbSettings()
        {
            return new Library2.Db3.oDbSettings("AxConDb");//, "jazeera");
        }
    }
}
