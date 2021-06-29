using Pwa.FrameWork.Bal.Smart1662;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Smart1662.Incident;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using static Pwa.FrameWork.Bal.PwaSystem;

namespace Pwa.Web.Controllers.Smart1662
{
    public class IncidentController : BaseController
    {
        public ActionResult Receive()
        {
            IncidentDisplayDto model = null;
            try
            {
                Pwa.FrameWork.Bal.Smart1662.IncidentManager incident = new FrameWork.Bal.Smart1662.IncidentManager();
                var ts = incident.GetRequestTypes();
                var SysBranch = GetDropDownBranchList(CurrentUser.Ba);//GetDropDownList("SysBranch");
                var incidentTypes = incident.GetRequestTypes().OrderBy(o => o.OrderSeq).Select(type => new
                {
                    ID = type.Id,
                    Name = type.Name,
                    Categories = type.Categories.OrderBy(o => o.OrderSeq).Select(c => new
                    {
                        ID = c.id,
                        Name = c.name,
                        Subjects = c.Subjects.OrderBy(o => o.OrderSeeq).Select(s => new
                        {
                            ID = s.id,
                            Name = s.name,
                            SLA = s.SLA
                        }).ToList()
                    }).ToList()
                }).ToList();

                var Provinces = incident.GetProvince().Select(p => new LocationDto
                {
                    ProvinceID = p.ProvinceId,
                    ProvinceName = p.ProvinceName,
                    Value = p.ProvinceId,
                    Text = p.ProvinceName

                }).OrderBy(x => x.ProvinceName).ToList();

                var Distincts = incident.GetDistricts().Select(p => new LocationDto
                {
                    ProvinceID = p.ProvinceId,

                    DistrictID = p.AmphureId,
                    DistrictName = p.AmphureName,
                    Value = p.AmphureId,
                    Text = p.AmphureName

                }).OrderBy(x => x.DistrictName).ToList();

                var SubDistincts = incident.GetSubDistricts();

                var customers = GetDropDownList("PWACustomerInfo").Select(c => new SelectListItem()
                {
                    Text = c.Text,
                    Value = c.Value
                });
                var informChannelRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetInformChannelRespository();
                var informChannels = informChannelRespository.GetInformChannels().Select(channel => new SelectListItem
                {
                    Text = channel.Name,
                    Value = channel.InformChannelID.ToString()
                }).ToList();

                var availableStatus = GetAvailableStatusAtReceive().Select(s => new DropDownlistDto()
                {
                    Text = s.status_name_th,
                    Value = s.status_id.ToString()
                });

                var pageInfo = new
                {
                    Mode = "NEW"
                };


                var SysChannels = GetDropDownList("SysChannel");
                ViewBag.Follows_RejectHistory = false;
                ViewBag.WFA = false;
                ViewBag.GeoSearchToken = WebConfigurationManager.AppSettings["GeoSearch"];
                ViewBag.InformChannels = informChannels;
                ViewBag.IncidentTypes = incidentTypes;
                ViewBag.sysRequestTypes = GetDropDownList("SysRequestType");
                ViewBag.Provinces = Provinces;
                ViewBag.Distincts = Distincts;
                ViewBag.SubDistincts = SubDistincts;
                ViewBag.Customers = customers;
                ViewBag.SysBranchs = SysBranch;
                ViewBag.PageInfo = pageInfo;
                ViewBag.SysChannels = SysChannels;
                ViewBag.Employees = GetDropDownList("Employee");
                ViewBag.PWACustomerCodes = GetDropDownList("PWACustomerCustCode");
                ViewBag.Reasons = GetDropDownList("SysReason");
                ViewBag.Titles = GetDropDownList("SysTitle");
                ViewBag.AvailableStatus = availableStatus;
                ViewBag.CurrentUser = CurrentUser;
                ViewBag.RejectHistory = null;
                ViewBag.Recorder = GetLoginDDL();
                ViewBag.CurrUser = new
                {
                    IsCallCenter = base.IsCurrUserAsCallcenter,
                    AccountID = this.CurrentUser.AccountId,
                    BA = this.CurrentUser.Ba ?? "",
                    RoleID = this.CurrentUser.RoleId,
                    WW_CODE = this.CurrentUser.WW_CODE
                };

            }
            catch (Exception ex)
            {
                Log("IncidentController.Receive", ex.Message);
            }

            return View("Receive2", model);
        }

        public JsonResult ActionApproval()
        {
            using (Smart1662Entities db = new Smart1662Entities())
            {
                int id = Convert.ToInt32(Request["id"]);
                int incident_id = Convert.ToInt32(Request["incident_id"]);
                PwaIncidentRequestEdit re = db.PwaIncidentRequestEdit.Where(f => f.RequestID == id && f.PwaIncidentID == incident_id).FirstOrDefault();

                re.ActionResult = HttpContext.Request["action"]; // 1 = Approved, 0 = Reject
                re.ActionReason = HttpContext.Request["reason"];
                re.ActionDatetime = DateTime.Now;
                re.ActionBy = Convert.ToInt32(CurrentUser.AccountId);

                PwaIncident incident = db.PwaIncident.Where(f => f.PwaIncidentID == re.PwaIncidentID).FirstOrDefault();
                incident.ProcessStage = (re.ActionResult == "1" ? 12 : 10);

                if(re.ActionResult == "1")
                {
                    PwaIncidentWorker worker = new PwaIncidentWorker();
                    worker.PwaIncidentID = incident_id;
                    worker.ProcessStep = 12;
                    worker.CreatedDate = DateTime.Now;
                    worker.WorkerID = Convert.ToInt32(CurrentUser.AccountId);
                    worker.CreatedBy = Convert.ToInt32(CurrentUser.AccountId);

                    db.PwaIncidentWorker.Add(worker);
                }

                db.SaveChanges(); 
                
                return Json(new
                {
                    Success = true,
                    Code = "200",
                    Message = "Success"
                });
            }
        }

