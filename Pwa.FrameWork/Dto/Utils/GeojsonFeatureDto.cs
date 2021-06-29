using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dto.Utils
{
    public class GeojsonFeatureDto
    {
        public string type { get; set; }
        public properties properties { get; set; }
        public geometry geometry { get; set; }
    }
    public class GeojsonFeatureDto2
    {
        public string type { get; set; }
        public properties properties { get; set; }
        public geometry2 geometry { get; set; }
    }


    public class geometry
    {
        public string type { get; set; }
        public string coordinates { get; set; }
    }

    public class geometry2
    {
        public string type { get; set; }
        public List<List<double>> coordinates { get; set; }
    }

    public class properties
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
