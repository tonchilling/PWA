using Microsoft.Reporting.WebForms;
using System.Data;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Pwa.Web.Controllers.Smart1662
{
    public class ComplaintReportController : BaseController
    {
        // GET: ComplaintReport

        private bool _reportViewStatus = false;

        private ReportViewer reportViewer = new ReportViewer()
        {
            ProcessingMode = ProcessingMode.Local,
            SizeToReportContent = true,
            Width = Unit.Percentage(100),
            Height = Unit.Percentage(100)
        };

        public ActionResult ComplaintReportView()
        {
            ViewData["_reportViewStatus"] = _reportViewStatus;
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult GenReport()
        {
            //var reportViewer = new ReportViewer()
            //{
            //    ProcessingMode = ProcessingMode.Local,
            //    SizeToReportContent = true,
            //    Width = Unit.Percentage(100),
            //    Height = Unit.Percentage(100)
            //};

            _reportViewStatus = true;
            ViewData["_reportViewStatus"] = _reportViewStatus;

            DataTable dt = GetMockupData();

            reportViewer.LocalReport.ReportPath = "./ReportTemplate/rtp_gui001_1.rdlc";
            reportViewer.ShowExportControls = true;
            reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("ReportNo1", dt);

            reportViewer.LocalReport.DataSources.Add(rdc);
            reportViewer.LocalReport.Refresh();

            ViewBag.ReportViewer = reportViewer;
            return View("ComplaintReportView");
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
            dt.Columns.Add("col7");
            dt.Columns.Add("col8");
            dt.Columns.Add("col9");

            DataRow dr = dt.NewRow();
            dr["col1"] = "1 (22)";
            dr["col2"] = 1115;
            dr["col3"] = 169;
            dr["col4"] = 73;
            dr["col5"] = 170;
            dr["col6"] = 1;
            dr["col7"] = 1570;
            dr["col8"] = 37.90;
            dr["col9"] = 52;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "2 (30)";
            dr["col2"] = 665;
            dr["col3"] = 324;
            dr["col4"] = 26;
            dr["col5"] = 204;
            dr["col6"] = 2;
            dr["col7"] = 1221;
            dr["col8"] = 24.93;
            dr["col9"] = 41;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "3 (23)";
            dr["col2"] = 263;
            dr["col3"] = 160;
            dr["col4"] = 7;
            dr["col5"] = 70;
            dr["col6"] = 0;
            dr["col7"] = 500;
            dr["col8"] = 27.50;
            dr["col9"] = 17;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "4 (24)";
            dr["col2"] = 394;
            dr["col3"] = 118;
            dr["col4"] = 25;
            dr["col5"] = 95;
            dr["col6"] = 1;
            dr["col7"] = 633;
            dr["col8"] = 38.16;
            dr["col9"] = 21;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "5 (20)";
            dr["col2"] = 119;
            dr["col3"] = 47;
            dr["col4"] = 2;
            dr["col5"] = 38;
            dr["col6"] = 0;
            dr["col7"] = 206;
            dr["col8"] = 18.80;
            dr["col9"] = 7;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "6 (22)";
            dr["col2"] = 121;
            dr["col3"] = 104;
            dr["col4"] = 6;
            dr["col5"] = 39;
            dr["col6"] = 1;
            dr["col7"] = 271;
            dr["col8"] = 26.94;
            dr["col9"] = 9;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "7 (20)";
            dr["col2"] = 107;
            dr["col3"] = 62;
            dr["col4"] = 3;
            dr["col5"] = 23;
            dr["col6"] = 0;
            dr["col7"] = 195;
            dr["col8"] = 28.68;
            dr["col9"] = 7;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "8 (20)";
            dr["col2"] = 94;
            dr["col3"] = 93;
            dr["col4"] = 9;
            dr["col5"] = 28;
            dr["col6"] = 0;
            dr["col7"] = 224;
            dr["col8"] = 32.46;
            dr["col9"] = 7;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "9 (27)";
            dr["col2"] = 239;
            dr["col3"] = 73;
            dr["col4"] = 6;
            dr["col5"] = 25;
            dr["col6"] = 0;
            dr["col7"] = 343;
            dr["col8"] = 29.80;
            dr["col9"] = 1;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "10 (26)";
            dr["col2"] = 74;
            dr["col3"] = 52;
            dr["col4"] = 1;
            dr["col5"] = 21;
            dr["col6"] = 0;
            dr["col7"] = 148;
            dr["col8"] = 19.05;
            dr["col9"] = 5;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "ไม่ระบุพื้นที่";
            dr["col2"] = 0;
            dr["col3"] = 0;
            dr["col4"] = 0;
            dr["col5"] = 0;
            dr["col6"] = 0;
            dr["col7"] = 0;
            dr["col8"] = 0;
            dr["col9"] = 0;
            dt.Rows.Add(dr);

            return dt;
        }
    }
}