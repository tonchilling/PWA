using Pwa.FrameWork.ShortUrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pwa.Web.Controllers.ShortUrl
{
    public class ShortUrlController : Controller
    {


        [HttpGet]
        [Route("{token}")]
        public ActionResult Get(string token)
        {
            try
            {
                ShortUrlManager manager = new ShortUrlManager();

                var target = manager.GetShortUrlByToken(token);
                if (target != null)
                {
                    return Redirect(target.Url);
                }
                else
                {

                    return Redirect("https://stackoverflow.com/questions/52308563/redirect-to-url-in-asp-net-core/55383749");
                }
            }
            catch (Exception ex)
            {

                return Redirect("https://stackoverflow.com/questions/52308563/redirect-to-url-in-asp-net-core/55383749");
            }
        }

        
    }
}
