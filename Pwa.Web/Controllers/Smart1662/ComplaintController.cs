using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Bal.Smart1662;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto;
using System.Web.Script.Serialization;
using System.IO;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Bal.Incident;
using Pwa.FrameWork.Dto.Smart1662.Incident;
namespace Pwa.Web.Controllers.Smart1662
{
    public class ComplaintController : BaseController
    {

        string jobFileName = "JobNo_photo_{Number}";
        string processfileName = "processfile", closefileName= "closefile";
        // GET: Complaint
        public ActionResult ComplaintView()
        {
            try
            {
                var SysBranchs = GetDropDownBranchList(CurrentUser.Ba); //GetDropDownBranchList(CurrentUser.Ba); //GetDropDownList("SysBranch");
                var SysChannels = GetDropDownList("SysChannel");
                var incidentGroupRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetIncidentGroupRespository();
                var informChannelRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetInformChannelRespository();
                var customerTypeRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetCustomerTypeRespository();

                Pwa.FrameWork.Bal.Smart1662.IncidentManager incident = new FrameWork.Bal.Smart1662.IncidentManager();
                var ts = incident.GetRequestTypes();

                var incidentGroups = incidentGroupRespository.GetPwaIncidentGroups().Select(group => new SelectListItem
                {
                    Text = group.Name,
                    Value = group.IncidentGroupID.ToString()
                }).ToList();

                var informChannels = informChannelRespository.GetInformChannels().Select(channel => new SelectListItem
                {
                    Text = channel.Name,
                    Value = channel.InformChannelID.ToString()
                }).ToList();

                var incidentTypes = customerTypeRespository.GetPwaIncidentTypes().Select(type => new SelectListItem
                {
                    Text = type.Name,
                    Value = type.IncidentTypeID.ToString()
                }).ToList();



                Pwa.FrameWork.Bal.Smart1662.IncidentManager managerInc = new FrameWork.Bal.Smart1662.IncidentManager();
                var incidents = managerInc.GetIncidents(new IncidentDto()).OrderByDescending(o => o.CreatedDate).ToList();
        
                ViewBag.sysRequestTypes = GetDropDownList("SysRequestType");
                ViewBag.SysRequestCategorys = GetDropDownList("SysRequestCategory");
                ViewBag.SysRequestCategorySubjects = GetDropDownList("SysRequestCategorySubject");
                ViewBag.IncidentGroups = incidentGroups;
                ViewBag.InformChannels = informChannels;
                ViewBag.IncidentTypes = incidentTypes;
                ViewBag.IncidentList = incidents;
                ViewBag.SysBranchs = SysBranchs;
                ViewBag.SysChannels = SysChannels;
                ViewBag.CurrentUser = CurrentUser;
            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Search()
        {
            var incidentNo = Request["txtIncidentNo"];
            var subject = Request["txtIncidentHeader"];
            var informer = Request["txtInformer"];
            var receiver = Request["txtReciever"];
            var startDate = Request["txtReceiveDateStart"];
            var endDate = Request["txtReceiveDateEnd"];
            var status = Request["ddlStatus"];

            Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();
            var incidents = manager.SearchIncidents(incidentNo, subject, informer, receiver, startDate, endDate, status);

            ViewData["GetData"] = incidents;

            return View("ComplaintView",new {

                IncidentNo = incidentNo,
                Subject = subject,
                Informer = informer,
                Receiver = receiver,
                StartDate = startDate,
                EndDate = endDate,
                Status = status
            });
        }

        public ActionResult RepaireView()
        {
            PwaRepaireWorkManager manager = new PwaRepaireWorkManager();
            var workingList= manager.GetAll();
          
            foreach (var item in workingList)
            {
                item.WorkingDate = Converting.DateOfdd_MM_yyyyTH(item.WorkingDate);
            }
            ViewData["GetData"] = workingList;
            return View();
        }
        public ActionResult AddRepaire(string id)
        {
            PwaRepaireWorkManager manager = new PwaRepaireWorkManager();
            SysItemManager itemManager = new SysItemManager();
            PwaRepaireWorkHeaderDto header = null;
            List<IncidentDto> incidents = null;
            var sysRequestTypes = GetDropDownList("SysRequestType");
            var SysRequestCategorys = GetDropDownList("SysRequestCategory");
            var SysRequestCategorySubjects = GetDropDownList("SysRequestCategorySubject");
            var SysItems = GetDropDownList("SysItem");
            var SysUnit = GetDropDownList("SysUnit");
            var SysBranch = GetDropDownBranchList(CurrentUser.Ba);  //GetDropDownList("SysBranch");
            var Employees = GetDropDownList("Employee");
            var SysGISPIPETYPE = GetDropDownList("GISPIPETYPE");
            var SysPipelineSize = GetDropDownList("PipelineSize");
            var SysSurface = GetDropDownList("SysSurface");
            var SysItemSetList = itemManager.GetSysItemSetAll(new SysItem_SetDto() { BACode=CurrentUser.Ba});
            Pwa.FrameWork.Bal.Smart1662.IncidentManager managerInc = new FrameWork.Bal.Smart1662.IncidentManager();
            if(Request.Form["hddSelectedInc"] !=null && Request.Form["hddSelectedInc"].Length>0)
            {
                incidents = managerInc.GetIncidents(Request.Form["hddSelectedInc"].Split(','),"1");

            }
            
        //    var incidents = managerInc.GetIncidents().OrderByDescending(o => o.CreatedDate).Take(3).ToList();

            ViewBag.SysItems = SysItems;
            ViewBag.SysUnit = SysUnit;
            ViewBag.SysRequestType = GetDropDownList("SysRequestType"); ;
            ViewBag.Reasons = GetDropDownList("SysReason");
            ViewBag.SysRequestCategorys = SysRequestCategorys;
            ViewBag.SysRequestCategorySubjects = SysRequestCategorySubjects;
            ViewBag.SysBranch = SysBranch;
            ViewBag.Employees = Employees;
            ViewBag.GetIncidents = incidents;
            ViewBag.SysItem_Set = SysItemSetList;
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.SysGISPIPETYPE = SysGISPIPETYPE;
            ViewBag.SysPipelineSize = SysPipelineSize;
            ViewBag.SysSurface = SysSurface;
            ViewBag.SysTeam = GetDropDownList("SysTeam").FindAll(o=>o.BACode==CurrentUser.Ba);
            ViewBag.SysDocumentType = GetDropDownList("SysDocumentType"); ;

            //  var SysGISPIPETYPE = GetDropDownList("GISPIPETYPE");
            //  var SysPipelineSize = GetDropDownList("PipelineSize");

            if (id != null)
            {
                header = manager.GetById(id);
                header.WorkingDate = Converting.DateOfdd_MM_yyyyTH(header.WorkingDate);
                header.NotificationDate = Converting.DateOfdd_MM_yyyyTH(header.NotificationDate);
                ViewBag.GetIncidents = header.Incidents;
                if (header.Survey != null)
                {
                    header.Survey.SurveyDate = Converting.DateOfdd_MM_yyyyTH(header.Survey.SurveyDate);
                }

                if (header.OpenCase != null)
                {
                    header.OpenCase.OpenDate = Converting.DateOfdd_MM_yyyyTH(header.OpenCase.OpenDate);
                    header.OpenCase.SurveyDate = Converting.DateOfdd_MM_yyyyTH(header.OpenCase.SurveyDate);

                }
                if (header.Process != null)
                {
                    header.Process.FromProcessDate = Converting.DateOfdd_MM_yyyyTH(header.Process.FromProcessDate);
                    header.Process.ToProcessDate = Converting.DateOfdd_MM_yyyyTH(header.Process.ToProcessDate);
                    header.Process.SurfaceFixedDate = Converting.DateOfdd_MM_yyyyTH(header.Process.SurfaceFixedDate);

                }
                if (header.CloseJob != null)
                {
                    header.CloseJob.CloseDate = Converting.DateOfdd_MM_yyyyTH(header.CloseJob.CloseDate);
                }
            }
           
            return View(header);
        }
        public JsonResult SaveRepaireWork()
        {
            var deserializer = new JavaScriptSerializer();
            PwaRepaireWorkManager manager = new PwaRepaireWorkManager();
            Response.StatusCode = 200;
            PwaRepaireWorkHeaderDto header = null;
            List<FileDto> ProcessfileUploads = new List<FileDto>();
            List<FileDto> ClosefileUploads = new List<FileDto>();
            try
            {

                header=  deserializer.Deserialize<PwaRepaireWorkHeaderDto>(context.Request.Form["Obj"]);

              //  header.Status = (Converting.ToInt(header.Status) + 1).ToString();

                if (Converting.ToInt(header.Status) > 10)
                {
                    header.Status = "10";
                }
            //    GeojsonFeatureDto geoData = (GeojsonFeatureDto)new JavaScriptSerializer().DeserializeObject(header.Survey.ShapeText);
                int number = 1;
               
                if (header.Effect != null && header.Effect.Shape != null)
                {
                   

                   header.Effect.ShapeText = Converting.ToMultiShapeText(header.Effect.Shape.geometry.type, header.Effect.Shape.geometry.coordinates);
                 //   header.Survey.ShapeText = string.Format("{0} {1}", header.Survey.Shape.geometry.type, coordinate.Replace("[", "(").Replace("]",")"));
                }
                if (header.Effect != null && header.Effect.MapImageEffectBase64 != null && header.Effect.MapImageEffectBase64 != "")
                {
                    var bytes = Convert.FromBase64String(header.Effect.MapImageEffectBase64);
                    var path = EffectPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                         .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                         .Replace("{JobNo}", header.RWCode);
                    var virtualPath = VirtualPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                             .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                             .Replace("{JobNo}", header.RWCode);
                    if (FileMng.HaveDirectory(path))
                    {

                        using (var imageFile = new FileStream(path, FileMode.Create))
                        {
                            imageFile.Write(bytes, 0, bytes.Length);
                            imageFile.Flush();
                        }
                    }
                }
                if (header.DeleteProcessFiles != null)
                {
                    foreach (FileDto deleteFile in header.DeleteProcessFiles)
                    {
                        if (deleteFile.HtmlFile != null && deleteFile.HtmlFile != "" && deleteFile.HtmlFile.IndexOf("icon_file")==-1)
                        {
                            var filePath = HttpContext.Server.MapPath(deleteFile.HtmlFile.Split('?')[0]);
                            FileMng.DeleteFile(filePath);
                        }

                    }
                }


                if (header.DeleteCloseFiles != null)
                {
                    foreach (FileDto deleteFile in header.DeleteCloseFiles)
                    {
                        if (deleteFile.HtmlFile != null && deleteFile.HtmlFile != "")
                        {
                            var filePath = HttpContext.Server.MapPath(deleteFile.HtmlFile.Split('?')[0]);
                            FileMng.DeleteFile(filePath);
                        }

                    }
                }

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    var extension = Path.GetExtension(file.FileName);
                    string path = "";
                    string virtualPath = "";

                    if (fileName.IndexOf(processfileName) >-1)
                    {
                        path = JobPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                                .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                                .Replace("{JobNo}", header.RWCode);
                        virtualPath = VirtualPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                                .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                                .Replace("{JobNo}", header.RWCode);

                        number = Converting.ToInt(fileName.Replace(processfileName, ""));
                    }
                    else if (fileName.IndexOf(closefileName) > -1)
                    {
                        path = CloseJobPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                              .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                              .Replace("{JobNo}", header.RWCode);
                        virtualPath = VirtualCloseJobPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                                .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                                .Replace("{JobNo}", header.RWCode);

                        number = Converting.ToInt(fileName.Replace(closefileName, ""));
                    }
                    if (FileMng.HaveDirectory(path))
                    {
                       
                        file.SaveAs(string.Format("{0}{1}{2}",path, jobFileName.Replace("{Number}",number.ToString()), extension));
                        if (fileName.IndexOf(processfileName) > -1)
                        {

                            ProcessfileUploads.Add(new FileDto()
                            {
                                FileID = header.RWId,
                                FilePath = path,
                                No = number.ToString(),
                                FullPath = string.Format("{0}{1}{2}", path, jobFileName.Replace("{Number}", number.ToString()), extension),
                                HtmlFile = string.Format("{0}{1}{2}", virtualPath, jobFileName.Replace("{Number}", number.ToString()), extension),
                                FileName = jobFileName.Replace("{Number}", number.ToString()),
                                FileSize = file.ContentLength.ToString(),
                                UploadDate = string.Format("{0}{1}{2}", DateTime.Now.Year.ToString()
                            , DateTime.Now.Month.ToString("##00")
                            , DateTime.Now.Day.ToString("##00"))
                            });
                        }
                        else if (fileName.IndexOf(closefileName) > -1)
                        {
                            ClosefileUploads.Add(new FileDto()
                            {
                                FileID = header.RWId,
                                FilePath = path,
                                No = number.ToString(),
                                FullPath = string.Format("{0}{1}{2}", path, jobFileName.Replace("{Number}", number.ToString()), extension),
                                HtmlFile = string.Format("{0}{1}{2}", virtualPath, jobFileName.Replace("{Number}", number.ToString()), extension),
                                FileName = jobFileName.Replace("{Number}", number.ToString()),
                                FileSize = file.ContentLength.ToString(),
                                UploadDate = string.Format("{0}{1}{2}", DateTime.Now.Year.ToString()
                          , DateTime.Now.Month.ToString("##00")
                          , DateTime.Now.Day.ToString("##00"))
                            });
                        }

                        }

                }
                if(header.Process!=null)
                header.Process.Files = ProcessfileUploads;

                if (header.CloseJob != null)
                    header.CloseJob.Files = ClosefileUploads;

                header.CreatorEmail = CurrentUser.AccountEmail;

                    return Json(manager.Insert(header));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                Log("SaveRepaireWork", ex.Message);
                return Json(new ErrorDto() { Page = "SaveRepaireWork", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        public JsonResult GetData(IncidentDto searchData)
        {
            Pwa.FrameWork.Bal.Smart1662.IncidentManager managerInc = new FrameWork.Bal.Smart1662.IncidentManager();
        //    Debug.WriteLine("CurrentUser.Ba  " + CurrentUser.Ba);
            //if (CurrentUser.Ba != null || searchData.BranchNo != null) {
            if (searchData.bUserCurrentBranchNo != null)
            {
                if (searchData.bUserCurrentBranchNo == "BRANCH")
                {
                    searchData.BranchNo = CurrentUser.Ba;
                }
            }
            else if (searchData.BranchNo != null)
            {
                searchData.BranchNo = searchData.BranchNo;
            }

            if (searchData.ReceivedCaseDateText != null)
            {
                searchData.ReceivedCaseDateText = Converting.DateOfyyyyMMdd(searchData.ReceivedCaseDateText);
            }
            if (searchData.CompletedCaseDateText != null)
            {
                searchData.CompletedCaseDateText = Converting.DateOfyyyyMMdd(searchData.CompletedCaseDateText);
            }
            /*else 
            {
                searchData.BranchNo = CurrentUser.Ba;
            }*/
            //}

            //  Debug.WriteLine("searchData.BranchNo  " + searchData.BranchNo);
           
            var incidents = managerInc.GetIncidentsByBranch(searchData).OrderByDescending(o => o.CreatedDate).ToList(); 
            foreach (IncidentDto vData in incidents)
            {
                vData.CreatedDateText = Converting.DateOfdd_MM_yyyyTH(vData.CreatedDate);
                vData.CompletedCaseDateText = (vData.CompletedCaseDate.HasValue ? Converting.DateOfdd_MM_yyyyTH(vData.CompletedCaseDate.Value) : "");
                vData.ReceivedCaseDateText = Converting.DateOfdd_MM_yyyyTH(vData.ReceivedCaseDate);
                vData.CaseDetail = vData.CaseDetail == null ? "" : vData.CaseDetail;
            }
            return Json(incidents);
        }
    }
}