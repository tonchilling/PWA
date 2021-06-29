using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Smart1662;
using System.Data.SqlClient;
using System.Data;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysAccountReponsitory : ISysAccountRepository
    {
        protected Smart1662ADO db;

        public SysAccountReponsitory()
        {

            db = new Smart1662ADO();

        }
        public bool Add(SysAccountDto dto)
        {
            bool result = false;
            SqlTransaction trasaction = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            int res = 0;
            try
            {
                db.OpenConnection();
                string sql = "sp_SysAccount_Add";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@UserLogin", dto.UserLogin));
                paramList.Add(new SqlParameter("@AccountFirstName", dto.AccountFirstName));
                paramList.Add(new SqlParameter("@AccountLastName", dto.AccountLastName));
                paramList.Add(new SqlParameter("@AccountEmail", dto.AccountEmail));
                paramList.Add(new SqlParameter("@FlagStatus", dto.FlagStatus));
                paramList.Add(new SqlParameter("@FlagSystem", dto.FlagSystem));
                paramList.Add(new SqlParameter("@FlagAdminCalc", dto.FlagAdminCalc));
                paramList.Add(new SqlParameter("@Ba", dto.Ba));
                paramList.Add(new SqlParameter("@RoleId", dto.RoleId));

                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());
                sqlCommand.Transaction = trasaction;
                res = sqlCommand.ExecuteNonQuery();
                trasaction.Commit();

                result = res > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }
            return result;
        }

        public async Task<bool> Add(List<SysAccount> reqList)
        {
            throw new NotImplementedException();
        }

        public Task Update(SysAccount channel)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysAccount channel)
        {
            throw new NotImplementedException();
        }

        public async Task<sp_SysAccount_GetAll_Result> GetById(string id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target =  context.sp_SysAccount_GetById(id);
                return null;
            }
        }
 
        public async Task<List<sp_SysAccount_GetAll_Result>> GetAll()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target =  context.sp_SysAccount_GetAll().ToList();
                return target;
            }
        }
        public sp_SysAccount_GetUserById_Result GetUserById(string id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.sp_SysAccount_GetUserById(id).FirstOrDefault();
                return target;
            }
        }
        public sp_SysAccount_UserLoginExist_Result GetByLogin(string UserLogin)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = context.sp_SysAccount_UserLoginExist(UserLogin).FirstOrDefault();
                return target;
            }
        }

        public List<SysAccountDto> Search(string AccountId, string UserLogin, string Name, string Status, string BaCode)
        {
            string sql = "sp_UserAccount_Search";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<SysAccountDto> list = new List<SysAccountDto>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@AccountId", AccountId));
                paramList.Add(new SqlParameter("@UserLogin", UserLogin));
                paramList.Add(new SqlParameter("@Name", Name));
                paramList.Add(new SqlParameter("@Status", Status));
                paramList.Add(new SqlParameter("@BaCode", BaCode));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<SysAccountDto>(reader);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }
            return list;
        }
        public bool SaveAccount(SysAccountDto dto)
        {
            bool result = false;
            SqlTransaction trasaction = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            int res = 0;
            try
            {
                db.OpenConnection();
                string sql = "sp_UserAccount_Add";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@AccountId", dto.AccountId));
                paramList.Add(new SqlParameter("@UserLogin", dto.UserLogin));
                paramList.Add(new SqlParameter("@Password", dto.Password));
                paramList.Add(new SqlParameter("@AccountFirstName", dto.AccountFirstName));
                paramList.Add(new SqlParameter("@AccountLastName", dto.AccountLastName));
                paramList.Add(new SqlParameter("@AccountEmail", dto.AccountEmail));
                paramList.Add(new SqlParameter("@FlagStatus", dto.FlagStatus));
                paramList.Add(new SqlParameter("@Ba", dto.Ba));
                paramList.Add(new SqlParameter("@RoleId", dto.RoleId));
                paramList.Add(new SqlParameter("@CreatedBy", dto.CreatedBy));
                paramList.Add(new SqlParameter("@UpdatedBy", dto.UpdatedBy));

                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());
                sqlCommand.Transaction = trasaction;
                res = sqlCommand.ExecuteNonQuery();
                trasaction.Commit();

                result = res > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }
            return result;
        }
        public bool DeleteAccount(SysAccountDto dto)
        {
            bool result = false;
            SqlTransaction trasaction = null;
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            int res = 0;
            try
            {
                db.OpenConnection();
                string sql = "sp_UserAccount_Delete";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@AccountId", dto.AccountId));

                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());
                sqlCommand.Transaction = trasaction;
                res = sqlCommand.ExecuteNonQuery();
                trasaction.Commit();

                result = res > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.CloseConnection();
            }
            return result;
        }

    }
}
