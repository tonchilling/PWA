using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pwa.FrameWork.Bal.PwaSystem;
using Pwa.FrameWork.Extension;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dao.EF.Smart1662;

namespace Pwa.FrameWork.Bal.Smart1662
{
    public class StatusManager
    {
        ISysStatusResponsitory  _sysStatusResp = RespositoryFactory.GetSysStatusResponsitory();
        public List<SysStatus> GetStatuses(ProcessStage[] stages)
        {
            int[] statusNos = stages.Select(s=>s.GetInt()).ToArray();

            return _sysStatusResp.GetStatuses(statusNos);
        }
    }
}
