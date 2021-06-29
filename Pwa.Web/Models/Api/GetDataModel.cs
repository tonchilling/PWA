using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class GetDataModel
    {
        public string Module { get; set; }
        public string ParentID { get; set; }

        public string Word { get; set; }
    }
}