using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Smart1662.Incident;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public interface IPwaIncidentRespository
    {
        bool IsCallcenter { get; set; }
         int Add(PwaIncident incident, List<PwaInformer> informs);
        void AddFiles(List<FileDto> files);
        List<VwIncidentList> GetIncidents();
        List<IncidentDto> GetIncidents(string [] id,string status );
        List<IncidentDto> GetIncidents(IncidentDto data);
        List<IncidentDto> GetIncidentsByBranch(IncidentDto data);
        string GetNextIncidentNo();
        PwaIncident GetLastIncident();

        List<CISProvince> GetProvince();
        List<CISAmphure> GetDistricts(string provinceID);
        List<CISAmphure> GetDistricts();
        List<CISDistrinct> GetSubDistricts(string districtID);

        List<PWACustomerInfo> GetCustomers();
        Pwa.FrameWork.Bal.Data.IncidentEditInfo GetIncident(int incidentID);

        void Update(PwaIncident incident, List<PwaInformer> informs);
        void Update(PwaIncident incident, List<PwaInformer> informs, List<FileDto> Files);

        SubdistrictDetail GetSubDistrictDetail(string subDistcitID);
        List<LocationDto> GetSubDistricts();

        List<SysTitle> GetTitles();

        PWACustomerInfo GetCustomers(string customerCode);
        List<VwSearchIncident> SearchIncidentByCriteria(SearchIncidentDto searchDto);
        List<VwSearchIncident> SearchIncidentByBranch(string branchNo, string provinceID, string detail, string area, DateTime startDate, DateTime endDate);
        List<VwSearchIncident> GetIncidentByBranch();

        List<FollowerDto> getFollowerByIncidentNo(string IncidentNo);
        List<FollowerDto> getFollowerById(string id);
        bool AddFollower(FollowerDto item);
        List<PwaIncidentFollower> GetFollowByIncidentID(int incidentID);

         void UpdateProcessStatus(List<PwaIncident> incidents);

        List<IncidentDto> SearchIncidents(string incidentNo, string subject, string informer, string receiver, DateTime? startDate, DateTime? endDate, string status);

        void UpdateStatus(PwaIncident incident, PwaIncidentWorker workerStatus, PwaBranchReject reject);
        List<VwPwaBranchReject> GetRejectHistory(int incidentNo);
        List<LocationDto> SearchSubDistricts(string name);
        VwLocation GetSubDistrictInfo(string id);
        List<VwIncidentListV2> GetIncidentsV2();
        List<sp_GetEffectCustomer_Result> GetEffectCustomer(string customerCode);
        PWAEmployee GetEmployee(string firstName, string lastName);
        VwIncidentListV2 GetIncidentWithInfo(int incidentID);

    }
}
