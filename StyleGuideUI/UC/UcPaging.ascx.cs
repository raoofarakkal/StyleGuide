using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StyleGuideUI.UC
{
    public partial class UcPaging : System.Web.UI.UserControl
    {
        public delegate void onPageChange(UcPaging sender, int Page);
        public event onPageChange PageChange;

        private int mTotalPages = -11;
        public int TotalPages
        {
            get
            {
                if (mTotalPages == -11)
                {
                    mTotalPages = this.ddlPgNo.Items.Count;
                }
                return mTotalPages;
            }
            set
            {
                mTotalPages = value;
                Refresh();
            }
        }

        private int mCurPg = -11;
        public int CurrentPage
        {
            get
            {
                if (mCurPg == -11)
                {
                    mCurPg = this.ddlPgNo.SelectedIndex + 1;
                }
                return mCurPg;
            }
            set { mCurPg = value; }
        }

        public void Refresh()
        {
            this.ddlPgNo.Items.Clear();
            if (TotalPages > 1)
            {
                ListItem mLi = default(ListItem);
                for (int ii = 1; ii <= TotalPages; ii++)
                {
                    mLi = new ListItem(string.Format("Page {0}", ii), ii.ToString());
                    this.ddlPgNo.Items.Add(mLi);
                    mLi = null;
                }
                if (CurrentPage >= TotalPages)
                {
                    this.ddlPgNo.SelectedIndex = TotalPages - 1;
                }
                else
                {
                    this.ddlPgNo.SelectedIndex = CurrentPage - 1;
                }
                this.Enabled = true;
            }
            else
            {
                this.Enabled = false;
            }
        }

        private bool mEnabled = true;
        public bool Enabled
        {
            get { return mEnabled; }
            set
            {
                mEnabled = value;
                this.ddlPgNo.Enabled = mEnabled;
                this.ibNext.Enabled = mEnabled;
                this.ibPrv.Enabled = mEnabled;
            }
        }

        protected void ddlPgNo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CurrentPage = Convert.ToInt32(this.ddlPgNo.SelectedItem.Value);
            if (PageChange != null)
            {
                PageChange(this, CurrentPage);
            }
        }

        protected void ibNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlPgNo.SelectedIndex < (this.ddlPgNo.Items.Count - 1))
            {
                this.ddlPgNo.SelectedIndex += 1;
                ddlPgNo_SelectedIndexChanged(this, null);
            }
        }

        protected void ibPrv_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.ddlPgNo.SelectedIndex > 0)
            {
                this.ddlPgNo.SelectedIndex -= 1;
                ddlPgNo_SelectedIndexChanged(this, null);
            }
        }

    }
}