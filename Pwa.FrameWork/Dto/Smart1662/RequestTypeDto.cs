using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dto.Smart1662
{
    public class RequestTypeDto : BaseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StrCreatedDate { get; set; }
        public string StrUpdatedDate { get; set; }
    }
}
