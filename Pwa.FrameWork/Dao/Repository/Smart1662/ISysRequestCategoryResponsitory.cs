using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysRequestCategoryResponsitory
    {
        bool Add(SysRequestCategory item);
        Task Update(SysRequestCategory item);
        Task Delete(SysRequestCategory item);
        SysRequestCategory GetById(int id);
        List<SysRequestCategory> Search(string requesttype, string name, string status);
        List<SysRequestCategory> GetAll();

    }
}
