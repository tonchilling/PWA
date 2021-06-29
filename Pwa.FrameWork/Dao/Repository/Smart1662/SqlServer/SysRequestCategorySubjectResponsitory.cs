using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysRequestCategorySubjectResponsitory : ISysRequestCategorySubjectResponsitory
    {
        public bool Add(SysRequestCategorySubject item)
        {
            int result = 0;

            try
            {
                using (Smart1662Entities context = new Smart1662Entities())
                {
                   result = context.sp_SysRequestCategorySubject_Add(item.id.ToString(), item.reqcategoryid.ToString(), item.name, item.description ,item.status.ToString(), item.CreatedBy.ToString(), item.UpdatedBy.ToString());
                   context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return true;
        }

        public Task Update(SysRequestCategorySubject item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysRequestCategorySubject item)
        {
            throw new NotImplementedException();
        }

        public SysRequestCategorySubject GetById(int Id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysRequestCategorySubject.FirstOrDefault(c=>c.id==Id);
                return target;
            }
        }
        public List<SysRequestCategorySubject> Search(string reqcategoryid, string name, string status)
        {
            List<SysRequestCategorySubject> target = new List<SysRequestCategorySubject>();
            using (Smart1662Entities context = new Smart1662Entities())
            {
                if (reqcategoryid != "" && name != "" && status != "")
                {
                    target = context.SysRequestCategorySubject.Where(c => c.reqcategoryid.ToString() == reqcategoryid && c.name.Contains(name) && c.status.ToString() == status).ToList();
                }
                if (reqcategoryid == "" && name != "" && status != "")
                {
                    target = context.SysRequestCategorySubject.Where(c => c.name.Contains(name) && c.status.ToString() == status).ToList();
                }
                if (reqcategoryid != "" && name == "" && status != "")
                {
                    target = context.SysRequestCategorySubject.Where(c => c.reqcategoryid.ToString() == reqcategoryid && c.status.ToString() == status).ToList();
                }
                if (reqcategoryid != "" && name != "" && status == "")
                {
                    target = context.SysRequestCategorySubject.Where(c => c.reqcategoryid.ToString() == reqcategoryid && c.name.Contains(name)).ToList();
                }
                if (reqcategoryid == "" && name == "" && status != "")
                {
                    target = context.SysRequestCategorySubject.Where(c => c.status.ToString() == status).ToList();
                }
                if (reqcategoryid != "" && name == "" && status == "")
                {
                    target = context.SysRequestCategorySubject.Where(c => c.reqcategoryid.ToString() == reqcategoryid).ToList();
                }
                if (reqcategoryid == "" && name != "" && status == "")
                {
                    target = context.SysRequestCategorySubject.Where(c => c.name.Contains(name)).ToList();
                }
                
                return target;
            }
        }
        public List<SysRequestCategorySubject> GetAll()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysRequestCategorySubject.ToList();
                return target;
            }
        }
    }
}
