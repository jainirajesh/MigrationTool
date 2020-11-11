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
        string constr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //DropDownList drpProject = (DropDownList)Master.FindControl("drpProject");
            constr = string.Format(ConfigurationManager.ConnectionStrings["Defaultdbconnection"].ConnectionString);

            lblErrPassword.Text = "";
            LinkButton lnkLogout = (LinkButton)Page.Master.FindControl("lnkLogout");
            lnkLogout.Visible = false;
            Label lblLoginUser = (Label)Page.Master.FindControl("lblLoginUser");
            lblLoginUser.Visible = false;
            Control divMenu = this.Master.FindControl("divMenu");
            divMenu.Visible = false;
            Control divLogo = this.Master.FindControl("divLogo1");
            divLogo.Visible = true;          
            Session["Login"] = null;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {            
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Users where UserName='" + txtusername.Text + "' and Password ='" + txtpassword.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["Role"] = dt.Rows[0]["UserRole"].ToString();
                Session["Login"] = dt.Rows[0]["FirstName"].ToString() + ", " + dt.Rows[0]["LastName"].ToString();                           

                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Response.Write("<script>alert('Please enter valid Username and Password.')</script>");
            }
        }

        protected void btnSubmitReset_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Users where UserName='" + txtResetUsername.Text + "' and Email ='" + txtResetEmail.Text + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string query2 = "update Users set Password = @Password where UserName = @UserName and Email = @Email";
                    using (cmd = new SqlCommand(query2))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UserName", txtResetUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtResetPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtResetEmail.Text.Trim());

                        cmd.ExecuteNonQuery();
                        con.Close();

                        lblErrPassword.Text = "Password changed successfully.";
                        lblErrPassword.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    lblErrPassword.Text = "Username doesnot exist.";
                    lblErrPassword.ForeColor = System.Drawing.Color.Red;
                }
            }
            mpResetPassword.Show();
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlConnection sqlconn = new SqlConnection(constr);
                sqlconn.Open();
                var sqlCommand = new SqlCommand("select * from Users where UserName = '" + UserName.Text + "'", sqlconn);

                SqlDataReader count = sqlCommand.ExecuteReader();

                if (count.HasRows)
                {
                    Label4.Text = "<b>" + "User already exists !!";
                    sqlconn.Close();
                }
                else
                {
                    sqlconn.Close();
                    sqlconn.Open();
                    DateTime today = DateTime.Today;

                    string CreatedDate = today.ToString();
                    SqlCommand sqlcomm;
                    //string Date = Convert.ToDateTime(CreatedDate.Text).ToString("yyyy-MM-dd");
                    String sqlquery = "INSERT INTO Users(FirstName,LastName,UserName,Password,Email,UserRole,CreatedDate) values('" + FirstName.Text + "','" + LastName.Text + "','" + UserName.Text + "','" + Password.Text + "','" + Email.Text + "','" + UserRole.SelectedValue + "','" + CreatedDate + "')";
                    sqlcomm = new SqlCommand(sqlquery, sqlconn);
                    sqlcomm.ExecuteNonQuery();
                    sqlconn.Close();
                    FirstName.Text = "";
                    LastName.Text = "";
                    UserName.Text = "";
                    Password.Text = "";
                    Email.Text = "";
                    //Label4.Text = "<b>" + "User is successfully created " + " !!";
                    Response.Write("<script>alert('User created Successfully.')</script>");
                }
            }
            mdlSignUp.Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            lblErrPassword.Text = "";
            txtResetUsername.Text = "";
            txtResetEmail.Text = "";
            txtResetConfirmPass.Text = "";
            txtResetPassword.Text = "";
            mpResetPassword.Hide();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            FirstName.Text = "";
            LastName.Text = "";
            UserName.Text = "";
            Email.Text = "";
            Password.Text = "";
            Label4.Text = "";
            mdlSignUp.Show();
        }

        protected void btnSign_Click(object sender, EventArgs e)
        {
            FirstName.Text = "";
            LastName.Text = "";
            UserName.Text = "";
            Password.Text = "";
            Email.Text = "";
        }
    }
}