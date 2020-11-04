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
            //if (!IsPostBack)
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
                //gvExcelFile.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
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
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["dt"];

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query1, query2 = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (rdoDataType.Text == "Users")
                    {
                        DataTable dtResult = BindGrid(dt.Rows[i]["UserName"].ToString());
                        if (dtResult.Rows.Count == 0)
                        {
                            query1 = "insert into UserDetails values (@FirstName, @LastName, @UserName, @Password, @Email, @UserRole, @CreatedDate)";
                            using (SqlCommand cmd = new SqlCommand(query1))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@FirstName", dt.Rows[i]["FirstName"].ToString());
                                cmd.Parameters.AddWithValue("@LastName", dt.Rows[i]["LastName"].ToString());
                                cmd.Parameters.AddWithValue("@UserName", dt.Rows[i]["UserName"].ToString());
                                cmd.Parameters.AddWithValue("@Password", dt.Rows[i]["Password"].ToString());
                                cmd.Parameters.AddWithValue("@Email", dt.Rows[i]["Email"].ToString());
                                cmd.Parameters.AddWithValue("@UserRole", dt.Rows[i]["UserRole"].ToString());
                                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    else if (rdoDataType.Text == "Hosts")
                    {
                        DataTable dtResult = BindGrid(dt.Rows[i]["Name"].ToString());
                        if (dtResult.Rows.Count == 0)
                        {
                            query2 = "insert into Hosts values (@Name,@OS,@OS_Version,@Physical_or_Virtual,@In_Scope,@Out_of_Scope_Justification,@Vendor,@Model,@Source_DC,@Function_or_Role,@Host_Type,@Technical_Contact,@Discovery_Source,@Environment,@Comments)";
                            using (SqlCommand cmd = new SqlCommand(query2))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@Name", dt.Rows[i]["Name"].ToString());
                                cmd.Parameters.AddWithValue("@OS", dt.Rows[i]["OS"].ToString());
                                cmd.Parameters.AddWithValue("@OS_Version", dt.Rows[i]["OS_Version"].ToString());
                                cmd.Parameters.AddWithValue("@Physical_or_Virtual", dt.Rows[i]["Physical_or_Virtual"].ToString());
                                cmd.Parameters.AddWithValue("@In_Scope", (dt.Rows[i]["In_Scope"].ToString()));
                                cmd.Parameters.AddWithValue("@Out_of_Scope_Justification", dt.Rows[i]["Out_of_Scope_Justification"].ToString());
                                cmd.Parameters.AddWithValue("@Vendor", dt.Rows[i]["Vendor"].ToString());
                                cmd.Parameters.AddWithValue("@Model", dt.Rows[i]["Model"].ToString());
                                cmd.Parameters.AddWithValue("@Source_DC", dt.Rows[i]["Source_DC"].ToString());
                                cmd.Parameters.AddWithValue("@Function_or_Role", dt.Rows[i]["Function_or_Role"].ToString());
                                cmd.Parameters.AddWithValue("@Host_Type", dt.Rows[i]["Host_Type"].ToString());
                                cmd.Parameters.AddWithValue("@Technical_Contact", dt.Rows[i]["Technical_Contact"].ToString());
                                cmd.Parameters.AddWithValue("@Discovery_Source", dt.Rows[i]["Discovery_Source"].ToString());
                                cmd.Parameters.AddWithValue("@Environment", dt.Rows[i]["Environment"].ToString());
                                cmd.Parameters.AddWithValue("@Comments", dt.Rows[i]["Comments"].ToString());
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    else if (rdoDataType.Text == "Applications")
                    {
                        DataTable dtResult = BindGrid(dt.Rows[i]["Name"].ToString());
                        if (dtResult.Rows.Count == 0)
                        {
                            query2 = "insert into Applications values (@Name,@Environment,@Owner_Primary,@Owner_Secondary,@In_Scope,@Out_of_Scope_Justification,@Analysis_Status,@Description,@Technical_Contact_Primary,@Technical_Contact_Secondary,@Business_Unit,@Vendor,@Version,@Business_Criticality,@Comments)";
                            using (SqlCommand cmd = new SqlCommand(query2))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@Name", dt.Rows[i]["Name"].ToString());
                                cmd.Parameters.AddWithValue("@Environment", dt.Rows[i]["Environment"].ToString());
                                cmd.Parameters.AddWithValue("@Owner_Primary", dt.Rows[i]["Owner_Primary"].ToString());
                                cmd.Parameters.AddWithValue("@Owner_Secondary", dt.Rows[i]["Owner_Secondary"].ToString());
                                cmd.Parameters.AddWithValue("@In_Scope", (dt.Rows[i]["In_Scope"].ToString()));
                                cmd.Parameters.AddWithValue("@Out_of_Scope_Justification", dt.Rows[i]["Out_of_Scope_Justification"].ToString());
                                cmd.Parameters.AddWithValue("@Analysis_Status", dt.Rows[i]["Analysis_Status"].ToString());
                                cmd.Parameters.AddWithValue("@Description", dt.Rows[i]["Description"].ToString());
                                cmd.Parameters.AddWithValue("@Technical_Contact_Primary", dt.Rows[i]["Technical_Contact_Primary"].ToString());
                                cmd.Parameters.AddWithValue("@Technical_Contact_Secondary", dt.Rows[i]["Technical_Contact_Secondary"].ToString());
                                cmd.Parameters.AddWithValue("@Business_Unit", dt.Rows[i]["Business_Unit"].ToString());
                                cmd.Parameters.AddWithValue("@Vendor", dt.Rows[i]["Vendor"].ToString());
                                cmd.Parameters.AddWithValue("@Version", dt.Rows[i]["Version"].ToString());
                                cmd.Parameters.AddWithValue("@Business_Criticality", dt.Rows[i]["Business_Criticality"].ToString());
                                cmd.Parameters.AddWithValue("@Comments", dt.Rows[i]["Comments"].ToString());
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    else if (rdoDataType.Text == "Storage")
                    {
                        DataTable dtResult = BindGrid(dt.Rows[i]["Name"].ToString());
                        if (dtResult.Rows.Count == 0)
                        {
                            query2 = "insert into Storage values (@Name,@Type,@Vendor,@Model,@In_Scope,@Out_of_Scope_Justification,@Storage_Capacity_Allocated_GB,@Storage_Capacity_Used_GB,@Owner_Primary,@Owner_Secondary,@Host_Type,@Technical_Contact_Primary,@Technical_Contact_Secondary,@Discovery_Source,@Environment,@Comments)";
                            using (SqlCommand cmd = new SqlCommand(query2))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@Name", dt.Rows[i]["Name"].ToString());
                                cmd.Parameters.AddWithValue("@Type", dt.Rows[i]["Type"].ToString());
                                cmd.Parameters.AddWithValue("@Vendor", dt.Rows[i]["Vendor"].ToString());
                                cmd.Parameters.AddWithValue("@Model", dt.Rows[i]["Model"].ToString());
                                cmd.Parameters.AddWithValue("@In_Scope", (dt.Rows[i]["In_Scope"].ToString()));
                                cmd.Parameters.AddWithValue("@Out_of_Scope_Justification", dt.Rows[i]["Out_of_Scope_Justification"].ToString());
                                cmd.Parameters.AddWithValue("@Storage_Capacity_Allocated_GB", dt.Rows[i]["Storage_Capacity_Allocated_GB"].ToString());
                                cmd.Parameters.AddWithValue("@Storage_Capacity_Used_GB", dt.Rows[i]["Storage_Capacity_Used_GB"].ToString());
                                cmd.Parameters.AddWithValue("@Owner_Primary", dt.Rows[i]["Owner_Primary"].ToString());
                                cmd.Parameters.AddWithValue("@Owner_Secondary", dt.Rows[i]["Owner_Secondary"].ToString());
                                cmd.Parameters.AddWithValue("@Host_Type", dt.Rows[i]["Host_Type"].ToString());
                                cmd.Parameters.AddWithValue("@Technical_Contact_Primary", dt.Rows[i]["Technical_Contact_Primary"].ToString());
                                cmd.Parameters.AddWithValue("@Technical_Contact_Secondary", dt.Rows[i]["Technical_Contact_Secondary"].ToString());
                                cmd.Parameters.AddWithValue("@Discovery_Source", dt.Rows[i]["Discovery_Source"].ToString());
                                cmd.Parameters.AddWithValue("@Environment", dt.Rows[i]["Environment"].ToString());
                                cmd.Parameters.AddWithValue("@Comments", dt.Rows[i]["Comments"].ToString());
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    else if (rdoDataType.Text == "Databases")
                    {
                        DataTable dtResult = BindGrid(dt.Rows[i]["Name"].ToString());
                        if (dtResult.Rows.Count == 0)
                        {
                            query2 = "insert into Databases values (@Name,@Server_Name,@DB_Type,@DB_Version,@In_Scope,@Out_of_Scope_Justification,@Technical_Contact,@DB_Instance,@DB_Server_Name,@DB_Size_GB,@Discovery_Source,@Environment,@Comments)";
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
                    else if (rdoDataType.Text == "Relationships")
                    {
                        DataTable dtResult = BindGrid(dt.Rows[i]["Entity1_Name"].ToString());
                        if (dtResult.Rows.Count == 0)
                        {
                            query2 = "insert into Relationships values (@Entity1_Name,@Entity1_Type,@Entity2_Name,@Entity2_Type,@Score,@Migration_Type)";
                            using (SqlCommand cmd = new SqlCommand(query2))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@Entity1_Name", dt.Rows[i]["Entity1_Name"].ToString());
                                cmd.Parameters.AddWithValue("@Entity1_Type", dt.Rows[i]["Entity1_Type"].ToString());
                                cmd.Parameters.AddWithValue("@Entity2_Name", dt.Rows[i]["Entity2_Name"].ToString());
                                cmd.Parameters.AddWithValue("@Entity2_Type", dt.Rows[i]["Entity2_Type"].ToString());
                                cmd.Parameters.AddWithValue("@Score", dt.Rows[i]["Score"].ToString());
                                cmd.Parameters.AddWithValue("@Migration_Type", dt.Rows[i]["Migration_Type"].ToString());
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
            }
            gvExcelFile.DataSource = null;
            gvExcelFile.DataBind();
            manageControls();
            btnUpload.Visible = false;
            ViewState["dt"] = null;
            string message = "Data uploaded to database successfully!!";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        private DataTable BindGrid(string search)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                if (rdoDataType.Text == "Users")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM UserDetails WHERE UserName LIKE '%' + @UserName + '%'"))
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

            //DataTable dt1 = dr.CopyToDataTable();
            //ViewState["dt"] = dt1;
            //gvExcelFile.DataSource = dt1;
            //gvExcelFile.DataBind();
            //manageControls();
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvExcelFile, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (rdoDataType.SelectedItem.Text == "Users")
                {
                    //e.Row.Cells[1].Text = "User ID";
                    e.Row.Cells[1].Text = "First Name";
                    e.Row.Cells[2].Text = "Last Name";
                    e.Row.Cells[3].Text = "User Name";
                    e.Row.Cells[4].Text = "Password";
                    e.Row.Cells[5].Text = "Email";
                    e.Row.Cells[6].Text = "User Role";
                    //e.Row.Cells[7].Text = "Creation Date";
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
                //if (rdoDataType.SelectedItem.Text == "Users")
                {
                    dt.Rows[row.RowIndex][j - 1] = (row.Cells[j].Controls[0] as TextBox).Text;
                }
                //else if (rdoDataType.SelectedItem.Text == "Hosts")
                //{
                //    dt.Rows[row.RowIndex][j - 1] = (row.Cells[j].Controls[0] as TextBox).Text;
                //}
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
            if (FileUpload1.HasFile)
            {
                string ConStr = "";
                string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
                //getting the path of the file   
                string path = Server.MapPath("~/Images/" + FileUpload1.FileName);
                //saving the file inside the MyFolder of the server  
                FileUpload1.SaveAs(path);
                Label1.Text = FileUpload1.FileName + " data uploaded sucessfully.";
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
                string query = "SELECT * FROM [Sheet1$]";
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
                if (rdoDataType.SelectedItem.Text == "Hosts")
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0].Clone();
                    dt1.Columns[4].DataType = typeof(string);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dt1.ImportRow(row);
                    }
                    ViewState["dt"] = dt1;
                }
                else if (rdoDataType.SelectedItem.Text == "Applications")
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0].Clone();
                    dt1.Columns[4].DataType = typeof(string);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dt1.ImportRow(row);
                    }
                    ViewState["dt"] = dt1;
                }
                else if (rdoDataType.SelectedItem.Text == "Storage")
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0].Clone();
                    dt1.Columns[4].DataType = typeof(string);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dt1.ImportRow(row);
                    }
                    ViewState["dt"] = dt1;
                }
                else if (rdoDataType.SelectedItem.Text == "Databases")
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0].Clone();
                    dt1.Columns[3].DataType = typeof(string);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dt1.ImportRow(row);
                    }
                    ViewState["dt"] = dt1;
                }
                else if (rdoDataType.SelectedItem.Text == "Relationships")
                {
                    //ds.Tables[0].Columns.Remove("F7");
                    //ds.Tables[0].Columns.Remove("F8");
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0].Clone();
                    dt1.Columns[4].DataType = typeof(string);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dt1.ImportRow(row);
                    }
                    ViewState["dt"] = dt1;
                }

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
            //else
            //{
            //    Response.Write("<script>alert('Please select the fileto fetch the data.')</script>");
            //}
        }
    }
}