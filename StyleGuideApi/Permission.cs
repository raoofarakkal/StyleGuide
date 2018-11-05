using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace StyleGuide
{
    public class Permission
    {
        private long mGuidAccessStyleGuide = -1;
        private AxConApi.API mAxConApi = null;

        public Permission(System.Web.UI.Page pPage)
        {
            _Permission(pPage.Request.ServerVariables["REMOTE_USER"], pPage.Request.ServerVariables["REMOTE_ADDR"]);
        }

        public Permission(System.Web.HttpContext pContext)
        {
            _Permission(pContext.Request.ServerVariables["REMOTE_USER"], pContext.Request.ServerVariables["REMOTE_ADDR"]);
        }

        public Permission(string pUserName, string pIP)
        {
            _Permission(pUserName, pIP);
        }

        private void _Permission(string pUserName, string pIP)
        {
            var mdb = new Library2.Db3.MyDB(SgConfig.AxConDbSettings());
            mAxConApi = new AxConApi.API(ref mdb);

            mGuidAccessStyleGuide = getAxConUseCaseGuid("AccessStyleGuide");

            if (pUserName.Contains('\\'))
            {
                UserName = pUserName.Split('\\')[1];
            }
            else
            {
                UserName = pUserName;
            }
            IP = pIP;
        }
        public string UserName { get; set; }
        public string IP { get; set; }

        private Exception mException;
        public Exception exception
        {
            get
            {
                return mException;
            }
            set
            {
                mException = value;
            }
        }

        private long getAxConUseCaseGuid(string pKey)
        {
            return Convert.ToInt64(((NameValueCollection)System.Configuration.ConfigurationManager.GetSection("AxConUseCase")).Get(pKey)); ;
        }

        public bool AccessStyleGuide()
        {
            return mAxConApi.isAllowed(mGuidAccessStyleGuide, UserName, IP, ref mException);
        }
    }
}