        public JsonResult LoadWFA()
        {
            string incident_no = HttpContext.Request["incident_no"];
            string branch = HttpContext.Request["branch"];
            int status = Convert.ToInt32(HttpContext.Request["status"]);
            DateTime? start = null;
            DateTime? end = null;

            string start_date = HttpContext.Request["start_date"];
            string end_date = HttpContext.Request["end_date"];

            if (!string.IsNullOrEmpty(start_date))
            {
                string[] tmp = start_date.Split('/');
                try
                {
                    start = Convert.ToDateTime(tmp[2] + "-" + tmp[1] + "-" + tmp[0]);
                    if (start.Value.Year > 2500) { start = start.Value.AddYears(-543); }
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                string[] tmp = end_date.Split('/');
                try
                {
                    end = Convert.ToDateTime(tmp[2] + "-" + tmp[1] + "-" + tmp[0]).AddDays(1).AddSeconds(-1);
                    if (end.Value.Year > 2500) { end = end.Value.AddYears(-543); }
                }
                catch { }
            }



            using (Smart1662Entities db = new Smart1662Entities())
            {
                var data = (from i in db.PwaIncident
                            join r in db.PwaIncidentRequestEdit on i.PwaIncidentID equals r.PwaIncidentID
                            join f in db.PwaInformer on i.PwaIncidentID equals f.PwaIncidentID
                            join s in db.SysRequestCategorySubject on i.CaseTitle equals s.id.ToString()
                            into s2 from s in s2.DefaultIfEmpty()
                            where r.RequestDatetime.HasValue
                            && ((status == 1 && r.ActionDatetime.HasValue) || (status == 0 && !r.ActionDatetime.HasValue))
                            //&& i.BracnchNo == branch
                            && ((start.HasValue && start <= r.RequestDatetime) || !start.HasValue)
                            && ((end.HasValue && end >= r.RequestDatetime) || !end.HasValue)
                            && (incident_no == "" || i.PwaIncidentNo == incident_no)
                            orderby r.RequestDatetime descending
                            select new
                            {
                                incident_id = i.PwaIncidentID,
                                incident_no = i.PwaIncidentNo,
                                request_date = r.RequestDatetime.Value,
                                customer_name = f.FirstName + " " + f.LastName,
                                topic = s.name,
                                desc = i.CaseDetail,
                                branch = i.BracnchNo,
                                completed_date = i.CompletedCaseDate,
                                reason = r.RequestReason
                            });

                return Json(data.ToArray());
            }
        }

        public JsonResult RequestEdit()
        {
            PwaIncidentRequestEdit re = new PwaIncidentRequestEdit();
            re.PwaIncidentID = Convert.ToInt32(HttpContext.Request["id"]);
            re.RequestReason = HttpContext.Request["reason"];
            re.RequestDatetime = DateTime.Now;
            re.RequestBy = Convert.ToInt32(CurrentUser.AccountId);

            using (Smart1662Entities db = new Smart1662Entities())
            {
                PwaIncident incident = db.PwaIncident.Where(f => f.PwaIncidentID == re.PwaIncidentID).FirstOrDefault();
                incident.ProcessStage = 9;

                db.PwaIncidentRequestEdit.Add(re);
                db.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Code = "200",
                    Message = "Success"
                });
            }
        }

