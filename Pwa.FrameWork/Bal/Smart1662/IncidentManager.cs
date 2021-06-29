using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Extension;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662.Incident;
using Pwa.FrameWork.Dto.Smart1662;

using Pwa.FrameWork.Bal.Data;

using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dao.Service;
using System.IO;

namespace Pwa.FrameWork.Bal.Smart1662
{
    public class IncidentManager:BaseBal
    {
        private ISysRequestTypeRespository _requestTypeRespository = RespositoryFactory.GetSysRequestTypeRespository();
        private IPwaIncidentRespository _incidentRespository = RespositoryFactory.GetPwaIncidentResponsitory();
        private ISysBranchRespository _sysBranchRespository = RespositoryFactory.GetSysBranchRespository();
        private ISysStatusResponsitory _sysStatusResponsitory = RespositoryFactory.GetSysStatusResponsitory();

        public bool IsCallcenter { get; set; }

        public void Add(Pwa.FrameWork.Bal.Data.Incident incident)
        {
            List<PwaInformer> info = incident.Imformers;
            try
            {
                _incidentRespository.IsCallcenter = this.IsCallcenter;
                if (incident.SysOwnerCode != "IC360")
                    incident.PwaIncidentNo = _incidentRespository.GetNextIncidentNo();

                string enc = DEncrypt.encrypt(incident.PwaIncidentNo, "1662" + /*Convert.ToString(incident.PwaIncidentID)*/ Path.GetRandomFileName());
                incident.RandomNo = enc.Replace('/','6').Replace('=','x').Substring(0, 6);
                incident.PwaIncidentID= _incidentRespository.Add(incident.CopyTo<Dao.EF.Smart1662.PwaIncident>(new Dao.EF.Smart1662.PwaIncident()), incident.Imformers);
            }
            catch (Exception ex)
            {
                Log("IncidentManager.Insert", "", "error", ex);
                throw new Exception("Cannot Insert :" + ex.Message);
            }
        }

        public void AddFiles(List<FileDto> Files )
        {
            try
            {

                _incidentRespository.AddFiles(Files);
            }
            catch (Exception ex)
            {
                Log("IncidentManager.Insert", "", "error", ex);
                throw new Exception("Cannot Insert :" + ex.Message);
            }
        }

        public void Update(Pwa.FrameWork.Bal.Data.Incident incident)
        {
            try
            {
                _incidentRespository.Update(incident.CopyTo<Dao.EF.Smart1662.PwaIncident>(new Dao.EF.Smart1662.PwaIncident()), incident.Imformers, incident.Files);

                //   _incidentRespository.Update(incident.CopyTo<Dao.EF.Smart1662.PwaIncident>(new Dao.EF.Smart1662.PwaIncident()), incident.Imformers);
            }
            catch (Exception ex)
            {
                Log("IncidentManager.Insert", "", "error", ex);
                throw new Exception("Cannot Insert :" + ex.Message);
            }
        }

        public void UpdateIncidentStatus(int incidentID,int accountID,string branchNo,int status, string detail)
        {
            try
            {
                var target = _incidentRespository.GetIncident(incidentID);
                if (target != null)
                {
                    var targetStatus = (Pwa.FrameWork.Bal.PwaSystem.ProcessStage)status;

                    var incident = new PwaIncident()
                    {
                        PwaIncidentID = incidentID,
                        ProcessStage = targetStatus.GetInt(),
                        CustomerProcessStage = targetStatus.GetCustomerProcessStage().GetInt()
                    };

                    PwaIncidentWorker worker = new PwaIncidentWorker()
                    {
                        PwaIncidentID = incidentID,
                        PwaIncidentWorkerID = accountID,
                        ProcessStep = status,
                        CreatedDate = DateTime.Now,
                        CreatedBy = accountID
                    };

                    PwaBranchReject branchReject = null;
                    if(status == Pwa.FrameWork.Bal.PwaSystem.ProcessStage.BRANCH_REJECT.GetInt())
                    {
                        branchReject = new PwaBranchReject()
                        {
                            PwaIncidentID = incidentID,
                            BranchNo = branchNo,
                            IncidentStage = targetStatus.GetInt(),
                            IncidentStageDetail = detail ?? "",
                            CreatedBy = accountID.ToString(),
                            CreatedDate = DateTime.Now
                        };
                    }

                    _incidentRespository.UpdateStatus(incident, worker, branchReject);
                }

            }
            catch (Exception ex)
            {

                Log("IncidentManager.UpdateIncidentStatus", "", "error", ex);
                throw new Exception("Cannot UpdateIncidentStatus :" + ex.Message);
            }
        }

