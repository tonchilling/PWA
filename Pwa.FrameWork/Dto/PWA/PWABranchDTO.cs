using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dto.PWA
{
    public class PWABranchDTO
    {
        public string status { get; set; }
        public string ba { get; set; }
        public string pwacode { get; set; }
        public string pwaname { get; set; }
        public string pwaaddress { get; set; }
        public string pwatel { get; set; }
    public string lat { get; set; }
        public string lng { get; set; }
        public string  wwzonecode { get; set; }
        public string zone { get; set; }
        public string region { get; set; }
        public string shapetext { get; set; }
        public string shapegeojson { get; set; }
    

        //  status":"in","ba":"1165","pwacode":"5541026","pwaname":"\u0e23\u0e31\u0e07\u0e2a\u0e34\u0e15"
    }

    public class PWAZoneDTO
    {

        public string pwacode { get; set; }
        public string zonepwacode { get; set; }
        public string pwaname { get; set; }
        public string zone { get; set; }
        public string region { get; set; }
        public string shapetext { get; set; }
        public string shapegeojson { get; set; }
        public string latitude { get; set; }
        public string longtitude { get; set; }


        //  status":"in","ba":"1165","pwacode":"5541026","pwaname":"\u0e23\u0e31\u0e07\u0e2a\u0e34\u0e15"
    }
}
