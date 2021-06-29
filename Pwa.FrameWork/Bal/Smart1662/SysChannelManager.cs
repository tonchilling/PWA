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
    public class SysChannelManager
    {
        private ISysChannelRespository _sysChannelResp = RespositoryFactory.GetSysChannelRespository();
        private ISysAccountRepository _sysResp = RespositoryFactory.GetSysAccountReponsitory(true);

        public List<ChannelDto> AllChannel()
        {
            List<ChannelDto> lstDto = new List<ChannelDto>();
            List<SysChannel> lstEf = new List<SysChannel>();
            SysAccountManager accManager = new SysAccountManager();

            try
            {
                lstEf = _sysChannelResp.GetAll();
                
                
                foreach (SysChannel ef in lstEf)
                {
                    ChannelDto dto = new ChannelDto();
                    dto.ChannelId = ef.ChannelId.ToString();
                    dto.ChannelName = ef.ChannelName;
                    dto.Status = ef.Status.ToString();
                    if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                    else dto.CreatedDate = "";
                    dto.CreatedBy = ef.CreatedBy.ToString();
                    dto.CreatedByName = accManager.GetUserById(ef.CreatedBy.ToString()).AccountFirstName;
                    if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                    else dto.UpdatedDate = ""; dto.UpdatedBy = ef.UpdatedBy.ToString();
                    dto.UpdatedByName = accManager.GetUserById(ef.UpdatedBy.ToString()).AccountFirstName;
                    lstDto.Add(dto);
                }
            }
            catch (Exception ex)
            {

            }

            return lstDto;
        }
        public ChannelDto GetChannelById(int id)
        {
            ChannelDto dto = new ChannelDto();
            SysChannel ef = new SysChannel();
            SysAccountManager accManager = new SysAccountManager();
            try
            {
                ef = _sysChannelResp.GetById(id);
                    
                dto.ChannelId = ef.ChannelId.ToString();
                dto.ChannelName = ef.ChannelName;
                dto.Status = ef.Status.ToString();
                if (ef.CreatedDate.HasValue) dto.CreatedDate = Converting.DateOfdd_MM_yyyyTH(ef.CreatedDate.HasValue ? ef.CreatedDate.Value : ef.CreatedDate.Value);
                else dto.CreatedDate = "";
                dto.CreatedBy = ef.CreatedBy.ToString();

                var Created = accManager.GetUserById(ef.CreatedBy.ToString());
                dto.CreatedByName = Created.AccountFirstName + " " + Created.AccountLastName;

                if (ef.UpdatedDate.HasValue) dto.UpdatedDate = Converting.DateOfdd_MM_yyyyTH(ef.UpdatedDate.HasValue ? ef.UpdatedDate.Value : ef.UpdatedDate.Value);
                else dto.UpdatedDate = "";
                dto.UpdatedBy = ef.UpdatedBy.ToString();

                var Updated = accManager.GetUserById(ef.UpdatedBy.ToString());
                dto.UpdatedByName = Updated.AccountFirstName + " " + Updated.AccountLastName;
            }
            catch (Exception ex)
            {

            }

            return dto;
        }
        public List<ChannelDto> SearchChannel(string name, string status)
        {
            List<ChannelDto> lstDto = new List<ChannelDto>();
            List<SysChannel> lstEf = new List<SysChannel>();
            SysChannel input = new SysChannel();
            SysAccountManager accManager = new SysAccountManager();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(name + status);
                if (!inj) throw new Exception("Character not allowed.");
                lstEf = (name == "" && status == "" ? _sysChannelResp.GetAll() : _sysChannelResp.SearchChannel(name,status));


                foreach (SysChannel ef in lstEf)
                {
                    ChannelDto dto = new ChannelDto();
                    dto.ChannelId = ef.ChannelId.ToString();
                    dto.ChannelName = ef.ChannelName;
                    dto.Status = ef.Status.ToString();
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
        public bool Add(ChannelDto data)
        {
            SysChannel reqData = new SysChannel();
            UserInfoDto userinfo = new UserInfoDto();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(data.ChannelName + data.Status);
                if (!inj) throw new Exception("Character not allowed.");
                if (data != null)
                {
                    if (data.ChannelId != null)
                    {
                        reqData.ChannelId = Convert.ToInt32(data.ChannelId);
                        reqData.UpdatedBy = Convert.ToInt32(data.UpdatedBy);
                    }
                    else
                    {
                        reqData.CreatedBy = Convert.ToInt32(data.CreatedBy);
                        reqData.UpdatedBy = Convert.ToInt32(data.UpdatedBy);
                    }
                    reqData.ChannelName = data.ChannelName;
                    reqData.Status = Convert.ToInt32(data.Status);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _sysChannelResp.Add(reqData);
        }

    }
}
