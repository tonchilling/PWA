using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pwa.FrameWork.Dto;
using System.Threading.Tasks;
using System.Web.Helpers;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Bal.Smart1662;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dao.EF.Smart1662;
using System.Net;
using System.Text;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pwa.FrameWork.Utilities;

namespace Pwa.Web.Controllers
{
    public class UserController : BaseController
    {
        SysAccountManager sysAccountManager = null;
        SysRolPermissionManager sysRoleManager = null;
        UserInfoDto userInfoDto = null;
        SysAccountDto sysAccount = null;
       List<SysAccountDto> sysAccountList = null;
        List<SysRolePermissionDto> sysRolePermissionList = null;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RoleAutorize()
        {

            var sysRoleList = GetDropDownList("SysRole");
            ViewData["objSysRole"] = sysRoleList;
            return View();
        }

        public ActionResult Account()
        {

            var sysRoleList = GetDropDownList("SysRole");
            ViewData["objSysRole"] = sysRoleList;
            return View();
        }



        #region Login Method

        [HttpPost, ActionName("VerifyLogin")]
        public async Task<JsonResult> VerifyLogin(UserInfoDto RequestDto)
        {
            bool success = false;
            try
            {
                SysBranchManager brm = new SysBranchManager();
                String Api_Login_Url = ConfigurationManager.AppSettings.Get("Api_Login_Url").ToString();
                String tokenString = ConfigurationManager.AppSettings.Get("tokenString").ToString();
                //“SMART 1662YYYY-mm-dd”
                string token = "";
                string tokenInput = "";
                DateTime now = DateTime.Now;
                string dd = now.Day.ToString("00");
                string mm = now.Month.ToString("00");
                string testEmail = "";
                var yyyy = now.Year;
                if (yyyy > 2500) yyyy = yyyy - 543;
                tokenInput = tokenString + yyyy + "-" + mm + "-" + dd;
                sysAccountManager = new  SysAccountManager();
                userInfoDto = new UserInfoDto();
                if (RequestDto.NonEmp == "1")
                {
                    RequestDto.Password = DEncrypt.encrypt(RequestDto.Password, EncryptKey);
                    userInfoDto = await sysAccountManager.Login(RequestDto);

                    testEmail = userInfoDto.AccountEmail;
                }
                else if (RequestDto.NonEmp == "0")
                {
                    token = DEncrypt.encryptSHA1(tokenInput);
                    WebClient client = new WebClient();
                    client.Encoding = UTF8Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    System.Collections.Specialized.NameValueCollection data =
                        new System.Collections.Specialized.NameValueCollection()
                        {
                            { "u" , RequestDto.UserLogin },
                            { "p" , RequestDto.Password },
                            { "token" , token }
                        };
                    Byte[] byteRequest = client.UploadValues(Api_Login_Url, "POST", data);
                    System.Text.ASCIIEncoding encodingUTF8 = new System.Text.ASCIIEncoding();
                    string resultTemp = encodingUTF8.GetString(byteRequest);
                    string strResult = System.Text.RegularExpressions.Regex.Unescape(resultTemp);
                    strResult = strResult.Replace(";", "");
                    while (strResult.Substring(0, 1) != "{")
                    {
                        strResult = strResult.Substring(1, strResult.Length - 2);
                    }
                    JObject jObj = JObject.Parse(strResult);
                    //string ssss = Result1["Myposition"].ToString();
                    //UserInfoDto printObj = JsonConvert.DeserializeObject<UserInfoDto>(jsObj.ToString());
                    if (jObj["check"] == null) return Json(new ErrorDto() { Page = "Login", Level = "Heigh", Message = jObj["ErrMsg"].ToString() });

                    userInfoDto = await sysAccountManager.Login(RequestDto);
                    SysAccountDto accDto = new SysAccountDto();
                    BranchDto br = new BranchDto();

                    if (userInfoDto == null)
                    {
                        accDto.UserLogin = Convert.ToString(jObj["username"]);

                        if (jObj["MyName"] != null && jObj["MyName"].ToString() != "") accDto.AccountFirstName = Convert.ToString(jObj["MyName"]);
                        else accDto.AccountFirstName = "FName" + accDto.UserLogin;

                        if (jObj["MySurname"] != null && jObj["MySurname"].ToString() != "") accDto.AccountLastName = Convert.ToString(jObj["MySurname"]);
                        else accDto.AccountLastName = "LName" + accDto.UserLogin;

                        //accDto.AccountLastName = jObj["MySurname"].ToString();

                        accDto.AccountEmail = Convert.ToString(jObj["email"]);
                        accDto.FlagStatus = "1";
                        accDto.FlagSystem = "1";
                        accDto.FlagAdminCalc = "1";
                        accDto.Ba = Convert.ToString(jObj["Myba"]);
                        string jobname = Convert.ToString(jObj["MyJob_name"]);
                        string position = Convert.ToString(jObj["Myposition"]);
                        br = brm.GetByCode(accDto.Ba);
                        accDto.RoleId = "";
                        switch (br.TypeCode)
                        {
                            case "3"://เขต
                                accDto.RoleId = "204"; 
                                break;
                            case "4"://สาขา
                                if (jobname.Trim() == "งานบริการและควบคุมน้ำสูญเสีย" || jobname.Trim() == "งานบริการและควบคุมน้ำสูญเสีย 2")
                                {
                                    if (position.Trim() == "หัวหน้างาน") accDto.RoleId = "207";
                                    else accDto.RoleId = "203";
                                }
                                else if (jobname.Trim() == "งานอำนวยการ" || jobname.Trim() == "งานผลิต")
                                {
                                    if (position.Trim() == "หัวหน้างาน") accDto.RoleId = "206";
                                    else accDto.RoleId = "202";
                                }
                                else if (jobname.Trim() == "งานผลิต") accDto.RoleId = "202";
                                break;
                            case "5"://ส่วนกลาง
                                accDto.RoleId = "205"; 
                                break;
                        }
                        //if (accDto.RoleId != "")
                        {
                            bool result_add_account = sysAccountManager.Add(accDto);
                            if (result_add_account)
                            {
                                userInfoDto = await sysAccountManager.Login(RequestDto);
                                testEmail = userInfoDto.AccountEmail;
                            }
                            else
                            {
                                Response.StatusCode = 400;
                                return Json(new ErrorDto() { Page = "Login", Level = "Heigh", Message = "Not Authorize"});
                            }

                        }

                        
                        //Response.StatusCode = 400;
                        //return Json(new ErrorDto() { Page = "Login", Level = "Heigh", Message = "Not access to this app, Please contact admin." });
                    }

                   /* if (br != null)
                        userInfoDto.TypeCode = br.TypeCode;*/

                    if (jObj["MyName"] != null && Convert.ToString(jObj["MyName"]) != "") userInfoDto.AccountFirstName = Convert.ToString(jObj["MyName"]);
                    if (jObj["MySurname"] != null && Convert.ToString(jObj["MySurname"]) != "") userInfoDto.AccountLastName = Convert.ToString(jObj["MySurname"]);
                    
                    userInfoDto.AccountEmail = Convert.ToString(jObj["email"]);

                    userInfoDto.Position = Convert.ToString(jObj["Myposition"]);
                    userInfoDto.Level = Convert.ToString(jObj["Mylevel"]);
                    userInfoDto.CostCenter = Convert.ToString(jObj["Mycostcenter"]);
                    userInfoDto.Ba = Convert.ToString(jObj["Myba"]);
                    userInfoDto.Area = Convert.ToString(jObj["MyArea"]);
                    userInfoDto.JobName = Convert.ToString(jObj["MyJob_name"]);
                    userInfoDto.DivName = Convert.ToString(jObj["MyDiv_name"]);
                    userInfoDto.DepName = Convert.ToString(jObj["MyDep_name"]);
                    userInfoDto.OrgName = Convert.ToString(jObj["MyOrg_name"]);
                    BranchDto branchDto= brm.GetByCode(userInfoDto.Ba);
                    userInfoDto.WW_CODE = branchDto.WW_CODE;
                    userInfoDto.AreaBaCode = branchDto.AreaBACode;
                    //userInfoDto = JsonConvert.DeserializeObject<UserInfoDto>(jObj.ToString());
                }

                userInfoDto.AccountEmail = testEmail;
                if (userInfoDto != null)
                {
                    userInfoDto.Status = "1";
                    context.Session.Add("userInfo", userInfoDto);
                }
                Response.StatusCode =200;
                return Json(userInfoDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page="Login", Level="Heigh", Message=ex.Message });
            }
            finally
            { }



        }


