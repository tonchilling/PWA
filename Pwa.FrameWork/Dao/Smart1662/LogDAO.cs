using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Pwa.FrameWork.Dto.Utils;

namespace Pwa.FrameWork.Dao.Smart1662
{
    public class LogDAO : Smart1662DBBase
    {
        public LogDAO(){
            //OpenConnection();
    }


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


        public  bool Log(object obj)
        {
            bool result = false;
            LogDTO dto = (LogDTO)obj;
            int id = 0;
            string sql = "sp_Log_Add";





            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Project", dto.Project));
            paramList.Add(new SqlParameter("@Page", dto.Page));
      //      paramList.Add(new SqlParameter("@Row_State", dto.Row_State));
            paramList.Add(new SqlParameter("@ValueField", dto.ValueField));
            paramList.Add(new SqlParameter("@Exception", dto.Exception));
            paramList.Add(new SqlParameter("@CreatedBy", dto.CreatedBy));



            try
            {
                result = ExcecuteNonQuery(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("LogDAO.Log::(" + sql + ")" + ex.ToString());
            }
            finally
            { }


            CloseConnection();


            return result;
        }
        public bool Log(string Project,string Page,string Status,string ValueField,string Exception,string CreateBy)
        {
            bool result = false;
           
            int id = 0;
            string sql = "sp_Log_Add";





            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Project", Project));
            paramList.Add(new SqlParameter("@Page", Page));
            paramList.Add(new SqlParameter("@Status", Status));
            paramList.Add(new SqlParameter("@ValueField", ValueField));
            paramList.Add(new SqlParameter("@Exception", Exception));
            paramList.Add(new SqlParameter("@CreateBy", CreateBy));



            try
            {
                result = ExcecuteNonQuery(sql, paramList);
            }
            catch (Exception ex)
            {
                throw new Exception("LogDAO.Log::(" + sql + ")" + ex.ToString());
            }
            finally
            { }


            CloseConnection();


            return result;
        }

    }
}
