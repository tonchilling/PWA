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
    public class BranchController : ApiController
    {
        //private string TraCkingUrl = System.Configuration.ConfigurationSettings.AppSettings["SmsUrl"];
        [HttpPost]
        public IHttpActionResult GetWwCode([FromBody]BranchDto br)
        {
            SysBranchManager Manager = new SysBranchManager();
            BranchDto lst = new BranchDto();
            try
            {
                string Result = "";
                if (br.BaCode == null || br.BaCode == "") 
                {
                    return Ok(new
                    {
                        resCode = "002",
                        msg = "Invalid BaCode",
                        WW_CODE = ""

                    });

                }
                else  
                {
                    lst = Manager.GetByCode(br.BaCode);

                }

                return Ok(new
                {
                    resCode = "001",
                    msg = "Success",
                    WW_CODE = lst.WW_CODE

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
