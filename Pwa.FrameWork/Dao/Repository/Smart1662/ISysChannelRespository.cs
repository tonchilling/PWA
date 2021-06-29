using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysChannelRespository
    {
        bool Add(SysChannel channel);
        Task Update(SysChannel channel);
        Task Delete(SysChannel channel);
        SysChannel GetById(int id);
        List<SysChannel> SearchChannel(string name, string status);
        List<SysChannel> GetAll();

    }
}
