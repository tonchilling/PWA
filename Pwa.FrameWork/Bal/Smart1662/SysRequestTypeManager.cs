using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Utils;



namespace Pwa.FrameWork.Bal.Smart1662
{
    public class SysRequestTypeManager
    {
        private ISysRequestTypeRespository _sysResp = RespositoryFactory.GetSysRequestTypeRespository();

        public List<RequestTypeDto> All()
        {
            List<RequestTypeDto> lstDto = new List<RequestTypeDto>();
            List<SysRequestType> lstEf = new List<SysRequestType>();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                lstEf = _sysResp.GetAll();
                
                
                foreach (SysRequestType ef in lstEf)
                {
                    RequestTypeDto dto = new RequestTypeDto();
                    dto.Id = ef.Id.ToString();
                    dto.Name = ef.Name;
                    dto.Description = ef.Description;
                    dto.Status = ef.Status.ToString();
                    if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                    else dto.CreatedDate = "";
                    dto.CreatedBy = ef.CreatedBy.ToString();
                    dto.CreatedByName = accManager.GetUserById(ef.CreatedBy.ToString()).UserLogin;
                    if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                    else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
                    dto.UpdatedBy = ef.UpdatedBy.ToString();
                    dto.UpdatedByName = accManager.GetUserById(ef.UpdatedBy.ToString()).UserLogin;
                    lstDto.Add(dto);
                }
            }
            catch (Exception ex)
            {

            }

            return lstDto;
        }
        public RequestTypeDto GetById(int id)
        {
            RequestTypeDto dto = new RequestTypeDto();
            SysRequestType ef = new SysRequestType();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                ef = _sysResp.GetById(id);

                dto.Id = ef.Id.ToString();
                dto.Name = ef.Name;
                dto.Description = ef.Description;
                dto.Status = ef.Status.ToString();
                if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                else dto.CreatedDate = "";
                dto.CreatedBy = ef.CreatedBy.ToString();
                var Created = accManager.GetUserById(ef.CreatedBy.ToString());
                dto.CreatedByName = Created.AccountFirstName + " " + Created.AccountLastName;

                if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
                dto.UpdatedBy = ef.UpdatedBy.ToString();

                var Updated = accManager.GetUserById(ef.UpdatedBy.ToString());
                dto.UpdatedByName = Updated.AccountFirstName + " " + Updated.AccountLastName;
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public List<RequestTypeDto> Search(string name, string status)
        {
            List<RequestTypeDto> lstDto = new List<RequestTypeDto>();
            List<SysRequestType> lstEf = new List<SysRequestType>();
            SysRequestType input = new SysRequestType();
            SysAccountManager accManager = new SysAccountManager();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(name + status);
                if (!inj) throw new Exception("Character not allowed.");
                lstEf = (name == "" && status == "" ? _sysResp.GetAll() : _sysResp.Search(name,status));


                foreach (SysRequestType ef in lstEf)
                {
                    RequestTypeDto dto = new RequestTypeDto();

                    dto.Id = ef.Id.ToString();
                    dto.Name = ef.Name;
                    dto.Description = ef.Description;
                    dto.Status = ef.Status.ToString();
                    if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                    else dto.CreatedDate = "";
                    dto.CreatedBy = ef.CreatedBy.ToString();
                    dto.CreatedByName = accManager.GetUserById(ef.CreatedBy.ToString()).UserLogin;
                    if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                    else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
                    dto.UpdatedBy = ef.UpdatedBy.ToString();
                    dto.UpdatedByName = accManager.GetUserById(ef.UpdatedBy.ToString()).UserLogin;
                    lstDto.Add(dto);
                }
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message); 
            }

            return lstDto;
        }
        public bool Add(RequestTypeDto data)
        {
            RequestTypeDto userinfo = new RequestTypeDto();
            SysRequestType reqData = new SysRequestType();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(data.Name + data.Status);
                if (!inj) throw new Exception("Character not allowed.");
                if (data != null)
                {
                    if (data.Id != null)
                    {
                        reqData.Id = Convert.ToInt32(data.Id);
                        reqData.UpdatedBy = Convert.ToInt32(data.UpdatedBy);
                    }
                    else
                    {
                        reqData.CreatedBy = Convert.ToInt32(data.CreatedBy);
                        reqData.UpdatedBy = Convert.ToInt32(data.UpdatedBy);
                }
                reqData.Name = data.Name;
                reqData.Status = Convert.ToInt32(data.Status);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _sysResp.Add(reqData);
        }

    }
}
