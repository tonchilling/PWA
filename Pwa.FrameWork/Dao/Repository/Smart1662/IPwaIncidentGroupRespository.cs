using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface IPwaIncidentGroupRespository
    {
        List<Pwa.FrameWork.Dao.EF.Smart1662.PwaIncidentGroup> GetPwaIncidentGroups();
    }
}
