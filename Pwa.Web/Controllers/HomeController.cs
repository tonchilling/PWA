using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Pwa.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //ViewBag.MessagesIncident = getMessages();
            return View("_layout");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}