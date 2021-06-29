﻿using Pwa.FrameWork.Utilities;
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
    public class GeosearchCdgController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AutoComplete([FromBody] GeosearchCdgAutocompleteModel model )
        {

            try
            {
                string url = "https://geosearch.cdg.co.th/g/search/autocomplete";
                string data = JsonHelper.Serialize(model);

                return Json(Post(url, data));
            }
            catch (Exception ex)
            {

                return Ok(new
                {
                    Msg= ex.Message,
                    Strack = ex.StackTrace
                });
            }

            
        }

        private string Post(string url,string data)
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


    }
}
