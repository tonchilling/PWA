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
    public class SysRequestCategorySubjectManager
    {
        private ISysRequestCategorySubjectResponsitory _sysResp = RespositoryFactory.GetSysRequestCategorySubjectResponsitory();

        public List<RequestCategorySubjectDto> All()
        {
            List<RequestCategorySubjectDto> lstDto = new List<RequestCategorySubjectDto>();
            List<SysRequestCategorySubject> lstEf = new List<SysRequestCategorySubject>();
            SysRequestCategoryManager rtManager = new SysRequestCategoryManager();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                lstEf = _sysResp.GetAll();
                
                
                foreach (SysRequestCategorySubject ef in lstEf)
                {
                    RequestCategorySubjectDto dto = new RequestCategorySubjectDto();
                    dto.Id = ef.id.ToString();
                    dto.ReqCategoryId = ef.reqcategoryid.ToString();
                    dto.ReqCategoryName = rtManager.GetById(ef.reqcategoryid).Name;
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
        public RequestCategorySubjectDto GetById(int id)
        {
            RequestCategorySubjectDto dto = new RequestCategorySubjectDto();
            SysRequestCategorySubject ef = new SysRequestCategorySubject();
            SysRequestCategoryManager rtManager = new SysRequestCategoryManager();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                ef = _sysResp.GetById(id);

                dto.Id = ef.id.ToString();
                dto.ReqCategoryId = ef.reqcategoryid.ToString();
                dto.ReqCategoryName = rtManager.GetById(ef.reqcategoryid).Name;
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
                dto.UpdatedBy = ef.UpdatedBy.ToString();

                var Updated = accManager.GetUserById(ef.UpdatedBy.ToString());
                dto.UpdatedByName = Updated.AccountFirstName + " " + Updated.AccountLastName;
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public List<RequestCategorySubjectDto> Search(string requestcategory, string name, string status)
        {
            List<RequestCategorySubjectDto> lstDto = new List<RequestCategorySubjectDto>();
            List<SysRequestCategorySubject> lstEf = new List<SysRequestCategorySubject>();
            RequestCategorySubjectDto input = new RequestCategorySubjectDto();
            SysRequestCategoryManager rtManager = new SysRequestCategoryManager();
            SysAccountManager accManager = new SysAccountManager();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(requestcategory + name + status);
                if (!inj) throw new Exception("Character not allowed.");
                lstEf = (requestcategory == "" && name == "" && status == "" ? _sysResp.GetAll() : _sysResp.Search(requestcategory, name, status));


                foreach (SysRequestCategorySubject ef in lstEf)
                {
                    RequestCategorySubjectDto dto = new RequestCategorySubjectDto();

                    dto.Id = ef.id.ToString();
                    dto.ReqCategoryId = ef.reqcategoryid.ToString();
                    dto.ReqCategoryName = rtManager.GetById(ef.reqcategoryid).Name;
                    dto.Name = ef.name;
                    dto.Description = ef.description;
                    dto.Status = ef.status.ToString();
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
        public bool Add(RequestCategorySubjectDto data)
        {
            RequestCategorySubjectDto userinfo = new RequestCategorySubjectDto();
            SysRequestCategorySubject reqData = new SysRequestCategorySubject();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(data.ReqCategoryId + data.Name + data.Status + data.Description);
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
                    if (data.ReqCategoryId == "" || data.ReqCategoryId == null) throw new Exception("Invalid input.");
                    reqData.reqcategoryid = Convert.ToInt32(data.ReqCategoryId);
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
