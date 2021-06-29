using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dto.Smart1662
{
    public class BranchDto : BaseDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string CCTR_CODE { get; set; }
        public string WW_CODE { get; set; }
        public string StrCreatedDate { get; set; }
        public string StrUpdatedDate { get; set; }
        public string TypeCode { get; set; }
        public string Parent { get; set; }
        public string ParentName { get; set; }
        public string BaCode { get; set; }
        public string AreaBACode { get; set; }
    }
}