        public ActionResult WaitForApproval()
        {
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.SysBranchs = GetDropDownBranchList(CurrentUser.Ba);
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.WFA = false;

            Smart1662Entities db = new Smart1662Entities();
            var re = db.PwaIncidentRequestEdit.Where(f => f.PwaIncidentID == id && f.ActionDatetime == null).FirstOrDefault();
            if (re != null)
            { 
                ViewBag.WFA = true;
                ViewBag.RequestID = re.RequestID;
            }

            IncidentDisplayDto editModel = null;
            try
            {
                var SysChannels = GetDropDownList("SysChannel");
                Pwa.FrameWork.Bal.Smart1662.IncidentManager incident = new FrameWork.Bal.Smart1662.IncidentManager();
                var ts = incident.GetRequestTypes();

                var incidentTypes = incident.GetRequestTypes().Select(type => new
                {
                    ID = type.Id,
                    Name = type.Name,
                    Categories = type.Categories.Select(c => new
                    {
                        ID = c.id,
                        Name = c.name,
                        Subjects = c.Subjects.Select(s => new
                        {
                            ID = s.id,
                            Name = s.name,
                            SLA = s.SLA
                        }).ToList()
                    }).ToList()

                }).ToList();


                var follows = incident.GetFollowByIncidentID(id).Select(p => new Models.Api.IncidentFollowHistoryModel()
                {
                    Date = p.FollowerDate.Value.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")),
                    Channel = p.ChannelName,
                    Detail = p.Information ?? ""
                }).ToList();


                var provinces = incident.GetProvince().Select(p => new DropDownlistDto
                {
                    Value = p.ProvinceId,
                    Text = p.ProvinceName
                }).ToList();


                var customers = GetDropDownList("PWACustomerInfo").Select(c => new SelectListItem()
                {
                    Text = $"{c.Text}",
                    Value = $"{c.Value}"
                });
                var informChannelRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetInformChannelRespository();
                var informChannels = informChannelRespository.GetInformChannels().Select(channel => new SelectListItem
                {
                    Text = channel.Name,
                    Value = channel.InformChannelID.ToString()
                }).ToList();

                var rejectHistory = incident.GetRejectHistory(id).Select(s => new Models.Api.BranchRejectHistoryModel
                {
                    Date = s.CreatedDate.Value.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")),
                    Branch = s.Name,
                    Detail = s.IncidentStageDetail
                }).ToList();

                var Provinces = incident.GetProvince().Select(p => new LocationDto
                {
                    ProvinceID = p.ProvinceId,
                    ProvinceName = p.ProvinceName,
                    Value = p.ProvinceId,
                    Text = p.ProvinceName

                }).ToList();

                var Distincts = incident.GetDistricts().Select(p => new LocationDto
                {
                    ProvinceID = p.ProvinceId,

                    DistrictID = p.AmphureId,
                    DistrictName = p.AmphureName,
                    Value = p.AmphureId,
                    Text = p.AmphureName

                }).ToList();

                var SubDistincts = incident.GetSubDistricts();



                var incidentInfo = incident.GetIncident(id);

                var inform = incidentInfo.Informers.First();
                VwLocation subdistrictInfo = null;
                if (inform.SubDistrictID != null && inform.SubDistrictID != "")
                {
                    subdistrictInfo = incident.GetSubDistrictInfo(inform.SubDistrictID);
                }

                editModel = new IncidentDisplayDto()
                {
                    PwaIncidentID = incidentInfo.Incident.PwaIncidentID,
                    PwaIncidentNo = incidentInfo.Incident.PwaIncidentNo,

                    PwaInformReceiver = incidentInfo.Incident.PwaInformReceiver,
                    PwaInformDate = incidentInfo.Incident.PwaInformDate.ToClientDateTime(),

                    PwaIncidentTypeID = incidentInfo.Incident.PwaIncidentTypeID,
                    PwaIncidentGroupID = incidentInfo.Incident.PwaIncidentGroupID,
                    //PwaInformChannel= incidentInfo.Incident.in,
                    CaseTitle = incidentInfo.Incident.CaseTitle,
                    CaseTitleDetail = incidentInfo.Incident.CaseTitleDetail,
                    CaseDetail = incidentInfo.Incident.CaseDetail,
                    ResolvedDetail = incidentInfo.Incident.ResolvedDetail,
                    Sla = incidentInfo.Incident.Sla,
                    SlaDetail = incidentInfo.Incident.SlaDetail,
                    ReceivedCaseDate = incidentInfo.Incident.ReceivedCaseDate.ToClientDateTime(),
                    CompletedCaseDate = (incidentInfo.Incident.CompletedCaseDate.HasValue ? incidentInfo.Incident.CompletedCaseDate.ToClientDateTime() : ""),
                    CaseLatitude = incidentInfo.Incident.CaseLatitude,
                    CaseLongtitude = incidentInfo.Incident.CaseLongtitude,
                    InformLatitude = incidentInfo.Incident.InformLatitude,
                    InformLongtitude = incidentInfo.Incident.InformLongtitude,

                    PwsIncidentAddress = incidentInfo.Incident.NearLocate, //incidentInfo.Incident.PwsIncidentAddress,

                    BracnchNo = incidentInfo.Incident.BracnchNo,
                    Recorder = incidentInfo.Incident.Recorder,
                    RecordDate = (incidentInfo.Incident.RecordDate.HasValue ? incidentInfo.Incident.RecordDate.Value.ToString("dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("th-TH")) : ""),
                    IncidentStatus = incidentInfo.Incident.ProcessStage,

                    InformID = inform.InformerID.ToString(),
                    CustomerID = inform.CustomerID,
                    CustCode = inform.CustCode,
                    Title = inform.Title,
                    FirstName = inform.FirstName,
                    LastName = inform.LastName,
                    CustomerName = inform.FirstName + " " + inform.LastName,
                    MeterNo = inform.MeterNo,
                    InformChannelID = inform.InformChannelID.ToString(),
                    InformReference = inform.InformReference,
                    Telephone = inform.Telephone,

                    Province = incidentInfo.Incident.ProvinceID,
                    District = incidentInfo.Incident.DistrictID,
                    SubDistrict = incidentInfo.Incident.SubDistrictID,

                    ProvinceName = (subdistrictInfo != null) ? subdistrictInfo.ProvinceName : "",
                    DistrictName = (subdistrictInfo != null) ? subdistrictInfo.AmphureName : "",
                    SubDistrictName = (subdistrictInfo != null) ? subdistrictInfo.DistrinctName : "",

                    CustomerAddress = inform.Address,
                    ProcessStatus = incidentInfo.Incident.ProcessStage,
                    IncidentStatusName = $"{incident.GetIncidentStatus(incidentInfo.Incident.ProcessStage).public_status.Replace("{0}", "")}",

                    Address_no = incidentInfo.Incident.AddressNo,
                    Village_no = incidentInfo.Incident.VillageNo,
                    Building = incidentInfo.Incident.VillageBuilding,
                    Soi = incidentInfo.Incident.Soi,
                    Road = incidentInfo.Incident.Road,

                    StartSLADate = incidentInfo.Incident.StartSLADate,

                    Files = (incidentInfo.Files != null ? incidentInfo.Files : null)
                };

                using (Smart1662Entities _conn = new Smart1662Entities())
                {
                    try
                    {
                        int create_by = Convert.ToInt32(incidentInfo.Incident.CreatedBy);
                        editModel.CreateBy = _conn.SysAccount.Where(f => f.AccountId == create_by).Select(f => f.AccountFirstName + " " + f.AccountLastName).FirstOrDefault();
                    }
                    catch { }
                    editModel.CreateDatetime = incidentInfo.Incident.CreatedDate;

                    try
                    {
                        int update_by = Convert.ToInt32(incidentInfo.Incident.UpdatedBy);
                        editModel.UpdateBy = _conn.SysAccount.Where(f => f.AccountId == update_by).Select(f => f.AccountFirstName + " " + f.AccountLastName).FirstOrDefault();
                    }
                    catch { }
                    editModel.UpdateDatetime = incidentInfo.Incident.UpdatedDate;
                }

                if (incidentInfo.Incident.ProvinceID != "")
                {
                    editModel.ProvinceText = "";
                    var temp = Provinces.Where(o => o.ProvinceID == incidentInfo.Incident.ProvinceID);
                    if (temp != null)
                    {
                        editModel.ProvinceText = temp.FirstOrDefault() != null ? temp.FirstOrDefault().ProvinceName : "";
                    }
                }
                if (incidentInfo.Incident.DistrictID != "")
                {
                    editModel.DistrictText = "";
                    var temp = Distincts.Where(o => o.DistrictID == incidentInfo.Incident.DistrictID);
                    if (temp != null)
                    {
                        editModel.DistrictText = temp.FirstOrDefault() != null ? temp.FirstOrDefault().DistrictName : "";
                    }
                }

                if (incidentInfo.Incident.SubDistrictID != "")
                {
                    editModel.SubDistrictText = "";
                    var temp = SubDistincts.Where(o => o.SubDistrictID == incidentInfo.Incident.SubDistrictID);
                    if (temp != null)
                    {
                        editModel.SubDistrictText = temp.FirstOrDefault() != null ? temp.FirstOrDefault().SubDistrictName : "";
                    }

                    //editModel.SubDistrictText = SubDistincts.Where(o => o.SubDistrictID == inform.SubDistrictID) != null ?
                    //                        SubDistincts.Where(o => o.SubDistrictID == inform.SubDistrictID).FirstOrDefault().SubDistrictName : "";

                }

                var availableStatus = GetAvailableStatusAtEdit().Select(s => new DropDownlistDto()
                {
                    Text = s.status_name_th,
                    Value = s.status_id.ToString()
                });




                var pageInfo = new
                {
                    Mode = "EDIT",
                    EditModel = editModel

                };

                ViewBag.Follows_RejectHistory = (follows != null && follows.Count > 0) || (rejectHistory != null && rejectHistory.Count > 0);
                ViewBag.InformChannels = informChannels;
                ViewBag.IncidentTypes = incidentTypes;
                ViewBag.GeoSearchToken = WebConfigurationManager.AppSettings["GeoSearch"];
                ViewBag.Customers = customers;
                ViewBag.PageInfo = pageInfo;
                ViewBag.sysRequestTypes = GetDropDownList("SysRequestType");
                ViewBag.Provinces = Provinces;
                ViewBag.Distincts = Distincts;
                ViewBag.SubDistincts = SubDistincts;
                ViewBag.SysBranchs = GetDropDownBranchList(CurrentUser.Ba); //GetDropDownList("SysBranch");
                ViewBag.Employees = GetDropDownList("Employee");
                ViewBag.SysChannels = SysChannels;
                ViewBag.PWACustomerCodes = GetDropDownList("PWACustomerCustCode"); ;
                ViewBag.Reasons = GetDropDownList("SysReason");
                ViewBag.Titles = GetDropDownList("SysTitle");
                ViewBag.Follows = follows;
                ViewBag.AvailableStatus = availableStatus;
                ViewBag.RejectHistory = rejectHistory;
                ViewBag.Recorder = GetLoginDDL();
                ViewBag.CurrUser = new
                {
                    IsCallCenter = base.IsCurrUserAsCallcenter,
                    AccountID = this.CurrentUser.AccountId,
                    BA = this.CurrentUser.Ba ?? "",
                    RoleID = this.CurrentUser.RoleId
                };
            }
            catch (Exception ex)
            {
                Log("IncidentController.Edit", ex.Message); 
            }


            return View("Receive2", editModel);
        }


        public List<DropDownlistDto> GetLoginDDL()
        {
            List<DropDownlistDto> result = new List<DropDownlistDto>(0);
            result.Add(new DropDownlistDto()
            {
                Text = $"{CurrentUser.AccountFirstName ?? ""}  {CurrentUser.AccountLastName ?? ""}",
                Value = CurrentUser.AccountId
            });

            return result;
        }

        public List<SysStatus> GetAvailableStatusAtReceive()
        {
            StatusManager manager = new StatusManager();
            ProcessStage[] stages = null;


            if (IsCurrUserAsCallcenter)
            {
                stages = new ProcessStage[] { ProcessStage.CALLCENTER_RECEIVE_CASE };

            }
            else
            {
                stages = new ProcessStage[] { ProcessStage.BRANCH_RECEIVE_CASE_BY_SELF };
            }

            return manager.GetStatuses(stages);
        }

        public List<SysStatus> GetAvailableStatusAtEdit()
        {
            StatusManager manager = new StatusManager();
            ProcessStage[] stages = null;


            if (IsCurrUserAsCallcenter)
            {
                stages = new ProcessStage[] { ProcessStage.COMPLETED_CASE };

            }
            else
            {
                stages = new ProcessStage[] { ProcessStage.COMPLETED_CASE };
            }

            return manager.GetStatuses(stages);
        }

        // GET: Incident
        public ActionResult Detail()
        {

            try
            {

                Pwa.FrameWork.Bal.Smart1662.IncidentManager incident = new FrameWork.Bal.Smart1662.IncidentManager();
                var ts = incident.GetRequestTypes();


                var incidentGroupRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetIncidentGroupRespository();
                var informChannelRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetInformChannelRespository();
                var customerTypeRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetCustomerTypeRespository();
                var SysBranch = GetDropDownBranchList(CurrentUser.Ba); //GetDropDownList("SysBranch");


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

                var sysRequestTypes = GetDropDownList("SysRequestType");
                var SysRequestCategorys = GetDropDownList("SysRequestCategory");
                var SysRequestCategorySubjects = GetDropDownList("SysRequestCategorySubject");
                var SysChannels = GetDropDownList("SysChannel");
                ViewBag.IncidentGroups = incidentGroups;
                ViewBag.InformChannels = informChannels;
                ViewBag.IncidentTypes = incidentTypes;
                ViewBag.SysBranch = SysBranch;
                ViewBag.SysChannels = SysChannels;
            }
            catch (Exception)
            {

                throw;
            }


            return View("Receive");
        }

        public string DeletePhoto()
        {
            using(Smart1662Entities db = new Smart1662Entities())
            {
                try
                {
                    int id = Convert.ToInt32(Request["id"]);
                    PwaIncident_Files del = db.PwaIncident_Files.Where(f => f.Id == id).FirstOrDefault();
                    db.PwaIncident_Files.Remove(del);
                    db.SaveChanges();

                    return "1";
                }
                catch (Exception ex)
                { return ex.Message; }
            }
        }

        public ActionResult Index()
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
                /*
                List<FollowerDto> Follower = new List<FollowerDto>();
                Follower = incident.getFollowerByIncidentNo(editModel.PwaIncidentNo);
                Follower = incident.getFollowerById(id.ToString());
                */
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
            catch (Exception)
            {

                throw;
            }
            return View();
            //    return View("ReceiveIncident");

        }


        private class TmpDataSocialGISC
        {
            public string publish_datetime { get; set; }
            public string comment_id { get; set; }
            public string message { get; set; }
            public string category { get; set; }
            public string display_name { get; set; }
            public string name { get; set; }
            public string ww_code { get; set; }
            public string brn_name { get; set; }
        }

        public JsonResult GetSocialGISC(DateTime? start, DateTime? end, string branch)
        {
            try
            {
                start = Convert.ToString(Request["start"]).ToDate();
            }
            catch { }
            try
            {
                end = Convert.ToString(Request["end"]).ToDate();
            }
            catch { }


            if (end == null) { end = DateTime.Today; }
            if (start == null) { start = DateTime.Today.AddDays(-3); }

            if (start.Value.Year > 2500) { start = start.Value.AddYears(-543); }
            if (end.Value.Year > 2500) { end = end.Value.AddYears(-543); }

            string[] social_category = new string[] { "น้ำไม่ไหล กปภ.", "ค่าน้ำ กปภ.", "บริการ กปภ.", "คุณภาพน้ำ", "ท่อแตก", "เรื่องทั่วไป กปภ.", "ข่าวสารประชาสัมพันธ์ กปภ.", "ติชมและแนะนำ กปภ.", "อื่นๆ" };
            branch = Request["ww"];
            SysBranch b = null;
            using (Smart1662Entities conn = new Smart1662Entities())
            {
                b = conn.SysBranch.Where(f => f.Code == branch).SingleOrDefault();
            }

            TmpDataSocialGISC[] data = null;
            FrameWork.Dao.Smart1662PostgresGISCForSocialDB db = new FrameWork.Dao.Smart1662PostgresGISCForSocialDB(System.Configuration.ConfigurationManager.AppSettings["GISCForSocialDB"]);
            if (db.OpenConnection())
            {
                string sql = @"select app_comment.publish_datetime,app_comment.message,app_comment.comment_id,app_analyzeresult.category, app_snsuser.display_name, app_datasource.name, app_branch.ww_code, app_branch.brn_name
from gisc_social.app_comment app_comment  
LEFT JOIN gisc_social.app_address app_address ON app_comment.address_id = app_address.id
JOIN gisc_social.app_datasource app_datasource ON app_comment.datasource_id = app_datasource.id
JOIN gisc_social.app_analyzeresult app_analyzeresult ON app_comment.id = app_analyzeresult.comment_id
LEFT JOIN gisc_social.app_snsuser app_snsuser  ON app_comment.sns_user_id = app_snsuser.id
LEFT JOIN gisc_social.app_branch app_branch ON app_branch.id = app_comment.branch_id
where publish_datetime >= '" + start.Value.ToString("yyyy-MM-dd 00:00:00") + @"' AND publish_datetime <= '" + end.Value.ToString("yyyy-MM-dd 23:59:59") + @"'  " + (b == null || b.Code == "1001" ? "" : " AND ww_code = '" + b.WW_CODE + @"'") + @" order by app_comment.publish_datetime desc limit 2000
";
                DataTable dt = db.ExcecuteToDataTable(sql);

                data = new TmpDataSocialGISC[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime tp_date = Convert.ToDateTime(dt.Rows[i]["publish_datetime"]);
                    string p_date = tp_date.ToString("dd/MM/") + (tp_date.Year < 2500 ? tp_date.Year + 543 : tp_date.Year).ToString() + tp_date.ToString(" HH:mm");

                    data[i] = new TmpDataSocialGISC()
                    {
                        comment_id = dt.Rows[i]["comment_id"]?.ToString(),
                        publish_datetime =p_date.Replace("-","/"),
                        message = dt.Rows[i]["message"]?.ToString(),
                        category = social_category[Convert.ToInt32(dt.Rows[i]["category"]) - 1],
                        display_name = dt.Rows[i]["display_name"]?.ToString(),
                        name = dt.Rows[i]["name"]?.ToString(),
                        ww_code = dt.Rows[i]["ww_code"]?.ToString(),
                        brn_name = dt.Rows[i]["brn_name"]?.ToString()
                    };
                }
            }

            return Json(data.Take(2000));
        }

        public string GetBranchForWWCode()
        {
            using (Smart1662Entities db = new Smart1662Entities())
            {
                string ww_code = Request["ww_code"];
                SysBranch b = db.SysBranch.Where(f => f.WW_CODE == ww_code).SingleOrDefault();

                return (b != null ? b.Code : "");
            }
        }

        public JsonResult GetData(IncidentDto searchData)
        {
            Pwa.FrameWork.Bal.Smart1662.IncidentManager managerInc = new FrameWork.Bal.Smart1662.IncidentManager();
            Debug.WriteLine("CurrentUser.Ba  " + CurrentUser.Ba);
            //Debug.WriteLine("CurrentUser.TypeCode  " + CurrentUser.TypeCode);

            /*if (searchData.bUserCurrentBranchNo != null)
            {
                if (searchData.bUserCurrentBranchNo == "BRANCH")
                {
                    searchData.BranchNo = CurrentUser.Ba;
                }
            }
            else if (searchData.BranchNo != null)
            {
                searchData.BranchNo = searchData.BranchNo;
            }*/

            if (searchData.BranchNo != null)
            {
                searchData.BranchNo = searchData.BranchNo;
            }
            else
            {
                searchData.BranchNo = CurrentUser.Ba;
            }

            Debug.WriteLine("searchData.BranchNo  " + searchData.BranchNo);

            var incidents = managerInc.GetIncidents(searchData).OrderByDescending(o => o.CreatedDate).ToList();
            foreach (IncidentDto vData in incidents)
            {
                vData.CreatedDateText = Converting.DateOfdd_MM_yyyyTH(vData.CreatedDate);
                vData.CompletedCaseDateText = (vData.CompletedCaseDate.HasValue ? Converting.DateOfdd_MM_yyyyTH(vData.CompletedCaseDate.Value) : "");
                vData.ReceivedCaseDateText = Converting.DateOfdd_MM_yyyyTH(vData.ReceivedCaseDate);
                vData.CaseDetail = vData.CaseDetail == null ? "" : vData.CaseDetail;
            }
            return Json(incidents);
        }


        [System.Web.Http.Route("{id}")]
        public ActionResult ViewEmail(int id)
        {
            IncidentNotify notifyer = new IncidentNotify(id);

            notifyer.SendNotifyEmail(CurrentUser.AccountEmail);
            ViewBag.Html = notifyer.GetNotifyEmailContent(CurrentUser.AccountEmail);

            return View("ViewEmail");

        }

        #region IncidentFollower
        public ActionResult FollowUp(int id)
        {
            IncidentDisplayDto editModel = null;
            try
            {
                var SysChannels = GetDropDownList("SysChannel");
                Pwa.FrameWork.Bal.Smart1662.IncidentManager incident = new FrameWork.Bal.Smart1662.IncidentManager();
                var ts = incident.GetRequestTypes();

                var incidentTypes = incident.GetRequestTypes().Select(type => new
                {
                    ID = type.Id,
                    Name = type.Name,
                    Categories = type.Categories.Select(c => new
                    {
                        ID = c.id,
                        Name = c.name,
                        Subjects = c.Subjects.Select(s => new
                        {
                            ID = s.id,
                            Name = s.name
                        }).ToList()
                    }).ToList()

                }).ToList();


                var follows = incident.GetFollowByIncidentID(id).Select(p => new Models.Api.IncidentFollowHistoryModel()
                {
                    Date = p.FollowerDate.Value.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")),
                    Channel = p.ChannelName,
                    Detail = p.Information ?? ""
                }).ToList();


                var provinces = incident.GetProvince().Select(p => new DropDownlistDto
                {
                    Value = p.ProvinceId,
                    Text = p.ProvinceName
                }).ToList();


                var customers = GetDropDownList("PWACustomerInfo").Select(c => new SelectListItem()
                {
                    Text = $"{c.Text}",
                    Value = $"{c.Value}"
                });
                var informChannelRespository = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetInformChannelRespository();
                var informChannels = informChannelRespository.GetInformChannels().Select(channel => new SelectListItem
                {
                    Text = channel.Name,
                    Value = channel.InformChannelID.ToString()
                }).ToList();


                var incidentInfo = incident.GetIncident(id);

                var inform = incidentInfo.Informers.First();

                editModel = new IncidentDisplayDto()
                {
                    PwaIncidentID = incidentInfo.Incident.PwaIncidentID,
                    PwaIncidentNo = incidentInfo.Incident.PwaIncidentNo,

                    PwaInformReceiver = incidentInfo.Incident.PwaInformReceiver,
                    PwaInformDate = incidentInfo.Incident.PwaInformDate.ToClientDateTime(),

                    PwaIncidentTypeID = incidentInfo.Incident.PwaIncidentTypeID,
                    PwaIncidentGroupID = incidentInfo.Incident.PwaIncidentGroupID,
                    //PwaInformChannel= incidentInfo.Incident.in,
                    CaseTitle = incidentInfo.Incident.CaseTitle,
                    CaseTitleDetail = incidentInfo.Incident.CaseTitleDetail,
                    CaseDetail = incidentInfo.Incident.CaseDetail,
                    ResolvedDetail = incidentInfo.Incident.ResolvedDetail,
                    Sla = incidentInfo.Incident.Sla,
                    SlaDetail = incidentInfo.Incident.SlaDetail,
                    ReceivedCaseDate = incidentInfo.Incident.ReceivedCaseDate.ToClientDateTime(),
                    CompletedCaseDate = (incidentInfo.Incident.CompletedCaseDate.HasValue ? incidentInfo.Incident.CompletedCaseDate.Value.ToClientDateTime() : ""),
                    CaseLatitude = incidentInfo.Incident.CaseLatitude,
                    CaseLongtitude = incidentInfo.Incident.CaseLongtitude,
                    PwsIncidentAddress = incidentInfo.Incident.PwsIncidentAddress,

                    BracnchNo = incidentInfo.Incident.BracnchNo,
                    Recorder = incidentInfo.Incident.Recorder,
                    RecordDate = incidentInfo.Incident.RecordDate.ToClientDate(),
                    IncidentStatus = incidentInfo.Incident.ProcessStage,
                    IncidentStatusName = "",
                    InformID = inform.InformerID.ToString(),
                    CustomerID = inform.CustomerID,
                    CustCode = inform.CustCode,
                    Title = inform.Title,
                    FirstName = inform.FirstName,
                    LastName = inform.LastName,
                    MeterNo = inform.MeterNo,
                    InformChannelID = inform.InformChannelID.ToString(),
                    InformReference = inform.InformReference,
                    Telephone = inform.Telephone,

                    Province = inform.ProvinceID.ToString(),
                    District = inform.DistrictID.ToString(),
                    SubDistrict = inform.SubDistrictID.ToString(),
                    CustomerAddress = inform.Address,

                    Files = (incidentInfo.Files != null ? incidentInfo.Files : null)
                };




                var pageInfo = new
                {
                    Mode = "EDIT",
                    EditModel = editModel

                };
                var Province = incident.GetProvince().Where(p => p.ProvinceId == editModel.Province).Select(p => p.ProvinceName).FirstOrDefault(); ;
                var Distinct = incident.GetDistricts().Where(p => p.AmphureId == editModel.District).Select(p => p.AmphureName).FirstOrDefault();
                //var SubDistinct = incident.GetSubDistricts().Where(p => p.DistrictID == editModel.SubDistrict).Select(p => p.DistrictName).FirstOrDefault();

                var SubDistinct = editModel.SubDistrict != null && editModel.SubDistrict != "" ? incident.GetSubDistrictDetail(editModel.SubDistrict).SubDistrict.DistrinctName : "";


                var Provinces = incident.GetProvince().OrderBy(o => o.ProvinceName).Select(p => new LocationDto
                {
                    ProvinceID = p.ProvinceId,
                    ProvinceName = p.ProvinceName,
                    Value = p.ProvinceId,
                    Text = p.ProvinceName

                }).ToList();

                var Distincts = incident.GetDistricts().OrderBy(d => d.AmphureName).Select(p => new LocationDto
                {
                    ProvinceID = p.ProvinceId,

                    DistrictID = p.AmphureId,
                    DistrictName = p.AmphureName,
                    Value = p.AmphureId,
                    Text = p.AmphureName

                }).ToList();

                IncidentManager Manager = new IncidentManager();
                List<FollowerDto> Follower = new List<FollowerDto>();
                //Follower = Manager.getFollowerByIncidentNo(editModel.PwaIncidentNo);
                Follower = Manager.getFollowerById(id.ToString());
                ViewBag.Follower = Follower;

                ViewBag.InformChannels = informChannels;
                ViewBag.IncidentTypes = incidentTypes;
                ViewBag.Provinces = Provinces;
                ViewBag.Customers = customers;
                ViewBag.PageInfo = pageInfo;
                ViewBag.sysRequestTypes = GetDropDownList("SysRequestType");
                ViewBag.Distincts = Distincts;
                ViewBag.SysBranchs = GetDropDownBranchList(CurrentUser.Ba);// GetDropDownList(CurrentUser.Ba);
                ViewBag.Employees = GetDropDownList("Employee");
                ViewBag.SysChannels = SysChannels;
                ViewBag.PWACustomerCodes = GetDropDownList("PWACustomerCustCode"); ;
                ViewBag.Reasons = GetDropDownList("SysReason");
                ViewBag.Titles = GetDropDownList("SysTitle");
                ViewBag.Follows = follows;
                ViewBag.Province = Province;
                ViewBag.Distinct = Distinct;
                ViewBag.SubDistinct = SubDistinct;

            }
            catch (Exception)
            {

                throw;
            }



            return View("FollowUp", editModel);
        }


        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("getFollowerByIncidentNo")]
        public async Task<JsonResult> getFollowerByIncidentNo(string id)
        {
            IncidentManager Manager = new IncidentManager();
            List<FollowerDto> dto = new List<FollowerDto>();
            try
            {
                dto = Manager.getFollowerByIncidentNo(id);
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "IncidentFollower", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("getFollowerById")]
        public async Task<JsonResult> getFollowerById(string id)
        {
            IncidentManager Manager = new IncidentManager();
            List<FollowerDto> dto = new List<FollowerDto>();
            try
            {
                dto = Manager.getFollowerById(id);
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "IncidentFollower", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("SaveFollower")]
        public async Task<JsonResult> SaveFollower(FollowerDto Requests)
        {
            IncidentManager Manager = new IncidentManager();
            FollowerDto dto = new FollowerDto();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "IncidentFollower", Level = "Heigh", Message = "Session Timeout." });
                }


