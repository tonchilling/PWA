using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.IO;

using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Bal;
using Pwa.FrameWork.Bal.Smart1662;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Utils;
using System.Diagnostics;
using System.Web.Http;

namespace Pwa.Web.Controllers
{
    public class BaseController : Controller, IActionFilter
    {
      protected   string EncryptKey = System.Configuration.ConfigurationSettings.AppSettings["EncryptKey"];
        protected string JobPath = System.Configuration.ConfigurationSettings.AppSettings["JobPath"];
        protected string VirtualPath= System.Configuration.ConfigurationSettings.AppSettings["VirtualPath"];
        protected string EffectPath = System.Configuration.ConfigurationSettings.AppSettings["JobPath"];

        protected string CloseJobPath = System.Configuration.ConfigurationSettings.AppSettings["CloseJobPath"];
        protected string VirtualCloseJobPath = System.Configuration.ConfigurationSettings.AppSettings["VirtualCloseJobPath"];
        // public string MVCArea => Request.RequestContext.RouteData.DataTokens["area"]?.Convert2String();
        //  public string MVCController => Request.RequestContext.RouteData.Values["controller"].Convert2String("Home");
        //  public string MVCAction => Request.RequestContext.RouteData.Values["action"].Convert2String("Index");

        protected HttpContext context = null;

