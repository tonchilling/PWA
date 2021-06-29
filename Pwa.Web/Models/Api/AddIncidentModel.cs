using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pwa.Web.Models.Api
{
    public class AddIncidentModel
    {/**/
        public string PwaIncidentID { get; set; }
        public string PwaIncidentNo { get; set; }
        public string PwaInformReceiver { get; set; }
        public string PwaInformDate { get; set; }

        public string PwaIncidentTypeID { get; set; }
        public string PwaIncidentGroupID { get; set; }
        public string PwaInformChannel { get; set; }
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
        public string PwsIncidentAddress { get; set; }

        public string BracnchNo { get; set; }
        public string Recorder { get; set; }
        public string RecordDate { get; set; }
        public string CommentID { get; set; }

        public string IncidentStatus { get; set; }
        public string IncidentStatusDetail { get; set; }
        public bool IsCallenter { get; set; }
       
        public List<AddInformer> Informers { get; set; }

        public CurrentUser User { get; set; }

        public string Reason { get; set; }


        public string NearLocate { get; set; }

        public string ProvinceID { get; set; }
        public string DistrictID { get; set; }
        public string SubDistrictID { get; set; }
        public string AddressNo { get; set; }
        public string VillageNo { get; set; }
        public string VillageBuilding { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
    }

    public class AddInformer
    {
        public string InformID { get; set; }
        public string CustomerID { get; set; }
        public string CustCode { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MeterNo { get; set; }
        public string InformChannelID { get; set; }
        public string InformReference { get; set; }
        public string Telephone { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string CustomerAddress { get; set; }

        public string AddressNo { get; set; }
        public string VillageNo { get; set; }
        public string VillageBuilding { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }

    }

}