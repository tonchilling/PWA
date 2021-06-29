using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class IC360MappingResponsitory : IIC360MappingResponsitory
    {
        public List<IC360Mapping> GetMappingConfigs()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                return context.IC360Mapping.ToList();

            }
        }
    }
}
