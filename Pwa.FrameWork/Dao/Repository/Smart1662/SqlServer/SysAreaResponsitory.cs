using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Utils;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysAreaResponsitory : ISysAreaRespository
    {
        public bool Add(SysArea item)
        {
            int result = 0;
            bool res = false;
            try
            {
                using (Smart1662Entities context = new Smart1662Entities())
                {
                   result = context.sp_SysArea_Add(item.Id.ToString(), item.Code ,item.Name ,item.Status.ToString(), item.CCTR_CODE, item.WW_CODE, item.CreatedBy, item.UpdatedBy);
                   context.SaveChanges();
                   res = result == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return res;
        }

        public Task Update(SysArea item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysArea item)
        {
            throw new NotImplementedException();
        }

        public SysArea GetById(int Id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysArea.FirstOrDefault(c=>c.Id==Id);
                return target;
            }
        }
        public SysArea GetByCode(string Code)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysArea.FirstOrDefault(c => c.Code.Contains(Code));
                return target;
            }
        }
        public List<SysArea> Search(string code, string name, string status)
        {
            List<SysArea> target = new List<SysArea>();
            using (Smart1662Entities context = new Smart1662Entities())
            {
                if (code != "" && name != "" && status != "")
                {
                    target = context.SysArea.Where(c => c.Code.Contains(code) && c.Name.Contains(name) && c.Status.ToString() == status).ToList();
                }
                if (code == "" && name != "" && status != "")
                {
                    target = context.SysArea.Where(c => c.Name.Contains(name) && c.Status.ToString() == status).ToList();
                }
                if (code != "" && name == "" && status != "")
                {
                    target = context.SysArea.Where(c => c.Code.Contains(code) && c.Status.ToString() == status).ToList();
                }
                if (code != "" && name != "" && status == "")
                {
                    target = context.SysArea.Where(c => c.Code.Contains(code) && c.Name.Contains(name)).ToList();
                }
                if (code == "" && name == "" && status != "")
                {
                    target = context.SysArea.Where(c => c.Status.ToString() == status).ToList();
                }
                if (code != "" && name == "" && status == "")
                {
                    target = context.SysArea.Where(c => c.Code.Contains(code)).ToList();
                }
                if (code == "" && name != "" && status == "")
                {
                    target = context.SysArea.Where(c => c.Name.Contains(name)).ToList();
                }

                target = target.AsEnumerable().OrderBy(x => Converting.ToInt(x.Code)).ToList();
                return target;
            }
        }
        public List<SysArea> GetAll()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysArea.AsEnumerable().OrderBy(x =>  Converting.ToInt(x.Code)).ToList();
                return target;
            }
        }
    }
}
