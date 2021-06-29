using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pwa.FrameWork.Dto;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using Pwa.FrameWork.Bal.Smart1662;
namespace Pwa.Web.Controllers
{
    public class ReportController : BaseController
    {
        ReportManager manager = null;
        ReportDocument reportDoc = null;
        ReportOptionDto reportOptionDto = null;
        CriterialDto criterialDto = null;
        DataSet dsForReport = null;
        DataTable dtForScreen = null;
        string mode = "";

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GUI_001()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_001";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_001.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";
            criterialDto = GetCriteriaAsRequestForm();
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }

                dsForReport = manager.GetGUI001(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = "GUI_001_D";//
                    dsForReport.Tables[1].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                /*     List<Parameters> lst = new List<Parameters>();
                     lst.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                     lst.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });
                     lst.Add(new Parameters() { Name = "Area", Value = "" });
                     lst.Add(new Parameters() { Name = "Branch", Value = "" });
                     reportOptionDto.Parameters = lst;*/

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }


        public ActionResult GUI_002()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_002";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_002.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";
            criterialDto = GetCriteriaAsRequestForm();
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI002(criterialDto);

                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;

                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                List<Parameters> lst = new List<Parameters>();
                lst.Add(new Parameters() { Name = "Month", Value = Request.Form["txtFromMonth"] });
                lst.Add(new Parameters() { Name = "ShowDetail", Value = "no" });
                reportOptionDto.Parameters = lst;

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_003()
        {
            criterialDto = GetCriteriaAsRequestForm();
            InitViewBag();
            return View(criterialDto);
        }
        public ActionResult GUI_004()
        {

            criterialDto = GetCriteriaAsRequestForm();

            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_004";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_004.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI004(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                List<Parameters> lst = new List<Parameters>();
                lst.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                lst.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });

                reportOptionDto.Parameters = lst;

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }
            ViewBag.Data = dtForScreen;
            InitViewBag();
            return View(criterialDto);

         

        }
        public ActionResult GUI_005()
        {
            criterialDto = GetCriteriaAsRequestForm();
            InitViewBag();
            return View(criterialDto);
        }

        public ActionResult GUI_006()
        {
            criterialDto = GetCriteriaAsRequestForm();

            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_006";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_006.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI006(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = "GUI_006_D";//
                    dsForReport.Tables[1].TableName = reportOptionDto.ReportName;

                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                //List<Parameters> lst = new List<Parameters>();
                //lst.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                //lst.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });

                //reportOptionDto.Parameters = lst;

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }
            ViewBag.Data = dtForScreen;
            InitViewBag();
            return View(criterialDto);
        }
        public ActionResult GUI_007()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_007";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_007.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";
            criterialDto = GetCriteriaAsRequestForm();
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI007(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                List<Parameters> lst = new List<Parameters>();
                lst.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                lst.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });
                reportOptionDto.Parameters = lst;

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_008()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_008";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_008.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            criterialDto = GetCriteriaAsRequestForm();
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI008(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                List<Parameters> lst = new List<Parameters>();
                lst.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                lst.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });
                lst.Add(new Parameters() { Name = "Area", Value = "" });
                lst.Add(new Parameters() { Name = "Branch", Value = "" });
                reportOptionDto.Parameters = lst;

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_009()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_009";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_009.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";
            criterialDto = GetCriteriaAsRequestForm();
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI009(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                List<Parameters> lst = new List<Parameters>();
                lst.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                lst.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });
                lst.Add(new Parameters() { Name = "Area", Value = "" });
                lst.Add(new Parameters() { Name = "Branch", Value = "" });
                reportOptionDto.Parameters = lst;

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_010()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;

            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_010";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_010.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";
            criterialDto = GetCriteriaAsRequestForm();
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI010(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                List<Parameters> lst = new List<Parameters>();
                lst.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                lst.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });
                lst.Add(new Parameters() { Name = "Area", Value = "" });
                lst.Add(new Parameters() { Name = "Branch", Value = "" });
                reportOptionDto.Parameters = lst;


                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_011()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_011";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_011.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            criterialDto = GetCriteriaAsRequestForm();
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI011(criterialDto);

                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;

                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }

                reportOptionDto.Parameters = new List<Parameters>();
                reportOptionDto.Parameters.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_012()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_012";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_012.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            criterialDto = GetCriteriaAsRequestForm();
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI012(criterialDto);

                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;

                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }

                reportOptionDto.Parameters = new List<Parameters>();
                reportOptionDto.Parameters.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_013()
        {
            manager = new ReportManager();
            criterialDto = GetCriteriaAsRequestForm();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_013";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_013.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            criterialDto = GetCriteriaAsRequestForm();
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI013(criterialDto);

                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;

                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                /*
                reportOptionDto.Parameters = new List<Parameters>();
                reportOptionDto.Parameters.Add(new Parameters() { Name = "FromMonth", Value = "xx/xx" });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "ToMonth", Value = "xx/xx" });
                */
                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_014()
        {
            criterialDto = GetCriteriaAsRequestForm();
            InitViewBag();
            return View(criterialDto);
        }
        public ActionResult GUI_015()
        {
            manager = new ReportManager();
            criterialDto = GetCriteriaAsRequestForm();            
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_015";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_015.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            criterialDto = GetCriteriaAsRequestForm();

            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI015(criterialDto);

                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;

                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }

                reportOptionDto.Parameters = new List<Parameters>();
                reportOptionDto.Parameters.Add(new Parameters() { Name = "FromMonth", Value = "xx/xx" });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "ToMonth", Value = "xx/xx" });

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }

        public DataSet GetData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Table1");
            DataRow dr = null;
            dt.Columns.Add("PwaIncidentNo");
            dr = dt.NewRow();
            dr["PwaIncidentNo"] = "S0000001";
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            return ds;

        }
        public ActionResult GUI_016()
        {

            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_016";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_016.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            criterialDto = GetCriteriaAsRequestForm();
        
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI016(criterialDto);

                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;

                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }

                reportOptionDto.Parameters = new List<Parameters>();
                reportOptionDto.Parameters.Add(new Parameters() { Name = "FromMonth", Value = "xx/xx" });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "ToMonth", Value = "xx/xx" });

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_017()
        {
            criterialDto = GetCriteriaAsRequestForm();
            InitViewBag();
            return View(criterialDto);
        }
        public ActionResult GUI_018()
        {
            manager = new ReportManager();
            criterialDto = GetCriteriaAsRequestForm();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_018";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_018.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            criterialDto = GetCriteriaAsRequestForm();
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI018(criterialDto);

                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;

                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                reportOptionDto.Parameters = new List<Parameters>();
                reportOptionDto.Parameters.Add(new Parameters() { Name = "Area", Value = "" });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "FromMonth", Value = "xx/xx" });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "ToMonth", Value = "xx/xx" });
                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_019()
        {
            manager = new ReportManager();
            criterialDto = GetCriteriaAsRequestForm();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_019";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_019.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            criterialDto = GetCriteriaAsRequestForm();
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI019(criterialDto);

                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;

                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                
                reportOptionDto.Parameters = new List<Parameters>();
                reportOptionDto.Parameters.Add(new Parameters() { Name = "Area", Value = "" });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "FromMonth", Value = "xx/xx" });
                reportOptionDto.Parameters.Add(new Parameters() { Name = "ToMonth", Value = "xx/xx" });
                
                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_020()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_020";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_020.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            criterialDto = GetCriteriaAsRequestForm();
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI020(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }
                List<Parameters> lst = new List<Parameters>();
                lst.Add(new Parameters() { Name = "FromMonth", Value = Request.Form["txtFromMonth"] });
                lst.Add(new Parameters() { Name = "ToMonth", Value = Request.Form["txtToMonth"] });
                lst.Add(new Parameters() { Name = "Area", Value = "" });
                lst.Add(new Parameters() { Name = "Branch", Value = "" });
                reportOptionDto.Parameters = lst;

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }
        public ActionResult GUI_021()
        {
            manager = new ReportManager();
            criterialDto = new CriterialDto();
            criterialDto.CreatedBy = CurrentUser.CreatedBy;
            reportOptionDto = new ReportOptionDto();
            reportOptionDto.ReportName = "GUI_021";
            reportOptionDto.ReportFile = "~/ReportFiles/GUI_021.rpt";
            reportOptionDto.ExportType = ExportType.PDF;
            mode = Request.Form["Action"] != null ? Request.Form["Action"] : "search";

            criterialDto = GetCriteriaAsRequestForm();
            /// For Search
            if (mode == "search")
            {
                if (Session["ReportOption"] != null)
                {
                    Session.Remove("ReportOption");
                }
                dsForReport = manager.GetGUI021(criterialDto);
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (dsForReport != null && dsForReport.Tables.Count > 0)
                {
                    dsForReport.Tables[0].TableName = reportOptionDto.ReportName;
                }
                reportOptionDto.DataSource = dsForReport;
                Session.Add("ReportOption", reportOptionDto);
            }
            /// For Export
            else if (mode == "export" && Session["ReportOption"] != null)
            {
                reportOptionDto = (ReportOptionDto)Session["ReportOption"];
                dsForReport = reportOptionDto.DataSource;
                dtForScreen = dsForReport != null ? dsForReport.Tables[0] : null;
                if (Request.Form["ReportType"].ToLower() == "excel")
                {
                    reportOptionDto.ExportType = ExportType.Excel;
                }
                else if (Request.Form["ReportType"].ToLower() == "pdf")
                {
                    reportOptionDto.ExportType = ExportType.PDF;
                }

                if (Request.Form["ReportType"] != null)
                {
                    Export(reportOptionDto);
                }
            }

            InitViewBag();
            ViewBag.Data = dtForScreen;
            return View(criterialDto);
        }

        public ActionResult GUI_022()
        {
            criterialDto = GetCriteriaAsRequestForm();
            InitViewBag();
            return View(criterialDto);
        }

        public ActionResult GUI_023()
        {
            criterialDto = GetCriteriaAsRequestForm();
            InitViewBag();
            return View(criterialDto);
        }

        public ActionResult GUI_024()
        {
            criterialDto = GetCriteriaAsRequestForm();
            InitViewBag();
            return View(criterialDto);
        }

        public ActionResult GUI_025()
        {
            criterialDto = GetCriteriaAsRequestForm();
            InitViewBag();
            return View(criterialDto);
        }


        #region Public Function
        public void InitViewBag()
        {
            var areaTypeCode = new string[] { "4"};
            List<DropDownlistDto> ddlBranch= GetDropDownList("SysBranch");
            List<DropDownlistDto> ddlArea = GetDropDownList("SysArea");
            ddlBranch = ddlBranch.FindAll(o=> areaTypeCode.Contains(o.TypeCode));

            if (CurrentUser.Ba != "1001" && CurrentUser.Ba != "")
            {
                ddlBranch = ddlBranch.FindAll(o => o.Value == CurrentUser.Ba);
                ddlArea = ddlArea.FindAll(o => o.Id == CurrentUser.Area);

            }
            ViewBag.SysArea = ddlArea;
            ViewBag.SysBranch = ddlBranch;//GetDropDownBranchList(CurrentUser.Ba);
            ViewBag.SysRequestType = GetDropDownList("SysRequestType");
            ViewBag.SysRequestCategorys = GetDropDownList("SysRequestCategory");
            ViewBag.SysRequestCategorySubjects = GetDropDownList("SysRequestCategorySubject");
            ViewBag.SysChannelCates = GetDropDownList("SysChannelCategory");
        }
        public CriterialDto GetCriteriaAsRequestForm()
        {
            CriterialDto criterialDto = new CriterialDto();
            criterialDto.AreaId = Request.Form["ddlAreaSearch"] != null ? Request.Form["ddlAreaSearch"] : "";
            if (criterialDto.AreaId == "")
            {
                criterialDto.AreaId = CurrentUser.AreaBaCode;
            }

            criterialDto.BranchId = Request.Form["ddlBranchSearch"] != null ? Request.Form["ddlBranchSearch"] : "";
           if (criterialDto.BranchId=="")
            {
                criterialDto.BranchId= CurrentUser.Ba;
            }

            criterialDto.DisplayData = Request.Form["ddlDisplayData"] != null ? Request.Form["ddlDisplayData"] : "";

            criterialDto.FromDateTH = Request.Form["txtFromDate"] != null ? Request.Form["txtFromDate"] : "";
            criterialDto.ToDateTH = Request.Form["txtToDate"] != null ? Request.Form["txtToDate"] : "";

            criterialDto.FromMonthYearTH = Request.Form["txtFromMonth"] != null ? Request.Form["txtFromMonth"] : "";
            criterialDto.ToMonthYearTH = Request.Form["txtToMonth"] != null ? Request.Form["txtToMonth"] : "";
            criterialDto.IsDay = Request.Form["rdDay"] != null && Request.Form["rdDay"] == "on" ? "1" : "0";
            criterialDto.IsMonth = Request.Form["rdMonth"] != null && Request.Form["rdMonth"] == "on" ? "1" : "0";
            criterialDto.IsQuarter = Request.Form["rdQuarter"] != null && Request.Form["rdQuarter"] =="on"? "1" : "0";
            criterialDto.PeriodType = Request.Form["rdPreiodType"] != null  ? Request.Form["rdPreiodType"] .ToString() : "0";
            
    

            criterialDto.Quarter = Request.Form["ddlQuater"] != null ? Request.Form["ddlQuater"] : "0";
            criterialDto.RequestType = Request.Form["ddlRequestType"] != null ? Request.Form["ddlRequestType"] : "";
            criterialDto.RequestCategory = Request.Form["ddlRequestCategory"] != null ? Request.Form["ddlRequestCategory"] : "";
            criterialDto.RequestCategorySubject = Request.Form["ddlRequestCategorySubject"] != null ? Request.Form["ddlRequestCategorySubject"] : "";

            criterialDto.ChannelType = Request.Form["ddlChannel"] != null ? Request.Form["ddlChannel"] : "";
            criterialDto.Year = Request.Form["txtYear"] != null ? Request.Form["txtYear"] : "";

            return criterialDto;
        }

        public void Export()
        {

            ReportOptionDto reportOption = (ReportOptionDto)Session["ReportOption"];

            reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath(reportOption.ReportFile));






            reportDoc.DataSourceConnections.Clear();

            reportDoc.Refresh();

            reportDoc.SetDataSource(reportOption.DataSource);
            if (reportOption != null && reportOption.Parameters != null)
            {
                foreach (Parameters parameters in reportOption.Parameters)
                {
                    reportDoc.SetParameterValue(parameters.Name, parameters.Value);
                }
            }

            if (reportOption.ExportType == ExportType.PDF)
            {
                reportDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportOption.ReportName);
            }
            else if (reportOption.ExportType == ExportType.Excel)
            {
                reportDoc.ExportToHttpResponse(ExportFormatType.Excel, System.Web.HttpContext.Current.Response, true, reportOption.ReportName);
            }


        }
        public void Export(ReportOptionDto reportOption)
        {

            reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath(reportOption.ReportFile));

            if (reportOption != null && reportOption.DataSource.Tables.Count > 0)
            {
                for (int i = 0; i < reportOption.DataSource.Tables.Count; i++)
                {
                    reportDoc.Database.Tables[i].SetDataSource(reportOption.DataSource.Tables[i]);
                }
            }

            if (reportOption != null && reportOption.Parameters != null)
            {
                foreach (Parameters parameters in reportOption.Parameters)
                {
                    reportDoc.ParameterFields[parameters.Name].CurrentValues.AddValue(parameters.Value);
                    //reportDoc.SetParameterValue(parameters.Name, parameters.Value);
                }
            }

            if (reportOption.ExportType == ExportType.PDF)
            {
                reportDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, reportOption.ReportName);
            }
            else if (reportOption.ExportType == ExportType.Excel)
            {
                reportDoc.ExportToHttpResponse(ExportFormatType.Excel, System.Web.HttpContext.Current.Response, true, reportOption.ReportName);
            }


        }
        #endregion
    }
}
