using Microsoft.Reporting.WebForms;
using System.Data;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Pwa.Web.Controllers.Smart1662
{
    public class ServiceReceiverSummaryReportController : BaseController
    {
        // GET: ServiceReceiverSummaryReport

        private bool _reportViewStatus = false;

        private ReportViewer reportViewer = new ReportViewer()
        {
            ProcessingMode = ProcessingMode.Local,
            SizeToReportContent = true,
            Width = Unit.Percentage(100),
            Height = Unit.Percentage(100)
        };

        public ActionResult ServiceReceiverSummaryReportView()
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

            reportViewer.LocalReport.ReportPath = "./ReportTemplate/rtp_gui010.rdlc";
            reportViewer.ShowExportControls = true;
            reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("ds_gui010", dt);

            reportViewer.LocalReport.DataSources.Add(rdc);
            reportViewer.LocalReport.Refresh();

            ViewBag.ReportViewer = reportViewer;
            return View("ServiceReceiverSummaryReportView");
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
            dr["col1"] = "1. PWA Coll Center 1662";
            dr["col2"] = 343;
            dr["col3"] = 29.80;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 808;
            dr["col7"] = 70.20;
            dr["col8"] = 1151;
            dr["col9"] = 38.81;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "2. WebSite กปภ ";
            dr["col2"] = 90;
            dr["col3"] = 45.45;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 108;
            dr["col7"] = 54.55;
            dr["col8"] = 198;
            dr["col9"] = 6.68;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "3. E-Mail (pr@pwa.co.th)";
            dr["col2"] = 0;
            dr["col3"] = 0.00;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 0;
            dr["col7"] = 0;
            dr["col8"] = 0;
            dr["col9"] = 0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "4. Facebook";
            dr["col2"] = 12;
            dr["col3"] = 100.00;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 0;
            dr["col7"] = 0.00;
            dr["col8"] = 12;
            dr["col9"] = 0.40;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "5. GCC1111";
            dr["col2"] = 0;
            dr["col3"] = 0.00;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 0;
            dr["col7"] = 0;
            dr["col8"] = 0;
            dr["col9"] = 0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "6. จดหมาย / หนังสือ";
            dr["col2"] = 0;
            dr["col3"] = 0.00;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 0;
            dr["col7"] = 0;
            dr["col8"] = 0;
            dr["col9"] = 0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "7. สื่อมวลชน";
            dr["col2"] = 5;
            dr["col3"] = 100.00;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 0;
            dr["col7"] = 0.00;
            dr["col8"] = 5;
            dr["col9"] = 0.17;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "8. โทรศัพไปที่ กปภ.สาขา/กปภ.เขต";
            dr["col2"] = 1409;
            dr["col3"] = 97.31;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 39;
            dr["col7"] = 2.69;
            dr["col8"] = 1448;
            dr["col9"] = 48.82;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "9. Line";
            dr["col2"] = 43;
            dr["col3"] = 93.48;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 3;
            dr["col7"] = 6.52;
            dr["col8"] = 46;
            dr["col9"] = 1.55;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "10. ลูกค้าเดินทางไปพบเจ้าหน้าที่";
            dr["col2"] = 91;
            dr["col3"] = 85.85;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 15;
            dr["col7"] = 14.15;
            dr["col8"] = 106;
            dr["col9"] = 3.57;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["col1"] = "12. ศูนย์ดำรงธรรม กระทรวงมหาดไทย";
            dr["col2"] = 0;
            dr["col3"] = 0.00;
            dr["col4"] = 0;
            dr["col5"] = 0.00;
            dr["col6"] = 0;
            dr["col7"] = 0;
            dr["col8"] = 0;
            dr["col9"] = 0;
            dt.Rows.Add(dr);

            return dt;
        }
    }
}