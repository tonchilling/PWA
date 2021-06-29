using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dto.Smart1662
{
    public class CustomerEffectDto : BaseDto
    {
        public string RWId { get; set; }
        public string Location { get; set; }
        public string CustomerCode { get; set; }
        public string incidentstatus { get; set; }

        public string CommentID { get; set; }
    }
}
