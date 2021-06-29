using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pwa.FrameWork.Bal.Smart1662;
using System.Threading.Tasks;
using Pwa.Web.Models.Api;
using System.Net;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto;
using System.Web.Script.Serialization;

namespace Pwa.Web.Controllers.Smart1662
{
    public class MasterController : BaseController
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        #region GetDropDownList
        [HttpPost, ActionName("getddl")]
        public async Task<JsonResult> getddl(string tn)
        {
            try
            {
                var lst = GetDropDownList(tn);
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "getddlMaster", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion
       
        #region Channel
        public ActionResult Channel()
        {
            SysChannelManager Manager = new SysChannelManager();

            List<ChannelDto> lst = new List<ChannelDto>();

            try
            {
                lst = Manager.AllChannel();
                ViewBag.AllChannel = lst;
                //string token = FrameWork.Dto.Utils.DEncrypt.encryptSHA1("SMART 1662.2020-06-23");
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        [HttpPost, ActionName("getChannelById")]
        public async Task<JsonResult> getChannelById(string id)
        {
            SysChannelManager Manager = new SysChannelManager();
            ChannelDto dto = new ChannelDto();
            try
            {
                dto = Manager.GetChannelById(Convert.ToInt32(id));
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Channel", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SearchChannel")]
        public async Task<JsonResult> SearchChannel(string name, string status)
        {
            SysChannelManager Manager = new SysChannelManager();
            List<ChannelDto> lst = new List<ChannelDto>();
            try
            {
                //Name = Name == null ? "" : Name;
                lst = Manager.SearchChannel(name, status);
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Channel", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveChannel")]
        public async Task<JsonResult> SaveChannel(ChannelDto Requests)
        {
            SysChannelManager Manager = new SysChannelManager();
            ChannelDto dto = new ChannelDto();
            List<ChannelDto> lst = new List<ChannelDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "Channel", Level = "Heigh", Message = "Session Timeout." });
                }
                if (Requests.ChannelId != null)
                {
                    Requests.ChannelId = Requests.ChannelId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                bool result = Manager.Add(Requests);
                lst = Manager.AllChannel();

                Response.StatusCode = 200;
                return Json(lst);
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

        #region RequestType
        public ActionResult RequestType()
        {
            SysRequestTypeManager Manager = new SysRequestTypeManager();

            List<RequestTypeDto> lst = new List<RequestTypeDto>();

            try
            {
                lst = Manager.All();
                ViewBag.AllRequestType = lst;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        [HttpPost, ActionName("getRequestTypeById")]
        public async Task<JsonResult> getRequestTypeById(string id)
        {
            SysRequestTypeManager Manager = new SysRequestTypeManager();
            RequestTypeDto dto = new RequestTypeDto();
            try
            {
                dto = Manager.GetById(Convert.ToInt32(id));
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestType", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SearchRequestType")]
        public async Task<JsonResult> SearchRequestType(string name, string status)
        {
            SysRequestTypeManager Manager = new SysRequestTypeManager();
            List<RequestTypeDto> lst = new List<RequestTypeDto>();
            try
            {
                lst = Manager.Search(name, status);
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestType", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveRequestType")]
        public async Task<JsonResult> SaveRequestType(RequestTypeDto Requests)
        {
            SysRequestTypeManager Manager = new SysRequestTypeManager();
            RequestTypeDto dto = new RequestTypeDto();
            List<RequestTypeDto> lst = new List<RequestTypeDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "RequestType", Level = "Low", Message = "Session Timeout." });
                }
                if (Requests.Id != null)
                {
                    Requests.Id = Requests.Id;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                bool result = Manager.Add(Requests);
                lst = Manager.All();

                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestType", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion

        #region RequestCategory
        public ActionResult RequestCategory()
        {
            SysRequestCategoryManager Manager = new SysRequestCategoryManager();

            List<RequestCategoryDto> lst = new List<RequestCategoryDto>();

            try
            {
                lst = Manager.All();
                ViewBag.AllRequestCategory = lst;
                var objSysRequestType = GetDropDownList("SysRequestType");
                ViewBag.objSysRequestType = objSysRequestType;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost, ActionName("getRequestCategoryById")]
        public async Task<JsonResult> getRequestCategoryById(string id)
        {
            SysRequestCategoryManager Manager = new SysRequestCategoryManager();
            RequestCategoryDto dto = new RequestCategoryDto();
            try
            {
                dto = Manager.GetById(Convert.ToInt32(id));
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestCategory", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SearchRequestCategory")]
        public async Task<JsonResult> SearchRequestCategory(string requesttype, string name, string status)
        {
            SysRequestCategoryManager Manager = new SysRequestCategoryManager();
            List<RequestCategoryDto> lst = new List<RequestCategoryDto>();
            try
            {
                lst = Manager.Search(requesttype, name, status);
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestCategory", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveRequestCategory")]
        public async Task<JsonResult> SaveRequestCategory(RequestCategoryDto Requests)
        {
            SysRequestCategoryManager Manager = new SysRequestCategoryManager();
            RequestCategoryDto dto = new RequestCategoryDto();
            List<RequestCategoryDto> lst = new List<RequestCategoryDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "RequestCategory", Level = "Low", Message = "Session Timeout." });
                }
                if (Requests.Id != null)
                {
                    Requests.Id = Requests.Id;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                bool result = Manager.Add(Requests);
                lst = Manager.All();

                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestCategory", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion

        #region RequestCategorySubject
        public ActionResult RequestCategorySubject()
        {
            SysRequestCategorySubjectManager Manager = new SysRequestCategorySubjectManager();

            List<RequestCategorySubjectDto> lst = new List<RequestCategorySubjectDto>();

            try
            {
                lst = Manager.All();
                ViewBag.AllRequestCategorySubject = lst;
                var objSysRequestCategory = GetDropDownList("SysRequestCategory");
                ViewBag.objSysRequestCategory = objSysRequestCategory;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost, ActionName("getRequestCategorySubjectById")]
        public async Task<JsonResult> getRequestCategorSubjectyById(string id)
        {
            SysRequestCategorySubjectManager Manager = new SysRequestCategorySubjectManager();
            RequestCategorySubjectDto dto = new RequestCategorySubjectDto();
            try
            {
                dto = Manager.GetById(Convert.ToInt32(id));
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestCategorySubject", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SearchRequestCategorySubject")]
        public async Task<JsonResult> SearchRequestCategorySubject(string requestcategory, string name, string status)
        {
            SysRequestCategorySubjectManager Manager = new SysRequestCategorySubjectManager();
            List<RequestCategorySubjectDto> lst = new List<RequestCategorySubjectDto>();
            try
            {
                lst = Manager.Search(requestcategory, name, status);
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestCategorySubject", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveRequestCategorySubject")]
        public async Task<JsonResult> SaveRequestCategorySubject(RequestCategorySubjectDto Requests)
        {
            SysRequestCategorySubjectManager Manager = new SysRequestCategorySubjectManager();
            RequestCategorySubjectDto dto = new RequestCategorySubjectDto();
            List<RequestCategorySubjectDto> lst = new List<RequestCategorySubjectDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "RequestCategorySubject", Level = "Low", Message = "Session Timeout." });
                }
                if (Requests.Id != null)
                {
                    Requests.Id = Requests.Id;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                bool result = Manager.Add(Requests);
                lst = Manager.All();

                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "RequestCategorySubject", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion

        #region Area
        public ActionResult Area()
        {
            SysAreaManager Manager = new SysAreaManager();

            List<AreaDto> lst = new List<AreaDto>();

            try
            {
                lst = Manager.All();
                ViewBag.AllArea = lst;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        [HttpPost, ActionName("getAreaById")]
        public async Task<JsonResult> getAreaById(string id)
        {
            SysAreaManager Manager = new SysAreaManager();
            AreaDto dto = new AreaDto();
            try
            {
                dto = Manager.GetById(Convert.ToInt32(id));
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Area", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("getAreByCode")]
        public async Task<JsonResult> getAreaByCode(string Code)
        {
            SysAreaManager Manager = new SysAreaManager();
            AreaDto dto = new AreaDto();
            try
            {
                dto = Manager.GetByCode(Code);
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Area", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SearchArea")]
        public async Task<JsonResult> SearchArea(string code, string name, string status)
        {
            SysAreaManager Manager = new SysAreaManager();
            List<AreaDto> lst = new List<AreaDto>();
            try
            {
                lst = Manager.Search(code, name, status);
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Area", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveArea")]
        public async Task<JsonResult> SaveArea(AreaDto Requests)
        {
            SysAreaManager Manager = new SysAreaManager();
            AreaDto dto = new AreaDto();
            List<AreaDto> lst = new List<AreaDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "Area", Level = "Low", Message = "Session Timeout." });
                }
                if (Requests.Id != null)
                {
                    Requests.Id = Requests.Id;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                bool result = Manager.Add(Requests);
                if (!result) throw new Exception("Duplicate area code.");

                lst = Manager.All();
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Area", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion

        #region Branch
        public ActionResult Branch()
        {
            SysBranchManager Manager = new SysBranchManager();
            List<BranchDto> lst = new List<BranchDto>();
            List<BranchDto> lstArea = new List<BranchDto>();
            try
            {
                lst = Manager.Search("","","","","","","","");
                foreach (BranchDto ss in lst)
                {
                    int year1 = Convert.ToInt32(ss.CreatedDate.Split('/')[2]);
                    if (year1 < 2500)
                    {
                        year1 = year1 + 543;
                    }
                    string CreatedDate = ss.CreatedDate.Split('/')[0] + '/' + ss.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
                    ss.CreatedDate = CreatedDate;

                    int year2 = Convert.ToInt32(ss.UpdatedDate.Split('/')[2]);
                    if (year2 < 2500)
                    {
                        year2 = year2 + 543;
                    }
                    string UpdatedDate = ss.UpdatedDate.Split('/')[0] + '/' + ss.UpdatedDate.Split('/')[1] + '/' + year2;// + ' ' + ss.UpdatedTime;
                    ss.UpdatedDate = UpdatedDate;
                }
                lstArea = lst.Where(e => e.TypeCode == "3" || e.TypeCode == "5").ToList();
                lstArea = lstArea.OrderBy(a => Convert.ToInt32(a.AreaCode)).ToList();
                ViewBag.AllBranch = lst;
                ViewBag.objSysArea = lstArea;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost, ActionName("getBranchById")]
        public async Task<JsonResult> getBranchById(string id)
        {
            SysBranchManager Manager = new SysBranchManager();
            BranchDto dto = new BranchDto();
            try
            {
                dto = Manager.GetById(Convert.ToInt32(id));
                Response.StatusCode = 200;
                return Json(dto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Branch", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SearchBranch")]
        public async Task<JsonResult> SearchBranch(string Id, string Code, string Name, string CCTR_CODE, string WW_CODE, string TypeCode, string Parent, string Status)
        {
            SysBranchManager Manager = new SysBranchManager();
            List<BranchDto> lst = new List<BranchDto>();
            try
            {
                lst = Manager.Search(Id, Code, Name, CCTR_CODE, WW_CODE, TypeCode, Parent, Status);
                foreach (BranchDto ss in lst)
                {
                    int year1 = Convert.ToInt32(ss.CreatedDate.Split('/')[2]);
                    if (year1 < 2500)
                    {
                        year1 = year1 + 543;
                    }
                    string CreatedDate = ss.CreatedDate.Split('/')[0] + '/' + ss.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
                    ss.CreatedDate = CreatedDate;

                    int year2 = Convert.ToInt32(ss.UpdatedDate.Split('/')[2]);
                    if (year2 < 2500)
                    {
                        year2 = year2 + 543;
                    }
                    string UpdatedDate = ss.UpdatedDate.Split('/')[0] + '/' + ss.UpdatedDate.Split('/')[1] + '/' + year2;// + ' ' + ss.UpdatedTime;
                    ss.UpdatedDate = UpdatedDate;
                }
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Branch", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveBranch")]
        public async Task<JsonResult> SaveBranch(BranchDto Requests)
        {
            SysBranchManager Manager = new SysBranchManager();
            BranchDto dto = new BranchDto();
            List<BranchDto> lst = new List<BranchDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "Branch", Level = "Low", Message = "Session Timeout." });
                }
                if (Requests.Id != null)
                {
                    Requests.Id = Requests.Id;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                bool result = Manager.Add(Requests);
                lst = Manager.Search("", "", "", "", "", "", "", "");

                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Branch", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion

        #region TemplateItem
        public ActionResult TemplateItem()
        {
            SysBranchManager Manager = new SysBranchManager();
            //List<BranchDto> lst = new List<BranchDto>();
            try
            {
                //lst = Manager.All();
                //ViewBag.AllBranch = lst;
                //var objSysArea = GetDropDownList("SysArea");
                //var newList = objSysArea.AsEnumerable().OrderBy(x => Convert.ToInt32(x.Value)).ToList();
                //ViewBag.objSysArea = newList;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public ActionResult ItemSet()
        {
            SysItemManager itemManager = new SysItemManager();

            List<ItemDto> lst = new List<ItemDto>();

            try
            {
                var objBranch = GetDropDownBranchList(CurrentUser.Ba);
                List<SysItem_SetDto> SysItemSetList = new List<SysItem_SetDto>();
                if (objBranch.Count() == 1)
                {
                    SysItemSetList = itemManager.GetSysItemSetAll(new SysItem_SetDto() { SysBranchID = objBranch[0].Id });
                }
                var SysItems = GetDropDownListItem("SysItemCodeName", objBranch[0].Id);
                var SysUnit = GetDropDownList("SysUnit");
                ViewBag.SysItems = SysItems;
                ViewBag.SysUnit = SysUnit;
                ViewBag.ItemSet = SysItemSetList;
                ViewBag.OwnerBranch = objBranch;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        [HttpPost, ActionName("GetBranchItemSet")]
        public async Task<JsonResult> GetBranchItemSet(string id)
        {
            SysItemManager itemManager = new SysItemManager();
            List<SysItem_SetDto> SysItemSetList = new List<SysItem_SetDto>();
            try
            {
                SysItemSetList = itemManager.GetSysItemSetAll(new SysItem_SetDto() { SysBranchID = id });
                
                Response.StatusCode = 200;
                return Json(SysItemSetList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "ItemSet", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }

        [HttpPost, ActionName("GetTemplate")]
        public async Task<JsonResult> GetTemplate(string id)
        {
            SysItemManager itemManager = new SysItemManager();
            TemplateHeader Template = new TemplateHeader();
            try
            {
                Template = itemManager.GetTemplate(id);

                Response.StatusCode = 200;
                return Json(Template);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "ItemSet", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("GetDropDownListItem")]
        public async Task<JsonResult> GetDropDownListItem(string id)
        {
            try
            {
                var SysItems = GetDropDownListItem("SysItemCodeName", id);

                Response.StatusCode = 200;
                return Json(SysItems);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "ItemSet", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }

        public JsonResult SaveTemplate()
        {
            var deserializer = new JavaScriptSerializer();
            SysItemManager manager = new SysItemManager();

            TemplateHeader header = null;
            var result = false;
            try
            {
                if (CurrentUser == null)
                {
                    //throw new Exception("Session Timeout.");
                    Response.StatusCode = 400;
                    Log("UpdateTemplate", "Session Timeout.");
                    return Json(new ErrorDto() { Page = "SaveTemplate", Level = "Low", Message = "Session Timeout." });
                }
                header = deserializer.Deserialize<TemplateHeader>(context.Request.Form["Obj"]);
                header.CreatedBy = CurrentUser.AccountId;
                header.UpdatedBy = CurrentUser.AccountId;
                if (header.FlagType != "D")
                {
                    if (header.SetName.Trim() == "")
                    {
                        //throw new Exception("Template Name is blank");
                        Response.StatusCode = 400;
                        Log("UpdateTemplate", "Template Name is blank");
                        return Json(new ErrorDto() { Page = "SaveTemplate", Level = "Low", Message = "Template Name is blank" });
                    }

                    if (header.Items.Count == 0)
                    {
                        //throw new Exception("Must have at least 1 item");
                        Response.StatusCode = 400;
                        Log("UpdateTemplate", "Must have at least 1 item");
                        return Json(new ErrorDto() { Page = "SaveTemplate", Level = "Low", Message = "Must have at least 1 item" });
                    }
                }
                foreach (TemplateDetail dt in header.Items)
                {
                    dt.CreatedBy = CurrentUser.AccountId;
                }
                SysItemManager itemManager = new SysItemManager();
                List<SysItem_SetDto> SysItemSetList = new List<SysItem_SetDto>();
                SysItemSetList = itemManager.GetSysItemSetAll(new SysItem_SetDto() { SysBranchID = header.SysBranchID });
                switch (header.FlagType)
                {
                    case "I":
                        foreach (SysItem_SetDto s in SysItemSetList)
                        {
                            if (header.SetName == s.SetName)
                            {
                                //throw new Exception("Already Exist Template name");
                                Response.StatusCode = 400;
                                Log("UpdateTemplate", "Already Exist Template name");
                                return Json(new ErrorDto() { Page = "SaveTemplate", Level = "Low", Message = "Already Exist Template name" });
                            }
                        }
                        result = manager.InsertTemplate(header);
                        break;
                    case "U":
                        result = manager.UpdateTemplate(header);
                        break;
                    case "D":
                        result = manager.DeleteTemplate(header);
                        break;
                    default: break;
                }
                Response.StatusCode = 200;
                return Json(result);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                Log("UpdateTemplate", ex.Message);
                //return Json(ex.Message)
                return Json(new ErrorDto() { Page = "UpdateTemplate", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion

        #region Item
        public ActionResult Item()
        {
            SysItemManager Manager = new SysItemManager();

            List<ItemDto> lst = new List<ItemDto>();

            try
            {
                lst = Manager.Search("","","","","1001"); //1001 BaCode Center
                foreach (ItemDto ss in lst)
                {
                    int year1 = Convert.ToInt32(ss.CreatedDate.Split('/')[2]);
                    if (year1 < 2500)
                    {
                        year1 = year1 + 543;
                    }
                    string CreatedDate = ss.CreatedDate.Split('/')[0] + '/' + ss.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
                    ss.CreatedDate = CreatedDate;

                    int year2 = Convert.ToInt32(ss.UpdatedDate.Split('/')[2]);
                    if (year2 < 2500)
                    {
                        year2 = year2 + 543;
                    }
                    string UpdatedDate = ss.UpdatedDate.Split('/')[0] + '/' + ss.UpdatedDate.Split('/')[1] + '/' + year2;// + ' ' + ss.UpdatedTime;
                    ss.UpdatedDate = UpdatedDate;
                }
                var objBranch = GetDropDownBranchList(CurrentUser.Ba);

                ViewBag.AllItem = lst;
                var objSysUnit = GetDropDownList("SysUnit");
                ViewBag.Unit = objSysUnit;
                ViewBag.OwnerBranch = objBranch;
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public ActionResult ItemBranch()
        {
            SysItemManager Manager = new SysItemManager();

            List<ItemDto> lst = new List<ItemDto>();
            List<ItemDto> lstItemCenter = new List<ItemDto>();

            try
            {
                if (CurrentUser.Ba == "1001")
                { }
                else
                {
                    lst = Manager.Search("", "", "", "", CurrentUser.Ba);
                    
                    foreach (ItemDto ss in lst)
                    {
                        int year1 = Convert.ToInt32(ss.CreatedDate.Split('/')[2]);
                        if (year1 < 2500)
                        {
                            year1 = year1 + 543;
                        }
                        string CreatedDate = ss.CreatedDate.Split('/')[0] + '/' + ss.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
                        ss.CreatedDate = CreatedDate;

                        int year2 = Convert.ToInt32(ss.UpdatedDate.Split('/')[2]);
                        if (year2 < 2500)
                        {
                            year2 = year2 + 543;
                        }
                        string UpdatedDate = ss.UpdatedDate.Split('/')[0] + '/' + ss.UpdatedDate.Split('/')[1] + '/' + year2;// + ' ' + ss.UpdatedTime;
                        ss.UpdatedDate = UpdatedDate;
                    }
                }
                var objBranch = GetDropDownBranchList(CurrentUser.Ba);

               
                var objSysUnit = GetDropDownList("SysUnit");
                SysBranchManager BrManager = new SysBranchManager();
                string BranchId = BrManager.GetByCode(CurrentUser.Ba).Id;
                //string BranchIdCenter = BrManager.GetByCode("1001").Id;
                //var lstItemCenter = GetDropDownListItem("SysItemCodeName", BranchIdCenter);
                lstItemCenter = Manager.Search("", "", "", "", "1001");

                List<ItemDto> lstNonAdd = new List<ItemDto>();
                ItemDto[] arr = new ItemDto[lstItemCenter.Count()];
                lstItemCenter.CopyTo(arr);
                lstNonAdd = arr.ToList();
                foreach (ItemDto lstCen in lstItemCenter)
                {
                    foreach (ItemDto ls in lst)
                    {
                        if (lstCen.ItemCode == ls.ItemCode)
                        {
                            lstNonAdd.Remove(lstCen);
                            break;
                        }
                    }
                }

                ViewBag.ItemCenter = lstNonAdd;
                ViewBag.AllItem = lst;
                ViewBag.OwnerBaCode = CurrentUser.Ba;
                ViewBag.Unit = objSysUnit;
                ViewBag.OwnerBranch = objBranch;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost, ActionName("SearchItem")]
        public async Task<JsonResult> SearchItem(string ItemId, string ItemCode, string ItemName, string Status, string BaCode)
        {
            SysItemManager Manager = new SysItemManager();
            List<ItemDto> lst = new List<ItemDto>();
            try
            {
                lst = Manager.Search(ItemId, ItemCode, ItemName, Status, BaCode);
                foreach (ItemDto ss in lst)
                {
                    int year1 = Convert.ToInt32(ss.CreatedDate.Split('/')[2]);
                    if (year1 < 2500)
                    {
                        year1 = year1 + 543;
                    }
                    string CreatedDate = ss.CreatedDate.Split('/')[0] + '/' + ss.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
                    ss.CreatedDate = CreatedDate;

                    int year2 = Convert.ToInt32(ss.UpdatedDate.Split('/')[2]);
                    if (year2 < 2500)
                    {
                        year2 = year2 + 543;
                    }
                    string UpdatedDate = ss.UpdatedDate.Split('/')[0] + '/' + ss.UpdatedDate.Split('/')[1] + '/' + year2;// + ' ' + ss.UpdatedTime;
                    ss.UpdatedDate = UpdatedDate;
                }
                List<ItemDto> lstItemCenter = new List<ItemDto>();
                lstItemCenter = Manager.Search("", "", "", "", "1001"); //1001 BaCode Center 
                List<ItemDto> lstNonAdd = new List<ItemDto>();
                ItemDto[] arr = new ItemDto[lstItemCenter.Count()];
                lstItemCenter.CopyTo(arr);
                lstNonAdd = arr.ToList();
                foreach (ItemDto lstCen in lstItemCenter)
                {
                    foreach (ItemDto ls in lst)
                    {
                        if (lstCen.ItemCode == ls.ItemCode)
                        {
                            lstNonAdd.Remove(lstCen);
                            break;
                        }
                    }
                }
                var jResult = Json(new { lst, lstNonAdd });

                Response.StatusCode = 200;
                return Json(jResult);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Item", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveItem")]
        public async Task<JsonResult> SaveItem(ItemDto Requests)
        {
            SysItemManager Manager = new SysItemManager();
            ItemDto dto = new ItemDto();
            List<ItemDto> lst = new List<ItemDto>();
            List<ItemDto> lstItemCenter = new List<ItemDto>();

            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "Item", Level = "Low", Message = "Session Timeout." });
                }

                if (Requests.ItemId != null)
                {
                    Requests.ItemId = Requests.ItemId;
                    Requests.ItemCode = Requests.ItemCode;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                SysBranchManager BrManager = new SysBranchManager();
                if(Requests.BaCode == "") throw new Exception("Please select branch.");
                Requests.BranchId = BrManager.GetByCode(Requests.BaCode).Id;

                //if (Requests.Action == "I" && Requests.ItemName != "")
                //{
                //    string ItemCodeName = Requests.ItemName.Replace(" - ", "|");
                //    string[] ItemCodeNames = ItemCodeName.Split('|');
                //    if (ItemCodeNames.Count() == 2)
                //    {
                //        Requests.ItemCode = ItemCodeNames[0];
                //        Requests.ItemName = ItemCodeNames[1];
                //    }

                //}
                bool result = Manager.Add(Requests);


                if (!result) throw new Exception("Duplicate Item code.");

                lst = Manager.Search("","","","", Requests.BaCode); // BaCode Branch/Area 
                lstItemCenter = Manager.Search("", "", "", "", "1001"); //1001 BaCode Center 
                List<ItemDto> lstNonAdd = new List<ItemDto>();
                ItemDto[] arr = new ItemDto[lstItemCenter.Count()];
                lstItemCenter.CopyTo(arr);
                lstNonAdd = arr.ToList();
                foreach (ItemDto lstCen in lstItemCenter)
                {
                    foreach (ItemDto ls in lst)
                    {
                        if (lstCen.ItemCode == ls.ItemCode)
                        {
                            lstNonAdd.Remove(lstCen);
                            break;
                        }
                    }
                }

                var jResult = Json( new { lst, lstNonAdd });

                Response.StatusCode = 200;
                return Json(jResult);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Item", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion

        #region Unit
        public ActionResult Unit()
        {
            SysUnitManager Manager = new SysUnitManager();

            List<UnitDto> lst = new List<UnitDto>();

            try
            {
                lst = Manager.Search("", "", "");

                foreach (UnitDto ss in lst)
                {
                    if (ss.CreatedDate == null || ss.CreatedDate == "") { }
                    else
                    {
                        int year1 = Convert.ToInt32(ss.CreatedDate.Split('/')[2]);
                        if (year1 < 2500)
                        {
                            year1 = year1 + 543;
                        }
                        string CreatedDate = ss.CreatedDate.Split('/')[0] + '/' + ss.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
                        ss.CreatedDate = CreatedDate;
                    }
                    if (ss.UpdatedDate == null || ss.UpdatedDate == "") { }
                    else
                    {
                        int year2 = Convert.ToInt32(ss.UpdatedDate.Split('/')[2]);
                        if (year2 < 2500)
                        {
                            year2 = year2 + 543;
                        }
                        string UpdatedDate = ss.UpdatedDate.Split('/')[0] + '/' + ss.UpdatedDate.Split('/')[1] + '/' + year2;// + ' ' + ss.UpdatedTime;
                        ss.UpdatedDate = UpdatedDate;
                    }
                }
                ViewBag.AllItem = lst;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        [HttpPost, ActionName("SearchUnit")]
        public async Task<JsonResult> SearchUnit(string UnitId, string UnitName, string Status)
        {
            SysUnitManager Manager = new SysUnitManager();
            List<UnitDto> lst = new List<UnitDto>();
            try
            {
                lst = Manager.Search(UnitId, UnitName, Status);
                foreach (UnitDto ss in lst)
                {
                    if (ss.CreatedDate == null || ss.CreatedDate == "") { }
                    else
                    {
                        int year1 = Convert.ToInt32(ss.CreatedDate.Split('/')[2]);
                        if (year1 < 2500)
                        {
                            year1 = year1 + 543;
                        }
                        string CreatedDate = ss.CreatedDate.Split('/')[0] + '/' + ss.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
                        ss.CreatedDate = CreatedDate;
                    }
                    if (ss.UpdatedDate == null || ss.UpdatedDate == "") { }
                    else
                    {
                        int year2 = Convert.ToInt32(ss.UpdatedDate.Split('/')[2]);
                        if (year2 < 2500)
                        {
                            year2 = year2 + 543;
                        }
                        string UpdatedDate = ss.UpdatedDate.Split('/')[0] + '/' + ss.UpdatedDate.Split('/')[1] + '/' + year2;// + ' ' + ss.UpdatedTime;
                        ss.UpdatedDate = UpdatedDate;
                    }
                }
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Unit", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveUnit")]
        public async Task<JsonResult> SaveUnit(UnitDto Requests)
        {
            SysUnitManager Manager = new SysUnitManager();
            UnitDto dto = new UnitDto();
            List<UnitDto> lst = new List<UnitDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "Unit", Level = "Low", Message = "Session Timeout." });
                }
                if (Requests.UnitId != null)
                {
                    Requests.UnitId = Requests.UnitId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                bool result = Manager.Add(Requests);
                if (!result) throw new Exception("Duplicate Unit code.");

                lst = Manager.Search("", "", "");
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Unit", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion
    }
}