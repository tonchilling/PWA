using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Extension;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysRequestTypeResponsitory : ISysRequestTypeRespository
    {
        public bool Add(SysRequestType item)
        {
            int result = 0;

            try
            {
                using (Smart1662Entities context = new Smart1662Entities())
                {
                   result = context.sp_SysRequestType_Add(item.Id.ToString(), item.Name, item.Description ,item.Status.ToString(), item.CreatedBy.ToString(), item.UpdatedBy.ToString());
                   context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return true;
        }

        public Task Update(SysRequestType item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysRequestType item)
        {
            throw new NotImplementedException();
        }

        public SysRequestType GetById(int Id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysRequestType.FirstOrDefault(c=>c.Id==Id);
                return target;
            }
        }
        public List<SysRequestType> Search(string name, string status)
        {
            List<SysRequestType> target = new List<SysRequestType>();
            using (Smart1662Entities context = new Smart1662Entities())
            {
                if (name != "" && status != "")
                {
                    target = context.SysRequestType.Where(c => c.Name.Contains(name) && c.Status.ToString() == status).ToList();

                }
                else if (name != "" && status == "")
                {
                    target = context.SysRequestType.Where(c => c.Name.Contains(name)).ToList();
                }
                else if (name == "" && status != "")
                {
                    target = context.SysRequestType.Where(c => c.Status.ToString() == status).ToList();
                }
                
                return target;
            }
        }
        public List<SysRequestType> GetAll()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysRequestType.ToList();
                return target;
            }
        }

        public List<RequestType> GetRequestTypes()
        {
            List<RequestType> result = new List<RequestType>(0);
            using (Smart1662Entities context = new Smart1662Entities())
            {
                result = context.SysRequestType.ToList().Select(t => t.CopyTo<RequestType>(new RequestType())).ToList();
                result.ForEach(r =>
                {
                    r.Categories = context.SysRequestCategory.Where(c => c.requesttypeid == r.Id).ToList().Select(sc => sc.CopyTo<RequestCategory>(new RequestCategory())).ToList();
                    r.Categories.ForEach(c =>
                    {
                        c.Subjects = context.SysRequestCategorySubject.Where(scs => scs.reqcategoryid == c.id).ToList();
                    });
                });
                
            }

            return result;
        }

    }

    public class RequestType : SysRequestType
    {
        public List<RequestCategory> Categories { get; set; }
    }

    public class RequestCategory : SysRequestCategory
    {
        public List<SysRequestCategorySubject> Subjects { get; set; }
    }

}
