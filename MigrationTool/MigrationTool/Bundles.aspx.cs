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
using System.Text;

namespace MigrationTool
{
    public partial class Bundles : System.Web.UI.Page
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
            if (!this.IsPostBack)
            {
                ViewState["dt"] = null;
                btnDownload.Visible = false;
                btnExportToCSV.Visible = false;
            }
        }
        protected void btnExportToCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename= Bundles.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }

            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Bundles.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void rdoDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoDataType.SelectedIndex == 0)
            {
                btnSubmit.Text = "Create";
                grdBundles.Visible = false;
                btnDownload.Visible = false;
                btnExportToCSV.Visible = false;
            }
            else
            {
                btnSubmit.Text = "View";
                grdBundles.DataSource = null;
                grdBundles.DataBind();
                grdBundles.Visible = true;
                btnDownload.Visible = false;
                btnExportToCSV.Visible = false;
            }
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void grdBundles_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";
            }
            DataView sortedView = new DataView((DataTable)ViewState["dt"]);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            grdBundles.DataSource = sortedView;
            grdBundles.DataBind();
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rdoDataType.SelectedIndex == 0)
            {
                try
                {
                    btnDownload.Visible = false;
                    btnExportToCSV.Visible = false;
                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    string filename = Server.MapPath("~/Images/CreateBunble.sql");
                    //List<string> lines = File.ReadAllLines(filename).ToList();
                    string text = File.ReadAllText(filename);
                    using (SqlCommand cmd = new SqlCommand(text))
                    {
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string strQuery = "SELECT LAppServer.Application, LAppServer.Server, LAppServer.bundle, Hosts.Physical_or_Virtual, Hosts.Source_DC, Hosts.In_Scope, Hosts.Environment, Applications.Owner_Primary FROM LAppServer INNER JOIN Applications ON LAppServer.Application = Applications.Name INNER JOIN Hosts ON LAppServer.Server = Hosts.Name";
                    using (SqlCommand cmd = new SqlCommand(strQuery))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            DataTable dtCloned = new DataTable();
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            sda.Fill(dtCloned);
                            ViewState["dt"] = dtCloned;
                            grdBundles.DataSource = dtCloned;
                            grdBundles.DataBind();
                            if (dtCloned.Rows.Count > 0)
                            {
                                btnDownload.Visible = true;
                                btnExportToCSV.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        protected void grdBundles_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grdBundles.PageIndex = e.NewPageIndex;
            if (Session["SortedView"] != null)
            {
                grdBundles.DataSource = Session["SortedView"];
                grdBundles.DataBind();
            }
            else
            {
                grdBundles.DataSource = (DataTable)ViewState["dt"];
                grdBundles.DataBind();
            }
        }
    }
}