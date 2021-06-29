using Pwa.FrameWork.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Service
{
    public class CisServices
    {
        public CisCustomer GetCustomer(string customerCode)
        {
            string url = "https://ws-app.pwa.co.th/customer/address";
            string token = "PWA:9D4i6eg1tyD100yt8opHay03h58t";


            string data = JsonHelper.Serialize(new CisCustomerReq
            {
                authen = token,
                custcode = customerCode
            });
            var jsonResp = Post(url, data); 

            var resp = JsonHelper.Deserialize<CisCustomerResp>(jsonResp);

            return (resp != null) ? resp.customer : null;

           


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

    }

   
    public class CisCustomerReq
    {
        public string authen { get; set; }
         public string custcode { get; set; }
    }
    
    public class CisCustomerResp
    {
        public string message { get; set; }
        public CisCustomer customer { get; set; }
    }

    public class CisCustomer
    {
    
        public string custcode { get; set; }
        public string custname { get; set; }
        public string custstatus { get; set; }
        public string card_id { get; set; }
        public string corperate_no { get; set; }
        public string address { get; set; }
        public string person_type { get; set; }
        public string account_no { get; set; }
        public string use_type { get; set; }
        public string use_name { get; set; }
        public string meter_size { get; set; }
        public string meter_status { get; set; }
        public string terminate_contract_date { get; set; }
        public string type { get; set; }


    }

}