        public CisCustomer GetCisCustomer(string customerCode)
        {
            Pwa.FrameWork.Dao.Service.CisServices service = new CisServices();
            return service.GetCustomer(customerCode);
        }

        public SysStatus GetIncidentStatus(int status)
        {
            return _sysStatusResponsitory.GetStatuses(new int[] { status }).FirstOrDefault();
        }




        public List<VwPwaBranchReject> GetRejectHistory(int incidentID)
        {
            return _incidentRespository.GetRejectHistory(incidentID);
        }

        public List<PwaIncidentFollower> GetFollowByIncidentID(int incidentID)
        {
            return _incidentRespository.GetFollowByIncidentID(incidentID);
        }

        public List<CISProvince> GetProvince()
        {
            return _incidentRespository.GetProvince();
        }

        public Pwa.FrameWork.Bal.Data.IncidentEditInfo GetIncident(int incidentID)
        {
            

            return _incidentRespository.GetIncident(incidentID);
        }


        public PWACustomerInfo GetCustomerIsProcess(string CustomerCode)
        {

            // cannot get last
            return null;
            //  return  _incidentRespository.GetCustomerIsProcess(CustomerCode);
        }


        public List<PWACustomerInfo> GetCustomers()
        {
            return _incidentRespository.GetCustomers();
        }

        public List<CISAmphure> GetDistricts(string provinceID)
        {
            return _incidentRespository.GetDistricts(provinceID);
        }

        public List<CISAmphure> GetDistricts()
        {
            return _incidentRespository.GetDistricts();
        }
        public List<CISDistrinct> GetSubDistricts(string districtID)
        {
            return _incidentRespository.GetSubDistricts(districtID);
        }
        public List<RequestType> GetRequestTypes()
        {
            return _requestTypeRespository.GetRequestTypes();
        }

        public List<VwIncidentList> GetIncidents()
        {
            return _incidentRespository.GetIncidents();
        }

        public PwaIncident GetLastIncident()
        {
            return _incidentRespository.GetLastIncident();
        }

        public List<IncidentDto> GetIncidents(string[] id,string status)
        {
            return _incidentRespository.GetIncidents(id, status);
        }
        public List<IncidentDto> GetIncidents(IncidentDto searchData)
        {
            return _incidentRespository.GetIncidents(searchData);
        }

        public List<IncidentDto> GetIncidentsByBranch(IncidentDto searchData)
        {
            return _incidentRespository.GetIncidentsByBranch(searchData);
        }

        public List<LocationDto> GetSubDistricts()
        {
            return _incidentRespository.GetSubDistricts();
        }

        public SubdistrictDetail GetSubDistrictDetail(string subdistrictID)
        {
            return _incidentRespository.GetSubDistrictDetail(subdistrictID);
        }

        public void GetPwaBranchDataByCustomer(string customerID)
        {
            string brachCode = customerID.Substring(0, 4);

            var branch = _sysBranchRespository.GetByCode(brachCode);


        }


