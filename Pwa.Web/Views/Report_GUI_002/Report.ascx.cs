using Microsoft.Reporting.WebForms;
using Pwa.Component.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pwa.Web.Views.Report_GUI_002
{
    public partial class Report : ViewUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptViewer.Reset();
                rptViewer.AsyncRendering = false;
                rptViewer.LocalReport.ReportPath = Server.MapPath("~/Views/Report_GUI_002/GUI_002.rdlc");
                rptViewer.LocalReport.DataSources.Clear();

                N_SqlDB conn = new N_SqlDB(ConfigurationManager.AppSettings["Smart1662DB"]);
                conn.Open();

                //DataTable dt = conn.ExecuteDataTable("SELECT * FROM vw_doc_buyin");
                DataTable dt = conn.ExecuteDataTable("SELECT * FROM vw_report_gui_002");

                // Must match the DataSource in the RDLC
                ReportDataSource reportDataSource = new ReportDataSource("data_source", dt);

                // Add any parameters to the collection
                rptViewer.LocalReport.DataSources.Add(reportDataSource);
                rptViewer.DataBind();
                rptViewer.LocalReport.Refresh();
            }
        }
    }
}