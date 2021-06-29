using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysChannelResponsitory : ISysChannelRespository
    {
        public bool Add(SysChannel item)
        {
            int result = 0;

            try
            {
                using (Smart1662Entities context = new Smart1662Entities())
                {
                   result = context.sp_SysChannel_Add(item.ChannelId.ToString(), item.ChannelName, item.Status.ToString(), item.CreatedBy.ToString(), item.UpdatedBy.ToString());
                   context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return true;
        }

        public Task Update(SysChannel channel)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysChannel channel)
        {
            throw new NotImplementedException();
        }

        public SysChannel GetById(int id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysChannel.FirstOrDefault(c=>c.ChannelId==id);
                return target;
            }
        }
        public List<SysChannel> SearchChannel(string name, string status)
        {
            List<SysChannel> target = new List<SysChannel>();
            using (Smart1662Entities context = new Smart1662Entities())
            {
                if (name != "" && status != "")
                {
                    target = context.SysChannel.Where(c => c.ChannelName.Contains(name) && c.Status.ToString() == status).ToList();

                }
                else if (name != "" && status == "")
                {
                    target = context.SysChannel.Where(c => c.ChannelName.Contains(name)).ToList();
                }
                else if (name == "" && status != "")
                {
                    target = context.SysChannel.Where(c => c.Status.ToString() == status).ToList();
                }
                
                return target;
            }
        }
        public List<SysChannel> GetAll()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.SysChannel.ToList();
                return target;
            }
        }
    }
}
