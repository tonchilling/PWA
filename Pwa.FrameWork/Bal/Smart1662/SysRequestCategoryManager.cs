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
    public class SysRequestCategoryManager
    {
        private ISysRequestCategoryResponsitory _sysResp = RespositoryFactory.GetSysRequestCategoryResponsitory();

        public List<RequestCategoryDto> All()
        {
            List<RequestCategoryDto> lstDto = new List<RequestCategoryDto>();
            List<SysRequestCategory> lstEf = new List<SysRequestCategory>();
            SysRequestTypeManager rtManager = new SysRequestTypeManager();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                lstEf = _sysResp.GetAll();
                
                
                foreach (SysRequestCategory ef in lstEf)
                {
                    RequestCategoryDto dto = new RequestCategoryDto();
                    dto.Id = ef.id.ToString();
                    dto.RequestTypeId = ef.requesttypeid.ToString();
                    dto.RequestTypeName = rtManager.GetById(ef.requesttypeid).Name;
                    dto.Name = ef.name;
                    dto.Description = ef.description;
                    dto.Status = ef.status.ToString();
                    if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                    else dto.CreatedDate = "";
                    dto.CreatedBy = ef.CreatedBy.ToString();
                    dto.CreatedByName = accManager.GetUserById(ef.CreatedBy.ToString()).UserLogin;
                    if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                    else dto.UpdatedDate = "";
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
        public RequestCategoryDto GetById(int id)
        {
            RequestCategoryDto dto = new RequestCategoryDto();
            SysRequestCategory ef = new SysRequestCategory();
            SysRequestTypeManager rtManager = new SysRequestTypeManager();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                ef = _sysResp.GetById(id);

                dto.Id = ef.id.ToString();
                dto.RequestTypeId = ef.requesttypeid.ToString();
                dto.RequestTypeName = rtManager.GetById(ef.requesttypeid).Name;
                dto.Name = ef.name;
                dto.Description = ef.description;
                dto.Status = ef.status.ToString();
                if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                else dto.CreatedDate = "";
                dto.CreatedBy = ef.CreatedBy.ToString();

                var Created = accManager.GetUserById(ef.CreatedBy.ToString());
                dto.CreatedByName = Created.AccountFirstName + " " + Created.AccountLastName;

                if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
                dto.UpdatedByName = ef.UpdatedBy.ToString();

                var Updated = accManager.GetUserById(ef.UpdatedBy.ToString());
                dto.UpdatedByName = Updated.AccountFirstName + " " + Updated.AccountLastName;
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public List<RequestCategoryDto> Search(string requesttype, string name, string status)
        {
            List<RequestCategoryDto> lstDto = new List<RequestCategoryDto>();
            List<SysRequestCategory> lstEf = new List<SysRequestCategory>();
            SysRequestCategory input = new SysRequestCategory();
            SysRequestTypeManager rtManager = new SysRequestTypeManager();
            SysAccountManager accManager = new SysAccountManager();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(requesttype + name + status);
                if (!inj) throw new Exception("Character not allowed.");
                lstEf = (requesttype == "" && name == "" && status == "" ? _sysResp.GetAll() : _sysResp.Search(requesttype, name, status));


                foreach (SysRequestCategory ef in lstEf)
                {
                    RequestCategoryDto dto = new RequestCategoryDto();

                    dto.Id = ef.id.ToString();
                    dto.RequestTypeId = ef.requesttypeid.ToString();
                    dto.RequestTypeName = rtManager.GetById(ef.requesttypeid).Name;
                    dto.Name = ef.name;
                    dto.Description = ef.description;
                    dto.Status = ef.status.ToString();
                    if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                    else dto.CreatedDate = "";
                    dto.CreatedBy = ef.CreatedBy.ToString();
                    dto.CreatedByName = accManager.GetUserById(ef.CreatedBy.ToString()).UserLogin;
                    if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                    else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
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
        public bool Add(RequestCategoryDto data)
        {
            RequestCategoryDto userinfo = new RequestCategoryDto();
            SysRequestCategory reqData = new SysRequestCategory();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(data.RequestTypeId + data.Name + data.Status + data.Description);
                if (!inj) throw new Exception("Character not allowed.");
                if (data != null)
                {
                    if (data.Id != null)
                    {
                        reqData.id = Convert.ToInt32(data.Id);
                        reqData.UpdatedBy = Convert.ToInt32(data.UpdatedBy);
                    }
                    else
                    {
                        reqData.CreatedBy = Convert.ToInt32(data.CreatedBy);
                        reqData.UpdatedBy = Convert.ToInt32(data.UpdatedBy);
                    }
                    reqData.name = data.Name;
                    reqData.status = Convert.ToInt32(data.Status);
                    if (data.RequestTypeId == "" || data.RequestTypeId == null) throw new Exception("Invalid input.");
                    reqData.requesttypeid = Convert.ToInt32(data.RequestTypeId);
                    reqData.description = data.Description;
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
