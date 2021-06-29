using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysRequestCategoryResponsitory : ISysRequestCategoryResponsitory
    {
        public bool Add(SysRequestCategory item)
        {
            int result = 0;

            try
            {
                using (Smart1662Entities context = new Smart1662Entities())
                {
                   result = context.sp_SysRequestCategory_Add(item.id.ToString(), item.requesttypeid.ToString(), item.name, item.description ,item.status.ToString(), item.CreatedBy.ToString(), item.UpdatedBy.ToString());
                   context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return true;
        }

        public Task Update(SysRequestCategory item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysRequestCategory item)
        {
            throw new NotImplementedException();
        }

        public SysRequestCategory GetById(int Id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysRequestCategory.FirstOrDefault(c=>c.id==Id);
                return target;
            }
        }
        public List<SysRequestCategory> Search(string requesttype, string name, string status)
        {
            List<SysRequestCategory> target = new List<SysRequestCategory>();
            using (Smart1662Entities context = new Smart1662Entities())
            {
                if (requesttype != "" && name != "" && status != "")
                {
                    target = context.SysRequestCategory.Where(c => c.requesttypeid.ToString() == requesttype && c.name.Contains(name) && c.status.ToString() == status).ToList();
                }
                if (requesttype == "" && name != "" && status != "")
                {
                    target = context.SysRequestCategory.Where(c => c.name.Contains(name) && c.status.ToString() == status).ToList();
                }
                if (requesttype != "" && name == "" && status != "")
                {
                    target = context.SysRequestCategory.Where(c => c.requesttypeid.ToString() == requesttype && c.status.ToString() == status).ToList();
                }
                if (requesttype != "" && name != "" && status == "")
                {
                    target = context.SysRequestCategory.Where(c => c.requesttypeid.ToString() == requesttype && c.name.Contains(name)).ToList();
                }
                if (requesttype == "" && name == "" && status != "")
                {
                    target = context.SysRequestCategory.Where(c => c.status.ToString() == status).ToList();
                }
                if (requesttype != "" && name == "" && status == "")
                {
                    target = context.SysRequestCategory.Where(c => c.requesttypeid.ToString() == requesttype).ToList();
                }
                if (requesttype == "" && name != "" && status == "")
                {
                    target = context.SysRequestCategory.Where(c => c.name.Contains(name)).ToList();
                }
                
                return target;
            }
        }
        public List<SysRequestCategory> GetAll()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysRequestCategory.ToList();
                return target;
            }
        }
    }
}
