using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662;
namespace Pwa.FrameWork.Bal.Data
{
    public class IncidentEditInfo
    {
        public PwaIncident Incident { get; set; }
        public List<PwaInformer> Informers { get; set; }
        public List<FileDto> Files { get; set; }

    }
}
