using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{

    public interface ISysAccountRepository
    {

        bool Add(SysAccountDto data);
        Task<bool> Add(List<SysAccount> comlain);
        Task Update(SysAccount comlain);
        Task Delete(SysAccount comlain);
        Task<sp_SysAccount_GetAll_Result> GetById(string id);
        Task<List<sp_SysAccount_GetAll_Result>> GetAll();
        sp_SysAccount_GetUserById_Result GetUserById(string id);
        sp_SysAccount_UserLoginExist_Result GetByLogin(string UserLogin);
        List<SysAccountDto> Search(string AccountId, string UserLogin, string Name, string Status, string BaCode);
        bool SaveAccount(SysAccountDto data);
        bool DeleteAccount(SysAccountDto data);



    }
}
