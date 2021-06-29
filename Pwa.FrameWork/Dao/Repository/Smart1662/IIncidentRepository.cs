using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface IIncidentRepository
    {
        Task Add(SysConplainant comlain);
        Task Update(SysConplainant comlain);
        Task Delete(SysConplainant comlain);
        Task<SysConplainant> GetComplain(string id);
        Task<IEnumerable<SysConplainant>> GetComplains();

    }
}
