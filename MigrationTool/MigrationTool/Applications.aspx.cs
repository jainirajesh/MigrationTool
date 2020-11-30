﻿using System;
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
    public partial class Applications : System.Web.UI.Page
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
                }
            }
            else
            {
                imgbtnExcel.Visible = false;
                imgbtnCSV.Visible = false;
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Applications"))
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
            Response.AddHeader("content-disposition", "attachment;filename= Applications.csv");
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
            Response.AddHeader("content-disposition", "attachment;filename=Applications.xls");
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
            lblApplication.Text = row1.Cells[1].Text.Trim();
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

            lblApplicationname.Text = "";
            txtEnvironment.Text = "";
            txtPrimaryOwner.Text = "";
            txtSecondaryOwner.Text = "";
            txtInScope.Text = "";
            txtJustification.Text = "";
            txtVendor.Text = "";
            txtAnalysisStatus.Text = "";
            txtDescription.Text = "";
            txtPrimaryTechContact.Text = "";
            txtSecTechContact.Text = "";
            txtBusinessUnit.Text = "";
            txtBusinessCriticality.Text = "";
            txtVersion.Text = "";
            txtComments.Text = "";

            if (row.Cells[1].Text.Trim() != "&nbsp;")
            {
                lblApplicationname.Text = row.Cells[1].Text.Trim();
            }
            if (row.Cells[2].Text.Trim() != "&nbsp;")
            {
                txtEnvironment.Text = row.Cells[2].Text.Trim();
            }
            if (row.Cells[3].Text.Trim() != "&nbsp;")
            {
                txtPrimaryOwner.Text = row.Cells[3].Text.Trim();
            }
            if (row.Cells[4].Text.Trim() != "&nbsp;")
            {
                txtSecondaryOwner.Text = row.Cells[4].Text.Trim();
            }
            if (row.Cells[5].Text.Trim() != "&nbsp;")
            {
                txtInScope.Text = row.Cells[5].Text.Trim();
            }
            if (row.Cells[6].Text.Trim() != "&nbsp;")
            {
                txtJustification.Text = row.Cells[6].Text.Trim();
            }
            if (row.Cells[7].Text.Trim() != "&nbsp;")
            {
                txtAnalysisStatus.Text = row.Cells[7].Text.Trim();
            }
            if (row.Cells[8].Text.Trim() != "&nbsp;")
            {
                txtDescription.Text = row.Cells[8].Text.Trim();
            }
            if (row.Cells[9].Text.Trim() != "&nbsp;")
            {
                txtPrimaryTechContact.Text = row.Cells[9].Text.Trim();
            }
            if (row.Cells[10].Text.Trim() != "&nbsp;")
            {
                txtSecTechContact.Text = row.Cells[10].Text.Trim();
            }
            if (row.Cells[11].Text.Trim() != "&nbsp;")
            {
                txtBusinessUnit.Text = row.Cells[11].Text.Trim();
            }
            if (row.Cells[12].Text.Trim() != "&nbsp;")
            {
                txtVendor.Text = row.Cells[12].Text.Trim();
            }
            if (row.Cells[13].Text.Trim() != "&nbsp;")
            {
                txtVersion.Text = row.Cells[13].Text.Trim();
            }
            if (row.Cells[14].Text.Trim() != "&nbsp;")
            {
                txtBusinessCriticality.Text = row.Cells[14].Text.Trim();
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
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(Name, 'System.String') like '{0}'", lblApplicationname.Text.Trim())).FirstOrDefault();
            dtUpdateDB.Rows.Add();
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                filterData1[0] = lblApplicationname.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][0] = lblApplicationname.Text.Trim();

                filterData1[1] = txtEnvironment.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][1] = txtEnvironment.Text.Trim();

                filterData1[2] = txtPrimaryOwner.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][2] = txtPrimaryOwner.Text.Trim();

                filterData1[3] = txtSecondaryOwner.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][3] = txtSecondaryOwner.Text.Trim();

                filterData1[4] = txtInScope.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][4] = txtInScope.Text.Trim();

                filterData1[5] = txtJustification.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][5] = txtJustification.Text.Trim();

                filterData1[6] = txtAnalysisStatus.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][6] = txtAnalysisStatus.Text.Trim();

                filterData1[7] = txtDescription.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][7] = txtDescription.Text.Trim();

                filterData1[8] = txtPrimaryTechContact.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][8] = txtPrimaryTechContact.Text.Trim();

                filterData1[9] = txtSecTechContact.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][9] = txtSecTechContact.Text.Trim();

                filterData1[10] = txtBusinessUnit.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][10] = txtBusinessUnit.Text.Trim();

                filterData1[11] = txtVendor.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][11] = txtVendor.Text.Trim();

                filterData1[12] = txtVersion.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][12] = txtVersion.Text.Trim();

                filterData1[13] = txtBusinessCriticality.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][13] = txtBusinessCriticality.Text.Trim();

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
                        query2 = "update Applications set Environment = @Environment ,Owner_Primary = @Owner_Primary ,Owner_Secondary = @Owner_Secondary ,In_Scope = @In_Scope ,Out_of_Scope_Justification = @Out_of_Scope_Justification ,Analysis_Status = @Analysis_Status ,Description = @Description ,Technical_Contact_Primary = @Technical_Contact_Primary ,Technical_Contact_Secondary = @Technical_Contact_Secondary ,Business_Unit = @Business_Unit ,Vendor = @Vendor ,Version = @Version  ,Business_Criticality = @Business_Criticality ,Comments = @Comments where Name = @Name";
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
            }
			if (ViewState["dtDelete"] != null)
            {
                DataTable dtDel = new DataTable();
                dtDel = (DataTable)ViewState["dtDelete"];
                for (int i = 0; i < dtDel.Rows.Count; i++)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {                        
                        query2 = "delete Applications where Name = @Name";
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
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(Name, 'System.String') like '{0}'", lblApplication.Text)).FirstOrDefault();
            dtDelete.Rows.Add();
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                dtDelete.Rows[dtDelete.Rows.Count - 1][0] = lblApplication.Text;
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