        public SearchBranchIncidentResult SearchBranchIncident(string customerNo,string provinceID,string incidentDetail,string incidentArea,DateTime startDate,DateTime endDate)
        {
            var customer = _incidentRespository.GetCustomers(customerNo);
            SearchBranchIncidentResult result = null;
            if (customer != null)
            {
                var branchNo = customerNo.Substring(0, 4);
                result = new SearchBranchIncidentResult()
                {
                    CustomerCode = customerNo.Substring(0, 4),
                    Customer = customer,
                    Branch = _sysBranchRespository.GetByCode(branchNo),
                    //Incidents = _incidentRespository.SearchIncidentByBranch(null, provinceID, incidentDetail ?? "", incidentArea ?? "", startDate, endDate)
                    Incidents = _incidentRespository.GetIncidentByBranch()
                };
            
            }

            return result;

        }


        public SearchBranchIncidentResult SearchIncidentByCriteria(SearchIncidentDto searchDto)
        {
            var branchNo = "";
            var customer = searchDto.CustomerCode!=null ?  _incidentRespository.GetCustomers(searchDto.CustomerCode) : null;
            SearchBranchIncidentResult result = null;
            if (customer != null)
            {
                 branchNo = searchDto.CustomerCode.Length >= 4 ? searchDto.CustomerCode.Substring(0, 4) : "";
            }
            result = new SearchBranchIncidentResult()
            {
                CustomerCode = searchDto.CustomerCode,
                    Customer = customer,
                    Branch = _sysBranchRespository.GetByCode(branchNo),
                    Incidents = _incidentRespository.SearchIncidentByCriteria(searchDto)
                   // Incidents = _incidentRespository.GetIncidentByBranch()
                };

          

            return result;

        }

        public List<sp_GetEffectCustomer_Result> GetEffectCustomer(string customerCode)
        {
            return _incidentRespository.GetEffectCustomer(customerCode);

        }

        




        public override bool Insert(object obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update(object obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public override object FindByPK(object obj)
        {
            throw new NotImplementedException();
        }

        public override List<object> FinByAll()
        {
            throw new NotImplementedException();
        }

        public List<FollowerDto> getFollowerByIncidentNo(string id)
        {
            List<FollowerDto> dto = new List<FollowerDto>();
            try
            {
                dto = _incidentRespository.getFollowerByIncidentNo(id);
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public List<FollowerDto> getFollowerById(string id)
        {
            List<FollowerDto> dto = new List<FollowerDto>();
            try
            {
                dto = _incidentRespository.getFollowerById(id);
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public bool AddFollower(FollowerDto data)
        {

            bool inj = false;
            try
            {
                if (data != null)
                {
                    if (data.Information == "" || data.Information == null)
                    {
                        throw new Exception("Information must not be blank.");
                    }
                }

                inj = Injection.SQLInjection(data.Information);
                if (!inj) throw new Exception("Character not allowed.");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _incidentRespository.AddFollower(data);
        }

        public void SetStatusToBranchReceiveCase(List<int> incidentsID)
        {
            var targets =incidentsID.Select(i => new PwaIncident()
            {
                PwaIncidentID = i,
                ProcessStage = 2,
                CustomerProcessStage = 2,
            }).ToList();

            _incidentRespository.UpdateProcessStatus(targets);
        }
             
        public List<IncidentDto> SearchIncidents(string incidentNo, string subject, string informer, string receiver, string startDate, string endDate, string status)
        {
            DateTime? ds = null;
            if((startDate != null && startDate != ""))
            {
                ds = DateTime.Parse(startDate, new System.Globalization.CultureInfo("th-TH"));
            }

            DateTime? de = null;
            if ((endDate != null && endDate != ""))
            {
                ds = DateTime.Parse(endDate, new System.Globalization.CultureInfo("th-TH"));
            }

            return _incidentRespository.SearchIncidents(incidentNo, subject, informer, receiver, ds, de,status);
        }

        public List<LocationDto> SearchSubDistrict(string name)
        {
            return _incidentRespository.SearchSubDistricts(name);
        }

        public VwLocation GetSubDistrictInfo(string id)
        {
            return _incidentRespository.GetSubDistrictInfo(id);
        }


        

    }
}
