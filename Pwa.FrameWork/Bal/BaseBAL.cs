using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dao.ADO;
namespace Pwa.FrameWork.Bal
{
   public abstract class BaseBal
    {

        #region Abstract Method

        public abstract bool Insert(object obj);
        public abstract bool Update(object obj);
        public abstract bool Delete(object obj);
        public abstract object FindByPK(object obj);
        public abstract List<object> FinByAll();

        #endregion

        protected void Log(string methodName, string CreateBy, string Msg)
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


        protected void Log(string methodName,string CreateBy,string Msg,Exception ex)
        {
            LogDAO logDao = new LogDAO();
            LogDTO logDto = new LogDTO();
            logDto.Project = "Smart1662";
            logDto.Page = methodName;
            logDto.CreatedBy = CreateBy;
            logDto.IssueDate = DateTime.Now.ToString();
            logDto.ValueField = Msg;
            logDto.Exception = ex.Message;
            logDao.Log(logDto);

        }
    }
     
}
