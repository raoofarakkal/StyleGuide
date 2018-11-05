using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StyleGuideUI.App_Code
{
    public class SgCommon
    {
        public static string getLoggedUser()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static bool IsDate(Object obj)
        {
            string strDate = obj.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumeric(string value)
        {
            try
            {
                int result = int.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string AccessDeniedPage()
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings["AccessDeniedPage"].ToString();
        }
    }
}