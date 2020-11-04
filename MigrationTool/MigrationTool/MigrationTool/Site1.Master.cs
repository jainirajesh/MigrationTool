using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MigrationTool
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                lblLoginUser.Text = Session["Login"].ToString();
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
            Session["Login"] = null;
        }
    }
}