using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dao.ADO;
namespace Pwa.FrameWork.Bal
{
    public class LogBal
    {

        public static void Log(string methodName, string CreateBy, string Msg)
        {
            LogDAO logDao = new LogDAO();
            LogDTO logDto = new LogDTO();
            logDto.Project = "Smart1662";
            logDto.Page = methodName;
            logDto.CreatedBy = CreateBy;
            logDto.IssueDate = DateTime.Now.ToString();
            logDto.ValueField = Msg;
            logDto.Exception = "";
            logDto.Status = "";
            logDao.Log(logDto);

        }
    }
}
