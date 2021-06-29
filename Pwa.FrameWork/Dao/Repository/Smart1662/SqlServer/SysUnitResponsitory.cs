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
    public class SysUnitResponsitory : ISysUnitRespository
    {
        protected Smart1662ADO db;

        public SysUnitResponsitory()
        {

            db = new Smart1662ADO();

        }
        public bool Add(UnitDto dto)
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
                string sql = "sp_SysUnit_Update";
                trasaction = db.Connection.BeginTransaction();

                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@UnitId", dto.UnitId));
                paramList.Add(new SqlParameter("@UnitName", dto.UnitName));
                paramList.Add(new SqlParameter("@Status", dto.Status));
                paramList.Add(new SqlParameter("@CreatedBy", dto.CreatedBy));
                paramList.Add(new SqlParameter("@UpdatedBy", dto.UpdatedBy));
                paramList.Add(new SqlParameter("@Action", dto.Action));

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

        public Task Update(UnitDto item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(UnitDto item)
        {
            throw new NotImplementedException();
        }

        public UnitDto GetById(string id)
        {
            throw new NotImplementedException();
        }
    
        public List<UnitDto> Search(string UnitId, string UnitName, string Status)
        {
            string sql = "sp_SysUnit_Search";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<UnitDto> list = new List<UnitDto>();

            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();

                paramList.Add(new SqlParameter("@UnitId", UnitId));
                paramList.Add(new SqlParameter("@UnitName", UnitName));
                paramList.Add(new SqlParameter("@Status", Status));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<UnitDto>(reader);

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
    }
}
