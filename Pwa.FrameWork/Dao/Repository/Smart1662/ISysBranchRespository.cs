using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.PWA;
using Pwa.FrameWork.Dto.Smart1662;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysBranchRespository
    {
        bool Add(BranchDto item);
        Task Update(SysBranch item);
        Task Delete(SysBranch item);
        SysBranch GetById(int id);
        SysBranch GetByCode(string code);
        PWABranchDTO GetPWABranchByCode(string code);
        List<BranchDto> Search(string Id, string Code, string Name, string CCTR_CODE, string WW_CODE, string TypeCode, string Parent, string Status);
        List<SysBranch> GetAll();

    }
}
