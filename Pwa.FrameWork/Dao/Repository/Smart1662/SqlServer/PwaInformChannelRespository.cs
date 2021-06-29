using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using System.Data.Entity;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class PwaInformChannelRespository : IPwaInformChannelRespository
    {
        public List<PwaInformChannel> GetInformChannels()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                return context.PwaInformChannel.Where(ig => ig.Status.Value == 1).ToList();
              
            }
        }
    }
}
