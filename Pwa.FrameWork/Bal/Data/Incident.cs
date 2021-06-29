using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto.Smart1662;
namespace Pwa.FrameWork.Bal.Data
{
    public class Incident : PwaIncident
    {
        public List<FileDto> Files { get; set; }
        public List<PwaInformer> Imformers { get; set; }

        public List<PwaBranchReject> BranchRejects { get; set; }

        public string CommentID { get; set; }
    }
}
