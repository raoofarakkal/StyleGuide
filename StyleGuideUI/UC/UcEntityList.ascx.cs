using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StyleGuideUI.UC
{
    public partial class UcEntityList : System.Web.UI.UserControl
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

        public void Refresh(StyleGuide.API api, Int32 pPageNo,string searchText = null)
        {
            StyleGuide.PagingInfo pgInf = null;
            if (pPageNo > 0)
            {
                pgInf = new StyleGuide.PagingInfo();
                pgInf.RecordsPerPage = REC_PER_PAGE;
                pgInf.CurrentPage = pPageNo;
            }
            if (searchText != null)
            {
                tbSearch.Text = searchText;
            }
            StyleGuide.SgEntities.Entities ents = null;
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                if (tbSearch.Text.StartsWith("{ID}"))
                {
                    string temp = tbSearch.Text.Replace("{ID}", "");
                    if (StyleGuideUI.App_Code.SgCommon.IsNumeric(temp))
                    {
                        long entID = Convert.ToInt64(temp);
                        StyleGuide.SgEntities.Entity ent = null;
                        ent = api.getEntityByID(entID);
                        ents = new StyleGuide.SgEntities.Entities();
                        ents.Add(ent);
                    }
                    else
                    {
                        ents = api.getAllEntities(pgInf);
                    }
                }
                else
                {
                    if (this.rbSearchOpt.SelectedItem.Value == "C")
                    {
                        ents = api.getAllEntitiesNameContains(tbSearch.Text, pgInf);
                    }
                    else
                    {
                        ents = api.getAllEntitiesNameStartsWith(tbSearch.Text, pgInf);
                    }
                }
            }
            else
            {
                ents = api.getAllEntities(pgInf);
            }
            this.gvEntList.DataSource = ents;
            this.gvEntList.DataBind();
            SetPaging(ents, (pPageNo == -1 ? true : false));

        }

        public string getMarkUpdatedCall(string name)
        {
            return "UEL_markUpdated(\"" + name + "\")";
        }

        protected void gvEntList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName.ToLower();
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                switch (cmd)
                {
                    case "deleterow":
                        {
                            string entID = e.CommandArgument.ToString();
                            if (entID != "-1")
                            {
                                try
                                {
                                    api.DropEntity(Convert.ToInt64(entID));
                                    initHandler();
                                }
                                catch (Exception ex) 
                                {
                                    if (ex.Message.Contains(" REFERENCE "))
                                    {
                                        throw new Exception("Entity already in use, and cannot be deleted.<br /><br />" + ex.Message);
                                    }
                                    else
                                    {
                                        throw new Exception("Error on DeleteRow(). " + ex.Message);
                                    }
                                }
                            }
                            Refresh(api,UcPaging1.CurrentPage);
                            break;
                        }
                    case "catnew": //Image Button
                        {
                            GridViewRow gvRow = ((e.CommandSource as Control).NamingContainer as GridViewRow);
                            if (gvRow != null)
                            {
                                ImageButton btCatNew = (ImageButton)(e.CommandSource as Control);
                                System.Web.UI.HtmlControls.HtmlGenericControl divAddNewCat = (System.Web.UI.HtmlControls.HtmlGenericControl)gvRow.FindControl("divAddNewCat");
                                DropDownList ddlEntCatList = (DropDownList)gvRow.FindControl("ddlEntCatList");
                                ddlEntCatList.DataSource = api.getAllCategories(null,true);
                                ddlEntCatList.DataBind();
                                divAddNewCat.Visible = true;
                                btCatNew.Visible = false;
                            }

                            break;
                        }
                    case "catadd": // Buton
                        {
                            string entID = e.CommandArgument.ToString();
                            if (entID != "-1")
                            {
                                try
                                {
                                    GridViewRow gvRow = ((e.CommandSource as Control).NamingContainer as GridViewRow);
                                    if (gvRow != null)
                                    {
                                        ImageButton btCatNew = (ImageButton)gvRow.FindControl("btCatNew");
                                        System.Web.UI.HtmlControls.HtmlGenericControl divAddNewCat = (System.Web.UI.HtmlControls.HtmlGenericControl)gvRow.FindControl("divAddNewCat");
                                        DropDownList ddlEntCatList = (DropDownList)gvRow.FindControl("ddlEntCatList");
                                        string catID = ddlEntCatList.SelectedItem.Value;
                                        
                                        api.AddCategoryToEntity(Convert.ToInt64(catID), Convert.ToInt64(entID));
                                        StyleGuide.SgEntities.Entity ent = api.getEntityByID(Convert.ToInt64(entID));
                                        if (ent != null)
                                        {
                                            ent.LMBY = StyleGuideUI.App_Code.SgCommon.getLoggedUser();
                                            api.SaveEntity(ent);
                                        }


                                        divAddNewCat.Visible = false;
                                        btCatNew.Visible = true;
                                    }
                                }
                                catch (Exception ex) 
                                {
                                    throw new Exception("Error on AddCategoryToEntity(). " + ex.Message);
                                }
                            }
                            Refresh(api, UcPaging1.CurrentPage);
                            break;
                        }
                    case "cataddcancel": //Button
                        {
                            string entID = e.CommandArgument.ToString();
                            if (entID != "-1")
                            {
                                try
                                {
                                    GridViewRow gvRow = ((e.CommandSource as Control).NamingContainer as GridViewRow);
                                    if (gvRow != null)
                                    {
                                        ImageButton btCatNew = (ImageButton)gvRow.FindControl("btCatNew");
                                        System.Web.UI.HtmlControls.HtmlGenericControl divAddNewCat = (System.Web.UI.HtmlControls.HtmlGenericControl)gvRow.FindControl("divAddNewCat");
                                        btCatNew.Visible = true;
                                        divAddNewCat.Visible = false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Error on AddCategoryToEntity(). " + ex.Message);
                                }
                            }
                            Refresh(api, UcPaging1.CurrentPage);
                            break;
                        }
                    default: 
                        {
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
            finally
            {
                api.Dispose();
            }
        }


        protected void gvEntList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool borderleft = false;
            int cellIdx = 0;
            foreach (TableCell tc in e.Row.Cells)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (!borderleft)
                    {
                        if (cellIdx == 1)
                        {
                            tc.Attributes["style"] = "border-left:1px solid #c3cecc;border-right:1px solid #c3cecc;border-bottom:1px solid #c3cecc;display:none;";
                        }
                        else
                        {
                            tc.Attributes["style"] = "border-left:1px solid #c3cecc;border-right:1px solid #c3cecc;border-bottom:1px solid #c3cecc;";
                        }
                        borderleft = true;
                    }
                    else
                    {
                        if (cellIdx == 1)
                        {
                            tc.Attributes["style"] = "border-right:1px solid #c3cecc;border-bottom:1px solid #c3cecc;display:none;";
                        }
                        else
                        {
                            tc.Attributes["style"] = "border-right:1px solid #c3cecc;border-bottom:1px solid #c3cecc;";
                        }
                    }
                }
                else
                {
                    if (!borderleft)
                    {
                        if (cellIdx == 1)
                        {
                            tc.Attributes["style"] = "border-left:1px solid #c3cecc;border-right:1px solid #c3cecc;display:none;";
                        }
                        else
                        {
                            tc.Attributes["style"] = "border-left:1px solid #c3cecc;border-right:1px solid #c3cecc;";
                        }
                        borderleft = true;
                    }
                    else
                    {
                        if (cellIdx == 1)
                        {
                            tc.Attributes["style"] = "border-right:1px solid #c3cecc;display:none;";
                        }
                        else
                        {
                            tc.Attributes["style"] = "border-right:1px solid #c3cecc;";
                        }
                    }
                }
                cellIdx++;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlEntTypes = (e.Row.FindControl("ddlEntTypes") as DropDownList);
                if (ddlEntTypes != null)
                {
                    try
                    {
                        StyleGuide.API api = new StyleGuide.API();
                        try
                        {
                            ddlEntTypes.DataSource = api.getAllEntityTypes(null, true);
                            ddlEntTypes.DataTextField = "Type";
                            ddlEntTypes.DataValueField = "ID";
                            ddlEntTypes.DataBind();
                            ddlEntTypes.Style.Add("-webkit-appearance", "none");
                            StyleGuide.SgEntities.EntityType entType = (StyleGuide.SgEntities.EntityType)DataBinder.Eval(e.Row.DataItem, "Type");
                            if (entType != null)
                            {
                                int idx = _Library._Web._Common.FindDdlItemIndex(ddlEntTypes, entType.ID.ToString(), true);
                                ddlEntTypes.SelectedIndex = idx;
                            }
                            else
                            {
                                ddlEntTypes.Items.Insert(0, new ListItem("Please select", ""));
                            }

                            long entID = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "ID"));
                            if (entID ==-1)
                            {
                                ImageButton btCatNew = (e.Row.FindControl("btCatNew") as ImageButton);
                                if (btCatNew != null)
                                {
                                    btCatNew.Visible = false;
                                }
                                (e.Row.FindControl("tbID") as TextBox).ForeColor = System.Drawing.Color.Red;
                                (e.Row.FindControl("tbName") as TextBox).ForeColor = System.Drawing.Color.Red;
                                (e.Row.FindControl("ddlEntTypes") as DropDownList).ForeColor = System.Drawing.Color.Red;
                                (e.Row.FindControl("tbNotes") as TextBox).ForeColor = System.Drawing.Color.Red;
                            }
                            GridView gvEntCats = (e.Row.FindControl("gvEntCats") as GridView);
                            RefreshEntCatGrid(api, gvEntCats, entID);
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

        public void RefreshEntCatGrid(StyleGuide.API api, GridView gvEntCats, long entID)
        {
            IList<EntCat> entCats = new List<EntCat>();
            IList<StyleGuide.SgCategories.Category> cats = api.getEntityByID(entID).categories;
            foreach (StyleGuide.SgCategories.Category cat in cats)
            {
                EntCat entCat = new EntCat();
                entCat.EntID = entID;
                entCat.CatID = cat.ID;
                entCat.CatName = cat.Name;
                entCats.Add(entCat);
            }
            gvEntCats.DataSource = entCats;// api.getEntityByID(entID).categories;
            gvEntCats.DataBind();

        }

        private class EntCat
        {
            public long EntID { get; set; }
            public long CatID { get; set; }
            public string CatName { get; set; }
        }


        protected void gvEntCats_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmd = e.CommandName.ToLower();
            string entID = e.CommandArgument.ToString().Split(',')[0];
            string catID = e.CommandArgument.ToString().Split(',')[1];
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                if (cmd == "DeleteCatRow".ToLower())
                {
                    if (entID != "-1")
                    {
                        api.RemoveCategoryFromEntity(Convert.ToInt64(catID), Convert.ToInt64(entID));

                        StyleGuide.SgEntities.Entity ent = api.getEntityByID(Convert.ToInt64(entID));
                        if (ent != null)
                        {
                            ent.LMBY = StyleGuideUI.App_Code.SgCommon.getLoggedUser();
                            api.SaveEntity(ent); 
                        }
                    }
                }
                Refresh(api, UcPaging1.CurrentPage);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error on DeleteCatRow(). " + ex.Message);
            }
            finally
            {
                api.Dispose();
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                Int32 PgIdx = UcPaging1.CurrentPage;
                foreach (GridViewRow r in this.gvEntList.Rows)
                {
                    string Changed = getPostedValue(r, "hfChanged");
                    string tbId = getPostedValue(r, "tbID");
                    if (Changed == "1" || tbId == "-1")
                    {
                        string tbName = getPostedValue(r, "tbName");
                        string tbSuspects = getPostedValue(r, "tbSuspects");
                        string tbSuggestions = getPostedValue(r, "tbSuggestions");
                        string ddlEntType = getPostedValue(r, "ddlEntTypes");
                        string tbNotes = getPostedValue(r, "tbNotes");

                        StyleGuide.SgEntities.Entity ent;
                        StyleGuide.SgEntities.EntityType entType = api.getEntityTypeByID(Convert.ToInt64(ddlEntType));

                        if (tbId == "-1")
                        {
                            ent = new StyleGuide.SgEntities.Entity();
                        }
                        else
                        {
                            ent = api.getEntityByID(Convert.ToInt64(tbId));
                        }
                        if (ent != null)
                        {
                            ent.Name = tbName;
                            ent.Type.ID = entType.ID;
                            ent.Type.Type = entType.Type;
                            ent.Notes = tbNotes;
                            ent.Suspects = tbSuspects;
                            ent.Suggestions = tbSuggestions;
                            ent.LMBY = StyleGuideUI.App_Code.SgCommon.getLoggedUser();
                            api.SaveEntity(ent);
                            initHandler();
                            ShowMessage("Entity saved");
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
                            throw new Exception("Entity ID not found.");
                        }
                    }
                }
                Refresh(api, PgIdx);
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
                btSave_Click(this.btNew, null);
                Refresh(api, UcPaging1.TotalPages);

                StyleGuide.PagingInfo pgInf = null;
                if (this.UcPaging1.CurrentPage > 0)
                {
                    pgInf = new StyleGuide.PagingInfo();
                    pgInf.RecordsPerPage = REC_PER_PAGE;
                    pgInf.CurrentPage = this.UcPaging1.TotalPages;
                }

                StyleGuide.SgEntities.Entities ents = api.getAllEntities(pgInf);
                if (ents == null)
                {
                    ents = new StyleGuide.SgEntities.Entities();
                }

                StyleGuide.SgEntities.Entity ent = new StyleGuide.SgEntities.Entity();
                ent.ID = -1;
                ent.Name = "New ";
                ents.Add(ent);
                
                this.gvEntList.DataSource = ents;
                this.gvEntList.DataBind();
                SetPaging(ents,  true);
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
                Refresh(api,Page);
            }
            finally
            {
                api.Dispose();
            }

        }

        private void SetPaging(StyleGuide.SgEntities.Entities ents, bool Move2LastPage)
        {
            if (ents != null)
            {
                this.UcPaging1.TotalPages = ents.PageInfo.TotalPages;
                if (Move2LastPage)
                {
                    this.UcPaging1.CurrentPage = this.UcPaging1.TotalPages;
                }
                else
                {
                    this.UcPaging1.CurrentPage = Convert.ToInt32(ents.PageInfo.CurrentPage);
                }
                this.UcPaging1.Refresh();
            }
            else
            {
                this.UcPaging1.TotalPages = 0;
                this.UcPaging1.Enabled = false;
            }
        }

        public string getLMDTBY(GridViewRow gvSelectedRow )
        {
            var lmBy = DataBinder.Eval(gvSelectedRow.DataItem, "LMBY");
            var lmDt = DataBinder.Eval(gvSelectedRow.DataItem, "LMDT");
            if (lmBy != null)
            {
                if(lmBy.ToString().Contains("\\"))
                {
                    lmBy = lmBy.ToString().Split('\\')[1];
                }
                if(StyleGuideUI.App_Code.SgCommon.IsDate(lmDt))
                {
                    return string.Format("Last modified by {0} on {1}", lmBy, ((DateTime)lmDt).ToString("dd MMM, yyyy HH:mm"));
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

        private void initHandler()
        {
            StyleGuideUI.App_Code.ArticleFr afr = new StyleGuideUI.App_Code.ArticleFr();
            afr.InitEntities();
            afr = null;
        }


    }
}