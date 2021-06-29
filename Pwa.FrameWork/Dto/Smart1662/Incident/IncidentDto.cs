using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dto.Smart1662.Incident
{
    public class IncidentDto
    { 
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
        public System.DateTime? CompletedCaseDate { get; set; }
        public string CompletedCaseDateText { get; set; }
        public string CaseLatitude { get; set; }
        public string CaseLongtitude { get; set; }
       
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
        public string bUserCurrentBranchNo { get; set; }
        public string ProcessStage { get; set; }
        public string GProcessStage { get; set; }
        public string IncidentStatus { get; set; }
        public string CanSelect { get; set; }


    }

    public class IncidentDisplayDto
    {

        public int PwaIncidentID { get; set; }
        public string PwaIncidentNo { get; set; }

        public string PwaInformReceiver { get; set; }
        public string PwaInformDate { get; set; }

        public string PwaIncidentTypeID { get; set; }
        public string PwaIncidentGroupID { get; set; }

        public string CaseTitle { get; set; }
        public string CaseTitleDetail { get; set; }
        public string CaseDetail { get; set; }
        public string ResolvedDetail { get; set; }
        public bool Sla { get; set; }
        public string SlaDetail { get; set; }
        public string ReceivedCaseDate { get; set; }
        public string CompletedCaseDate { get; set; }
        public string CaseLatitude { get; set; }
        public string CaseLongtitude { get; set; }
        public string InformLatitude { get; set; }
        public string InformLongtitude { get; set; }
        public string PwsIncidentAddress { get; set; }

        public string BracnchNo { get; set; }
        public string Recorder { get; set; }
        public string RecordDate { get; set; }
        public int IncidentStatus { get; set; }

        public string InformID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustCode { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MeterNo { get; set; }
        public string InformChannelID { get; set; }
        public string InformReference { get; set; }
        public string Telephone { get; set; }

        public string Province { get; set; }
        public string ProvinceText { get; set; }
        public string District { get; set; }
        public string DistrictText { get; set; }
        public string SubDistrict { get; set; }
        public string SubDistrictText { get; set; }
        public string CustomerAddress { get; set; }
        public List<FileDto> Files { get; set; }
        public string NumOfFollow { get; set; }


        public int ProcessStatus { get; set; }
        public string IncidentStatusName { get; set; }

        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string SubDistrictName { get; set; }

        public string Address_no { get; set; }
        public string Village_no{ get; set; }
        public string Building { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }

        public string CreateBy { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDatetime { get; set; }


        public DateTime? StartSLADate { get; set; }

    }
}
