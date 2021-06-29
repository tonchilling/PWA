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
using Pwa.FrameWork.Dto.PWA;


namespace Pwa.FrameWork.Bal.Smart1662
{
    public class SysBranchManager
    {
        private ISysBranchRespository _sysResp = RespositoryFactory.GetSysBranchRespository();

        public List<BranchDto> All()
        {
            List<BranchDto> lstDto = new List<BranchDto>();
            List<SysBranch> lstEf = new List<SysBranch>();
            SysAreaManager areaManager = new SysAreaManager();
            SysAccountManager accManager = new SysAccountManager();
            List<BranchDto> target = new List<BranchDto>();
            try
            {
                lstEf = _sysResp.GetAll();

                lstDto = (from c in lstEf
                          join a in areaManager.All().ToList() on c.AreaCode equals a.Code
                 select new BranchDto
                 {
                     Id = c.Id.ToString(),
                     Code = c.Code,
                     Name = c.Name,
                     AreaCode = c.AreaCode,
                     AreaName = a.Name,
                     CCTR_CODE = c.CCTR_CODE,
                     WW_CODE = c.WW_CODE,
                     CreatedDate = Converting.DateOfdd_MM_yyyyTH(c.CreatedDate.HasValue ? c.CreatedDate.Value : c.CreatedDate.Value),
                     CreatedBy = c.CreatedBy,
                     UpdatedDate = Converting.DateOfdd_MM_yyyyTH(c.UpdatedDate.HasValue ? c.UpdatedDate.Value : c.UpdatedDate.Value),
                     UpdatedBy = c.UpdatedBy,
                     Status = c.Status.ToString()
                 }).ToList();

            }
            catch (Exception ex)
            {

            }

            return lstDto;
        }
        public BranchDto GetById(int id)
        {
            BranchDto dto = new BranchDto();
            SysBranch ef = new SysBranch();
            SysAreaManager areaManager = new SysAreaManager();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                ef = _sysResp.GetById(id);

                dto.Id = ef.Id.ToString();
                dto.Code = ef.Code;
                dto.Name = ef.Name;
                dto.AreaCode = ef.AreaCode;
                dto.AreaName = areaManager.GetByCode(ef.AreaCode).Name;
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
        public BranchDto GetByCode(string code)
        {
            BranchDto dto = new BranchDto();
            SysBranch ef = new SysBranch();
            SysAreaManager areaManager = new SysAreaManager();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                ef = _sysResp.GetByCode(code);

                dto.Id = ef.Id.ToString();
                dto.Code = ef.Code;
                dto.Name = ef.Name;
                dto.AreaCode = ef.AreaCode;
                dto.AreaName = areaManager.GetByCode(ef.AreaCode).Name;
                dto.AreaBACode = ef.Parent;
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
                dto.TypeCode = ef.TypeCode;
            }
            catch (Exception ex)
            {

            }

            return dto;
        }

        public PWABranchDTO GetPWAByCode(string code)
        {
            PWABranchDTO dto = new PWABranchDTO();
            SysBranch ef = new SysBranch();
            SysAreaManager areaManager = new SysAreaManager();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                dto = _sysResp.GetPWABranchByCode(code);

              
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public List<BranchDto> Search(string Id, string Code, string Name, string CCTR_CODE, string WW_CODE, string TypeCode, string Parent, string Status)
        {
            List<BranchDto> dto = new List<BranchDto>();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(Id + Code + Name + CCTR_CODE + WW_CODE + TypeCode + Parent + Status);
                if (!inj) throw new Exception("Character not allowed.");
                dto = _sysResp.Search(Id, Code, Name, CCTR_CODE, WW_CODE, TypeCode, Parent, Status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dto;
        }
        public bool Add(BranchDto data)
        {
            BranchDto userinfo = new BranchDto();
            BranchDto reqData = new BranchDto();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(data.Code + data.Name + data.Status);
                if (!inj) throw new Exception("Character not allowed.");
                if (data != null)
                {
                    if (data.Id != null)
                    {
                        reqData.Id = data.Id;
                        reqData.UpdatedBy = data.UpdatedBy;
                        //string code = GetById(reqData.Id).Code;
                        //if(code != data.Code) throw new Exception("Can not change the area code.");
                    }
                    else
                    {
                        reqData.CreatedBy = data.CreatedBy;
                        reqData.UpdatedBy = data.UpdatedBy;
                    }
                    reqData.Code = data.Code;
                    reqData.Name = data.Name;
                    reqData.AreaCode = data.AreaCode;
                    reqData.Status = data.Status;
                    reqData.CCTR_CODE = data.CCTR_CODE;
                    reqData.WW_CODE = data.WW_CODE;
                    reqData.Parent = data.Parent;
                    reqData.TypeCode = data.TypeCode;
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
