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
    public partial class Dependencies : System.Web.UI.Page
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
                imgbtnExcel.Visible = true;
                imgbtnCSV.Visible = true;
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Relationships"))
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
            sortedView.RowFilter = "Entity1_Name LIKE '%" + txtSearch.Text.ToString() + "%'";
            gvExcelFile.DataSource = sortedView;
            gvExcelFile.DataBind();
            manageControls();
        }

        protected void imgbtnCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            dt.DefaultView.RowFilter = "Entity1_Name LIKE '%" + txtSearch.Text.ToString() + "%'";
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
            dt.DefaultView.RowFilter = "Entity1_Name LIKE '%" + txtSearch.Text.ToString() + "%'";

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
            lblReleationship.Text = row1.Cells[1].Text.Trim();
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

            lblEntity1Name.Text = "";
            txtEntity1Type.Text = "";
            txtEntity2Name.Text = "";
            txtEntity2Type.Text = "";
            txtScore.Text = "";
            txtMigrationType.Text = "";

            if (row.Cells[1].Text.Trim() != "&nbsp;")
            {
                lblEntity1Name.Text = row.Cells[1].Text.Trim();
            }
            if (row.Cells[2].Text.Trim() != "&nbsp;")
            {
                txtEntity1Type.Text = row.Cells[2].Text.Trim();
            }
            if (row.Cells[3].Text.Trim() != "&nbsp;")
            {
                txtEntity2Name.Text = row.Cells[3].Text.Trim();
            }
            if (row.Cells[4].Text.Trim() != "&nbsp;")
            {
                txtEntity2Type.Text = row.Cells[4].Text.Trim();
            }
            if (row.Cells[5].Text.Trim() != "&nbsp;")
            {
                txtScore.Text = row.Cells[5].Text.Trim();
            }
            if (row.Cells[6].Text.Trim() != "&nbsp;")
            {
                txtMigrationType.Text = row.Cells[6].Text.Trim();
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
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(Name, 'System.String') like '{0}'", lblEntity1Name.Text.Trim())).FirstOrDefault();
            dtUpdateDB.Rows.Add();
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                filterData1[0] = lblEntity1Name.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][0] = lblEntity1Name.Text.Trim();

                filterData1[1] = txtEntity1Type.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][1] = txtEntity1Type.Text.Trim();

                filterData1[2] = txtEntity2Name.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][2] = txtEntity2Name.Text.Trim();

                filterData1[3] = txtEntity2Type.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][3] = txtEntity2Type.Text.Trim();

                filterData1[4] = txtScore.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][4] = txtScore.Text.Trim();

                filterData1[5] = txtMigrationType.Text.Trim();
                dtUpdateDB.Rows[dtUpdateDB.Rows.Count - 1][5] = txtMigrationType.Text.Trim();
               
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
                        query2 = "update Relationships set Entity1_Name = @Entity1_Name, Entity1_Type = @Entity1_Type, Entity2_Name = @Entity2_Name, Score = @Score, Migration_Type = @Migration_Type where Entity1_Name=@Entity1_Name";
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
			if (ViewState["dtDelete"] != null)
            {
                DataTable dtDel = new DataTable();
                dtDel = (DataTable)ViewState["dtDelete"];
                for (int i = 0; i < dtDel.Rows.Count; i++)
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {                        
                        query2 = "delete Relationships where Entity1_Name = @Name";
                        using (SqlCommand cmd = new SqlCommand(query2))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Name", dtDel.Rows[i]["Entity1_Name"].ToString());
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
            dtToBind.DefaultView.RowFilter = "Entity1_Name LIKE '%" + txtSearch.Text.ToString() + "%'";
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
            DataRow filterData1 = dtTemp.Select(string.Format("Convert(Name, 'System.String') like '{0}'", lblReleationship.Text)).FirstOrDefault();
            dtDelete.Rows.Add();
            //for (int j = 1; j < gvExcelFile.Rows[0].Cells.Count; j++)
            {
                dtDelete.Rows[dtDelete.Rows.Count - 1][0] = lblReleationship.Text;
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