using Pwa.FrameWork.ShortUrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pwa.Web.Controllers.ShortUrl
{
    public class SController : Controller
    {
        

        // GET: S/Details/5
        [HttpGet]
        [Route("{id}")]
        public ActionResult G(string id)
        {
            try
            {
                ShortUrlManager manager = new ShortUrlManager();

                var target = manager.GetShortUrlByToken(id);
                if (target != null)
                {
                    return Redirect(target.Url);
                }
                else
                {

                    return Redirect("https://www.google.com/abc");
                }
            }
            catch (Exception ex)
            {

                return Redirect("https://www.google.com/abc");
            }
        }

        
    }
}
