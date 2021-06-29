using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.Data
{
    public class SearchBranchIncidentResult
    {
        public string CustomerCode { get; set; }
        public SysBranch Branch { get; set; }

        public PWACustomerInfo Customer { get; set; }
        public List<VwSearchIncident> Incidents { get; set; }

    }
}
