using Pwa.FrameWork.Bal.Incident;
using Pwa.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Pwa.FrameWork.Extension;
using Pwa.FrameWork.Utilities;
 using System.IO;
using System.Web;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dao.EF.Smart1662;

using Pwa.FrameWork.Dao.ADO;
using System.Globalization;
using Pwa.FrameWork.Bal;
using System.Configuration;

namespace Pwa.Web.Controllers.Api
{
    public class IncidentController : ApiController
    {

        protected string IncPath = System.Configuration.ConfigurationSettings.AppSettings["IncPath"];
        protected string VirtualPath = System.Configuration.ConfigurationSettings.AppSettings["VirtualIncPath"];
        string incFileName = "{Timestamp}_{Number}";
        [HttpGet]
        public async Task<IHttpActionResult> GetincidentCases()
        {

            try
            {
                IncidentManager manager = new IncidentManager();
                var incs = await manager.SearchIncidentCase(null);

                return await Task.FromResult(Ok(new IncidentRespModel
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",
                    Incidents = incs.Select(inc => inc.CopyTo<IncidentModel>(new IncidentModel())).ToList()
                }));

            }
            catch (Exception ex)
            {
                //log exception to App log system and throw message to clinet


                return await Task.FromResult(Ok(new IncidentRespModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                }));
            }

        }


        public void Log(string methodName, string CreateBy, string Msg, Exception ex)
        {
            LogDAO logDao = new LogDAO();
            LogDTO logDto = new LogDTO();
            logDto.Project = "Smart1662";
            logDto.Page = methodName;
            logDto.CreatedBy = CreateBy;
            logDto.IssueDate = DateTime.Now.ToString();
            logDto.ValueField = Msg;
            logDto.Exception = ex.Message;
            logDao.Log(logDto);
        }
        [HttpPost]
        public async Task<IHttpActionResult> AddcidentCase([FromBody] IncidentModel model)
        {

            try
            {
                IncidentManager manager = new IncidentManager();

                //var incs = await manager.ReceiveIncidentCase()

                return await Task.FromResult(Ok(new IncidentRespModel
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",

                }));

            }
            catch (Exception ex)
            {
                //log exception to App log system and throw message to client


                return await Task.FromResult(Ok(new IncidentRespModel
                {
                    Success = true,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                }));
            }

        }
        [HttpPost]
        public IHttpActionResult AddIncident()
        {


            var locale = new System.Globalization.CultureInfo("en-US");
            List<FileDto> fileUploads = null;
            try
            {
                var Request = System.Web.HttpContext.Current.Request;
                //var file = System.Web.HttpContext.Current

                string json = Request.Form[0];
                var model = JsonHelper.Deserialize<AddIncidentModel>(json);

                var _incidentRespository = FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetPwaIncidentResponsitory();

                var data = new FrameWork.Bal.Data.Incident()
                {
                    //PwaIncidentID  = model.pw
                    //PwaIncidentNo = $"S{DateTime.Now.ToString("yyyyMMdd", locale)}00001",

                    PwaIncidentTypeID = model.PwaIncidentTypeID.ToString(),
                    PwaIncidentGroupID = model.PwaIncidentGroupID.ToString(),
                    CaseTitle = model.CaseTitle.ToString(),
                    CaseTitleDetail = model.CaseTitleDetail,
                    CaseDetail = model.CaseDetail,
                    ResolvedDetail = model.ResolvedDetail,
                    Sla = model.Sla,
                    SlaDetail = model.SlaDetail,

                    CaseLatitude = model.CaseLatitude,
                    CaseLongtitude = model.CaseLongtitude,

                    InformLatitude = model.CaseLatitude,
                    InformLongtitude = model.CaseLongtitude,

                    ProcessStage = Converting.ToInt(model.IncidentStatus),
                    CustomerProcessStage = Converting.ToInt(model.IncidentStatus),
                    PwaParentIncidentID = "0",
                    Status = 1,
                    CreatedDate = DateTime.Now,

                    ReceivedCaseDate = model.ReceivedCaseDate.ToDateTime(),
                    //CompletedCaseDate = DateTime.Now, //model.CompletedCaseDate.ToDateTime(),
                    PwsIncidentAddress = model.PwsIncidentAddress,
                    BracnchNo = model.BracnchNo != null ? model.BracnchNo : "",
                    Recorder = model.Recorder,
                    RecordDate = DateTime.Now, //(model.Recorder == null || model.Recorder == "") ? model.Recorder.ToDate() : DateTime.Now,

                    PwaInformReceiver = model.PwaInformReceiver,
                    PwaInformDate = (model.PwaInformDate == null || model.Recorder == "") ? DateTime.Now : DateTime.Now,
                    CreatedBy = model.User.AccountID,
                    CreatedBA = model.User.BA ?? "",
                    Reason = model.Reason,
                    CommentID = model.CommentID,

                    SysOwnerCode = SystemCode.Smart1662,


                    StartSLADate = (!model.Sla) ? (DateTime?)DateTime.Now : null, 
                    EndSLADate = (!model.Sla) ? (Converting.ToInt(model.IncidentStatus)==10) ? (DateTime?)DateTime.Now : null : null,
                };


                data.NearLocate = model.NearLocate;
                data.ProvinceID = model.ProvinceID;
                data.DistrictID = model.DistrictID;
                data.SubDistrictID = model.SubDistrictID;
                data.AddressNo = model.AddressNo;
                data.VillageNo = model.VillageNo;
                data.VillageBuilding = model.VillageBuilding;
                data.Soi = model.Soi;
                data.Road = model.Road;

                if (Converting.ToInt(model.IncidentStatus) == 10)
                {
                    data.CompletedCaseDate = DateTime.Now;
                }

                if (!model.Sla && Converting.ToInt(model.IncidentStatus) == 10)
                {
                    SlaManager slaManager = new SlaManager();
                    var calSla = slaManager.CalculateSla(data.Sla, data.ProcessStage, data.CaseTitle, data.StartSLADate, data.EndSLADate);
                    if (calSla != null)
                    {
                        data.SlaStatus = calSla.SLAStatus;
                        data.MinuteWorking = calSla.MinuteWorking;
                    }
                }
                

                List<Pwa.FrameWork.Dao.EF.Smart1662.PwaInformer> Informers = new List<PwaInformer>();
                Pwa.FrameWork.Dao.EF.Smart1662.PwaInformer infomer = null;
                foreach (AddInformer i in model.Informers)
                {
                    infomer = new PwaInformer();
                    infomer.InformerID = Converting.ToInt(i.InformID);
                    infomer.CustomerID = i.CustomerID != null ? i.CustomerID : "";
                    infomer.CustCode = i.CustCode != null ? i.CustCode : "";
                    infomer.Title = i.Title.ToString();
                    infomer.FirstName = i.FirstName != null ? i.FirstName : "";
                    infomer.LastName = i.LastName;
                    infomer.MeterNo = i.MeterNo;
                    infomer.Telephone = i.Telephone;
                    infomer.InformChannelID = i.InformChannelID != null ? i.InformChannelID.ToString() : "";
                    infomer.InformReference = i.InformReference;
                    infomer.CreatedDate = DateTime.Now;
                    infomer.CreatedBy = "1";
                    //infomer.ProvinceID = i.Province != null ? i.Province.ToString() : "";
                    //infomer.DistrictID = i.District != null ? i.District.ToString() : "";
                    //infomer.SubDistrictID = i.SubDistrict != null ? i.SubDistrict.ToString() : "";
                    infomer.Address = i.CustomerAddress;

                    //infomer.Address_no = i.AddressNo;
                    //infomer.Village_no = i.VillageNo;
                    //infomer.Building = i.VillageBuilding;
                    //infomer.Soi = i.Soi;
                    //infomer.Road = i.Road;

                    Informers.Add(infomer);
                }
                data.Imformers = Informers;


                Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();
                manager.Add(data);

                //  PwaIncident currentIncidentObj=   manager.GetLastIncident();
                int number = 1;
                fileUploads = new List<FileDto>();
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFile file = Request.Files[fileName];
                    var extension = Path.GetExtension(file.FileName);
                    // var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    var path = IncPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                             .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                             .Replace("{IncNo}", data.PwaIncidentNo.Trim());
                    var virtualPath = VirtualPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                             .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                             .Replace("{IncNo}", data.PwaIncidentNo.Trim());

                    if (FileMng.HaveDirectory(path))
                    {
                        incFileName = incFileName.Replace("{Timestamp}", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                        file.SaveAs(string.Format("{0}{1}{2}", path, incFileName.Replace("{Number}", number.ToString()), extension));
                        fileUploads.Add(new FileDto()
                        {
                            FileID = data.PwaIncidentID.ToString(),
                            PwaIncidentID = data.PwaIncidentID.ToString(),
                            FilePath = path,
                            No = number.ToString(),
                            FullPath = string.Format("{0}{1}{2}", path, incFileName.Replace("{Number}", number.ToString()), extension),
                            HtmlFile = string.Format("{0}{1}{2}", virtualPath, incFileName.Replace("{Number}", number.ToString()), extension),
                            FileName = incFileName.Replace("{Number}", number.ToString()),
                            FileSize = file.ContentLength.ToString(),
                            UploadDate = string.Format("{0}{1}{2}", DateTime.Now.Year.ToString()
                            , DateTime.Now.Month.ToString("##00")
                            , DateTime.Now.Day.ToString("##00"))
                        });

                    }
                    number = number + 1;
                }

                if (fileUploads != null)
                {



                    manager.AddFiles(fileUploads);
                }

                string detail = data.CaseDetail;
                Pwa.FrameWork.Bal.Smart1662.SysAccountManager account = new FrameWork.Bal.Smart1662.SysAccountManager();
                FrameWork.Dao.EF.Smart1662.sp_SysAccount_GetUserById_Result accResult = account.GetUserById("1");

                SmsController sms = new SmsController();
                String UrlTracking = System.Configuration.ConfigurationManager.AppSettings.Get("UrlTracking").ToString();
                sms.SendSMS(new SmsModel() {
                    message = string.Format("หมายเลขการแจ้ง : {0}\r\nURL สำหรับตรวจสอบ : "+ UrlTracking + "?tracking_no={0}", data.PwaIncidentNo+"-"+data.RandomNo )
                    ,
                    telephone = "" });

                return Ok(new
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",
                    IncidentNo = data.PwaIncidentNo

                });

            }
            //catch (System.Data.Entity.Validation.DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {

            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw e;
            //}
            catch (Exception ex)
            {

                Log("AddIncident Controller", "System", ex.Message, ex);

                return Ok(new
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later",
                    IncidentNo = ""


                });
            }







        }

        [HttpPost]
        public IHttpActionResult SaveEditIncident()
        {
            var locale = new System.Globalization.CultureInfo("en-US");
            List<FileDto> fileUploads = new List<FileDto>();
            var deserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                var Request = System.Web.HttpContext.Current.Request;
                //var file = System.Web.HttpContext.Current

                string json = Request.Form[0];
                //  var model = JsonHelper.Deserialize<AddIncidentModel>(json);
                var model = deserializer.Deserialize<AddIncidentModel>(Request.Form[0]);
                var _incidentRespository = FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetPwaIncidentResponsitory();

                var  target = _incidentRespository.GetIncident(Converting.ToInt(model.PwaIncidentID));

                /* if reject and re define branch then set process status to 2*/
                if(target.Incident.ProcessStage == 4 && target.Incident.BracnchNo!= model.BracnchNo)
                {
                    model.IncidentStatus = "1";
                }
                

                var data = new FrameWork.Bal.Data.Incident();

                data.PwaIncidentID = Converting.ToInt(model.PwaIncidentID);
                data.PwaIncidentNo = model.PwaIncidentNo;
                //PwaIncidentNo = $"S{DateTime.Now.ToString("yyyyMMdd"; locale)}00001";
                data.PwaIncidentTypeID = model.PwaIncidentTypeID.ToString();
                data.PwaIncidentGroupID = model.PwaIncidentGroupID != null ? model.PwaIncidentGroupID.ToString() : "";
                data.CaseTitle = model.CaseTitle != null ? model.CaseTitle.ToString() : "";
                data.CaseTitleDetail = model.CaseTitleDetail;

                data.InformLatitude = model.CaseLatitude;
                data.InformLongtitude = model.CaseLongtitude;

                data.CaseDetail = model.CaseDetail;
                data.ResolvedDetail = model.ResolvedDetail;
                data.Sla = model.Sla;
                if (!model.Sla &&  target.Incident.StartSLADate == null)
                { data.StartSLADate = DateTime.Now; }
                data.SlaDetail = model.SlaDetail;

                data.CaseLatitude = model.CaseLatitude;
                data.CaseLongtitude = model.CaseLongtitude;
                data.ProcessStage = Converting.ToInt(model.IncidentStatus);
                data.CustomerProcessStage = Converting.ToInt(model.IncidentStatus);
                data.PwaParentIncidentID = "0";
                data.Status = 1;
                //data.CreatedDate = DateTime.Now;
                //data.CreatedBy = "1";
                data.ReceivedCaseDate = model.ReceivedCaseDate.ToDateTime();
                //data.CompletedCaseDate = model.CompletedCaseDate.ToDateTime();
                data.PwsIncidentAddress = model.PwsIncidentAddress;

                if (target.Incident.BracnchNo != model.BracnchNo && model.BracnchNo != "1001")
                { data.StartSLADate = (!model.Sla) ? (DateTime?)DateTime.Now : null; }

                data.BracnchNo = model.BracnchNo;
                data.Recorder = model.User.AccountID;
                data.RecordDate = DateTime.Now;

                data.PwaInformReceiver = model.PwaInformReceiver;
                data.PwaInformDate = (model.PwaInformDate == null || model.Recorder == "") ? DateTime.Now : DateTime.Now;
                data.UpdatedDate = DateTime.Now;

                data.UpdatedDate = DateTime.Now;
                data.UpdatedBA = model.User.BA ?? "";
                data.UpdatedBy = model.User.AccountID;

                data.NearLocate = model.NearLocate;
                data.ProvinceID = model.ProvinceID;
                data.DistrictID = model.DistrictID;
                data.SubDistrictID = model.SubDistrictID;
                data.AddressNo = model.AddressNo;
                data.VillageNo = model.VillageNo;
                data.VillageBuilding = model.VillageBuilding;
                data.Soi = model.Soi;
                data.Road = model.Road;

                try
                {
                    data.OverSlaId = string.IsNullOrEmpty(Request["reason_sla"]) ? null : (int?)Convert.ToInt32(Request["reason_sla"]);
                }
                catch {
                    data.OverSlaId = null;
                }
                data.OverSlaOther = Request["reason_sla_txt"];

                if (Converting.ToInt(model.IncidentStatus) == 10)
                {
                    data.CompletedCaseDate = DateTime.Now;
                    
                    if(target.Incident.SysOwnerCode == "IC360") // ส่ง Email หา AppSetting
                    {
                        using (Smart1662Entities db = new Smart1662Entities())
                        {
                            int subject_id = Convert.ToInt32(data.PwaIncidentGroupID);

                            int year = data.CompletedCaseDate.Value.Year;
                            if (year < 2500)
                            { year += 543; }

                            string finish_date = data.CompletedCaseDate.Value.ToString("dd/MM/" + year + " HH:mm");
                            finish_date = finish_date.Replace("-", "/");

                            string to_email = ConfigurationManager.AppSettings["ToEmail"];
                            string subject = "เลขที่ [" + data.PwaIncidentNo + "] เรื่อง [XXX]";
                            string body = @"
***สาขาแก้ไข &gt;&gt; ได้เข้าดำเนินการตรวจสอบแล้วพบว่า<br />
สาเหตุของปัญหา: " + data.ResolvedDetail + @"<br />
อื่นๆ: วันที่ดำเนินการแล้วเสร็จ " + finish_date + @"***";

                            EmailServices.SendMail(to_email, subject, body);
                        }
                    }
                }

                if ( target.Incident.ProcessStage!=10 && data.ProcessStage == 10 &&  !data.Sla )
                {
                    data.EndSLADate = DateTime.Now;
                    SlaManager slaManager = new SlaManager();
                    var calSla = slaManager.CalculateSla(data.Sla, data.ProcessStage, data.CaseTitle, target.Incident.StartSLADate, data.EndSLADate);
                    if (calSla != null)
                    {
                        data.SlaStatus = calSla.SLAStatus;
                        data.MinuteWorking = calSla.MinuteWorking;
                    }
                }



                List<Pwa.FrameWork.Dao.EF.Smart1662.PwaInformer> Informers = new List<PwaInformer>();
                Pwa.FrameWork.Dao.EF.Smart1662.PwaInformer infomer = null;
                foreach (AddInformer i in model.Informers)
                {
                    infomer = new PwaInformer();
                    infomer.InformerID = Converting.ToInt(i.InformID);
                    infomer.CustomerID = i.CustomerID;
                    infomer.CustCode = i.CustCode;
                    infomer.Title = i.Title.ToString();
                    infomer.FirstName = i.FirstName;
                    infomer.LastName = i.LastName;
                    infomer.MeterNo = i.MeterNo;
                    infomer.Telephone = i.Telephone != null ? i.Telephone : "";
                    infomer.InformChannelID = i.InformChannelID.ToString();
                    infomer.InformReference = i.InformReference;
                    infomer.CreatedDate = DateTime.Now;
                    infomer.CreatedBy = "1";
                    //infomer.ProvinceID = i.Province != null ? i.Province.ToString() : "";
                    //infomer.DistrictID = i.District != null ? i.District.ToString() : "";
                    //infomer.SubDistrictID = i.SubDistrict != null ? i.SubDistrict.ToString() : "";
                    infomer.Address = i.CustomerAddress;

                    //infomer.Address_no = i.AddressNo;
                    //infomer.Village_no = i.VillageNo;
                    //infomer.Building = i.VillageBuilding;
                    //infomer.Soi = i.Soi;
                    //infomer.Road = i.Road;

                    Informers.Add(infomer);
                }
                data.Imformers = Informers;
               

                PwaBranchReject reject = null;
                if (int.Parse(model.IncidentStatus) == Pwa.FrameWork.Bal.PwaSystem.ProcessStage.BRANCH_REJECT.GetInt())
                {
                    reject = new PwaBranchReject()
                    {
                        PwaIncidentID = int.Parse(model.PwaIncidentID),
                        BranchNo = model.BracnchNo,
                        IncidentStage = int.Parse(model.IncidentStatus),
                        IncidentStageDetail = model.IncidentStatusDetail ?? ""
                    };
                }

                data.BranchRejects = null;
                if (reject != null)
                {
                    data.BranchRejects = new List<PwaBranchReject>();
                    data.BranchRejects.Add(reject);
                }
                Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();
                manager.IsCallcenter = model.IsCallenter;

                int number = 1;

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFile file = Request.Files[fileName];
                    var extension = Path.GetExtension(file.FileName);
                    // var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    var path = IncPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                             .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                             .Replace("{IncNo}", data.PwaIncidentNo.Trim());
                    var virtualPath = VirtualPath.Replace("{Year}", DateTime.Now.Year.ToString())
                                             .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                                             .Replace("{IncNo}", data.PwaIncidentNo.Trim());

                    if (FileMng.HaveDirectory(path))
                    {
                        incFileName = incFileName.Replace("{Timestamp}", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                        file.SaveAs(string.Format("{0}{1}{2}", path, incFileName.Replace("{Number}", number.ToString()), extension));
                        fileUploads.Add(new FileDto()
                        {
                            FileID = data.PwaIncidentID.ToString(),
                            PwaIncidentID = data.PwaIncidentID.ToString(),
                            FilePath = path,
                            No = number.ToString(),
                            FullPath = string.Format("{0}{1}{2}", path, incFileName.Replace("{Number}", number.ToString()), extension),
                            HtmlFile = string.Format("{0}{1}{2}", virtualPath, incFileName.Replace("{Number}", number.ToString()), extension),
                            FileName = incFileName.Replace("{Number}", number.ToString()),
                            FileSize = file.ContentLength.ToString(),
                            UploadDate = string.Format("{0}{1}{2}", DateTime.Now.Year.ToString()
                            , DateTime.Now.Month.ToString("##00")
                            , DateTime.Now.Day.ToString("##00"))
                        });

                    }
                    number = number + 1;
                }

                //foreach (string fileName in Request.Files)
                //{
                //    HttpPostedFile file = Request.Files[fileName];
                //    var extension = Path.GetExtension(file.FileName);
                //    // var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                //    var path = IncPath.Replace("{Year}", DateTime.Now.Year.ToString())
                //                             .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                //                             .Replace("{IncNo}", data.PwaIncidentNo.Trim());
                //    var virtualPath = VirtualPath.Replace("{Year}", DateTime.Now.Year.ToString())
                //                             .Replace("{Month}", DateTime.Now.Month.ToString("##00"))
                //                             .Replace("{IncNo}", data.PwaIncidentNo.Trim());
                //    if (FileMng.HaveDirectory(path))
                //    {
                //        number = Converting.ToInt(fileName.Replace("file", ""));
                //        file.SaveAs(string.Format("{0}{1}{2}", path, incFileName.Replace("{Number}", number.ToString()), extension));
                //        fileUploads.Add(new FileDto()
                //        {
                //            FileID = data.PwaIncidentID.ToString(),
                //            FilePath = path,
                //            No = number.ToString(),
                //            FullPath = string.Format("{0}{1}{2}", path, incFileName.Replace("{Number}", number.ToString()), extension),
                //            HtmlFile = string.Format("{0}{1}{2}", virtualPath, incFileName.Replace("{Number}", number.ToString()), extension),
                //            FileName = incFileName.Replace("{Number}", number.ToString()),
                //            FileSize = file.ContentLength.ToString(),
                //            UploadDate = string.Format("{0}{1}{2}", DateTime.Now.Year.ToString()
                //            , DateTime.Now.Month.ToString("##00")
                //            , DateTime.Now.Day.ToString("##00"))
                //        });

                //    }

                //}

                if (fileUploads != null)
                {
                    data.Files = fileUploads;
                }

                manager.Update(data);


                if(target.Incident.SysOwnerCode!=null && target.Incident.SysOwnerCode != SystemCode.Smart1662
                    && target.Incident.ProcessStage != 4
                    && data.ProcessStage == 4
                    )
                {
                    FrameWork.Bal.Smart1662.IncidentNotifyOtherSystem notify = new FrameWork.Bal.Smart1662.IncidentNotifyOtherSystem();
                    notify.SendEmailRejectNotify(target.Incident.PwaIncidentID);
                }

              
                return Ok(new IncidentRespModel
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",

                });

               

            }
            catch (Exception ex)
            {
                Log("SaveEditIncident", "System", ex.Message, ex);
                return Ok(new IncidentRespModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }







        }

        [HttpPost]
        public IHttpActionResult UpdateIncidentStatus([FromBody] UpdateIncidentStatus model)
        {
            try
            {


                Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();
                manager.UpdateIncidentStatus(int.Parse(model.PwaIncidentID), 12385, model.BranchNo, int.Parse(model.IncidentStatus), model.IncidentStatusDetail ?? "");

                return Ok(new IncidentRespModel
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",

                });
            }
            catch (Exception ex)
            {

                Log("UpdateIncidentStatus", "System", ex.Message, ex);
                return Ok(new IncidentRespModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }
        }


        [HttpPost]
        public IHttpActionResult Addcident([FromBody] IncidentInfoModel model)
        {

            try
            {
                var locale = new System.Globalization.CultureInfo("en-US");
                //IncidentManager manager = new IncidentManager();

                Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();
                manager.Add(new FrameWork.Bal.Data.Incident()
                {
                    //PwaIncidentID  = model.pw
                    PwaIncidentNo = $"S{DateTime.Now.ToString("yyyyMMdd", locale)}00001",
                    PwaIncidentTypeID = model.PwaIncidentTypeID.ToString(),
                    PwaIncidentGroupID = model.PwaIncidentGroupID.ToString(),
                    //  CaseTitle = model.CaseTitle.ToString(),
                    CaseDetail = model.CaseDetail,
                    Sla = model.Sla,
                    SlaDetail = model.SlaDetail,

                    CaseLatitude = model.CaseLatitude,
                    CaseLongtitude = model.CaseLongtitude,
                    ProcessStage = 1,
                    CustomerProcessStage = 1,
                    PwaParentIncidentID = "0",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "1",
                    ReceivedCaseDate = DateTime.Parse($"{model.ReceivedCaseDate} {model.ReceivedCaseTime}", locale),
                    //CompletedCaseDate = DateTime.Parse($"{model.CompletedCaseDate} {model.CompletedCaseTime}", locale),
                    Imformers = model.Informers.Select(i => i.CopyTo<FrameWork.Dao.EF.Smart1662.PwaInformer>(new FrameWork.Dao.EF.Smart1662.PwaInformer() { CreatedDate = DateTime.Now })).ToList()

                });

                //var incs = await manager.ReceiveIncidentCase()

                return Ok(new IncidentRespModel
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",

                });

            }
            catch (Exception ex)
            {
                //log exception to App log system and throw message to client


                return Ok(new IncidentRespModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }

        }

        [HttpPost]

        public IHttpActionResult GetData([FromBody] GetDataModel model)
        {
            Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();
            try
            {

                if (model.Module == "DISTRICT")
                {
                    var result = manager.GetDistricts(model.ParentID).Select(s => new { ID = s.AmphureId, Name = s.AmphureName }).ToList();
                    return Ok(result);
                }
                else if (model.Module == "SUBDISTRICT")
                {
                    var result = manager.GetSubDistricts(model.ParentID).Select(s => new { ID = s.DistrinctId, Name = s.DistrinctName }).ToList();
                    return Ok(result);
                }
                else if (model.Module == "SEARCH_SUBDISTRICT")
                {
                    var result = manager.SearchSubDistrict(model.Word).ToList();
                    return Ok(result);
                }
                else
                {
                    return Ok(new string[0]);
                }

            }
            catch (Exception ex)
            {

                return Ok(new string[0]);
            }
        }

        [HttpGet]
        public IHttpActionResult GetIncidents()
        {
            try
            {
                Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();
                //var incidents = manager.GetIncidents().OrderByDescending(o=>o.CreatedDate).Take(4).ToList();

                return Ok(new IncidentListModel
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",
                    Incidents = null
                });

            }
            catch (Exception)
            {

                return Ok(new IncidentListModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }
        }


        [HttpPost]
        public IHttpActionResult SearchBranchIncidents([FromBody] SearchIncidentDto model)
        {

            try
            {
                Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();

                DateTime startDate = (model.StartDate != null && model.StartDate != "") ? model.StartDate.ToDate() : DateTime.Now.AddDays(-20);
                DateTime endDate = (model.EndDate != null && model.EndDate != "") ? model.EndDate.ToDate() : DateTime.Now;

                model.dtStartDate = startDate;
                model.dtEndDate = endDate;
                var result = manager.SearchIncidentByCriteria(model);  
                return Ok(new
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new IncidentListModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }
        }


        [HttpPost]
        public IHttpActionResult SearchLocation(string locationPrefix, string word)
        {
            try
            {
                Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();

                var result = manager.SearchSubDistrict(word);
                return Ok(new
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new IncidentListModel
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }
        }

        [Route("api/Incident/GetEffectCustomer/{customerCode}")]
        [HttpGet]
        public IHttpActionResult GetEffectCustomer(string customerCode)
        {
            try
            {
                Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();

                var result = manager.GetEffectCustomer(customerCode);
                return Ok(new
                {
                    Success = true,
                    Code = "200",
                    Message = "Success",
                    Result = result
                });

            }
            catch (Exception)
            {

                return Ok(new
                {
                    Success = false,
                    Code = "500",
                    Message = "Sevice unavailable.Please try again later"

                });
            }
        }



    }
}