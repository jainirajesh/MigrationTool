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
    public partial class Upload : System.Web.UI.Page
    {
        string constr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                if (ViewState["dt"] != null)
                {
                    btnUpload.Visible = true;
                }
                else
                {
                    btnUpload.Visible = false;
                }
                manageControls();
                Label1.Visible = false;
            }
            catch
            {
                Response.Write("<script>alert('Session Expired. Please login again')</script>");
            }
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dt"];

                using (SqlConnection con = new SqlConnection(constr))
                {
                    if (rdoDataType.Text == "Users")
                    {
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Defaultdbconnection"].ConnectionString);
                        SqlBulkCopy sqlBulk = new SqlBulkCopy(con1);
                        sqlBulk.DestinationTableName = "Users";
                        con1.Open();
                        sqlBulk.WriteToServer(dt);
                        con1.Close();
                    }
                    else if (rdoDataType.Text == "Hosts")
                    {
                        SqlBulkCopy sqlBulk = new SqlBulkCopy(con);
                        sqlBulk.DestinationTableName = "Hosts";
                        con.Open();
                        sqlBulk.WriteToServer(dt);
                        con.Close();
                    }
                    else if (rdoDataType.Text == "Applications")
                    {
                        SqlBulkCopy sqlBulk = new SqlBulkCopy(con);
                        sqlBulk.DestinationTableName = "Applications";
                        con.Open();
                        sqlBulk.WriteToServer(dt);
                        con.Close();
                    }
                    else if (rdoDataType.Text == "Storage")
                    {
                        SqlBulkCopy sqlBulk = new SqlBulkCopy(con);
                        sqlBulk.DestinationTableName = "Storage";
                        con.Open();
                        sqlBulk.WriteToServer(dt);
                        con.Close();
                    }
                    else if (rdoDataType.Text == "Databases")
                    {
                        SqlBulkCopy sqlBulk = new SqlBulkCopy(con);
                        sqlBulk.DestinationTableName = "Databases";
                        con.Open();
                        sqlBulk.WriteToServer(dt);
                        con.Close();
                    }
                    else if (rdoDataType.Text == "Relationships")
                    {
                        SqlBulkCopy sqlBulk = new SqlBulkCopy(con);
                        sqlBulk.DestinationTableName = "Relationships";
                        con.Open();
                        sqlBulk.WriteToServer(dt);
                        con.Close();
                    }
                }

                gvExcelFile.DataSource = null;
                gvExcelFile.DataBind();
                manageControls();
                btnUpload.Visible = false;
                ViewState["dt"] = null;
                Response.Write("<script>alert('Data uploaded to Project database successfully.')</script>");
            }
            catch
            {
                Response.Write("<script>alert('Unable to upload the data to project detabase. Please validate the data and try again.')</script>");
            }
        }

        private DataTable BindGrid(string search)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                if (rdoDataType.Text == "Users")
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
                }
                else if (rdoDataType.Text == "Hosts")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Hosts WHERE Name LIKE '%' + @Name + '%'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            DataTable dtCloned = new DataTable();
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Name", search);
                            sda.SelectCommand = cmd;
                            sda.Fill(dtCloned);
                            dt = dtCloned.Clone();
                            dt.Columns[4].DataType = typeof(string);
                            foreach (DataRow row in dtCloned.Rows)
                            {
                                dt.ImportRow(row);
                            }
                            ViewState["dt"] = dt;
                        }
                    }
                }
                else if (rdoDataType.Text == "Applications")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications WHERE Name LIKE '%' + @Name + '%'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            DataTable dtCloned = new DataTable();
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Name", search);
                            sda.SelectCommand = cmd;
                            sda.Fill(dtCloned);
                            dt = dtCloned.Clone();
                            dt.Columns[4].DataType = typeof(string);
                            foreach (DataRow row in dtCloned.Rows)
                            {
                                dt.ImportRow(row);
                            }
                            ViewState["dt"] = dt;
                        }
                    }
                }
                else if (rdoDataType.Text == "Storage")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Storage WHERE Name LIKE '%' + @Name + '%'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            DataTable dtCloned = new DataTable();
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Name", search);
                            sda.SelectCommand = cmd;
                            sda.Fill(dtCloned);
                            dt = dtCloned.Clone();
                            dt.Columns[4].DataType = typeof(string);
                            foreach (DataRow row in dtCloned.Rows)
                            {
                                dt.ImportRow(row);
                            }

                            ViewState["dt"] = dt;
                        }
                    }
                }
                else if (rdoDataType.Text == "Databases")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM databases WHERE Name LIKE '%' + @Name + '%'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            DataTable dtCloned = new DataTable();
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Name", search);
                            sda.SelectCommand = cmd;
                            sda.Fill(dtCloned);
                            dt = dtCloned.Clone();
                            dt.Columns[4].DataType = typeof(string);
                            foreach (DataRow row in dtCloned.Rows)
                            {
                                dt.ImportRow(row);
                            }

                            ViewState["dt"] = dt;
                        }
                    }
                }
                else if (rdoDataType.Text == "Relationships")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Relationships WHERE Entity1_Name LIKE '%' + @Entity1_Name + '%'"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Entity1_Name", search);
                            sda.SelectCommand = cmd;
                            sda.Fill(dt);
                            ViewState["dt"] = dt;
                        }
                    }
                }
                con.Close();
            }
            return dt;
        }

        protected void Search(object sender, EventArgs e)
        {
            if (rdoDataType.Text == "Users")
            {
                DataRow[] dr = ((DataTable)ViewState["dt"]).Select("UserName like '%" + txtSearch.Text.ToString() + "%'");
                DataTable dt1 = dr.CopyToDataTable();
                ViewState["dt"] = dt1;
                gvExcelFile.DataSource = dt1;
                gvExcelFile.DataBind();
                manageControls();
            }
            else if (rdoDataType.Text == "Hosts" || rdoDataType.Text == "Applications" || rdoDataType.Text == "Storage" || rdoDataType.Text == "Databases")
            {
                DataRow[] dr = ((DataTable)ViewState["dt"]).Select("Name like '%" + txtSearch.Text.ToString() + "%'");
                DataTable dt1 = dr.CopyToDataTable();
                ViewState["dt"] = dt1;
                gvExcelFile.DataSource = dt1;
                gvExcelFile.DataBind();
                manageControls();
            }
            else if (rdoDataType.Text == "Relationships")
            {
                DataRow[] dr = ((DataTable)ViewState["dt"]).Select("Entity1_Name like '%" + txtSearch.Text.ToString() + "%'");
                DataTable dt1 = dr.CopyToDataTable();
                ViewState["dt"] = dt1;
                gvExcelFile.DataSource = dt1;
                gvExcelFile.DataBind();
                manageControls();
            }
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
            DataView sortedView = new DataView((DataTable)ViewState["dt"]);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvExcelFile.DataSource = sortedView;
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void btnExportToCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + rdoDataType.Text + ".csv");
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
            Response.AddHeader("content-disposition", "attachment;filename=" + rdoDataType.Text + ".xls");
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
            gvExcelFile.EditIndex = e.NewEditIndex;
            gvExcelFile.DataSource = ViewState["dt"];
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvExcelFile, "Edit$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (rdoDataType.SelectedItem.Text == "Users")
                    {
                        e.Row.Cells[1].Text = "User ID";
                        e.Row.Cells[2].Text = "First Name";
                        e.Row.Cells[3].Text = "Last Name";
                        e.Row.Cells[4].Text = "User Name";
                        e.Row.Cells[5].Text = "Password";
                        e.Row.Cells[6].Text = "Email";
                        e.Row.Cells[7].Text = "User Role";
                        e.Row.Cells[8].Text = "Creation Date";
                    }
                    if (rdoDataType.SelectedItem.Text == "Hosts")
                    {
                        e.Row.Cells[1].Text = "Name";
                        e.Row.Cells[2].Text = "OS";
                        e.Row.Cells[3].Text = "OS Version";
                        e.Row.Cells[4].Text = "Physical / Virtual";
                        e.Row.Cells[5].Text = "In Scope";
                        e.Row.Cells[6].Text = "Justification";
                        e.Row.Cells[7].Text = "Vendor";
                        e.Row.Cells[8].Text = "Model";
                        e.Row.Cells[9].Text = "Source DC";
                        e.Row.Cells[10].Text = "Function / Role";
                        e.Row.Cells[11].Text = "Host Type";
                        e.Row.Cells[12].Text = "Technical Contact";
                        e.Row.Cells[13].Text = "Discovery Source";
                        e.Row.Cells[14].Text = "Environment";
                        e.Row.Cells[15].Text = "Comments";
                    }
                    if (rdoDataType.SelectedItem.Text == "Applications")
                    {
                        e.Row.Cells[1].Text = "Name";
                        e.Row.Cells[2].Text = "Environment";
                        e.Row.Cells[3].Text = "Primary Owner";
                        e.Row.Cells[4].Text = "Secondary Owner";
                        e.Row.Cells[5].Text = "In Scope";
                        e.Row.Cells[6].Text = "Justification";
                        e.Row.Cells[7].Text = "Analysis Status";
                        e.Row.Cells[8].Text = "Description";
                        e.Row.Cells[9].Text = "Primary Technical Contact";
                        e.Row.Cells[10].Text = "Secondary Technical Contact";
                        e.Row.Cells[11].Text = "Business Unit";
                        e.Row.Cells[12].Text = "Vendor";
                        e.Row.Cells[13].Text = "Version";
                        e.Row.Cells[14].Text = "Business Criticality";
                        e.Row.Cells[15].Text = "Comments";
                    }
                    if (rdoDataType.SelectedItem.Text == "Storage")
                    {
                        e.Row.Cells[1].Text = "Name";
                        e.Row.Cells[2].Text = "Type";
                        e.Row.Cells[3].Text = "Vendor";
                        e.Row.Cells[4].Text = "Model";
                        e.Row.Cells[5].Text = "In Scope";
                        e.Row.Cells[6].Text = "Justification";
                        e.Row.Cells[7].Text = "Storage Capacity Allocated - GB";
                        e.Row.Cells[8].Text = "Storage Capacity Used - GB";
                        e.Row.Cells[9].Text = "Primary Owner";
                        e.Row.Cells[10].Text = "Secondary Owner";
                        e.Row.Cells[11].Text = "Host Type";
                        e.Row.Cells[12].Text = "Primary Technical Contact";
                        e.Row.Cells[13].Text = "Secondary Technical Contact";
                        e.Row.Cells[14].Text = "Discovery Source";
                        e.Row.Cells[15].Text = "Environment";
                        e.Row.Cells[16].Text = "Comments";
                    }
                    if (rdoDataType.SelectedItem.Text == "Databases")
                    {
                        e.Row.Cells[1].Text = "Name";
                        e.Row.Cells[2].Text = "DB Type";
                        e.Row.Cells[3].Text = "DB Version";
                        e.Row.Cells[4].Text = "In Scope";
                        e.Row.Cells[5].Text = "Justification";
                        e.Row.Cells[6].Text = "Technical Contact";
                        e.Row.Cells[7].Text = "DB Instance";
                        e.Row.Cells[8].Text = "DB Server Name";
                        e.Row.Cells[9].Text = "DB Size(GB)";
                        e.Row.Cells[10].Text = "Source Discovery";
                        e.Row.Cells[11].Text = "Environment";
                        e.Row.Cells[12].Text = "Comments";
                    }
                    else if (rdoDataType.SelectedItem.Text == "Relationships")
                    {
                        e.Row.Cells[1].Text = "Entity1 Name";
                        e.Row.Cells[2].Text = "Entity1 Type";
                        e.Row.Cells[3].Text = "Entity2 Name";
                        e.Row.Cells[4].Text = "Entity2 Type";
                        e.Row.Cells[5].Text = "Score";
                        e.Row.Cells[6].Text = "Migration Type";
                    }
                }
            }
            catch
            {
                Response.Write("<script>alert('Please upload valid excel file.')</script>");
            }
        }

        protected void rdoDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvExcelFile.DataSource = null;
            gvExcelFile.PageIndex = 0;
            gvExcelFile.DataBind();
            manageControls();
            btnUpload.Visible = false;
            ViewState["dt"] = null;
        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["dt"] as DataTable;
            gvExcelFile.DataSource = dt;
            gvExcelFile.DataBind();
            for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                dt.Rows[row.RowIndex][j - 1] = (row.Cells[j].Controls[0] as TextBox).Text;
            }
            ViewState["dt"] = dt;
            gvExcelFile.EditIndex = -1;
            gvExcelFile.DataSource = ViewState["dt"];
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            gvExcelFile.EditIndex = -1;
            gvExcelFile.DataSource = ViewState["dt"];
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void btnFetchData_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string ConStr = "";
                    string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
                    //getting the path of the file   
                    string path = Server.MapPath("~/Images/" + FileUpload1.FileName);
                    //saving the file inside the MyFolder of the server  
                    FileUpload1.SaveAs(path);
                    FileUpload1.FileContent.Dispose();
                    //Label1.Text = FileUpload1.FileName + " data uploaded sucessfully.";
                    //checking that extantion is .xls or .xlsx  
                    if (ext.Trim() == ".xls")
                    {
                        //connection string for that file which extantion is .xls  
                        ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (ext.Trim() == ".xlsx")
                    {
                        //connection string for that file which extantion is .xlsx  
                        ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //string query = "SELECT * FROM [Sheet1$]";
                    string query = "SELECT * FROM [" + rdoDataType.SelectedItem.Text + "$]";
                    //Providing connection  
                    OleDbConnection conn = new OleDbConnection(ConStr);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    //create command object  
                    OleDbCommand cmd1 = new OleDbCommand(query, conn);
                    // create a data adapter and get the data into dataadapter  
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd1);
                    DataSet ds = new DataSet();
                    //fill the Excel data to data set  
                    da.Fill(ds);
                    ViewState["dt"] = ds.Tables[0];

                    if (ViewState["dt"] != null)
                    {
                        btnUpload.Visible = true;
                    }
                    else
                    {
                        btnUpload.Visible = false;
                    }
                    UpdatePanel1.Update();
                }
                if (ViewState["dt"] != null)
                {
                    gvExcelFile.DataSource = (DataTable)ViewState["dt"];
                    gvExcelFile.DataBind();
                    manageControls();
                }
            }
            catch (Exception ex)
            {
                FileUpload1.FileContent.Dispose();
                Response.Write("<script>alert('Please upload valid excel file.')</script>");
            }
        }
    }
}