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
    public class SysAreaManager
    {
        private ISysAreaRespository _sysResp = RespositoryFactory.GetSysAreaRespository();
        private Smart1662Entities context = new Smart1662Entities();

        public List<AreaDto> All()
        {
            List<AreaDto> lstDto = new List<AreaDto>();
            List<SysArea> lstEf = new List<SysArea>();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                lstEf = _sysResp.GetAll();

                foreach (SysArea ef in lstEf)
                {
                    AreaDto dto = new AreaDto();
                    dto.Id = ef.Id.ToString();
                    dto.Code = ef.Code;
                    dto.Name = ef.Name;
                    dto.Status = ef.Status.ToString();
                    dto.CCTR_CODE = ef.CCTR_CODE;
                    dto.WW_CODE = ef.WW_CODE;
                    dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                    dto.CreatedBy = ef.CreatedBy.ToString();
                    dto.CreatedByName = "";
                    dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                    dto.UpdatedBy = ef.UpdatedBy.ToString();
                    dto.UpdatedByName = "";
                    lstDto.Add(dto);
                }
            }
            catch (Exception ex)
            {

            }

            return lstDto;
        }
        public AreaDto GetById(int id)
        {
            AreaDto dto = new AreaDto();
            SysArea ef = new SysArea();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                ef = _sysResp.GetById(id);

                dto.Id = ef.Id.ToString();
                dto.Code = ef.Code;
                dto.Name = ef.Name;
                dto.Status = ef.Status.ToString();
                dto.CCTR_CODE = ef.CCTR_CODE;
                dto.WW_CODE = ef.WW_CODE;
                if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                else dto.CreatedDate = "";
                dto.CreatedBy = ef.CreatedBy.ToString();
                dto.CreatedByName = accManager.GetUserById(ef.CreatedBy.ToString()).UserLogin;
                if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
                dto.UpdatedBy = ef.UpdatedBy.ToString();
                dto.UpdatedByName = accManager.GetUserById(ef.UpdatedBy.ToString()).UserLogin;
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public AreaDto GetByCode(string code)
        {
            AreaDto dto = new AreaDto();
            SysArea ef = new SysArea();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                ef = _sysResp.GetByCode(code);

                dto.Id = ef.Id.ToString();
                dto.Code = ef.Code;
                dto.Name = ef.Name;
                dto.Status = ef.Status.ToString();
                dto.CCTR_CODE = ef.CCTR_CODE;
                dto.WW_CODE = ef.WW_CODE;
                if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                else dto.CreatedDate = "";
                dto.CreatedBy = ef.CreatedBy.ToString();
                dto.CreatedByName = accManager.GetUserById(ef.CreatedBy.ToString()).UserLogin;
                if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
                dto.UpdatedBy = ef.UpdatedBy.ToString();
                dto.UpdatedByName = accManager.GetUserById(ef.UpdatedBy.ToString()).UserLogin;
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public List<AreaDto> Search(string code, string name, string status)
        {
            List<AreaDto> lstDto = new List<AreaDto>();
            List<SysArea> lstEf = new List<SysArea>();
            SysArea input = new SysArea();
            SysAccountManager accManager = new SysAccountManager();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(name + status);
                if (!inj) throw new Exception("Character not allowed.");
                lstEf = (code == "" && name == "" && status == "" ? _sysResp.GetAll() : _sysResp.Search(code, name,status));


                foreach (SysArea ef in lstEf)
                {
                    AreaDto dto = new AreaDto();

                    dto.Id = ef.Id.ToString();
                    dto.Code = ef.Code;
                    dto.Name = ef.Name;
                    dto.Status = ef.Status.ToString();
                    dto.CCTR_CODE = ef.CCTR_CODE;
                    dto.WW_CODE = ef.WW_CODE;
                    if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                    else dto.CreatedDate = "";
                    dto.CreatedBy = ef.CreatedBy.ToString();
                    dto.CreatedByName = "";
                    if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                    else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
                    dto.UpdatedBy = ef.UpdatedBy.ToString();
                    dto.UpdatedByName = "";
                    lstDto.Add(dto);
                }
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message); 
            }

            return lstDto;
        }
        public bool Add(AreaDto data)
        {
            AreaDto userinfo = new AreaDto();
            SysArea reqData = new SysArea();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(data.Code + data.Name + data.Status);
                if (!inj) throw new Exception("Character not allowed.");
                if (data != null)
                {
                    if (data.Id != null)
                    {
                        reqData.Id = Convert.ToInt32(data.Id);
                        reqData.UpdatedBy = data.UpdatedBy;
                        string code = GetById(reqData.Id).Code;
                        if(code != data.Code) throw new Exception("Can not change the area code.");
                    }
                    else
                    {
                        reqData.CreatedBy = data.CreatedBy;
                        reqData.UpdatedBy = data.UpdatedBy;
                    }
                    reqData.Code = data.Code;
                    reqData.Name = data.Name;
                    reqData.Status = Convert.ToInt32(data.Status);
                    reqData.CCTR_CODE = data.CCTR_CODE;
                    reqData.WW_CODE = data.WW_CODE;
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
