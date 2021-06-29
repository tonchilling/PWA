using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{

    public interface ISysRolPermissionRepository
    {

        Task Add(SysRolePermission comlain);
        Task<bool> Add(List<SysRolePermission> comlain);
        Task Update(SysRolePermission comlain);
        Task Delete(SysRolePermission comlain);
        Task<SysRolePermission> GetById(int id);
        Task<IEnumerable<SysRolePermission>> GetAll();

        Task<List<SysRolePermission>> GetRoles();
     List<sp_SysRolePermission_GetAllMenu_Result> GetPermissionPage(string roleId);

    }
}
