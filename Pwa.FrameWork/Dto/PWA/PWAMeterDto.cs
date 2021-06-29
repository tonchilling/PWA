using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Pwa.FrameWork.Dto.Utils;
namespace Pwa.FrameWork.Dto.PWA
{
    public class PWAMeterDto
    {
        public string ShapeText { get; set; }
        public string pipe_id { get; set; }
        public string custcode { get; set; }
        public string contracno { get; set; }
        public string pin { get; set; }
        public string custname { get; set; }
        public string meterno { get; set; }
        public string mtrmkcode { get; set; }
        public string metersize { get; set; }
        public string custaddr { get; set; }
        public string pwa_code { get; set; }
        public GeojsonFeatureDto2 Shape { get; set; }
    }


    public class PWALeakPointHistoryDto
    {

        public string repairdate { get; set; }
        public string repairtime { get; set; }
        public string locate  { get; set; }
        public string  leakcause { get; set; }
        public string leakdetail  { get; set; }
        public string leakShapeText { get; set; }
        public string leakShapeGeoJson { get; set; }
        public string pipeShapeGeoJson { get; set; }

    }


}
