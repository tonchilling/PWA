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
using Pwa.FrameWork.Dto.Smart1662.Incident;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class SysMessagesIncidentSqlRepository : ISysMessagesIncidentRespository
    {
        protected Smart1662ADO db;

        public SysMessagesIncidentSqlRepository()
        {
            db = new Smart1662ADO();
        }
        public bool Add(SysMessagesDto dto)
        {
          /*  bool result = false;
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
            }*/
            return false;
        }

        public Task Update(SysMessagesDto message)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SysMessagesDto message)
        {
            throw new NotImplementedException();
        }

        public SysMessagesDto GetById(string id)
        {
            throw new NotImplementedException();
        }
    
        public List<SysMessagesDto> GetMessagesIncident(string BranchNo)
        {
            string sql = "sp_GetMessagesIncident_BranchNo";
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<SysMessagesDto> list = new List<SysMessagesDto>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            try
            {
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@BranchNo", BranchNo));
                //paramList.Add(new SqlParameter("@PwaIncidentID", PwaIncidentID));

                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<SysMessagesDto>(reader);
            }
            catch (Exception ex)
            {
                throw new Exception("SysMessagesDto.FindByPK::" + ex.ToString());
            }
            finally
            {

            }

            return list;
        }

        public void UpdateMessagesIncident(string PwaIncidentID, int isRead, string BranchNo)
        {
            string sql = "sp_UpdateMessagesIncident";
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;

            int pwa_real_id = 0;
            using(Smart1662Entities db = new Smart1662Entities())
            {
                pwa_real_id = db.PwaIncident.Where(f => f.PwaIncidentNo == PwaIncidentID).Select(f => f.PwaIncidentID).FirstOrDefault();
            }


            try
            {
                paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@PwaIncidentID", (pwa_real_id == 0 ? PwaIncidentID : pwa_real_id.ToString())));
                paramList.Add(new SqlParameter("@isRead", isRead));
                paramList.Add(new SqlParameter("@BranchNo", BranchNo));


                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                //list = Dto.Utils.Converting.ConvertDataReaderToObjList<SysMessagesDto>(reader);
            }
            catch (Exception ex)
            {
                throw new Exception("SysMessagesDto.FindByPK::" + ex.ToString());
            }
            finally
            {

            }

        }
    }
}