        public BaseController()
        {
            
            UserInfoDto userInfo = null;
            RNGCryptoServiceProvider rng_iv = new RNGCryptoServiceProvider();

            context = System.Web.HttpContext.Current;


            if (context.Session["userInfo"] == null && GetPageName.ToLower() != "login")
            {
                Log("BaseController", "Not have session");
            }
            else if (context.Session["userInfo"] == null)
            {
                 RedirectToAction("Login", "User");
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (context.Session["userInfo"] == null
                && GetPageName.ToLower() != "login"   && filterContext.ActionDescriptor.ActionName!= "VerifyLogin"
                )
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                                new
                                {
                                    controller = "User",
                                    action = "Login"
                                }));

            }
     
         
                return;

 }


        public string GetGeoSearch
        {
            get
            {
                return System.Configuration.ConfigurationSettings.AppSettings["GeoSearch"];
            }
        }

        public UserInfoDto CurrentUser {
            get {
                return (UserInfoDto)context.Session["userInfo"];
            }
        }


        public MenuLayoutDto GetMenu()
        {
            SysMenuManager manager = new SysMenuManager();
            MenuLayoutDto menuLayout = null;
            List<MenuDto> sysMenus = null;
           
            if (context.Session["menu"] == null && CurrentUser != null)
            {
                menuLayout = new MenuLayoutDto();
                sysMenus =  manager.GetAllMenu(Converting.ToInt(CurrentUser.RoleId));
                menuLayout.Menus = new List<MenuDto>();
              //  sysMenus =  manager.GetSysMenus();
                foreach (MenuDto menu in sysMenus.Where(o => o.ParentId == null  ))
                {
                    menuLayout.Menus.Add(ConvertToMenuModel(menu, sysMenus));
                }

            }
            else
            {
                menuLayout = (MenuLayoutDto)context.Session["menu"];

            }

            return menuLayout;
        }

        protected List<DropDownlistDto> GetDropDownList(string tableName)
        {
            List<DropDownlistDto> respList = null;
           UtilManager bal = new UtilManager();
            respList = bal.GetDropDownList(tableName);

            

            return respList;
        }
        protected List<DropDownlistDto> GetDropDownListItem(string tableName, string BranchID)
        {
            List<DropDownlistDto> respList = null;
            UtilManager bal = new UtilManager();
            respList = bal.GetDropDownListItem(tableName, BranchID);



            return respList;
        }
        protected List<DropDownlistDto> GetDropDownBranchList(string BaCode)
        {
            List<DropDownlistDto> respList = null;
            UtilManager bal = new UtilManager();
            respList = bal.GetDropDownBranchList(BaCode);



            return respList;
        }
        private MenuDto ConvertToMenuModel2(SysMenu ob)
        {
            MenuDto rs = new MenuDto()
            {
                MenuId = ob.MenuId,
                MenuIcon = ob.MenuIcon,
                MenuName = ob.MenuName,
                MenuOrder = ob.MenuOrder,
                MvcArea = ob.MvcArea,
                MvcController = ob.MvcController,
                MvcAction = ob.MvcAction,
                //  SubMenus = ob.InverseParent.Select(x => ConvertToMenuModel(x)).OrderBy(o => o.MenuOrder).ToList()
            };
            return rs;

        }

        private MenuDto ConvertToMenuModel2(MenuDto ob)
        {
            MenuDto rs = new MenuDto()
            {
                MenuId = ob.MenuId,
                MenuIcon = ob.MenuIcon,
                MenuName = ob.MenuName,
                MenuOrder = ob.MenuOrder,
                MvcArea = ob.MvcArea,
                MvcController = ob.MvcController,
                MvcAction = ob.MvcAction,
                //  SubMenus = ob.InverseParent.Select(x => ConvertToMenuModel(x)).OrderBy(o => o.MenuOrder).ToList()
            };
            return rs;

        }

        private MenuDto ConvertToMenuModel(SysMenu ob, List<SysMenu> menuAll)
        {
            MenuDto rs = new MenuDto()
            {
                MenuId = ob.MenuId,
                MenuIcon = ob.MenuIcon,
                MenuName = ob.MenuName,
                MenuOrder = ob.MenuOrder,
                MvcArea = ob.MvcArea,
                MvcController = ob.MvcController,
                MvcAction = ob.MvcAction,
                SubMenus = menuAll.Where(o => o.ParentId == ob.MenuId).Select(x => ConvertToMenuModel2(x)).OrderBy(o => o.MenuOrder).ToList()
            };
            return rs;
        }
        private MenuDto ConvertToMenuModel(MenuDto ob, List<MenuDto> menuAll)
        {
            MenuDto rs = new MenuDto()
            {
                MenuId = ob.MenuId,
                MenuIcon = ob.MenuIcon,
                MenuName = ob.MenuName,
                MenuOrder = ob.MenuOrder,
                MvcArea = ob.MvcArea,
                MvcController = ob.MvcController,
                MvcAction = ob.MvcAction,
                SubMenus = menuAll.Where(o => o.ParentId == ob.MenuId.ToString()).Select(x => ConvertToMenuModel2(x)).OrderBy(o => o.MenuOrder).ToList()
            };
            return rs;
        }

        public string GetPageName {
            get {
                return System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["Action"].ToString();
            }
        }

        public void Log(string methodName,string msg)
        {
            string userLogin = "Anonymous";
           

            LogBal.Log(methodName, userLogin, msg);
         
        }

        public bool IsMobileBrowser()
        {
            var userAgent = HttpContext.Request.UserAgent;
            if (HttpContext.Request.Browser.IsMobileDevice)
            {
                return true;
            }

            return false;
        }

        public string UserAgent(HttpContext request)
        {
            return request.Request.Headers["User-Agent"];
        }
        public string PathViewShared(string FileName)
        {
            return null;
         /*   return string.Format("~/Views/Shared/{2}",
               MVCController,
                (MVCArea + "/"),
                string.IsNullOrWhiteSpace(FileName) ? "" : FileName + ".cshtml");
                */
        }

        public bool IsCurrUserAsCallcenter
        {
            get
            {
                string[] callcenterRoles = new string[] { "200", "201" };
                return callcenterRoles.Contains(CurrentUser.RoleId);
            }

        }

        public List<SysMessagesDto> GetMessages()
        {
            SysMessagesIncidentManager managerInc = new SysMessagesIncidentManager();
            //Debug.WriteLine("getMessages CurrentUser.Ba  " + CurrentUser.Ba);
            var messages = managerInc.GetMessagesIncident(CurrentUser.Ba).OrderByDescending(o => o.CreatedDate).ToList();
            //.OrderByDescending(o => o.UpdatedDate).ToList();
            //Debug.WriteLine("getMessages messages count: " + messages.Count());
            return messages;
        }

        public void UpdateReadMessage(string PwaIncidentNo, string BranchNo)
        {
            SysMessagesIncidentManager managerInc = new SysMessagesIncidentManager();
            Debug.WriteLine("UpdateReadMessage: " + PwaIncidentNo + "  " + BranchNo);
            managerInc.UpdateMessagesIncident(PwaIncidentNo, 1, BranchNo);
            return;
        }

        public void InsertMessage(string PwaIncidentNo, string BranchNo)
        {
            /*Hot fix
             We found error when 'PwaIncidentNo' is empty
             */
            if (PwaIncidentNo==null || PwaIncidentNo.Trim() == "")
            {
                return;
            }

            SysMessagesIncidentManager managerInc = new SysMessagesIncidentManager();
            //Debug.WriteLine("getMessages CurrentUser.Ba  " + CurrentUser.Ba);
            managerInc.UpdateMessagesIncident(PwaIncidentNo, 0, BranchNo);
            return;
            //.OrderByDescending(o => o.UpdatedDate).ToList();
            //Debug.WriteLine("getMessages messages count: " + messages.Count());
        }

        private IHttpActionResult Ok(object p)
        {
            throw new NotImplementedException();
        }
    }

}


