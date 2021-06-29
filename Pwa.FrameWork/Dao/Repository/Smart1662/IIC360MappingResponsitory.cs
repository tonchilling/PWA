using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface IIC360MappingResponsitory 
    {
        List<IC360Mapping> GetMappingConfigs();
    }
}
