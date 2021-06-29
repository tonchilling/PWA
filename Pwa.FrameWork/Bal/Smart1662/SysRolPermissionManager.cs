using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662;

namespace Pwa.FrameWork.Bal.Smart1662
{
    public class SysRolPermissionManager
    {
        private ISysRolPermissionRepository _sysResp = RespositoryFactory.GetSysRolPermissionRepository(true);

        public async Task<bool> Add(List<SysRolePermissionDto> data)
        {
            List<SysRolePermission> reqData = new List<SysRolePermission>();

            if (data != null)
            {
                foreach (SysRolePermissionDto item in data)
                {
                    reqData.Add(
                        new SysRolePermission() {
                            RoleId = item.RoleId,
                            MenuId = item.MenuId,
                            PermissionId = 1,
                            PermissionFlag = item.View == "1" ? true : false
                        }
                        );

                    reqData.Add(
                       new SysRolePermission()
                       {
                           RoleId = item.RoleId,
                           MenuId = item.MenuId,
                           PermissionId = 2,
                           PermissionFlag = item.Edit == "1" ? true : false
                       }
                       );

                    reqData.Add(
                     new SysRolePermission()
                     {
                         RoleId = item.RoleId,
                         MenuId = item.MenuId,
                         PermissionId = 3,
                         PermissionFlag = item.Delete == "1" ? true : false
                     }
                     );

                }
            }


            return await _sysResp.Add(reqData);
        }

        public async Task<List<SysRolePermissionDto>> GetRoles(string roleId)
        {
            List<SysRolePermissionDto> result = null;
            List<sp_SysRolePermission_GetAllMenu_Result> respData = _sysResp.GetPermissionPage(roleId);
            result = new List<SysRolePermissionDto>();

            foreach (sp_SysRolePermission_GetAllMenu_Result data in respData.FindAll(x=>x.ParentId==null))
            {
                result.Add(new SysRolePermissionDto() {
                RoleId=data.RoleId,
                RoleName=data.RoleName,
                    MenuId = data.MenuId,
         MenuName = data.MenuName,
         ParentId = data.ParentId !=null ? data.ParentId.Value.ToString():"",
         View=data.View.ToString(),
                    Edit =data.Edit.ToString(),
                    Delete =data.Delete.ToString(),
                    SubMenus= (respData.Where(x=>x.ParentId==data.MenuId).Select(x=> new SysRolePermissionDto() {
                        RoleId = x.RoleId,
                        RoleName = x.RoleName,
                        MenuId = x.MenuId,
                        MenuName = x.MenuName,
                        ParentId = x.ParentId != null ? x.ParentId.Value.ToString() : "",
                        View = x.View.ToString(),
                        Edit = x.Edit.ToString(),
                        Delete = x.Delete.ToString(),
                    }).ToList())
                });
            }

            return  result;
        }

    }
}
