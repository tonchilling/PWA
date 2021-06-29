using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface ISysRequestTypeRespository
    {
        bool Add(SysRequestType item);
        Task Update(SysRequestType item);
        Task Delete(SysRequestType item);
        SysRequestType GetById(int id);
        List<SysRequestType> Search(string name, string status);
        List<SysRequestType> GetAll();

        List<RequestType> GetRequestTypes();

    }
}
