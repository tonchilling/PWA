using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class PWACustomerTypeRespository : IPWACustomerTypeRespository
    {
        public List<PwaIncidentType> GetPwaIncidentTypes()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                return context.PwaIncidentType.Where(ig => ig.Status.Value == 1).ToList();
                
            }
        }
    }
}
