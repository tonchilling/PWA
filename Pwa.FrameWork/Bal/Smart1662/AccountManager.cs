using Pwa.FrameWork.Bal.Data;
using Pwa.FrameWork.Dao.ADO;
using Pwa.FrameWork.Dto.Smart1662;
using Pwa.FrameWork.Dto.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.Smart1662
{
    public class AccountManager
    {
        public void Login(LoginInfo login)
        {

            string EncryptKey = ConfigurationManager.AppSettings["EncryptKey"];
            String Api_Login_Url = ConfigurationManager.AppSettings["Api_Login_Url"];
            String tokenString = ConfigurationManager.AppSettings["tokenString"];

            string token = DEncrypt.encryptSHA1($"{tokenString}{DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("th-TH"))}");
            string password = login.PassWord;

            UserInfoDto user = null;
            if (!login.IsPwaEmployee)
            {
                password = DEncrypt.encrypt(password, EncryptKey);
                var dao = new SysAccountDao();
                user = (UserInfoDto)dao.Login(new UserInfoDto
                {
                   UserLogin = login.UserName,
                   Password = password,
                   NonEmp = (!login.IsPwaEmployee)?"1":"0"

                });
            }
            else
            {
                
            }


        }
    }
}
