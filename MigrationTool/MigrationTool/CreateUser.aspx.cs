using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Threading;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Reflection;

namespace MigrationTool
{
    public partial class CreateUser : System.Web.UI.Page
    {
        SqlCommand sqlcomm;
        string constr="";
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownList drpProject = (DropDownList)Master.FindControl("drpProject");
            if (Session["drpProject"] == null)
            {
                Session["constr"] = string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, System.Configuration.ConfigurationManager.AppSettings["defaultdb"].ToString());
            }
            else if (drpProject.Items.Count > 1)
            {
                Session["constr"] = string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, drpProject.SelectedItem.Text);
            }
            else
            {
                Session["constr"] = string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, Session["drpProject"].ToString());
            }
            constr = Session["constr"].ToString();
        }
        protected void SignUp_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlConnection sqlconn = new SqlConnection(constr);
                sqlconn.Open();
                var sqlCommand = new SqlCommand("select * from Users where UserName = '" + UserName.Text + "'", sqlconn);

                SqlDataReader count = sqlCommand.ExecuteReader();

                if (count.HasRows)
                {
                    Label1.Text = "<b>" + "User already exists !!";
                    sqlconn.Close();
                }
                else
                {
                    sqlconn.Close();
                    sqlconn.Open();
                    DateTime today = DateTime.Today;
                    string CreatedDate = today.ToString();
                    //string Date = Convert.ToDateTime(CreatedDate.Text).ToString("yyyy-MM-dd");
                    String sqlquery = "INSERT INTO Users(FirstName,LastName,UserName,Password,Email,UserRole,CreatedDate) values('" + FirstName.Text + "','" + LastName.Text + "','" + UserName.Text + "','" + Password.Text + "','" + Email.Text + "','" + UserRole.SelectedValue + "','" + CreatedDate + "')";
                    sqlcomm = new SqlCommand(sqlquery, sqlconn);
                    sqlcomm.ExecuteNonQuery();
                    sqlconn.Close();
                    Label1.Text = "<b>" + "User is successfully created " + " !!";
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            FirstName.Text = "";
            LastName.Text = "";
            UserName.Text = "";
            Email.Text = "";
            Password.Text = "";
            Label1.Text = "";
        }
    }
}