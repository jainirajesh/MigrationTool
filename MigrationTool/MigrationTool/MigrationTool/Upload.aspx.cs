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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!this.IsPostBack)
            {
                gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
                gvExcelFile.PageIndex = 0;
                gvExcelFile.DataBind();                
            }
            if (((DataTable)ViewState["dt"]).Rows.Count > 0)
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
            Label1.Visible = false;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //if (FileUpload1.HasFile && drpDataType.SelectedIndex > 0)
            if (FileUpload1.HasFile)
            {
                //Coneection String by default empty  
                string ConStr = "";
                //Extantion of the file upload control saving into ext because   
                //there are two types of extation .xls and .xlsx of Excel   
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
                //making query  
                string query = "SELECT * FROM [Sheet1$]";
                //Providing connection  
                OleDbConnection conn = new OleDbConnection(ConStr);
                //checking that connection state is closed or not if closed the   
                //open the connection  
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
                //set data source of the grid view  

                string constr = ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query1 = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataTable dtResult = BindGrid(ds.Tables[0].Rows[i]["UserName"].ToString());
                        if (dtResult.Rows.Count == 0)
                        {
                            query1 = "insert into UserDetails values (@FirstName, @LastName, @UserName, @Password, @Email, @CreatedDate)";
                            using (SqlCommand cmd = new SqlCommand(query1))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@FirstName", ds.Tables[0].Rows[i]["FirstName"].ToString());
                                cmd.Parameters.AddWithValue("@LastName", ds.Tables[0].Rows[i]["LastName"].ToString());
                                cmd.Parameters.AddWithValue("@UserName", ds.Tables[0].Rows[i]["UserName"].ToString());
                                cmd.Parameters.AddWithValue("@Password", ds.Tables[0].Rows[i]["Password"].ToString());
                                cmd.Parameters.AddWithValue("@Email", ds.Tables[0].Rows[i]["Email"].ToString());
                                cmd.Parameters.AddWithValue("@CreatedDate", ds.Tables[0].Rows[i]["CreatedDate"].ToString());
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }

                gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
                gvExcelFile.DataBind();
                //close the connection  
                conn.Close();
            }
            else
            {
                Label1.Text = "Please select the Data Type and file first !!";
            }
        }

        private DataTable BindGrid(string search)
        {
            string constr = ConfigurationManager.ConnectionStrings["sqldbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM UserDetails WHERE UserName LIKE '%' + @UserName + '%'"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@UserName", search);
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            ViewState["dt"] = dt;
                            return dt;
                        }
                    }
                }
            }
            
        }

        protected void Search(object sender, EventArgs e)
        {
            gvExcelFile.DataSource = BindGrid(txtSearch.Text.Trim());
            gvExcelFile.DataBind();
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
        }

        protected void btnExportToCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = BindGrid(txtSearch.Text.Trim());

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + drpDataType.SelectedItem.Text + ".csv");
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
            Response.AddHeader("content-disposition", "attachment;filename=" + drpDataType.SelectedItem.Text + ".xls");
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

    }
}