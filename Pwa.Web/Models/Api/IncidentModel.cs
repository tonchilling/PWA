using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class IncidentModel 
    {
        public string complainant_id { get; set; }
        public string pwa_id { get; set; }
        public string meter_id { get; set; }
        public string title { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string citizen_id { get; set; }
        public string tel { get; set; }
        public string address_no { get; set; }
        public string street { get; set; }
        public string sub_district { get; set; }
        public string district { get; set; }
        public string province { get; set; }
        public string postal_code { get; set; }
        public string email { get; set; }
    }
}