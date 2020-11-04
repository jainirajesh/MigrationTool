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
using System.Text.RegularExpressions;

namespace MigrationTool
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        SqlCommand sqlcomm;

        public object DatabaseList1 { get; private set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            DropDownList drpProject = (DropDownList)Master.FindControl("drpProject");
            binddata();
        }

        public void binddata()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Defaultdbconnection"].ConnectionString);
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * from projects", con))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            con.Close();
        }

        public void btnCreateProject_Click(object sender, EventArgs e)
        {
            if (DatabaseBox1.Text.Trim() != "")
            {
                try
                {
                    SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["Defaultdbconnection"].ConnectionString);
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand("Select * from Projects where ProjectName='" + DatabaseBox1.Text + "'", sqlconn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {                      
                        String sqlquery = "CREATE Database " + DatabaseBox1.Text;
                        sqlcomm = new SqlCommand(sqlquery, sqlconn);
                        sqlcomm.ExecuteNonQuery();

                        string query2 = "insert into Projects values (@ProjectName,@ProjectDescription,@CreatedBy,@CreatedDate)";
                        using (cmd = new SqlCommand(query2))
                        {
                            cmd.Connection = sqlconn;
                            cmd.Parameters.AddWithValue("@ProjectName", DatabaseBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@ProjectDescription", txtDesc.Text.Trim());
                            cmd.Parameters.AddWithValue("@CreatedBy", Session["Login"].ToString());
                            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("dd/MM/yyyy"));
                            cmd.ExecuteNonQuery();
                        }                        

                        //string datasource = System.Configuration.ConfigurationManager.AppSettings["datasource"];
                        //string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, DatabaseBox1.Text);
                        SqlConnection sqlconn1 = new SqlConnection(string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, DatabaseBox1.Text));
                        sqlconn1.Open();
                        string filename = Server.MapPath("~/Images/Check.sql");
                        List<string> lines = File.ReadAllLines(filename).ToList();

                        string text = File.ReadAllText(filename);
                        using (cmd = new SqlCommand(text))
                        {
                            cmd.Connection = sqlconn1;
                            cmd.ExecuteNonQuery();
                        }
                        sqlconn1.Close();

                        //Label1.Text = "<b> Project <b>" + DatabaseBox1.Text + "<b> is created successfully!!";
                        Response.Write("<script>alert('Project created successfully.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Project Name already exists.')</script>");
                    }
                    sqlconn.Close();
                }
                catch (Exception ex)
                {
                    Label1.Text = "<b>" + ex.Message;
                }
                binddata();
            }
            else
            {
                Response.Write("<script>alert('Please enter valid Project Name.')</script>");
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string DbName = GridView1.Rows[e.RowIndex].Cells[1].Text;
            string mainconn = ConfigurationManager.ConnectionStrings["Defaultdbconnection"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                String sqlquery = "delete from projects where projectname ='" + DbName + "'";
                sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();
                sqlconn.Open();
                sqlquery = "use master; ALTER DATABASE " + DbName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE; EXEC sp_renamedb " + DbName + ", " + DbName + "_ToBeDeleted; ALTER DATABASE " + DbName + "_ToBeDeleted SET MULTI_USER";
                sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.ExecuteNonQuery();
                sqlconn.Close();

                binddata();
            }
        }
    }
}