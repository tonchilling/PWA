using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class CurrentUser
    {
        public bool IsCallCenter { get; set; }
        public string AccountID { get; set; }
        public string BA { get; set; }
        public string RoleID { get; set; }
    }
}