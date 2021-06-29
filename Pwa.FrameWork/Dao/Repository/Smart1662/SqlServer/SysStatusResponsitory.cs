using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysStatusResponsitory : ISysStatusResponsitory
    {
        public List<SysStatus> GetStatuses()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                return context.SysStatus.ToList();
            }
        }

        public List<SysStatus> GetStatuses(int[] statusNo)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                return context.SysStatus.Where(s=> statusNo.Contains( s.status_id)).ToList();
            }
        }

        
    }
}
