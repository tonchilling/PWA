using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysRolPermissionReponsitory : ISysRolPermissionRepository
    {
        public Task Add(SysRolePermission channel)
        {
            throw new NotImplementedException();
        }

        public Task Update(SysRolePermission channel)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysRolePermission channel)
        {
            throw new NotImplementedException();
        }

        public async Task<SysRolePermission> GetById(int id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = await context.SysRolePermission.FirstOrDefaultAsync(c=>c.RoleId==id);
              
                return target;
            }
        }
        public async Task<SysRolePermission> GetByName(string Name)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<SysRolePermission>> GetAll()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = await context.SysRolePermission.ToListAsync();
                return target;
            }
        }
        public async Task<List<SysRolePermission>> GetRoles()
        {
           List<SysRolePermission> target = null;
            try
            {
                using (Smart1662Entities context = new Smart1662Entities())
                {
                    target = await context.SysRolePermission.Include(x=>x.SysRole).Include(x=>x.SysMenu).ToListAsync();

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return target;
        }

        public  List<sp_SysRolePermission_GetAllMenu_Result> GetPermissionPage(string rolId)
        {
            List<sp_SysRolePermission_GetAllMenu_Result> target = null;
            try
            {
                using (Smart1662Entities context = new Smart1662Entities())
                {
                    target =  context.sp_SysRolePermission_GetAllMenu(rolId).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return target;
        }


        public async Task <bool> Add(List<SysRolePermission> reqList)
        {
            int result = 0;
           
            try
            {
                using (Smart1662Entities context = new Smart1662Entities())
                {

                    foreach(SysRolePermission  item in reqList)
                    {
                        result= context.sp_SysRolePermission_Add(item.RoleId.ToString(), item.MenuId.ToString(), item.PermissionId.ToString(), item.PermissionFlag?"1":"0");

                    }
                    context.SaveChanges();



                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return  true;
        }
    }
}
