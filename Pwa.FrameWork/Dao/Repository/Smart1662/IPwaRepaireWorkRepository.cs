using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface IPwaRepaireWorkRepository
    {
        PwaRepaireWorkHeaderDto Add(PwaRepaireWorkHeaderDto data);
        Task Update(PwaRepaireWorkHeaderDto data);
        Task Delete(PwaRepaireWorkHeaderDto data);
        PwaRepaireWorkHeaderDto GetById(int id);

        List<PwaRepaireWorkHeaderDto> GetAll();

    }
}
