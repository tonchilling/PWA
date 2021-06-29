using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class SearchBranchIncident
    {
        public string CustomerNo { get; set; }
        public string ProvinceID { get; set; }
        public string Detail { get; set; }
        public string Area { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }


    }
}