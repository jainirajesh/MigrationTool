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
    public partial class Databases : System.Web.UI.Page
    {
        string constr = "";
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
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!this.IsPostBack)
            {
                Session["SortedView"] = null;
                ViewState["dtEdit"] = null;
                ViewState["dtUpdateDB"] = null;
                gvExcelFile.DataSource = BindGrid();
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
                if (Session["Role"].ToString() == "Admin")
                {
                    btnCommit.Visible = true;
                    btnRevert.Visible = true;
                }
                else
                {
                    btnCommit.Visible = false;
                    btnRevert.Visible = false;
                }
            }
            else
            {
                btnExportToCSV.Visible = false;
                btnDownload.Visible = false;
                txtSearch.Visible = false;
                btnSearch.Visible = false;
                btnCommit.Visible = false;
                btnRevert.Visible = false;
            }
        }

        private DataTable BindGrid()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Databases"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {                        
                        cmd.Connection = con;                        
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
            DataTable dtSearch = new DataTable();
            if (ViewState["dtEdit"] != null)
            {
                dtSearch = (DataTable)ViewState["dtEdit"];
            }
            else
            {
                dtSearch = (DataTable)ViewState["dt"];
            }
            dtSearch.DefaultView.RowFilter = "Name LIKE '%" + txtSearch.Text.ToString() + "%'";
            //ViewState["dtSearch"] = dtSearch.DefaultView.ToTable();
            gvExcelFile.DataSource = dtSearch;
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
                DataTable dt = new DataTable();
                if (ViewState["dtEdit"] != null)
                {
                    dt = (DataTable)ViewState["dtEdit"];
                }
                else
                {
                    dt = (DataTable)ViewState["dt"];
                }
                BindToGridview(dt);
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
            DataView sortedView = new DataView((DataTable)ViewState["dt"]);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            sortedView.RowFilter = "Name LIKE '%" + txtSearch.Text.ToString() + "%'";
            gvExcelFile.DataSource = sortedView;
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void btnExportToCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            dt.DefaultView.RowFilter = "Name LIKE '%" + txtSearch.Text.ToString() + "%'";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename= Databases.csv");
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
            dt.DefaultView.RowFilter = "Name LIKE '%" + txtSearch.Text.ToString() + "%'";

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Databases.xls");
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
                DataTable dt = new DataTable();
                if (ViewState["dtEdit"] != null)
                {
                    dt = (DataTable)ViewState["dtEdit"];
                }
                else
                {
                    dt = (DataTable)ViewState["dt"];
                }
                BindToGridview(dt);
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
            txtSearch.Text = "";
            gvExcelFile.DataSource = BindGrid();
            gvExcelFile.PageIndex = 0;
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            DataTable dt1, dtUpdateDB, dtTemp = new DataTable();

            if (ViewState["dtEdit"] != null)
            {
                dt1 = (DataTable)ViewState["dtEdit"];                
            }
            else
            {
                dt1 = (DataTable)ViewState["dt"];
            }

            if (ViewState["dtUpdateDB"] != null)
            {
                dtUpdateDB = (DataTable)ViewState["dtUpdateDB"];
            }
            else
            {
                dtUpdateDB = dt1.Clone();
            }
            dtTemp = dt1.Copy();
            GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
            int rowindex = (gvExcelFile.PageSize * gvExcelFile.PageIndex) + row.RowIndex;
            BindToGridview(dt1);
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(Name, 'System.String') like '{0}'", (row.Cells[1].Controls[0] as TextBox).Text)).FirstOrDefault();
            dtUpdateDB.Rows.Add();
            for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                filterData1[j - 1] = (row.Cells[j].Controls[0] as TextBox).Text;
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][j - 1] = (row.Cells[j].Controls[0] as TextBox).Text;
            }

            ViewState["dtUpdateDB"] = dtUpdateDB;
            ViewState["dtEdit"] = dtTemp;
            gvExcelFile.EditIndex = -1;
            BindToGridview(dtTemp);
            manageControls();
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            gvExcelFile.EditIndex = -1;
            DataTable dt = new DataTable();
            if (ViewState["dtEdit"] != null)
            {
                dt = (DataTable)ViewState["dtEdit"];
            }
            else
            {
                dt = (DataTable)ViewState["dt"];
            }
            BindToGridview(dt);
            manageControls();
        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query2 = "";
            dt = (DataTable)ViewState["dtUpdateDB"];
            if (ViewState["dtUpdateDB"] != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        query2 = "update Databases set DB_Type = @DB_Type, DB_Version = @DB_Version, In_Scope = @In_Scope, Out_of_Scope_Justification = @Out_of_Scope_Justification, Technical_Contact = @Technical_Contact, DB_Instance = @DB_Instance, DB_Server_Name = @DB_Server_Name, DB_Size_GB = @DB_Size_GB, Discovery_Source = @Discovery_Source, Environment = @Environment, Comments = @Comments where Name = @Name";
                        using (SqlCommand cmd = new SqlCommand(query2))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Name", dt.Rows[i]["Name"].ToString());                            
                            cmd.Parameters.AddWithValue("@Server_Name", dt.Rows[i]["Server_Name"].ToString());
                            cmd.Parameters.AddWithValue("@DB_Type", dt.Rows[i]["DB_Type"].ToString());
                            cmd.Parameters.AddWithValue("@DB_Version", dt.Rows[i]["DB_Version"].ToString());
                            cmd.Parameters.AddWithValue("@In_Scope", (dt.Rows[i]["In_Scope"].ToString()));
                            cmd.Parameters.AddWithValue("@Out_of_Scope_Justification", dt.Rows[i]["Out_of_Scope_Justification"].ToString());
                            cmd.Parameters.AddWithValue("@Technical_Contact", dt.Rows[i]["Technical_Contact"].ToString());
                            cmd.Parameters.AddWithValue("@DB_Instance", dt.Rows[i]["DB_Instance"].ToString());
                            cmd.Parameters.AddWithValue("@DB_Server_Name", dt.Rows[i]["DB_Server_Name"].ToString());
                            cmd.Parameters.AddWithValue("@DB_Size_GB", dt.Rows[i]["DB_Size_GB"].ToString());
                            cmd.Parameters.AddWithValue("@Discovery_Source", dt.Rows[i]["Discovery_Source"].ToString());
                            cmd.Parameters.AddWithValue("@Environment", dt.Rows[i]["Environment"].ToString());
                            cmd.Parameters.AddWithValue("@Comments", dt.Rows[i]["Comments"].ToString());
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            txtSearch.Text = "";
            ViewState["dtUpdateDB"] = null;
            gvExcelFile.EditIndex = -1;
            gvExcelFile.DataSource = BindGrid();
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void btnRevert_Click(object sender, EventArgs e)
        {
            ViewState["dtEdit"] = null;
            ViewState["dtUpdateDB"] = null;

            gvExcelFile.EditIndex = -1;
            BindToGridview((DataTable)ViewState["dt"]);
            manageControls();
        }

        public void BindToGridview(DataTable dtToBind)
        {
            dtToBind.DefaultView.RowFilter = "Name LIKE '%" + txtSearch.Text.ToString() + "%'";
            gvExcelFile.DataSource = dtToBind;
            gvExcelFile.DataBind();
        }
    }
}
