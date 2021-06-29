using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysRequestCategorySubjectResponsitory
    {
        bool Add(SysRequestCategorySubject item);
        Task Update(SysRequestCategorySubject item);
        Task Delete(SysRequestCategorySubject item);
        SysRequestCategorySubject GetById(int id);
        List<SysRequestCategorySubject> Search(string requesttype, string name, string status);
        List<SysRequestCategorySubject> GetAll();

    }
}