                Requests.UpdatedBy = CurrentUser.AccountId;
                Requests.UpdatedByName = CurrentUser.AccountFirstName;

                bool result = Manager.AddFollower(Requests);

                Response.StatusCode = 200;
                return Json(result);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Channel", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion
        public JsonResult GetEmployees(SearchDTO searchData)
        {
            PWACustomerManager manager = new PWACustomerManager();

            return Json(GetDropDownList("Employee"));
        }

        public JsonResult GetCustomer(SearchDTO searchData)
        {
            PWACustomerManager manager = new PWACustomerManager();
            BranchDto branchDto = null;
            SysBranchManager branch = new SysBranchManager();

            if (searchData.CustomerCode != null && searchData.CustomerCode != "")
            {
                branchDto = branch.GetByCode(searchData.CustomerCode.Substring(0, 4));
            }
            else if (searchData != null && searchData.BACode != null)
            {
                branchDto = branch.GetByCode(searchData.BACode);
            }

            searchData.AreaCode = branchDto.AreaCode;
            return Json(manager.GetById(searchData));
        }
        public JsonResult GetCustomerNameAndCode(SearchDTO searchData)
        {
            PWACustomerManager manager = new PWACustomerManager();
            var result = manager.GetByNameAndCode(searchData);

            if (result != null
                && result.Count() == 0
                && searchData.CustomerCode != null
                && searchData.CustomerCode != "")
            {

                Pwa.FrameWork.Bal.Smart1662.IncidentManager incident = new FrameWork.Bal.Smart1662.IncidentManager();
                var cisCustomer = incident.GetCisCustomer(searchData.CustomerCode);
                //Debug.WriteLine("cisCustomer address = " + cisCustomer.address);

                if (cisCustomer != null)
                {

                    result = new List<PWACustomerDto>();
                    result.Add(new PWACustomerDto()
                    {
                        CustCode = cisCustomer.custcode,
                        Text = cisCustomer.custcode,
                        Value = cisCustomer.custcode,
                        CustTypeId = cisCustomer.use_type,
                        CustomerName = cisCustomer.custname,
                        FName = cisCustomer.custname,
                        address_no = cisCustomer.address,
                        Active = "ws_api"
                    });
                    return Json(result);
                }
                else
                {
                    return Json(result);
                }
            }
            else
            {
                return Json(result);
            }

        }

        public JsonResult GetCustomerByAddress(SearchAddressDTO searchData)
        {
            PWACustomerManager manager = new PWACustomerManager();
            return Json(manager.GetByAddress(searchData));
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult GetCustomerEffect(SearchDTO searchData)
        {
            PWACustomerManager manager = new PWACustomerManager();
            var data = manager.GetCustomerEffectById(searchData);
            return Json(new {
                Success = true,
                Message = "",
                Result = data
            });
            //return Json(data );
        }




        //private IHttpActionResult Ok(object p)
        //{
        //    throw new NotImplementedException();
        //}


    }
}