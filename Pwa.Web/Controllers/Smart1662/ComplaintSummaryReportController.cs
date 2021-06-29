using Microsoft.Reporting.WebForms;
using System.Data;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Pwa.Web.Controllers.Smart1662
{
    public class ComplaintSummaryReportController : BaseController
    {
        private bool _reportViewStatus = false;

        private ReportViewer reportViewer = new ReportViewer()
        {
            ProcessingMode = ProcessingMode.Local,
            SizeToReportContent = true,
            Width = Unit.Percentage(100),
            Height = Unit.Percentage(100)
        };

        public ActionResult ComplaintSummaryReportView()
        {
            ViewData["_reportViewStatus"] = _reportViewStatus;
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult GenReport()
        {
            _reportViewStatus = true;
            ViewData["_reportViewStatus"] = _reportViewStatus;

            DataTable dt = GetMockupData();

            reportViewer.LocalReport.ReportPath = "./ReportTemplate/rtp_gui004.rdlc";
            reportViewer.ShowExportControls = true;
            reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("ds_gui004", dt);

            reportViewer.LocalReport.DataSources.Add(rdc);
            reportViewer.LocalReport.Refresh();

            ViewBag.ReportViewer = reportViewer;
            return View("ComplaintSummaryReportView");
        }

        public DataTable GetMockupData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("col1");
            dt.Columns.Add("col2");
            dt.Columns.Add("col3");
            dt.Columns.Add("col4");
            dt.Columns.Add("col5");
            dt.Columns.Add("col6");

            DataRow dr = dt.NewRow();
            dr["col1"] = "1.ปริมาณน้ำ";
            dr["col2"] = "1.1 น้ำไม่ไหล";
            dr["col3"] = "";
            dr["col4"] = "";
            dr["col5"] = "";
            dr["col6"] = "";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "";
            dr["col2"] = "1.2 น้ำไหลอ่อน  น้ำไหล ๆ หยุด ๆ น้ำไหลเป็นเวลา";
            dr["col3"] = "";
            dr["col4"] = "";
            dr["col5"] = "";
            dr["col6"] = "";
            dt.Rows.Add(dr);
            return dt;
        }
    }
}