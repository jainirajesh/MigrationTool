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
    public partial class Hosts : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Hosts"))
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

        protected void imgbtnCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            dt.DefaultView.RowFilter = "Name LIKE '%" + txtSearch.Text.ToString() + "%'";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename= Hosts.csv");
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
            dt.DefaultView.RowFilter = "Name LIKE '%" + txtSearch.Text.ToString() + "%'";

            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Hosts.xls");
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
            lblHost.Text = row1.Cells[1].Text.Trim();
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

            lblHostname.Text = "";
            txtOS.Text = "";
            txtOSVersion.Text = "";
            txtPhyVir.Text = "";
            txtInScope.Text = "";
            txtJustificatio.Text = "";
            txtVendor.Text = "";
            txtModel.Text = "";
            txtSourceDC.Text = "";
            txtFunRole.Text = "";
            txtHostType.Text = "";
            txtTechContact.Text = "";
            txtDiscSource.Text = "";
            txtEnvironment.Text = "";
            txtComments.Text = "";

            if (row.Cells[1].Text.Trim() != "&nbsp;")
            {
                lblHostname.Text = row.Cells[1].Text.Trim();
            }
            if (row.Cells[2].Text.Trim() != "&nbsp;")
            {
                txtOS.Text = row.Cells[2].Text.Trim();
            }
            if (row.Cells[3].Text.Trim() != "&nbsp;")
            {
                txtOSVersion.Text = row.Cells[3].Text.Trim();
            }
            if (row.Cells[4].Text.Trim() != "&nbsp;")
            {
                txtPhyVir.Text = row.Cells[4].Text.Trim();
            }
            if (row.Cells[5].Text.Trim() != "&nbsp;")
            {
                txtInScope.Text = row.Cells[5].Text.Trim();
            }
            if (row.Cells[6].Text.Trim() != "&nbsp;")
            {
                txtJustificatio.Text = row.Cells[6].Text.Trim();
            }
            if (row.Cells[7].Text.Trim() != "&nbsp;")
            {
                txtVendor.Text = row.Cells[7].Text.Trim();
            }
            if (row.Cells[8].Text.Trim() != "&nbsp;")
            {
                txtModel.Text = row.Cells[8].Text.Trim();
            }
            if (row.Cells[9].Text.Trim() != "&nbsp;")
            {
                txtSourceDC.Text = row.Cells[9].Text.Trim();
            }
            if (row.Cells[10].Text.Trim() != "&nbsp;")
            {
                txtFunRole.Text = row.Cells[10].Text.Trim();
            }
            if (row.Cells[11].Text.Trim() != "&nbsp;")
            {
                txtHostType.Text = row.Cells[11].Text.Trim();
            }
            if (row.Cells[12].Text.Trim() != "&nbsp;")
            {
                txtTechContact.Text = row.Cells[12].Text.Trim();
            }
            if (row.Cells[13].Text.Trim() != "&nbsp;")
            {
                txtDiscSource.Text = row.Cells[13].Text.Trim();
            }
            if (row.Cells[14].Text.Trim() != "&nbsp;")
            {
                txtEnvironment.Text = row.Cells[14].Text.Trim();
            }
            if (row.Cells[15].Text.Trim() != "&nbsp;")
            {
                txtComments.Text = row.Cells[15].Text.Trim();
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
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(Name, 'System.String') like '{0}'", lblHostname.Text.Trim())).FirstOrDefault();
            dtUpdateDB.Rows.Add();
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                filterData1[0] = lblHostname.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][0] = lblHostname.Text.Trim();

                filterData1[1] = txtOS.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][1] = txtOS.Text.Trim();

                filterData1[2] = txtOSVersion.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][2] = txtOSVersion.Text.Trim();

                filterData1[3] = txtPhyVir.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][3] = txtPhyVir.Text.Trim();

                filterData1[4] = txtInScope.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][4] = txtInScope.Text.Trim();

                filterData1[5] = txtJustificatio.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][5] = txtJustificatio.Text.Trim();

                filterData1[6] = txtVendor.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][6] = txtVendor.Text.Trim();

                filterData1[7] = txtModel.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][7] = txtModel.Text.Trim();

                filterData1[8] = txtSourceDC.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][8] = txtSourceDC.Text.Trim();

                filterData1[9] = txtFunRole.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][9] = txtFunRole.Text.Trim();

                filterData1[10] = txtHostType.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][10] = txtHostType.Text.Trim();

                filterData1[11] = txtTechContact.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][11] = txtTechContact.Text.Trim();

                filterData1[12] = txtDiscSource.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][12] = txtDiscSource.Text.Trim();

                filterData1[13] = txtEnvironment.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][13] = txtEnvironment.Text.Trim();

                filterData1[14] = txtComments.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][14] = txtComments.Text.Trim();
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
                        query2 = "update Hosts set OS = @OS, OS_Version = @OS_Version, Physical_or_Virtual = @Physical_or_Virtual, In_Scope = @In_Scope, Out_of_Scope_Justification = @Out_of_Scope_Justification, Vendor = @Vendor, Model = @Model, Source_DC = @Source_DC, Function_or_Role = @Function_or_Role, Host_Type = @Host_Type, Technical_Contact = @Technical_Contact, Discovery_Source = @Discovery_Source, Environment = @Environment, Comments = @Comments where Name = @Name";
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
            }

            if (ViewState["dtDelete"] != null)
            {
                DataTable dtDel = new DataTable();
                dtDel = (DataTable)ViewState["dtDelete"];
                for (int i = 0; i < dtDel.Rows.Count; i++)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        query2 = "delete Hosts where Name = @Name";
                        using (SqlCommand cmd = new SqlCommand(query2))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Name", dtDel.Rows[i]["Name"].ToString());
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
            dtToBind.DefaultView.RowFilter = "Name LIKE '%" + txtSearch.Text.ToString() + "%'";
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
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(Name, 'System.String') like '{0}'", lblHost.Text)).FirstOrDefault();
            dtDelete.Rows.Add();
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                dtDelete.Rows[dtDelete.Rows.Count - 1][0] = lblHost.Text;
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