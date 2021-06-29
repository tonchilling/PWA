using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Bal;
using Pwa.FrameWork.Bal.Smart1662;
namespace Pwa.Web.Controllers.Mobile
{
    public class PwaMobileController : Controller
    {
        // GET: PwaMobile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {


            var SysBranch =GetDropDownList("SysBranch");
            ViewBag.SysBranch = SysBranch;
            return View();
        }

         List<DropDownlistDto> GetDropDownList(string tableName)
        {
            List<DropDownlistDto> respList = null;
            UtilManager bal = new UtilManager();
            respList = bal.GetDropDownList(tableName);



            return respList;
        }

    }
}