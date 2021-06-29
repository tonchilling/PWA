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
using Pwa.FrameWork.Dto.PWA;
namespace Pwa.Web.Controllers.Api
{
    public class UtilController : ApiController
    {

       private string branchUrl = System.Configuration.ConfigurationSettings.AppSettings["Api_Branch_Url"];
        [HttpPost]
        public async Task<IHttpActionResult>  GetBranch(GeosearchCdgAutocompleteModel model)
        {
            string query = "";
            List<PWABranchDTO> jObj =null ;
         HttpResponseMessage responseMessage = null;
            try
            {
                query = string.Format("lat={0}&lng={1}", model.Lat,model.Lng);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(branchUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json; charset=utf-8") );
                    responseMessage = await  client.GetAsync(branchUrl + query);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var response = Request.CreateResponse(HttpStatusCode.OK);
                        //   response.Content = responseMessage.cont;
                        var value = responseMessage.Content.ReadAsStringAsync().Result;
                 
                        string strResult = System.Text.RegularExpressions.Regex.Unescape(value);
                         jObj = JsonHelper.Deserialize<List<PWABranchDTO>>(strResult);

                        if (jObj != null && jObj.Count > 0)
                        {
                            jObj[0].lat = model.Lat;

                            jObj[0].lng = model.Lng;
                        }
                    }
                }

          
                return Json(jObj);
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
            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                result = reader.ReadToEnd();

            }

            // Close the response.
            response.Close();

            return result;
        }
        private string Get(string url, string data)
        {
            string result = "";
            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "Get";

            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                result = reader.ReadToEnd();

            }

            // Close the response.
            response.Close();

            return result;
        }

    }
}