        [HttpPost, ActionName("GetRoles")]
        public async Task<JsonResult> GetRoles(SearchDTO dto)
        {
            bool succss = false;
            try
            {

                sysRoleManager = new SysRolPermissionManager();

                sysRolePermissionList = await sysRoleManager.GetRoles(dto.Id);
             
                Response.StatusCode = 200;
                return Json(sysRolePermissionList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "GetRoles", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }



        }

        #endregion

        #region RoleAutorize Method
        [HttpPost, ActionName("SaveRoles")]
        public async Task<JsonResult> SaveRoles(List<SysRolePermissionDto> Requests,string roleId)
        {
            bool succss = false;
            try
            {

                sysRoleManager = new SysRolPermissionManager();
                sysRoleManager.Add(Requests);
                sysRolePermissionList = await sysRoleManager.GetRoles(roleId);

                Response.StatusCode = 200;
                return Json(sysRolePermissionList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "Login", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }



        }

        #endregion

        #region Account Method
        [HttpPost, ActionName("GetAccounts")]
        public async Task<JsonResult> GetAccounts(SearchDTO dto)
        {
            bool succss = false;
            try
            {

                sysAccountManager = new SysAccountManager();

                sysAccountList = await sysAccountManager.GetAll();

                Response.StatusCode = 200;
                return Json(sysAccountList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "GetAccounts", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }



        }


        [HttpPost, ActionName("GetAccountById")]
        public async Task<JsonResult> GetAccountById(SearchDTO dto)
        {
            bool succss = false;
            try
            {

                sysAccountManager = new SysAccountManager();

                sysAccount = await sysAccountManager.GetById(dto.AccountId);

                Response.StatusCode = 200;
                return Json(sysAccount);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "GetAccountById", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }



        }
        #endregion

        #region UserManagement
        public ActionResult UserManagement()
        {
            sysAccountManager = new SysAccountManager();
            SysAccountDto acc = new SysAccountDto();
            List<SysAccountDto> lst = new List<SysAccountDto>();
            try
            {
                lst = sysAccountManager.Search("", "", "", "", "");

                foreach (SysAccountDto ss in lst)
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
                var objBranch = GetDropDownList("SysBranch");

                ViewBag.Branch = objBranch;
                ViewBag.ListAccount = lst;
            }
            catch (Exception ex)
            {
                
            }
            return View();
        }
        public ActionResult UserManagementEdit()
        {
            sysAccountManager = new SysAccountManager();
            SysAccountDto acc = new SysAccountDto();
            try
            {
                var hddAction = Request.Form["hdd_action"];
                var hddAccountId = Request.Form["hdd_accountid"];
                var urlPreviousPage = Request.UrlReferrer.AbsoluteUri;
                //var urlPreviousPage2 = Request.ServerVariables["HTTP_REFERER"];
                var urlCurrent = Request.Url.AbsoluteUri;
                if (urlPreviousPage.ToLower() != urlCurrent.ToLower().Replace("edit", "")) return View("Login");

                switch (hddAction)
                {
                    case "I":
                        
                        break;
                    case "U":
                        
                        acc = sysAccountManager.Search(hddAccountId,"","","","").FirstOrDefault();
                        
                        {
                            int year1 = Convert.ToInt32(acc.CreatedDate.Split('/')[2]);
                            if (year1 < 2500)
                            {
                                year1 = year1 + 543;
                            }
                            string CreatedDate = acc.CreatedDate.Split('/')[0] + '/' + acc.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
                            acc.CreatedDate = CreatedDate;

                            int year2 = Convert.ToInt32(acc.UpdatedDate.Split('/')[2]);
                            if (year2 < 2500)
                            {
                                year2 = year2 + 543;
                            }
                            string UpdatedDate = acc.UpdatedDate.Split('/')[0] + '/' + acc.UpdatedDate.Split('/')[1] + '/' + year2;// + ' ' + ss.UpdatedTime;
                            acc.UpdatedDate = UpdatedDate;
                        }
                        break;
                    default:
                        break;
                }
                var objBranch = GetDropDownList("SysBranch");
                var objRole = GetDropDownList("SysRole");
                ViewBag.Branch = objBranch;
                ViewBag.Role = objRole;
                ViewBag.Account = acc;
            }
            catch (Exception ex)
            {
                return View("Login");

            }
            return View();
        }

        [HttpPost, ActionName("SearchUser")]
        public async Task<JsonResult> SearchUser(string AccountId, string UserLogin, string Name, string Status, string BaCode)
        {
            sysAccountManager = new SysAccountManager();
            SysAccountDto acc = new SysAccountDto();
            List<SysAccountDto> lst = new List<SysAccountDto>();
            try
            {

                lst = sysAccountManager.Search(AccountId, UserLogin, Name, Status, BaCode);

                foreach (SysAccountDto ss in lst)
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
                return Json(new ErrorDto() { Page = "UserManagement", Level = "Low", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("SaveAccount")]
        public async Task<JsonResult> SaveAccount(SysAccountDto Requests)
        {
            SysAccountManager Manager = new SysAccountManager();
            SysAccountDto dto = new SysAccountDto();
            List<SysAccountDto> lst = new List<SysAccountDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "UserAccount", Level = "Low", Message = "Session Timeout." });
                }
                if (Requests.AccountId != null)
                {
                    Requests.AccountId = Requests.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                else
                {
                    Requests.CreatedBy = CurrentUser.AccountId;
                    Requests.UpdatedBy = CurrentUser.AccountId;
                }
                if (Requests.Password == null) Requests.Password = "";
                else Requests.Password = DEncrypt.encrypt(Requests.Password, EncryptKey);
                bool result = Manager.SaveAccount(Requests);
                if (!result) throw new Exception("Duplicate Unit code.");

                //lst = Manager.Search("", "", "");
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "UserAccount", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        [HttpPost, ActionName("DeleteAccount")]
        public async Task<JsonResult> DeleteAccount(SysAccountDto Requests)
        {
            SysAccountManager Manager = new SysAccountManager();
            SysAccountDto dto = new SysAccountDto();
            List<SysAccountDto> lst = new List<SysAccountDto>();
            try
            {
                if (CurrentUser == null)
                {
                    Response.StatusCode = 400;
                    return Json(new ErrorDto() { Page = "UserAccount", Level = "Low", Message = "Session Timeout." });
                }

                bool result = Manager.DeleteAccount(Requests);
                lst = Manager.Search("", "", "", "", "");
                Response.StatusCode = 200;
                return Json(lst);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new ErrorDto() { Page = "UserAccount", Level = "Heigh", Message = ex.Message });
            }
            finally
            { }
        }
        #endregion

    }
}