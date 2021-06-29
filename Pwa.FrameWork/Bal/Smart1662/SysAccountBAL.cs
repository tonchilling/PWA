using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.ADO;
using Pwa.FrameWork.Dto.Smart1662;
namespace Pwa.FrameWork.Bal.Smart1662
{
   public class SysAccountBal: BaseBal
    {
        List<SysAccountDto> resultList = null;
        SysAccountDao dao = null;
        SysAccountDto dto = null;
        UserInfoDto userInfoDto = null;
        public SysAccountBal()
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
                Log("SysAccountBal.Login", "", "error",ex);
                throw new Exception("Cannot Login :"+ex.Message);
            }
            return userInfoDto;
        }

    }
}
