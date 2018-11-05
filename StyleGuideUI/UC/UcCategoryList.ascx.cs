using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StyleGuideUI.UC
{
    public partial class UcCategoryList : System.Web.UI.UserControl
    {
        private const Int32 REC_PER_PAGE = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblResult.Text = "";
            _Library._Web._Common.MaintainScrollPos(this.divGv);
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

        public void Refresh(StyleGuide.API api, Int32 pPageNo)
        {
            StyleGuide.PagingInfo pgInf = null;
            if (pPageNo > 0)
            {
                pgInf = new StyleGuide.PagingInfo();
                pgInf.RecordsPerPage = REC_PER_PAGE;
                pgInf.CurrentPage = pPageNo;
            }
            StyleGuide.SgCategories.Categories cats = null;
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                if (this.rbSearchOpt.SelectedItem.Value == "C")
                {
                    cats = api.getAllCategoriesNameContains(tbSearch.Text, pgInf);
                }
                else
                {
                    cats = api.getAllCategoriesNameStartsWith(tbSearch.Text, pgInf);
                }
            }
            else
            {
                cats = api.getAllCategories(pgInf);
            }
            this.gvCatList.DataSource = cats;
            this.gvCatList.DataBind();
            SetPaging(cats, (pPageNo == -1 ? true : false));
        }

        public string getMarkUpdatedCall(string name)
        {
            return "UCL_markUpdated(\"" + name + "\")";
        }

        protected void gvCatList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        api.DropCategory(Convert.ToInt64(id));
                    }
                }
                Refresh(api,UcPaging1.CurrentPage);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(" REFERENCE "))
                {
                    ShowErrorMessage("Category already in use, and cannot be deleted.<br /><br />" + ex.Message);
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

        protected void gvCatList_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlCatTypes = (e.Row.FindControl("ddlCatTypes") as DropDownList);
                if (ddlCatTypes != null)
                {
                    try
                    {
                        StyleGuide.API api = new StyleGuide.API();
                        try 
                        {
                            ddlCatTypes.DataSource = api.getAllCategoryTypes(null, true);
                            ddlCatTypes.DataTextField = "Type";
                            ddlCatTypes.DataValueField = "ID";
                            ddlCatTypes.DataBind();
                            ddlCatTypes.Style.Add("-webkit-appearance","none");
                            StyleGuide.SgCategories.CategoryType catType = (StyleGuide.SgCategories.CategoryType)DataBinder.Eval(e.Row.DataItem, "Type");
                            if (catType != null)
                            {
                                int idx = _Library._Web._Common.FindDdlItemIndex(ddlCatTypes, catType.ID.ToString(), true);
                                ddlCatTypes.SelectedIndex = idx;
                            }
                            else
                            {
                                ddlCatTypes.Items.Insert(0, new ListItem("Please select", ""));
                            }

                            long catID = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "ID"));
                            if (catID == -1)
                            {
                                (e.Row.FindControl("tbID") as TextBox).ForeColor = System.Drawing.Color.Red;
                                (e.Row.FindControl("tbName") as TextBox).ForeColor = System.Drawing.Color.Red;
                                (e.Row.FindControl("ddlCatTypes") as DropDownList).ForeColor = System.Drawing.Color.Red;
                                (e.Row.FindControl("tbNotes") as TextBox).ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            api.Dispose();
                        }

                    }
                    catch (Exception) { }
                }
            }

        }


        protected void btSave_Click(object sender, EventArgs e)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                Int32 PgIdx = UcPaging1.CurrentPage;
                foreach (GridViewRow r in this.gvCatList.Rows)
                {
                    string Changed = getPostedValue(r, "hfChanged");
                    string tbId = getPostedValue(r, "tbID");
                    if (Changed == "1" || tbId == "-1")
                    {
                        string tbName = getPostedValue(r, "tbName");
                        string ddlCatType = getPostedValue(r, "ddlCatTypes");
                        string tbNotes = getPostedValue(r, "tbNotes");

                        StyleGuide.SgCategories.Category cat;// = api.getCategoryByID(Convert.ToInt64(tbId));
                        StyleGuide.SgCategories.CategoryType catType = api.getCategoryTypeByID(Convert.ToInt64(ddlCatType));

                        if (tbId == "-1")
                        {
                            cat = new StyleGuide.SgCategories.Category();
                        }
                        else
                        {
                            cat = api.getCategoryByID(Convert.ToInt64(tbId));
                        }
                        if (cat != null)
                        {
                            cat.Name = tbName;
                            cat.Type.ID = catType.ID;
                            cat.Type.Type = catType.Type;
                            cat.Notes = tbNotes;
                            cat.LMBY = StyleGuideUI.App_Code.SgCommon.getLoggedUser();
                            api.SaveCategory(cat);
                            ShowMessage("Category saved");
                            if (tbId == "-1")
                            {
                                Refresh(api, UcPaging1.TotalPages);
                                PgIdx = UcPaging1.TotalPages;
                            }
                            else
                            {
                                PgIdx = UcPaging1.CurrentPage;
                            }
                        }
                        else
                        {
                            throw new Exception("Category ID not found.");
                        }
                    }
                }
                Refresh(api,PgIdx);
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
                this.tbSearch.Text = "";
                btSave_Click(this.btSave, null);
                Refresh(api, UcPaging1.TotalPages);

                StyleGuide.PagingInfo pgInf = null;
                if (this.UcPaging1.CurrentPage > 0)
                {
                    pgInf = new StyleGuide.PagingInfo();
                    pgInf.RecordsPerPage = REC_PER_PAGE;
                    pgInf.CurrentPage = this.UcPaging1.TotalPages;
                }

                StyleGuide.SgCategories.Categories cats = api.getAllCategories(pgInf);
                if (cats == null)
                {
                    cats = new StyleGuide.SgCategories.Categories();
                }
                
                StyleGuide.SgCategories.Category cat = new StyleGuide.SgCategories.Category();
                cat.ID = -1;
                cat.Name = "New ";
                cats.Add(cat);

                this.gvCatList.DataSource = cats;
                this.gvCatList.DataBind();
                SetPaging(cats, true);

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

        protected void on_PageChange(object sender, int Page)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                Refresh(api, Page);
            }
            finally
            {
                api.Dispose();
            }

        }

        private void SetPaging(StyleGuide.SgCategories.Categories cats, bool Move2LastPage)
        {
            if (cats != null)
            {
                this.UcPaging1.TotalPages = cats.PageInfo.TotalPages;
                if (Move2LastPage)
                {
                    this.UcPaging1.CurrentPage = this.UcPaging1.TotalPages;
                }
                else
                {
                    this.UcPaging1.CurrentPage = Convert.ToInt32(cats.PageInfo.CurrentPage);
                }
                this.UcPaging1.Refresh();
            }
            else
            {
                this.UcPaging1.TotalPages = 0;
                this.UcPaging1.Enabled = false;
            }
        }

        public string getLMDTBY(GridViewRow gvSelectedRow)
        {
            var lmBy = DataBinder.Eval(gvSelectedRow.DataItem, "LMBY");
            var lmDt = DataBinder.Eval(gvSelectedRow.DataItem, "LMDT");
            if (lmBy != null)
            {
                if (lmBy.ToString().Contains("\\"))
                {
                    lmBy = lmBy.ToString().Split('\\')[1];
                }
                if (StyleGuideUI.App_Code.SgCommon.IsDate(lmDt))
                {
                    return string.Format("by {0} <br/>on {1}", lmBy, ((DateTime)lmDt).ToString("dd MMM, yyyy HH:mm"));
                }
                else
                {
                    return lmBy.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                Refresh(api, 1);
            }
            finally
            {
                api.Dispose();
            }
        }

        protected void btClear_Click(object sender, EventArgs e)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                this.tbSearch.Text = "";
                Refresh(api, 1);
            }
            finally
            {
                api.Dispose();
            }
        }



    }
}