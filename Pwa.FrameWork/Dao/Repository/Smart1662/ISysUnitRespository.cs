using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysUnitRespository
    {
        bool Add(UnitDto channel);
        Task Update(UnitDto channel);
        Task Delete(UnitDto channel);
        UnitDto GetById(string id);
        List<UnitDto> Search(string UnitId, string UnitName, string Status);

    }
}
