using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MigrationTool
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton lnkLogout = (LinkButton)Page.Master.FindControl("lnkLogout");
            lnkLogout.Visible = false;
            Label lblLoginUser = (Label)Page.Master.FindControl("lblLoginUser");
            lblLoginUser.Visible = false;
            Control divMenu = this.Master.FindControl("divMenu");
            divMenu.Visible = false;
            
            Session["Login"] = null;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from UserDetails where UserName='" + txtusername.Text + "' and Password ='" + txtpassword.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Login"] = "Welcome " + dt.Rows[0]["FirstName"].ToString() + ", " + dt.Rows[0]["LastName"].ToString();
                Response.Redirect("Upload.aspx");                
            }
            else
            {
                Response.Write("<script>alert('Please enter valid Username and Password.')</script>");
            }
        }

    }
}