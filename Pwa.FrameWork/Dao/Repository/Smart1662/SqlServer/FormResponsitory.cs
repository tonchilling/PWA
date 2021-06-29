using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto.Utils;
using Pwa.FrameWork.Dto.Smart1662;
using System.Data.SqlClient;
using System.Data;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class FormResponsitory : IFormRespository
    {
        protected Smart1662ADO db;

        public FormResponsitory()
        {

            db = new Smart1662ADO();

        }
        
        public OpenRepairDto GetOpenRepair(string id)
        {
            string sql = "sp_PwaRepaireWork_OpenRepair_GetById";
            List<SqlParameter> paramList = new List<SqlParameter>();
            OpenRepairDto result = null;


            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
                paramList = new List<SqlParameter>();


                paramList.Add(new SqlParameter("@RWId", id));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                result = Dto.Utils.Converting.ConvertDataReaderToObjList<OpenRepairDto>(reader).FirstOrDefault();
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
        
        public RequisitionDto GetRequisition(string id)
        {
            string sql = "sp_PwaRepaireWork_Requisition_GetById";
            List<SqlParameter> paramList = new List<SqlParameter>();
            RequisitionDto result = null;


            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
                paramList = new List<SqlParameter>();


                paramList.Add(new SqlParameter("@RWId", id));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                result = Dto.Utils.Converting.ConvertDataReaderToObjList<RequisitionDto>(reader).FirstOrDefault();
                reader.NextResult();

                if (result != null)
                    result.PwaRepaireWork_ItemDto = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_ItemDto>(reader);
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
        public CloseRepairDto GetCloseRepair(string id)
        {
            string sql = "sp_PwaRepaireWork_CloseRepair_GetById";
            List<SqlParameter> paramList = new List<SqlParameter>();
            CloseRepairDto result = null;


            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
                paramList = new List<SqlParameter>();


                paramList.Add(new SqlParameter("@RWId", id));
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                result = Dto.Utils.Converting.ConvertDataReaderToObjList<CloseRepairDto>(reader).FirstOrDefault();

                reader.NextResult();

                if (result != null)
                    result.PwaRepaireWork_ItemDto = Dto.Utils.Converting.ConvertDataReaderToObjList<PwaRepaireWork_ItemDto>(reader);

                reader.NextResult();
                if (result != null)
                    result.Files = Dto.Utils.Converting.ConvertDataReaderToObjList<FileCloseDto>(reader);
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
