using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto;
namespace Pwa.FrameWork.Dao.Smart1662
{
    public class SysAccountDao : Smart1662DBBase
    {
        SearchDTO searchDto = null;
        SysAccountDto requestDto = null;
        UserInfoDto userInfReqDto = null;
        SysAccountDto result = null;
        UserInfoDto userInfResDto = null;
        List<SysAccountDto> resultList = null;
        public SysAccountDao()
        { }
        public override object FindByPK(object obj)
        {
            string sql = "sp_SysAccount_FindByPK";
            List<SqlParameter> paramList = new List<SqlParameter>();
              result = new SysAccountDto();
            requestDto = (SysAccountDto)obj;


            paramList.Add(new SqlParameter("@AccountId", requestDto.AccountId));
         

            try
            {
                result = this.FindByPK<SysAccountDto>(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("SysAccountDao.FindByPK::" + ex.ToString());
            }
            finally
            {

            }

            return result;
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

        public override List<object> FinByAll()
        {
            throw new NotImplementedException();
        }

        public  object Login(object obj)
        {
            string sql = "sp_SysAccount_Login";
            List<SqlParameter> paramList = new List<SqlParameter>();
            result = new SysAccountDto();
            userInfReqDto = (UserInfoDto)obj;


            paramList.Add(new SqlParameter("@UserLogin", userInfReqDto.UserLogin));
            paramList.Add(new SqlParameter("@Password", userInfReqDto.Password));

            try
            {
                userInfResDto = this.FindByPK<UserInfoDto>(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("SysAccountDao.Login::" + ex.ToString());
            }
            finally
            {

            }

            return userInfResDto;
        }



    }

}
