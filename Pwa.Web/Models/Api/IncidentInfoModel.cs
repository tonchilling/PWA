using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
//using Microsoft.AspNetCore.Http;

namespace Pwa.Web.Models.Api
{
    public class IncidentInfoModelTest
    {
        public IncidentInfoModel model { get; set; }
        public System.Web.HttpPostedFileBase file { get; set; }
    }

    public class IncidentInfoModel
    {
        public int PwaIncidentID { get; set; }
        public string PwaIncidentNo { get; set; }
        public string PwaIncidentTypeID { get; set; }
        public string PwaIncidentGroupID { get; set; }
        public string CaseTitle { get; set; }
        public string CaseDetail { get; set; }
        public bool Sla { get; set; }
        public string SlaDetail { get; set; }
        public string ReceivedCaseDate { get; set; }
        public string CompletedCaseDate { get; set; }

        public string ReceivedCaseTime { get; set; }
        public string CompletedCaseTime { get; set; }
        public string CaseLatitude { get; set; }
        public string CaseLongtitude { get; set; }
        
        public string ResolvedDetail { get; set; }

        public List<InformerInfoModel> Informers { get; set; }

        
        /*
         public int ProcessStage { get; set; }
        public int CustomerProcessStage { get; set; }
        public int PwaParentIncidentID { get; set; }

        public System.DateTime ReceivedCaseDate { get; set; }
        public System.DateTime CompletedCaseDate { get; set; }  
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        */
    }

    public class InformerInfoModel
    {
        public int InformerID { get; set; }
        public int PwaIncidentID { get; set; }
        public string CustomerID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MeterNo { get; set; }
        public string Telephone { get; set; }
        public Nullable<int> InformChannelID { get; set; }
        public string InformReference { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateIncidentStatus
    {
        public string PwaIncidentID { get; set; }
        public string BranchNo { get; set; }
        public string AccountNo { get; set; }
       
        public string IncidentStatus { get; set; }

        public string IncidentStatusDetail { get; set; }
    }

}