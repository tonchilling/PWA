using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysAreaRespository
    {
        bool Add(SysArea item);
        Task Update(SysArea item);
        Task Delete(SysArea item);
        SysArea GetById(int id);
        SysArea GetByCode(string code);
        List<SysArea> Search(string code, string name, string status);
        List<SysArea> GetAll();

    }
}
