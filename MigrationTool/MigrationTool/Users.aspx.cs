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
                ViewState["dtDelete"] = null;
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
                imgbtnCSV.Visible = true;
                imgbtnExcel.Visible = true;
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
                    btnRevert.Visible = false;
                }
            }
            else
            {
                imgbtnCSV.Visible = false;
                imgbtnExcel.Visible = false;
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users"))
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
            dtSearch.DefaultView.RowFilter = "username LIKE '%" + txtSearch.Text.ToString() + "%'";
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
            sortedView.RowFilter = "username LIKE '%" + txtSearch.Text.ToString() + "%'";
            gvExcelFile.DataSource = sortedView;
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void imgbtnCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            dt.DefaultView.RowFilter = "username LIKE '%" + txtSearch.Text.ToString() + "%'";
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

        protected void imgbtnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            dt.DefaultView.RowFilter = "username LIKE '%" + txtSearch.Text.ToString() + "%'";

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
            //if (Session["Role"].ToString() == "Admin")
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvExcelFile, "Edit$" + e.Row.RowIndex);
            //        e.Row.Attributes["style"] = "cursor:pointer";
            //    }
            //}
        }

        protected void rdoDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            gvExcelFile.DataSource = BindGrid();
            gvExcelFile.PageIndex = 0;
            gvExcelFile.DataBind();
            manageControls();            
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            GridViewRow row1 = (sender as ImageButton).NamingContainer as GridViewRow;                                   
            lblUser.Text = row1.Cells[3].Text.Trim();
            ModalPopupExtender2.Show();
            //DataTable dt1, dtDelete, dtTemp = new DataTable();

            //if (ViewState["dtEdit"] != null)
            //{
            //    dt1 = (DataTable)ViewState["dtEdit"];
            //}
            //else
            //{
            //    dt1 = (DataTable)ViewState["dt"];
            //}

            //if (ViewState["dtDelete"] != null)
            //{
            //    dtDelete = (DataTable)ViewState["dtDelete"];
            //}
            //else
            //{
            //    dtDelete = dt1.Clone();
            //}
            //dtTemp = dt1.Copy();
            //GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
            //int rowindex = (gvExcelFile.PageSize * gvExcelFile.PageIndex) + row.RowIndex;
            //BindToGridview(dt1);
            //DataRow filterData1 = dtTemp.Select(string.Format("Convert(Name, 'System.String') like '{0}'", row.Cells[1].Text)).FirstOrDefault();
            //dtDelete.Rows.Add();            
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            //{                
            //    dtDelete.Rows[dtDelete.Rows.Count - 1][0] = row.Cells[j].Text;               
            //}
            //filterData1.Delete();
            //ViewState["dtDelete"] = dtDelete;
            ////ViewState["dtUpdateDB"] = dtUpdateDB;
            //ViewState["dtEdit"] = dtTemp;
            //gvExcelFile.EditIndex = -1;
            //BindToGridview(dtTemp);
            //manageControls();
        }

        protected void OnEdit(object sender, EventArgs e)
        {
            GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
            int rowindex = (gvExcelFile.PageSize * gvExcelFile.PageIndex) + row.RowIndex;

            lblUsername.Text = "";
            txtFirstname.Text = "";
            txtLastname.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            drpUserRole.SelectedItem.Text = "";          

            if (row.Cells[3].Text.Trim() != "&nbsp;")
            {
                lblUsername.Text = row.Cells[3].Text.Trim();
            }
            if (row.Cells[1].Text.Trim() != "&nbsp;")
            {
                txtFirstname.Text = row.Cells[1].Text.Trim();
            }
            if (row.Cells[2].Text.Trim() != "&nbsp;")
            {
                txtLastname.Text = row.Cells[2].Text.Trim();
            }
            if (row.Cells[4].Text.Trim() != "&nbsp;")
            {
                txtPassword.Text = row.Cells[4].Text.Trim();
            }
            if (row.Cells[5].Text.Trim() != "&nbsp;")
            {
                txtEmail.Text = row.Cells[5].Text.Trim();
            }
            if (row.Cells[6].Text.Trim() != "&nbsp;")
            {
                drpUserRole.SelectedItem.Text = row.Cells[6].Text.Trim();
            }

            ModalPopupExtender1.Show();
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
            // GridViewRow row = (GridViewRow)ViewState["row"]; // (sender as ImageButton).NamingContainer as GridViewRow;
            // int rowindex = (gvExcelFile.PageSize * gvExcelFile.PageIndex) + row.RowIndex;
            BindToGridview(dt1);
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(username, 'System.String') like '{0}'", lblUsername.Text.Trim())).FirstOrDefault();
            dtUpdateDB.Rows.Add();
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                filterData1[3] = lblUsername.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][3] = lblUsername.Text.Trim();

                filterData1[1] = txtFirstname.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][1] = txtFirstname.Text.Trim();

                filterData1[2] = txtLastname.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][2] = txtLastname.Text.Trim();

                filterData1[4] = txtPassword.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][4] = txtPassword.Text.Trim();

                filterData1[5] = txtEmail.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][5] = txtEmail.Text.Trim();

                filterData1[6] = drpUserRole.SelectedItem.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][6] = drpUserRole.SelectedItem.Text.Trim();

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
            string query1, query2 = "";
            dt = (DataTable)ViewState["dtUpdateDB"];
            if (ViewState["dtUpdateDB"] != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        query1 = "update Users set FirstName= @FirstName, LastName = @LastName, Password = @Password, Email = @Email, CreatedDate = @CreatedDate where UserName = @UserName";
                        using (SqlCommand cmd = new SqlCommand(query1))
                        {
                            cmd.Connection = con;                            
                            cmd.Parameters.AddWithValue("@FirstName", dt.Rows[i]["FirstName"].ToString());
                            cmd.Parameters.AddWithValue("@LastName", dt.Rows[i]["LastName"].ToString());
                            cmd.Parameters.AddWithValue("@UserName", dt.Rows[i]["UserName"].ToString());
                            cmd.Parameters.AddWithValue("@Password", dt.Rows[i]["Password"].ToString());
                            cmd.Parameters.AddWithValue("@Email", dt.Rows[i]["Email"].ToString());
                            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString());
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }

            if (ViewState["dtDelete"] != null)
            {
                DataTable dtDel = new DataTable();
                dtDel = (DataTable)ViewState["dtDelete"];
                for (int i = 0; i < dtDel.Rows.Count; i++)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        query2 = "delete Users where Username = @Username";
                        using (SqlCommand cmd = new SqlCommand(query2))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Username", dtDel.Rows[i]["Username"].ToString());
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            txtSearch.Text = "";
            ViewState["dtUpdateDB"] = null;
            ViewState["dtDelete"] = null;
            gvExcelFile.EditIndex = -1;
            gvExcelFile.DataSource = BindGrid();
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void btnRevert_Click(object sender, EventArgs e)
        {
            ViewState["dtEdit"] = null;
            ViewState["dtUpdateDB"] = null;
            ViewState["dtDelete"] = null;
            gvExcelFile.EditIndex = -1;
            BindToGridview((DataTable)ViewState["dt"]);
            manageControls();
        }

        public void BindToGridview(DataTable dtToBind)
        {
            dtToBind.DefaultView.RowFilter = "username LIKE '%" + txtSearch.Text.ToString() + "%'";
            gvExcelFile.DataSource = dtToBind;
            gvExcelFile.DataBind();
        }

        protected void OnDeleteConfirm(object sender, EventArgs e)
        {
            //GridViewRow row1 = (sender as ImageButton).NamingContainer as GridViewRow;
            //lblHost.Text = row1.Cells[1].Text.Trim();
            //ModalPopupExtender2.Show();
            DataTable dt1, dtDelete, dtTemp = new DataTable();

            if (ViewState["dtEdit"] != null)
            {
                dt1 = (DataTable)ViewState["dtEdit"];
            }
            else
            {
                dt1 = (DataTable)ViewState["dt"];
            }

            if (ViewState["dtDelete"] != null)
            {
                dtDelete = (DataTable)ViewState["dtDelete"];
            }
            else
            {
                dtDelete = dt1.Clone();
            }
            dtTemp = dt1.Copy();
            //GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;
            //int rowindex = (gvExcelFile.PageSize * gvExcelFile.PageIndex) + row.RowIndex;
            BindToGridview(dt1);
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(username, 'System.String') like '{0}'", lblUser.Text)).FirstOrDefault();
            dtDelete.Rows.Add();
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                dtDelete.Rows[dtDelete.Rows.Count - 1][3] = lblUser.Text;
            }
            filterData1.Delete();
            ViewState["dtDelete"] = dtDelete;
            //ViewState["dtUpdateDB"] = dtUpdateDB;
            ViewState["dtEdit"] = dtTemp;
            gvExcelFile.EditIndex = -1;
            BindToGridview(dtTemp);
            manageControls();
        }
        
    }
}