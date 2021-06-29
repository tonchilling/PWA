using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class IncidentFollowHistoryModel
    {
         public string Date { get; set; }
         public string Channel { get; set; }
        public string Detail { get; set; }
    }


    public class BranchRejectHistoryModel
    {
        public string Date { get; set; }
        public string Branch { get; set; }
        public string Detail { get; set; }
    }
}