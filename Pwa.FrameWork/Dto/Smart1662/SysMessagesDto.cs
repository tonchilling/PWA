using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dto.Smart1662
{
    public class SysMessagesDto:BaseDto
    {
        public int MessageId { get; set; }
        public int isRead { get; set; }
        public string BracnchNo { get; set; }
        public string id { get; set; }
        public string Expr1 { get; set; }
        public string PwaIncidentID { get; set; }
        public string PwaIncidentNo { get; set; }
        public string PwaIncidentTypeID { get; set; }
        public string PwaIncidentGroupID { get; set; }
        public string CaseTitle { get; set; }
        public string CaseDetail { get; set; }
        public bool Sla { get; set; }
        public string SlaDetail { get; set; }
        public System.DateTime ReceivedCaseDate { get; set; }
        public string ReceivedCaseDateText { get; set; }
        public System.DateTime CompletedCaseDate { get; set; }
        public string CompletedCaseDateText { get; set; }
        public string CaseLatitude { get; set; }
        public string CaseLongtitude { get; set; }
        public string ProcessStage { get; set; }
        public string PwsIncidentAddress { get; set; }
        public string ProcessStageText { get; set; }
        public string CustomerProcessStage { get; set; }
        public string PwaParentIncidentID { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedDateText { get; set; }
        public string CreatedBy { get; set; }
        public string RequestType { get; set; }
        public string RequestCategory { get; set; }
        public string RequestCategorySubject { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string ChannelId { get; set; }
        public string BranchNo { get; set; }
        public string TypeCode { get; set; }
        public string BranchName { get; set; }
        public string NumOfFollow { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
