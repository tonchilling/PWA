using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dto.Smart1662
{
    public class UnitDto : BaseDto
    {

        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string CreatedByLogin { get; set; }
        public string UpdatedByLogin { get; set; }
        public string Action { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }

    }
}
