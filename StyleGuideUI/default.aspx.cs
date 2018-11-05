using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StyleGuideUI
{
    public partial class _default : System.Web.UI.Page
    {
        StyleGuide.Permission mPermission = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            mPermission = new StyleGuide.Permission(this.Page);
            if (!mPermission.AccessStyleGuide())
            {
                Server.Transfer(StyleGuideUI.App_Code.SgCommon.AccessDeniedPage());
            }

            this.Page.MaintainScrollPositionOnPostBack = true;

            if (!IsPostBack)
            {
                StyleGuide.API api = new StyleGuide.API();
                try
                {
                    UcSearch1.Refresh(api);
                }
                finally
                {
                    api.Dispose();
                }
            }
        }
    }
}