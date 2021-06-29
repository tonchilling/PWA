using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Utils;



namespace Pwa.FrameWork.Bal.Smart1662
{
    public class TrackingManager
    {
        private ITrackingRespository _sysResp = RespositoryFactory.GetTrackingRespository();
        private Smart1662Entities context = new Smart1662Entities();

        public TrackingHeaderDto Search(string IncidentNo)
        {
            TrackingHeaderDto dto = new TrackingHeaderDto();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(IncidentNo);
                if (!inj) throw new Exception("Character not allowed.");
                dto = _sysResp.GetByIncidentNo(IncidentNo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dto;
        }
       

    }
}
