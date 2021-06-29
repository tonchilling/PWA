using Microsoft.Reporting.WebForms;
using Org.BouncyCastle.Ocsp;
using Pwa.Component.Database;
using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Pwa.Web.Controllers.Report
{
    public class Report_GUI_002Controller : BaseController
    {
        private bool _reportViewStatus = false;

        private ReportViewer reportViewer = new ReportViewer()
        {
            ProcessingMode = ProcessingMode.Local,
            SizeToReportContent = true,
            Width = Unit.Percentage(100),
            Height = Unit.Percentage(100)
        };

        // GET: Report_GUI_002
        public ActionResult Index()
        {
            ViewData["_reportViewStatus"] = _reportViewStatus;
            return View();
        }

        public class b_data
        {
            public string code { get; set; }
            public string name { get; set; }
        }
        public JsonResult LoadBranchForMain(string main)
        {
            string[] tmp = main.Split(',');
            //int[] m = new int[tmp.Length];

            //for(int i = 0; i < tmp.Length; i++)
            //{ m[i] = Convert.ToInt32(tmp[i]); }

            using (Smart1662Entities db = new Smart1662Entities())
            {
                List<b_data> branch = new List<b_data>();
                for (int i = 0; i < tmp.Length; i++)
                {
                    string code = tmp[i];
                    var b = db.SysBranch.Where(f => (code == f.Parent || code == "-1") && f.TypeCode == "4").Select(f => new b_data()
                    {
                        name = f.Name,
                        code = f.Code
                    }).ToArray();

                    branch.AddRange(b);
                }
                branch = branch.Distinct().ToList();
                return Json(branch.OrderBy(f => f.name).ToArray());
            }
        }

        public ActionResult Display()
        {
            _reportViewStatus = true;
            ViewData["_reportViewStatus"] = _reportViewStatus;

            reportViewer.Reset();
            //reportViewer.AsyncRendering = false;
            reportViewer.LocalReport.ReportPath = Server.MapPath("~/Views/Report_GUI_002/GUI_002.rdlc");
            reportViewer.ShowExportControls = true;
            reportViewer.LocalReport.DataSources.Clear();

            N_SqlDB conn = new N_SqlDB(ConfigurationManager.AppSettings["Smart1662DB"]);
            conn.Open();

            string sql = "SELECT * FROM vw_report_gui_002 WHERE CONCAT(CONCAT(Y_Year, '-'), IIF(M_Month < 10, CONCAT('0', M_Month), CAST(M_Month AS NVARCHAR))) >= @P0 AND CONCAT(CONCAT(Y_Year, '-'), IIF(M_Month < 10, CONCAT('0', M_Month), CAST(M_Month AS NVARCHAR))) <= @P1";

            if (!string.IsNullOrEmpty(Request["main_branch"]))
            { sql += " AND Code IN(" + Request["main_branch"] + ")"; }
            if (!string.IsNullOrEmpty(Request["branch"]))
            { sql += " AND BracnchNo IN(" + Request["channel"] + ")"; }
            if (!string.IsNullOrEmpty(Request["channel"]))
            { sql += " AND ChannelCategoryld IN(" + Request["channel"] + ")"; }

            string[] param = new string[2];
            int month_start = Convert.ToInt32(Request["month_start"]);
            int month_end = Convert.ToInt32(Request["month_end"]);
            param[0] = Request["year_start"] + "-" + ((month_start < 10 ? "0" : "") + Request["month_start"]);
            param[1] = Request["year_end"] + "-" + ((month_end < 10 ? "0" : "") + Request["month_end"]);

            DataTable dt = conn.ExecuteDataTable(sql, param);

            // Must match the DataSource in the RDLC
            ReportDataSource reportDataSource = new ReportDataSource("data_source", dt);

            ReportParameter _params = new ReportParameter();
            _params = new ReportParameter("show_detail", Request["show_detail"], false); 
            reportViewer.LocalReport.SetParameters(_params);

            // Add any parameters to the collection
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            //reportViewer.DataBind();
            reportViewer.LocalReport.Refresh();

            ViewBag.ReportViewer = reportViewer;
            return View("Index");
        }
    }
}
