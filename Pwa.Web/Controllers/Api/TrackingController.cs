using Pwa.FrameWork.Bal.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Utilities;
using Pwa.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pwa.Web.Controllers.Api
{
    //[RoutePrefix("api/TrackingHeader")]
    public class TrackingController : ApiController
    {
        //private string TraCkingUrl = System.Configuration.ConfigurationSettings.AppSettings["SmsUrl"];
        [HttpPost]
        public IHttpActionResult Search([FromBody]InputTrackingDto Model)
        {
            TrackingManager Manager = new TrackingManager();
            TrackingHeaderDto lst = new TrackingHeaderDto();
            try
            {
                string Result = "";
                if (Model == null) 
                {
                    return Ok(new
                    {
                        resCode = "002",
                        msg = "Invalid Tracking Number",
                        result = ""

                    });

                }
                else if (Model.TrackingNo == "" || Model.TrackingNo == null)
                {
                    return Ok(new
                    {
                        resCode = "002",
                        msg = "Invalid Tracking Number",
                        result = ""

                    });

                }
                else   ///tracking not null
                {
                    lst = Manager.Search(Model.TrackingNo);
                    //if (lst != null)
                    //{
                        
                    //    Result = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(lst);
                    //}
                    var ccc = Ok(new
                    {
                        resCode = "001",
                        msg = "Success",
                        result = lst

                    });

                }

                return Ok(new
                {
                    resCode = "001",
                    msg = "Success",
                    result = lst

                });
            }
            catch (Exception ex)
            {

                return Ok(new
                {
                    Msg = ex.Message,
                    Strack = ex.StackTrace
                });
            }


        }
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
