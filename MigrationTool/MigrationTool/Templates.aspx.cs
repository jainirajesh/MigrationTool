using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MigrationTool
{
    public partial class Templates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkHosts_Click(object sender, EventArgs e)
        {
            Download("Servers");
        }

        public void Download(string strFilename)
        {
            Response.ContentType = "application/x-msexcel";
            Response.AppendHeader("Content-Disposition", "attachment; filename= " + strFilename + ".xlsx");
            Response.TransmitFile(Server.MapPath("~/Files/" + strFilename + ".xlsx"));
            Response.End();
        }

        protected void lnkApplications_Click(object sender, EventArgs e)
        {
            Download("Applications");
        }

        protected void lnkStorage_Click(object sender, EventArgs e)
        {
            Download("Storage");
        }

        protected void lnkDatabase_Click(object sender, EventArgs e)
        {
            Download("Databases");
        }

        protected void lnkRelations_Click(object sender, EventArgs e)
        {
            Download("Dependencies");
        }
    }
}