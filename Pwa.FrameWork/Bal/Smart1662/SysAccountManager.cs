using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.ADO;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dto.Utils;

namespace Pwa.FrameWork.Bal.Smart1662
{
   public class SysAccountManager: BaseBal
    {
        List<SysAccountDto> resultList = null;
        SysAccountDao dao = null;
        SysAccountDto dto = null;
        UserInfoDto userInfoDto = null;

        private ISysAccountRepository _sysResp = RespositoryFactory.GetSysAccountReponsitory(true);
        public SysAccountManager()
        { }

        public override bool Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public override List<object> FinByAll()
        {
            throw new NotImplementedException();
        }

        public override object FindByPK(object obj)
        {
            throw new NotImplementedException();
        }

     
        public override bool Insert(object obj)
        {
            throw new NotImplementedException();
        }


        public override bool Update(object obj)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfoDto> Login(UserInfoDto dto)
        {
            dao = new SysAccountDao();

            try
            {
                userInfoDto = (UserInfoDto)dao.Login(dto);
            }
            catch (Exception ex)
            {
                Log("SysAccountManager.Login", "", "error",ex);
                throw new Exception("Cannot Login :"+ex.Message);
            }
            return userInfoDto;
        }


        public async Task<List<SysAccountDto>> GetAll()
        {
            List<SysAccountDto> result = new List<SysAccountDto>();
            List<sp_SysAccount_GetAll_Result> respData = await _sysResp.GetAll();
            foreach (sp_SysAccount_GetAll_Result data in respData)
            {
                result.Add(new SysAccountDto()
                {
                   AccountId=data.AccountId.ToString(),
                   UserLogin = data.UserLogin,
                   AccountFirstName = data.AccountFirstName,
                   AccountLastName = data.AccountLastName,
                   AccountEmail = data.AccountEmail,
                   AccountRemark = data.AccountRemark,
                   FlagStatus = data.FlagStatus.ToString(),
                   RoleId = data.RoleId.ToString(),
                   RoleName = data.RoleName,
                    EmpId = data.EmpId.ToString(),
                    EmpCode=data.EmpCode
      
                });
                }

                    return result;

                }



        public async Task<SysAccountDto> GetById(string id)
        {
            SysAccountDto result = null;
            sp_SysAccount_GetAll_Result data = await _sysResp.GetById(id);

            result = new SysAccountDto()
            {
                AccountId = data.AccountId.ToString(),
                UserLogin = data.UserLogin,
                AccountFirstName = data.AccountFirstName,
                AccountLastName = data.AccountLastName,
                AccountEmail = data.AccountEmail,
                AccountRemark = data.AccountRemark,
                FlagStatus = data.FlagStatus.ToString(),
                RoleId = data.RoleId.ToString(),
                RoleName = data.RoleName,
                EmpId = data.EmpId.ToString(),
                EmpCode = data.EmpCode

            };
       

            return result;

        }
        public sp_SysAccount_GetUserById_Result GetUserById(string id)
        {
            sp_SysAccount_GetUserById_Result result = new sp_SysAccount_GetUserById_Result();
            sp_SysAccount_GetUserById_Result data = _sysResp.GetUserById(id);
            if (data == null)
            {
                result.UserLogin = "";
                result.AccountFirstName = "";
                result.EmpCode = "";
                result.RoleName = "";
            }
            else
            { 

                result = new sp_SysAccount_GetUserById_Result()
                {
                    AccountId = data.AccountId,
                    UserLogin = data.UserLogin,
                    AccountFirstName = data.AccountFirstName,
                    AccountLastName = data.AccountLastName,
                    AccountEmail = data.AccountEmail,
                    AccountRemark = data.AccountRemark,
                    FlagStatus = data.FlagStatus,
                    RoleId = data.RoleId,
                    RoleName = data.RoleName,
                    EmpId = data.EmpId,
                    EmpCode = data.EmpCode

                };
            }


            return result;

        }
        public async Task<SysAccountDto> GetByLogin(string UserLogin)
        {
            SysAccountDto result = null;
            sp_SysAccount_UserLoginExist_Result data = _sysResp.GetByLogin(UserLogin);
            //if(data == null)
            result = new SysAccountDto()
            {
                //AccountId = data.AccountId.ToString(),
                //UserLogin = data.UserLogin,
                //AccountFirstName = data.AccountFirstName,
                //AccountLastName = data.AccountLastName,
                //AccountEmail = data.AccountEmail,
                //AccountRemark = data.AccountRemark,
                //FlagStatus = data.FlagStatus.ToString(),
                //RoleId = data.RoleId.ToString(),
                //RoleName = data.RoleName,
                //EmpId = data.EmpId.ToString(),
                //EmpCode = data.EmpCode

            };


            return result;

        }
        public bool Add(SysAccountDto data)
        {

            bool inj = false;
            try
            {
                //if (data == null)
                //{
                    
                //            throw new Exception("Information must not be blank.");
                //}

                //inj = Injection.SQLInjection(data.ItemCode + data.ItemName);
                //if (!inj) throw new Exception("Character not allowed.");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _sysResp.Add(data);
        }
        public List<SysAccountDto> Search(string AccountId, string UserLogin, string Name, string Status, string BaCode)
        {
            List<SysAccountDto> dto = new List<SysAccountDto>();
            bool inj = false;
            try
            {
                inj = Injection.SQLInjection(AccountId + Name + Status + BaCode);
                if (!inj) throw new Exception("Character not allowed.");
                dto = _sysResp.Search(AccountId, UserLogin, Name, Status, BaCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dto;
        }
        public bool SaveAccount(SysAccountDto data)
        {

            bool inj = false;
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _sysResp.SaveAccount(data);
        }
        public bool DeleteAccount(SysAccountDto data)
        {

            bool inj = false;
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _sysResp.DeleteAccount(data);
        }

    }
}
