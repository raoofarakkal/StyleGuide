using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StyleGuideUI.UC
{
    public partial class UcCategoryTypeList : System.Web.UI.UserControl
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
            this.gvCatTypeList.DataSource = api.getAllCategoryTypes();
            this.gvCatTypeList.DataBind();
        }

        public string getMarkUpdatedCall(string name)
        {
            return "UCTL_markUpdated(\"" + name + "\")";
        }

        protected void gvCatTypeList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        api.DropCategoryType(Convert.ToInt64(id));
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

        protected void gvCatTypeList_RowDataBound(object sender, GridViewRowEventArgs e)
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
                foreach (GridViewRow r in this.gvCatTypeList.Rows)
                {
                    string Changed = getPostedValue(r, "hfChanged");
                    string tbId = getPostedValue(r, "tbID");
                    if (Changed == "1" || tbId == "-1")
                    {
                        string tbType = getPostedValue(r, "tbType");
                        StyleGuide.SgCategories.CategoryType type;
                        if (tbId == "-1")
                        {
                            type = new StyleGuide.SgCategories.CategoryType();
                        }
                        else
                        {
                            type = api.getCategoryTypeByID(Convert.ToInt64(tbId));
                        }
                        if (type != null)
                        {
                            type.ID = Convert.ToInt64(tbId);
                            type.Type = tbType;
                            api.SaveCategoryType(type);
                            ShowMessage("Category Type saved");
                        }
                        else
                        {
                            throw new Exception("Category Type ID not found.");
                        }
                    }
                }
                Refresh(api);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error on Save(). " +ex.Message);
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
                StyleGuide.SgCategories.CategoryType type = new StyleGuide.SgCategories.CategoryType();
                type.ID = -1;
                type.Type = "New ";
                StyleGuide.SgCategories.CategoryTypes types = api.getAllCategoryTypes();
                if (types == null)
                {
                    types = new StyleGuide.SgCategories.CategoryTypes();
                }
                types.Add(type);
                this.gvCatTypeList.DataSource = types;
                this.gvCatTypeList.DataBind();
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