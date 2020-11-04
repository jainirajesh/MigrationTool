using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            if (drpProject.Items.Count < 1)
            {
                if (Session["drpProject"] == null)
                {
                    Session["drpProject"] = System.Configuration.ConfigurationManager.AppSettings["defaultdb"].ToString();
                }
                Session["constr"] = string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, System.Configuration.ConfigurationManager.AppSettings["defaultdb"].ToString());
            }
            else
            {
                if (Session["drpProject"] == null)
                {
                    Session["drpProject"] = drpProject.SelectedItem.Text;
                }
                Session["constr"] = string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, drpProject.SelectedItem.Text);
            }
            if (Session["Login"] != null)
            {
                lblLoginUser.Text = "Welcome " + Session["Login"].ToString();
                string AdminMenu = System.Configuration.ConfigurationManager.AppSettings["AdminMenu"];
                String[] strAdminlist = AdminMenu.Split(',');
                if (Session["Role"].ToString() == "Admin")
                {
                    for (int i = 0; i < strAdminlist.Length; i++)
                    {
                        Control divMenu = FindControl(strAdminlist[i].ToString());
                        divMenu.Visible = true;
                    }
                }
                else
                {
                    for (int i = 0; i < strAdminlist.Length; i++)
                    {
                        Control divMenu = FindControl(strAdminlist[i].ToString());
                        divMenu.Visible = false;
                    }
                }
                if (!IsPostBack)
                {
                    string mainconn = string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["Defaultdbconnection"].ConnectionString);
                    SqlConnection sqlconn = new SqlConnection(mainconn);
                    sqlconn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * from Projects", sqlconn))
                    {
                        drpProject.Items.Clear();
                        drpProject.DataSource = cmd.ExecuteReader();
                        drpProject.DataTextField = "ProjectName";
                        drpProject.DataValueField = "ProjectName";
                        drpProject.DataBind();
                        drpProject.ClearSelection();
                        drpProject.Items.FindByValue(System.Configuration.ConfigurationManager.AppSettings["defaultdb"].ToString()).Selected = true;
                    }
                }
                if (Session["drpProject"] != null && !IsPostBack)
                {
                    drpProject.ClearSelection();
                    drpProject.Items.FindByValue(Session["drpProject"].ToString()).Selected = true;
                }
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
            Session["Login"] = null;
        }

        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["drpProject"] = drpProject.SelectedItem.Text;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}