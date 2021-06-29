using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysHolidayResponsitory : ISysHolidayResponsitory
    {
        public List<SysHoliday> GetHolidates(DateTime start,DateTime end)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                return context.SysHoliday.Where(h=>h.HolidayDate >= start && h.HolidayDate <= end).ToList();

            }
        }

        public void Add(List<SysHoliday> holidays)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                context.SysHoliday.AddRange(holidays);
                context.SaveChanges();
            }
        }
    }
}
