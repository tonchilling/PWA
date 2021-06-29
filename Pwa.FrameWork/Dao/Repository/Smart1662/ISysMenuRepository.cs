using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{

    public interface ISysMenuRepository
    {

        Task Add(SysMenu comlain);
        Task Update(SysMenu comlain);
        Task Delete(SysMenu comlain);
        Task<SysMenu> GetById(int id);
        Task<IEnumerable<SysMenu>> GetAll();

    }
}
