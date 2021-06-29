using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ITrackingRespository
    {
        bool Add(TrackingHeaderDto item);
        Task Update(TrackingHeaderDto item);
        Task Delete(TrackingHeaderDto item);
        TrackingHeaderDto GetByIncidentNo(string TrackingNo);
     

    }
}
