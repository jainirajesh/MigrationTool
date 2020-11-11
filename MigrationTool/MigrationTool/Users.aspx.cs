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

namespace MigrationTool
{
    public partial class Users : System.Web.UI.Page
    {
        string constr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownList drpProject = (DropDownList)Master.FindControl("drpProject");
            //if (Session["drpProject"] == null)
            //{
            //    Session["constr"] = string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, System.Configuration.ConfigurationManager.AppSettings["defaultdb"].ToString());
            //}
            //else if (drpProject.Items.Count > 1)
            //{
            //    Session["constr"] = string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, drpProject.SelectedItem.Text);
            //}
            //else
            //{
            //    Session["constr"] = string.Format(ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString, Session["drpProject"].ToString());
            //}
            constr = ConfigurationManager.ConnectionStrings["Defaultdbconnection"].ConnectionString;
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!this.IsPostBack)
            {
                gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
                gvExcelFile.PageIndex = 0;
                gvExcelFile.DataBind();
            }
            manageControls();
            Label1.Visible = false;
            gvExcelFile.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
        }

        public void manageControls()
        {
            if (gvExcelFile.Rows.Count > 0)
            {
                btnExportToCSV.Visible = true;
                btnDownload.Visible = true;
                txtSearch.Visible = true;
                btnSearch.Visible = true;
            }
            else
            {
                btnExportToCSV.Visible = false;
                btnDownload.Visible = false;
                txtSearch.Visible = false;
                btnSearch.Visible = false;
            }
        }

        private DataTable BindGrid(string search)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserName LIKE '%' + @UserName + '%'"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UserName", search);
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                        ViewState["dt"] = dt;
                    }
                }
                return dt;
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void gvExcelFile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExcelFile.PageIndex = e.NewPageIndex;
            if (Session["SortedView"] != null)
            {
                gvExcelFile.DataSource = Session["SortedView"];
                gvExcelFile.DataBind();
            }
            else
            {
                gvExcelFile.DataSource = ViewState["dt"];
                gvExcelFile.DataBind();
            }
            manageControls();
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

        protected void gvExcelFile_Sorting(object sender, GridViewSortEventArgs e)
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
            DataView sortedView = new DataView(BindGrid(txtSearch.Text.Trim()));
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvExcelFile.DataSource = sortedView;
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void btnExportToCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = BindGrid(txtSearch.Text.Trim());

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Users.csv");
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
            DataTable dt = BindGrid(txtSearch.Text.Trim());

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Users.xls");
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

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["Role"].ToString() == "Admin")
            {
                gvExcelFile.EditIndex = e.NewEditIndex;
                gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
                gvExcelFile.DataBind();
                manageControls();
            }
        }

        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (Session["Role"].ToString() == "Admin")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvExcelFile, "Edit$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
        }

        protected void rdoDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
            gvExcelFile.PageIndex = 0;
            gvExcelFile.DataBind();
            manageControls();            
        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            gvExcelFile.DataSource = dt;
            gvExcelFile.DataBind();
            for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                dt.Rows[row.RowIndex][j] = (row.Cells[j].Controls[0] as TextBox).Text;
            }
            string query1 = "";
            ViewState["dt"] = dt;
            using (SqlConnection con = new SqlConnection(constr))
            {
                query1 = "update Users set FirstName= @FirstName, LastName = @LastName, UserName = @UserName, Password = @Password, Email = @Email, CreatedDate = @CreatedDate where UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@UserId", dt.Rows[row.RowIndex]["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@FirstName", dt.Rows[row.RowIndex]["FirstName"].ToString());
                    cmd.Parameters.AddWithValue("@LastName", dt.Rows[row.RowIndex]["LastName"].ToString());
                    cmd.Parameters.AddWithValue("@UserName", dt.Rows[row.RowIndex]["UserName"].ToString());
                    cmd.Parameters.AddWithValue("@Password", dt.Rows[row.RowIndex]["Password"].ToString());
                    cmd.Parameters.AddWithValue("@Email", dt.Rows[row.RowIndex]["Email"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedDate", dt.Rows[row.RowIndex]["CreatedDate"].ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            gvExcelFile.EditIndex = -1;
            gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            gvExcelFile.EditIndex = -1;
            gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
            gvExcelFile.DataBind();
            manageControls();
        }
    }
}