using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class PwaIncidentGroupResponsitory : IPwaIncidentGroupRespository
    {
        

        public List<PwaIncidentGroup> GetPwaIncidentGroups()
        {

            using (Smart1662Entities context = new Smart1662Entities())
            {
                return context.PwaIncidentGroup.Where(ig => ig.Status.Value == 1).ToList();
                
            }

            
        }
    }
}
