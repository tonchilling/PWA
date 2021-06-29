using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.Data
{
    public class SubdistrictDetail
    {
        public CISDistrinct SubDistrict { get; set; }
        public CISAmphure District { get; set; }
        public CISProvince Province { get; set; }
    }
}
