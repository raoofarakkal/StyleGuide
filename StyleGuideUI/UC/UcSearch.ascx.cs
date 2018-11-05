using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StyleGuideUI.UC
{
    public partial class UcSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _Library._Web._Common.MaintainScrollPos(this.divTrvCat);
        }

        public void Refresh(StyleGuide.API api)
        {
            StyleGuide.SgCategories.Categories cats = null;
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                cats = api.getAllCategoriesContainsEntitiesOrderByType(tbSearch.Text, true);
            }
            else
            {
                cats = api.getAllCategoriesOrderByType(null, true);
            }
            if (cats != null)
            {
                this.lbError.Text = "";

                this.trvCat.Nodes.Clear();
                bool selected = false;
                foreach (StyleGuide.SgCategories.Category cat in cats)
                {
                    TreeNode node = new TreeNode();
                    int xDays = App_Code.webConfig.ShowEntityCountModifiedInLastXdays();
                    int count = api.getEntityCountModifiedLaterthan(cat.ID, DateTime.Now.AddDays(xDays * -1));
                    if (count >= 1)
                    {
                        node.Text = cat.Name + string.Format("<span style='color:red;padding-left:5px;'>{0}*</span>", count);
                    }
                    else
                    {
                        node.Text = cat.Name;
                    }
                    node.Value = cat.ID.ToString();
                    node.ToolTip = cat.Name;
                    if (!selected)
                    {
                        node.Select();
                        selected = true;
                    }
                    this.trvCat.Nodes.Add(node);
                    node = null;
                }
                trvCat_SelectedNodeChanged(this.trvCat, null);

            }
            else
            {
                this.trvCat.Nodes.Clear();
                this.rptType.DataSource = null;
                this.rptType.DataBind();
                this.divShowAllEntityType.Visible = false;
                this.lbError.Text = "No Entity found.";
            }
        }

        protected void trvCat_SelectedNodeChanged(object sender, EventArgs e)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                StyleGuide.SgEntities.EntitiesSearchResultsByCategory entitiesSearchResultsByCategory = null;

                if (!string.IsNullOrWhiteSpace(tbSearch.Text))
                {
                    entitiesSearchResultsByCategory = api.getEntitiesForSearchModule(Convert.ToInt64(trvCat.SelectedNode.Value), tbSearch.Text);
                    if (entitiesSearchResultsByCategory != null)
                    {
                        this.divShowAllEntityType.Visible = true;
                    }
                }
                else
                {
                    entitiesSearchResultsByCategory = api.getEntitiesForSearchModule(Convert.ToInt64(trvCat.SelectedNode.Value), null);
                    this.divShowAllEntityType.Visible = false;
                }
                
                this.rptType.DataSource = entitiesSearchResultsByCategory;
                this.rptType.DataBind();
            }
            finally
            {
                api.Dispose();
            }
        }

        protected void rptType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
            {
                StyleGuide.SgEntities.Entities ents = (StyleGuide.SgEntities.Entities)DataBinder.Eval(e.Item.DataItem, "entities");
                Repeater rptEntity = (e.Item.FindControl("rptEntity") as Repeater);
                rptEntity.DataSource = ents;
                rptEntity.DataBind();
            }
        }

        public string getLMDTBY(RepeaterItem item)
        {
            var lmBy = DataBinder.Eval(item.DataItem, "LMBY");
            var lmDt = DataBinder.Eval(item.DataItem, "LMDT");
            if (lmBy != null)
            {
                if (lmBy.ToString().Contains("\\"))
                {
                    lmBy = lmBy.ToString().Split('\\')[1];
                }
                if (StyleGuideUI.App_Code.SgCommon.IsDate(lmDt))
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

        public string setAsModified(RepeaterItem item)
        {
            var lmDt = DataBinder.Eval(item.DataItem, "LMDT");
            if (StyleGuideUI.App_Code.SgCommon.IsDate(lmDt))
            {
                int xDays = App_Code.webConfig.ShowEntityCountModifiedInLastXdays();
                if (Convert.ToDateTime(lmDt) > DateTime.Now.AddDays(xDays * -1))
                {
                    return "background-color:#FFFFD4;";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        protected void btSearch_Click(object sender, EventArgs e)
        {

            StyleGuide.API api = new StyleGuide.API();
            try
            {
                Refresh(api);
            }
            finally
            {
                api.Dispose();
            }
        }

        protected void btClear_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
            btSearch_Click(this.btSearch, null);
        }

        protected void lbShowAllEntityType_Click(object sender, EventArgs e)
        {
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                StyleGuide.SgEntities.EntitiesSearchResultsByCategory entitiesSearchResultsByCategory = null;

                entitiesSearchResultsByCategory = api.getEntitiesForSearchModule(Convert.ToInt64(trvCat.SelectedNode.Value), null);

                this.rptType.DataSource = entitiesSearchResultsByCategory;
                this.rptType.DataBind();
            }
            finally
            {
                api.Dispose();
            }
            this.divShowAllEntityType.Visible = false;
        }




        


    }
}