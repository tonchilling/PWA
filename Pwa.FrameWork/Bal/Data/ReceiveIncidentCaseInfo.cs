using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.Data
{
    public class ReceiveIncidentCaseInfo : SysConplainant
    {
        public List<Employee> Customers { get; set; }
    }
}
