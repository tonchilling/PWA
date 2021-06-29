using Pwa.FrameWork.Bal.IC360.Data;
using Pwa.FrameWork.Dao.ADO;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Extension;

namespace Pwa.FrameWork.Bal.IC360
{
    public class Mapper
    {
        private MapperConfig _mapperConfig = new MapperConfig();

        public Bal.Data.Incident GetMappingData(VwIC360 data)
        {
            Bal.Data.Incident result = new Bal.Data.Incident();

            var incident = GetIncident(data);
            var informer = GetInformer(data);

            incident.CopyTo<Bal.Data.Incident>(result);
            result.Imformers = new List<PwaInformer>(0);
            result.Imformers.Add(informer);

            return result;
        }

        private PwaInformer GetInformer(VwIC360 data)
        {
            return new PwaInformer()
            {

                Title = "",
                FirstName = data.KS_C_FIRST_NAME??"",
                LastName = data.KS_C_LAST_NAME??"",
                MeterNo = "",
                Telephone = data.CALLBACK_PHONE??"",
                InformChannelID = _mapperConfig.MapInformChannel(data.CONTACT_CHANNEL_ID),
                InformReference = _mapperConfig.MapInformChannelReference(data.CONTACT_CHANNEL_ID),
                ProvinceID = "",
                DistrictID = "",
                SubDistrictID = "",
                Address = "",
                CreatedDate = DateTime.Now,
                CreatedBy = "1",
                Email = data.EMAIL??"",
                FaceBook = "",
                ICustomerType = "",
            };
        }

        /**/
        private PwaIncident GetIncident(VwIC360 data)
        {
            IPwaIncidentRespository incident = RespositoryFactory.GetPwaIncidentResponsitory();

           
            var cateGories = new string[0];
            if (data.CATEGORY_TREE != null)
                cateGories = data.CATEGORY_TREE.Split("|".ToCharArray());

            string incTypeID = (cateGories.Length > 0) ? cateGories[0] : "";
            string incCategory = (cateGories.Length > 1) ? cateGories[1] : "";
            string incCategorySubject = (cateGories.Length > 2) ? cateGories[2] : "";

            PWAEmployee employee = null;

            if(!string.IsNullOrEmpty(data.AG_O_FIRST_NAME) && !string.IsNullOrEmpty(data.AG_O_LAST_NAME))
            {
                employee = incident.GetEmployee(data.AG_O_FIRST_NAME, data.AG_O_LAST_NAME);
                if (employee == null)
                {
                }

            }

            return new PwaIncident()
            {
                //PwaIncidentID = "",
                PwaIncidentNo = data.INCIDENT_ID,
                PwaIncidentTypeID = _mapperConfig.GetMappingIncidentType(incTypeID),
                PwaIncidentGroupID = _mapperConfig.GetMappingIncidentCategory(incCategory),
                CaseTitle = _mapperConfig.GetMappingIncidentCategorySubject(incCategorySubject),
                CaseTitleDetail = "",
                CaseDetail = data.SUBJECT??"",
                ResolvedDetail = data.SOLUTION_DETAIL??"",
                Sla = (data.SLA!=null && data.SLA == "insla") ,
                SlaDetail = "",
                ReceivedCaseDate = data.RESOLVED_BEF_DT.Value,
                CompletedCaseDate = data.CLOSED_DT.Value,
                CaseLatitude = "",
                CaseLongtitude = "",
                PwsIncidentAddress = "",
                ProcessStage = ( data.CLOSED_DT.HasValue)?10:1,
                CustomerProcessStage = (data.CLOSED_DT.HasValue) ? 10 : 1,
                PwaParentIncidentID = "",
                Status = 1,
                PwaInformReceiver = "1",
                PwaInformDate = data.RESOLVED_BEF_DT.Value,
                BracnchNo = "1001",
                Recorder = "1",
                RecordDate = data.RESOLVED_BEF_DT.Value,
                ImageFilePath = "",
                CreatedDate = DateTime.Now,
                CreatedBy = "1",
                CreatedBA = "1001",
                UpdatedDate = DateTime.Now,
                UpdatedBy = "1",
                UpdatedBA = "1001",
                SysOwnerCode = "IC360"

                };

        }
         
    }

    class MapperConfig
    {
        public IIC360MappingResponsitory _maConfig = RespositoryFactory.GetIC360MappingResponsitory();

        public List<IC360Mapping> _mappingConfig = null;

        public MapperConfig()
        {
            _mappingConfig = _maConfig.GetMappingConfigs();
        }
        
        public string GetMappingIncidentType(string oldValue)
        {
            var result = GetConfig(oldValue);
            return (result != null) ? result.NewValue : "7";
        }

        public string GetMappingIncidentCategory(string oldValue)
        {
            var result = GetConfig(oldValue);
            return (result != null) ? result.NewValue : "1010";
        }
        public string GetMappingIncidentCategorySubject(string oldValue)
        {
            var result = GetConfig(oldValue);
            return (result != null) ? result.NewValue : "32";
        }

        public string MapInformChannel(string oldValue)
        {
            var result = GetConfig(oldValue, "INFORMCHL");
            return (result != null) ? result.NewValue : "1004";
        }

        public string MapInformChannelReference(string oldValue)
        {
            var result = GetConfig(oldValue, "INFORMCHL");
            return (result != null && result.OldValue=="1004") ? result.OldValue : "";
        }

        private IC360Mapping GetConfig(string oldValue,string prefix=null)
        {
            IC360Mapping result = null;
            if (prefix == null)
            {
                result = _mappingConfig.Where(c => c.OldValue == oldValue).FirstOrDefault();
            }
            else
            {
                result = _mappingConfig.Where(c => c.OldValue == oldValue && c.Prefix == prefix).FirstOrDefault();
            }

            return result;
            
        }

        
    }


}
