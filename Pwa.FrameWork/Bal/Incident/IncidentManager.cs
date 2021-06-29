using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.Incident
{
    public class IncidentManager
    {
        private IIncidentRepository _incidentResp = RespositoryFactory.GetIncidentRepository();
        public async Task ReceiveIncidentCase(ReceiveIncidentCaseInfo info)
        {

                
            await _incidentResp.Add((SysConplainant)info);
        }

        public async Task<IEnumerable<SysConplainant>> SearchIncidentCase(ReceiveIncidentCaseInfo info)
        {
            return await _incidentResp.GetComplains();
        }

        

    }
}
