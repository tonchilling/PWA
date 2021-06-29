using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Pwa.FrameWork.Bal.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.IO;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.parser;
using iTextSharp.text.html;
using System.Text;

namespace Pwa.Web.Controllers.Smart1662
{
    public class PdfController : BaseController
    {
        BaseFont f_cb = null;
        BaseFont f_cn = null;
        public ActionResult Index()
        {
            return View();
        }
        #region open Repair
        public FileResult PDFOpenRepair(string id)
        {
            //var css = " body { font-family: 'TH Sarabun New'; font-size: 14pt; width: 1024px; padding-left: 20px; padding-right: 20px; padding-top: 20px; margin-top: 20px; background-color: white; line-height: 30px; /* Width 684 */ } table { font-size: 14pt; width: 100%; line-height: 25px; margin-bottom: 10px; border-color: white; border-bottom-style: solid; background-color: white; } table tr td { line-height: 25px; } .underline { border-bottom-style: solid; border-color: #b0b0b0; text-align: center; border-bottom: 0.5px; } .nonunderline { border: 0 0 1px 0; border-bottom-style: solid; border-color: blue; text-align: center; }";

            using (MemoryStream stream = new MemoryStream())
            {
                String html = string.Empty;
                html = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/html/" + "OpenRepair.html"));
                html = html.Replace("{Logo}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                html = html.Replace("{C1}", HttpContext.Server.MapPath("~/html/img/checkbox_1.gif"));
                html = html.Replace("{C0}", HttpContext.Server.MapPath("~/html/img/checkbox_0.gif"));
                FormManager fm = new FormManager();
                OpenRepairDto opr = new OpenRepairDto();
                opr = fm.getOpenRepair(id);
                string dateTh = Converting.DateOfdd_MM_yyyyToThLongDate(opr.CreatedDate);
                html = html.Replace("{OpenRepairDate}", dateTh);
                html = html.Replace("{OpenRepairTime}", opr.CreatedTime);
                html = html.Replace("{InformalFullName}", "คุณ " + opr.FirstName + " " + opr.LastName); 
                html = html.Replace("{CustCode}", opr.CustCode);
                html = html.Replace("{Tel}", opr.Telephone);
                
                html = html.Replace("{Conpany}", "-"); 
                html = html.Replace("{ShopStore}", "-");

                html = html.Replace("{HomeNo}", opr.Address);
                html = html.Replace("{Moo}", "");
                html = html.Replace("{Soi}", "");
                html = html.Replace("{Village}", "");
                html = html.Replace("{Street}", "");

                html = html.Replace("{SubDistrict}", opr.SubDistrict);
                html = html.Replace("{District}", opr.District);
                html = html.Replace("{Province}", opr.Province);

                html = html.Replace("{Landmark}", "");
                html = html.Replace("{Type}", opr.PiplineType);
                html = html.Replace("{Size}", opr.PiplineSize);
                html = html.Replace("{LandmarkBroken}", opr.BrokenAppearance);

                StringReader sr = new StringReader(html);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                string fontPath = HttpContext.Server.MapPath("~/fonts/THSarabunNew.ttf");

                var EnCodefont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font bFont = new Font(EnCodefont, 18, Font.NORMAL);

                XMLWorkerFontProvider fontProvider = new XMLWorkerFontProvider(XMLWorkerFontProvider.DONTLOOKFORFONTS);
                fontProvider.Register(fontPath);
                FontFactory.Register(fontPath);

                pdfDoc.Open();
                using (var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
                {
                    //Get a stream of our CSS
                    //using (var cssStream = new MemoryStream(Encoding.UTF8.GetBytes(css)))
                    //{
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, htmlStream, null, Encoding.UTF8, fontProvider);
                    //}
                }
                //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "OpenRepair_"+ opr.RWId +".pdf");
            }
        }

        #endregion
        #region Requisition
        public FileResult PDFRequisition(string id)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                String html = string.Empty;
                html = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/html/" + "Requisition.html"));
                html = html.Replace("{Logo}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                html = html.Replace("{C1}", HttpContext.Server.MapPath("~/html/img/checkbox_1.gif"));
                html = html.Replace("{C0}", HttpContext.Server.MapPath("~/html/img/checkbox_0.gif"));
                FormManager fm = new FormManager();
                RequisitionDto req = new RequisitionDto();
                RequisitionDto opr = new RequisitionDto();
                opr.PwaRepaireWork_ItemDto = new List<PwaRepaireWork_ItemDto>();
                req = fm.getRequisition(id);
                opr = (req==null) ? opr: req;
                
                //string dateToday = DateTime.Now.Day.ToString("##00") + "/" + DateTime.Now.Month.ToString("##00") + "/" + DateTime.Now.Year;
                //string dateTh = Converting.DateOfdd_MM_yyyyToThLongDate(dateToday);
                string dateTh = Converting.DateOfdd_MM_yyyyTH(DateTime.Now);
                html = html.Replace("{DocDate}", dateTh);


                html = html.Replace("{CostCenter}",  Convert.ToString(opr.CCTR_CODE));
                string BranchName = "";
                switch (opr.TypeCode)
                {
                    case "3": //Area
                        BranchName = "เขต" + opr.BaName;
                        break;
                    case "4": //Branch
                        BranchName = "สาขา" + opr.BaName;
                        break;
                    case "5": //Center
                        BranchName = "" + opr.BaName;
                        break;
                    default:
                        BranchName = "_________";
                        break;
                }
                html = html.Replace("{BranchName}", BranchName);


                string items = string.Empty;
                decimal totalItem = 0;
                decimal totalAll = 0;
                int row = 0;
                foreach (PwaRepaireWork_ItemDto item in opr.PwaRepaireWork_ItemDto)
                {
                    row++;
                    items += "<tr style=\"height:25px;\">";
                    items += "<td align=\"center\">" + item.No + "</td>";
                    items += "<td align=\"center\">" + item.ItemCode + "</td>";
                    items += "<td align=\"left\">" + item.ItemName + "</td>";
                    items += "<td align=\"center\">" + item.UnitName + "</td>";
                    items += "<td align=\"center\">" + item.Price + "</td>";
                    items += "<td align=\"center\">" + item.Qty + "</td>";
                    totalItem = Convert.ToDecimal(item.Qty) * Convert.ToDecimal(item.Price);
                    items += "<td align=\"center\">" + totalItem + "</td>";
                    items += "<td></td>";
                    items += "<td></td>";
                    items += "<td></td>";
                    items += "</tr>";
                    items += "";
                    totalAll += totalItem;
                }
                for (int i = row; i <= 8; i++)
                {
                    items += "<tr><td></td><td>&nbsp;</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>";
                }
                html = html.Replace("{items}", items);
                html = html.Replace("{total}", (totalAll==0?"": Convert.ToString(totalAll)));
                




                StringReader sr = new StringReader(html);
                Document pdfDoc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Requisition_" + opr.RWId + ".pdf");
            }
        }
        #endregion
        #region Close Repair
        public FileResult PDFCloseRepair(string id)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                String html = string.Empty;
                html = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/html/" + "CloseRepair.html"));
                html = html.Replace("{Logo}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                html = html.Replace("{C1}", HttpContext.Server.MapPath("~/html/img/checkbox_1.gif"));
                html = html.Replace("{C0}", HttpContext.Server.MapPath("~/html/img/checkbox_0.gif"));
                FormManager fm = new FormManager();
                CloseRepairDto req = new CloseRepairDto();
                CloseRepairDto opr = new CloseRepairDto();
                opr.PwaRepaireWork_ItemDto = new List<PwaRepaireWork_ItemDto>();
                req = fm.getCloseRepair(id);
                opr = (req == null) ? opr : req;

                //string RandomNo = DEncrypt.encrypt("S2020072600001", "1662" + "1234").Substring(0, 6);
                //string RandomNo2 = DEncrypt.encrypt("S2020072600006", "1662" + "1233").Substring(0, 6);
                //string RandomNo3 = DEncrypt.encrypt("S2020072600006", "1662123334234234234");

                string BranchName = string.Empty;
                switch (opr.TypeCode)
                {
                    case "3":
                        BranchName = "เขต" + opr.BaName;
                        break;
                    case "4":
                        BranchName = "สาขา" + opr.BaName;
                        break;
                    case "5":
                        BranchName = "" + opr.BaName;
                        break;
                    default:
                        BranchName = "________";
                        break;
                }
                html = html.Replace("{BranchName}", BranchName);
                //string dateTh = Converting.DateOfDateToThLongDate(DateTime.Now);
                //html = html.Replace("{NowDate}", dateTh);

                string dateCloseTh = Converting.DateOfyyyyMMddToThLongDate(opr.CloseDate);
                html = html.Replace("{CloseDate}", dateCloseTh);

                html = html.Replace("{CloseJobNumber}", opr.CloseJobNumber);

                string SurveydateTh = Converting.DateOfyyyyMMddToThLongDate(opr.SurfaceFixedDate);
                html = html.Replace("{SurveyDate}", SurveydateTh);

                html = html.Replace("{DMA}", "____________");

                html = html.Replace("{Surveyer}", opr.Surveyer); 
                html = html.Replace("{IncidentLocation}", opr.IncidentLocation);
                html = html.Replace("{BrokenAppearance_Survey}", opr.BrokenAppearance_Survey);
                html = html.Replace("{PipeTypeSize}", opr.PipelineType_Survey + " " + opr.PipelineSize_Survey + " มม. ");
                html = html.Replace("{RWCode}", opr.RWCode);
                html = html.Replace("{SurfaceAppearance_Survey}", opr.SurfaceAppearance_Survey);

                html = html.Replace("{BrokenAppearance_Process}", opr.BrokenAppearance_Process);
                html = html.Replace("{SurfaceAppearance_Process}", opr.SurfaceAppearance_Process);
                html = html.Replace("{Hole}", opr.HoleWidth + " x " + opr.HoleLength + " x " + opr.HoleDepth + " ม. ");
                html = html.Replace("{ToolType}", opr.ToolType);

                string ProcessdateFromTh = Converting.DateOfyyyyMMddToThShortDate(opr.FromProcessDate);
                string ProcessdateToTh = Converting.DateOfyyyyMMddToThShortDate(opr.ToProcessDate);
                string ProcessDateTime = ProcessdateFromTh + " " + opr.FromProcessTime + " น. - " + ProcessdateToTh + " " + opr.ToProcessTime + " น.";
                html = html.Replace("{ProcessDateTime}", ProcessDateTime);

                string SurfaceFixedDateTh = Converting.DateOfyyyyMMddToThShortDate(opr.SurfaceFixedDate);
                string SurfaceFixedDate = SurfaceFixedDateTh + " " + opr.SurfaceFixedTime + " น.";
                html = html.Replace("{SurfaceFixedDate}", SurfaceFixedDate);

                string items = string.Empty;
                decimal totalItem = 0;
                decimal totalAll = 0;
                foreach (PwaRepaireWork_ItemDto item in opr.PwaRepaireWork_ItemDto)
                {
                    items += "<tr>";
                    items += "<td>"+ item.ItemName + "</td>";
                    items += "<td align=\"center\">" + item.Qty + " " + item.UnitName + "</td>";
                    items += "<td align=\"center\">" + item.Price + "</td>";
                    //items += "<td align=\"center\">" + "" + "</td>";
                    totalItem = Convert.ToDecimal(item.Qty) * Convert.ToDecimal(item.Price);
                    items += "<td align=\"center\">" + totalItem + "</td>";
                    items += "</tr>";
                    items += "";
                    totalAll += totalItem;
                }
                html = html.Replace("{items}", items);
                //html = html.Replace("{total}", Convert.ToString(totalAll));
                html = html.Replace("{total}", (totalAll == 0 ? "" : Convert.ToString(totalAll)));
                Decimal Factor = 1;
                html = html.Replace("{Factor}", Convert.ToString(Factor));
                html = html.Replace("{FactorBaht}", Convert.ToString(Factor * totalAll));

                html = html.Replace("{Remark}", opr.Remark);

                html = html.Replace("{MapImg1}", HttpContext.Server.MapPath("~/html/img/map-img.png"));
                html = html.Replace("{MapImg2}", HttpContext.Server.MapPath("~/html/img/map-img.png"));
                int row = 1;
                foreach (FileCloseDto item in opr.Files)
                {
                    html = html.Replace("{Image"+ item .No+ "}", HttpContext.Server.MapPath(item.HtmlFile));
                }
             /*       html = html.Replace("{MapImg}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                html = html.Replace("{Image1}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                html = html.Replace("{Image2}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                html = html.Replace("{Image3}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                html = html.Replace("{Image4}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                html = html.Replace("{Image5}", HttpContext.Server.MapPath("~/html/img/logo.jpg"));
                */



                StringReader sr = new StringReader(html);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                //SET FONT
                string fontPath = HttpContext.Server.MapPath("~/fonts/THSarabunNew.ttf");
                var EnCodefont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font bFont = new Font(EnCodefont, 18, Font.NORMAL);
                XMLWorkerFontProvider fontProvider = new XMLWorkerFontProvider(XMLWorkerFontProvider.DONTLOOKFORFONTS);
                fontProvider.Register(fontPath);
                FontFactory.Register(fontPath);
                //SET FONT

                pdfDoc.Open();
                using (var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, htmlStream, null, Encoding.UTF8, fontProvider);
                }
                //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "CloseRepair_" + opr.RWId + ".pdf");
            }
        }
        #endregion


    }
}