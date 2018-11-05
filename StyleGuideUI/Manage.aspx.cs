using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StyleGuideUI
{
    public partial class _Manage : System.Web.UI.Page
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
                if (this.Page.Request.QueryString.Count > 0)
                {
                    if(this.Page.Request.QueryString[0].Split(',').Count() > 1)
                    {
                         ShowModule(this.Page.Request.QueryString[0].Split(',')[0],this.Page.Request.QueryString[0].Split(',')[1]);

                    }
                    else
                    {
                         ShowModule(this.Page.Request.QueryString[0].Split(',')[0],null);
                    }
                }
                else
                {
                    ShowModule("E",null);
                }
            }
        }

        private void ShowModule(string module,string searchText)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                switch (module)
                {
                    case "E":
                        {
                            this.MultiView1.SetActiveView(this.E);
                            this.UcEntityList1.Refresh(api,1,searchText);
                            break;
                        }
                    case "C":
                        {
                            this.MultiView1.SetActiveView(this.C);
                            this.UcCategoryList1.Refresh(api,1);
                            break;
                        }
                    case "ET":
                        {
                            this.MultiView1.SetActiveView(this.ET);
                            this.UcEntityTypeList1.Refresh(api);
                            break;
                        }
                    case "CT":
                        {
                            this.MultiView1.SetActiveView(this.CT);
                            this.UcCategoryTypeList1.Refresh(api);
                            break;
                        }
                    default:
                        {
                            this.MultiView1.SetActiveView(this.E);
                            this.UcEntityList1.Refresh(api,1);
                            break;
                        }
                }
            }
            finally
            {
                api.Dispose();
            }
        }
    }
}