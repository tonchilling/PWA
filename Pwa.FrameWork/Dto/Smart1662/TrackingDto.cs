using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dto.Smart1662
{

    public class InputTrackingDto : BaseDto
    {
        public string TrackingNo { get; set; }
    }
    public class TrackingHeaderDto : BaseDto
    {
        public string PwaIncidentID { get; set; }
        public string PwaIncidentNo { get; set; }
        public string PwaIncidentTypeID { get; set; }
        public string PwaIncidentGroupID { get; set; }
        public string CaseTitle { get; set; }
        public string CaseTitleName { get; set; }
        public string CustomerName { get; set; }
        public string Telephone { get; set; }
        public List<TrackingDetailDto> TrackingDetail { get; set; }

    }
    public class TrackingDetailDto
    {
        public string PwaIncidentWorkerID { get; set; }
        public string PwaIncidentID { get; set; }
        public string WorkerID { get; set; }
        public string ProcessStep { get; set; }
        public string StatusName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }

    }
}
