using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository
{
    public interface ISysHolidayResponsitory
    {
        List<SysHoliday> GetHolidates(DateTime start, DateTime end);
        void Add(List<SysHoliday> holidays);
    }
}
