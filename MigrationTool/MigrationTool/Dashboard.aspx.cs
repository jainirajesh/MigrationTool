using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;

namespace MigrationTool
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string constr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
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
            string Dashboard = System.Configuration.ConfigurationManager.AppSettings["Dashboard"];
            String[] strAdminlist = Dashboard.Split(',');
            foreach (string db in strAdminlist)
            {
                BindGrid(db);
            }
            GetCounts();
        }

        public void GetCounts()
        {                    
            using (SqlConnection thisConnection = new SqlConnection(constr))
            {
                thisConnection.Open();
                using (SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM Hosts", thisConnection))
                {                    
                    lblServers.Text = cmdCount.ExecuteScalar().ToString();
                }

                using (SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM Applications", thisConnection))
                {                    
                    lblApplications.Text = cmdCount.ExecuteScalar().ToString();
                }

                using (SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM Storage", thisConnection))
                {                    
                    lblStorage.Text = cmdCount.ExecuteScalar().ToString();
                }

                using (SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM Databases", thisConnection))
                {
                    lblDatabases.Text = cmdCount.ExecuteScalar().ToString();
                }

                using (SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM Relationships", thisConnection))
                {
                    lblDependencies.Text = cmdCount.ExecuteScalar().ToString();
                }

                //using (SqlCommand cmdCount = new SqlCommand("SELECT COUNT(*) FROM Hosts", thisConnection))
                //{
                //    thisConnection.Open();
                //    lblServers.Text = cmdCount.ExecuteScalar().ToString();
                //}
                thisConnection.Close();
            }            
        }

        private void BindGrid(string table)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT top 5 * FROM " + table))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        DataTable dtCloned = new DataTable();
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);

                        ViewState["dt"] = dt;
                    }
                }
                if (table == "Hosts")
                {
                    Hosts.DataSource = dt;
                    Hosts.DataBind();

                    string[] x = new string[dt.Rows.Count];
                    int[] y = new int[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        x[i] = dt.Rows[i][0].ToString();
                        y[i] = i;
                    }
                    Chart2.Series[0].Points.DataBindXY(x, y);
                    Chart2.Series[0].ChartType = SeriesChartType.Pie;
                    Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    //Chart2.Legends[0].Enabled = true;
                    Chart2.DataSource = dt;
                    Chart2.DataBind();

                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pyramid;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    //Chart1.Legends[0].Enabled = true;
                    Chart1.DataSource = dt;
                    Chart1.DataBind();

                    Chart3.Series[0].Points.DataBindXY(x, y);
                    Chart3.Series[0].ChartType = SeriesChartType.Column;
                    Chart3.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    //Chart3.Legends[0].Enabled = true;
                    Chart3.DataSource = dt;
                    Chart3.DataBind();

                    Chart4.Series[0].Points.DataBindXY(x, y);
                    Chart4.Series[0].ChartType = SeriesChartType.Doughnut;
                    Chart4.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    //Chart4.Legends[0].Enabled = true;
                    Chart4.DataSource = dt;
                    Chart4.DataBind();
                }
                else if (table == "Applications")
                {
                    Applications.DataSource = dt;
                    Applications.DataBind();
                }
                else if (table == "Storage")
                {
                    Storage.DataSource = dt;
                    Storage.DataBind();
                }
                else if (table == "Databases")
                {
                    Databases.DataSource = dt;
                    Databases.DataBind();
                }
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.ToolTip = "Name : " + (e.Row.DataItem as DataRowView)["Name"].ToString() + "OS :" + (e.Row.DataItem as DataRowView)["OS"].ToString();
            }
        }

    }
}