using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class GeosearchCdgAutocompleteModel
    {
        public string keyword { get; set; }
        public string key { get; set; }
        public string maxResult { get; set; }
        public string Lat { get; set; }

        public string Lng { get; set; }

    }
}