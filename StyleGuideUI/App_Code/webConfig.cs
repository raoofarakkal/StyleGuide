using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StyleGuideUI.App_Code
{
    public class webConfig
    {

        public static int ShowEntityCountModifiedInLastXdays()
        {
            return Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowEntityCountModifiedInLastXdays"].ToString());
        }
    }
}