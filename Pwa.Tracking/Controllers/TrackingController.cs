
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pwa.Tracking.Models;

namespace Pwa.Tracking.Controllers
{
    public class TrackingController : Controller
    {
        // GET: Tracking
        public ActionResult Index()
        {
            var tracking_no = Request.QueryString["tracking_no"];
            ViewBag.tracking_no = tracking_no;
            return View();
        }
        //public ActionResult Tracking()
        //{
        //    var tracking_no = Request.QueryString["tracking_no"];
        //    ViewBag.tracking_no = tracking_no;
        //    return View();
        //}


        //[HttpPost, ActionName("GetTrackingHeader")]
        //public async Task<JsonResult> GetTrackingHeader(string TrackingNo)
        //{
        //    bool success = false;
        //    try
        //    {
        //        String Api_Url = ConfigurationManager.AppSettings.Get("Api_Url").ToString();
        //        //String Api_Url = "http://localhost:26189/api/Tracking/Search";
        //        //“SMART 1662YYYY-mm-dd”
        //        string token = "";
        //        string tokenInput = "";
        //        DateTime now = DateTime.Now;
        //        string dd = now.Day.ToString("00");
        //        string mm = now.Month.ToString("00");
        //        var yyyy = now.Year;
        //        if (yyyy > 2500) yyyy = yyyy - 543;

        //        WebClient client = new WebClient();
        //        client.Encoding = UTF8Encoding.UTF8;
        //        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        //        object input = new
        //        {
        //            TrackingNo = TrackingNo
        //        };
        //        string inputJson = (new JavaScriptSerializer()).Serialize(input);
        //        client.Headers["Content-type"] = "application/json";
        //        client.Encoding = Encoding.UTF8;
        //        string result1 = client.UploadString(Api_Url, inputJson);
        //        JObject jObj = JObject.Parse(result1);
        //        TrackingHeaderDto TrackingHeader = new TrackingHeaderDto();
        //        TrackingHeader = JsonConvert.DeserializeObject<TrackingHeaderDto>(jObj["result"].ToString());
        //        foreach (TrackingDetailDto ss in TrackingHeader.TrackingDetail)
        //        {
        //            int year1 = Convert.ToInt32(ss.CreatedDate.Split('/')[2]);
        //            if (year1 < 2500)
        //            {
        //                year1 = year1 + 543;
        //            }
        //            string CreatedDate = ss.CreatedDate.Split('/')[0] + '/' + ss.CreatedDate.Split('/')[1] + '/' + year1;// + ' ' + ss.UpdatedTime;
        //            ss.CreatedDate = CreatedDate;
        //        }


        //        Response.StatusCode = 200;
        //        return Json(TrackingHeader);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 400;
        //        return Json(new { Error="002", Message = ex.Message });
        //    }
        //    finally
        //    { }



        //}
        private string Post(string url, string data)
        {
            string result = "";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();

            }
            response.Close();
            return result;
        }

    }
}