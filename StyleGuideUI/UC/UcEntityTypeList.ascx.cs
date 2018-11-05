using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StyleGuideUI.UC
{
    public partial class UcEntityTypeList : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblResult.Text = "";
        }

        private string getPostedValue(GridViewRow r, string rowControlName)
        {
            string data = null;
            Control c = r.FindControl(rowControlName);
            if (c != null)
            {
                data = this.Request.Form[c.UniqueID];
            }
            return data;
        }

        private void ShowErrorMessage(string ErrMsg)
        {
            this.lblResult.ForeColor = System.Drawing.Color.Red;
            this.lblResult.Font.Bold = true;
            this.lblResult.Text = ErrMsg;
        }
        private void ShowMessage(string msg)
        {
            this.lblResult.ForeColor = System.Drawing.Color.Green;
            this.lblResult.Font.Bold = true;
            this.lblResult.Text = msg;
        }

        public void Refresh(StyleGuide.API api)
        {
            this.gvEntTypeList.DataSource = api.getAllEntityTypes();
            this.gvEntTypeList.DataBind();
        }

        public string getMarkUpdatedCall(string name)
        {
            return "UETL_markUpdated(\"" + name + "\")";
        }

        protected void gvEntTypeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName.ToLower();
            string id = e.CommandArgument.ToString();
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                if (cmd == "DeleteRow".ToLower())
                {
                    if (id != "-1")
                    {
                        api.DropEntityType(Convert.ToInt64(id));
                    }
                }
                Refresh(api);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(" REFERENCE "))
                {
                    ShowErrorMessage("Type already in use, and cannot be deleted.<br /><br />" + ex.Message);
                }
                else
                {
                    ShowErrorMessage("Error on DeleteRow(). " + ex.Message);
                }
            }
            finally
            {
                api.Dispose();
            }
        }

        protected void gvEntTypeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool borderleft = false;
            foreach (TableCell tc in e.Row.Cells)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (!borderleft)
                    {
                        tc.Attributes["style"] = "border-left:1px solid #c3cecc;border-right:1px solid #c3cecc;border-bottom:1px solid #c3cecc;";
                        borderleft = true;
                    }
                    else
                    {
                        tc.Attributes["style"] = "border-right:1px solid #c3cecc;border-bottom:1px solid #c3cecc;";
                    }
                }
                else
                {
                    if (!borderleft)
                    {
                        tc.Attributes["style"] = "border-left:1px solid #c3cecc;border-right:1px solid #c3cecc;";
                        borderleft = true;
                    }
                    else
                    {
                        tc.Attributes["style"] = "border-right:1px solid #c3cecc;";
                    }
                }
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                foreach (GridViewRow r in this.gvEntTypeList.Rows)
                {
                    string Changed = getPostedValue(r, "hfChanged");
                    string tbId = getPostedValue(r, "tbID");
                    if (Changed == "1" || tbId == "-1")
                    {
                        string tbType = getPostedValue(r, "tbType");
                        StyleGuide.SgEntities.EntityType type;
                        if (tbId == "-1")
                        {
                            type = new StyleGuide.SgEntities.EntityType();
                        }
                        else
                        {
                            type = api.getEntityTypeByID(Convert.ToInt64(tbId));
                        }
                        if (type != null)
                        {
                            type.ID = Convert.ToInt64(tbId);
                            type.Type = tbType;
                            api.SaveEntityType(type);
                            ShowMessage("Entity Type saved");
                        }
                        else
                        {
                            throw new Exception("Entity Type ID not found.");
                        }
                    }
                }
                Refresh(api);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error on Save(). " + ex.Message);
            }
            finally
            {
                api.Dispose();
            }
        }

        protected void btNew_Click(object sender, EventArgs e)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                btSave_Click(this.btSave, null);
                StyleGuide.SgEntities.EntityType type = new StyleGuide.SgEntities.EntityType();
                type.ID = -1;
                type.Type = "New ";
                StyleGuide.SgEntities.EntityTypes types = api.getAllEntityTypes();
                if (types == null)
                {
                    types = new StyleGuide.SgEntities.EntityTypes();
                }
                types.Add(type);
                this.gvEntTypeList.DataSource = types;
                this.gvEntTypeList.DataBind();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error on CreateNew(). " + ex.Message);
            }
            finally
            {
                api.Dispose();
            }

        }
    }
}