using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class CustomerCisPostModel
    {
        public string authen { get; set; }
        public string custcode { get; set; }
    }

    public class CustomerWsResponse
    {
        public string message { get; set; }
        public CustomerInfoWsResponse customer { get; set; }
        public List<CustomerAddressWsResponse> address { get; set; }

    }

    public class CustomerInfoWsResponse
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

    public class CustomerAddressWsResponse
    {
        public string reg { get; set; }
        public string ba { get; set; }
        public string custcode { get; set; }
        public string custname { get; set; }
        public string custstatus { get; set; }
        public string address { get; set; }
        public string type { get; set; }
    }
}