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

namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{
    public class TrackingResponsitory : ITrackingRespository
    {
        protected Smart1662ADO db;

        public TrackingResponsitory()
        {

            db = new Smart1662ADO();

        }
        public bool Add(TrackingHeaderDto item)
        {
            throw new NotImplementedException();
        }
        public Task Update(TrackingHeaderDto item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(TrackingHeaderDto item)
        {
            throw new NotImplementedException();
        }
        public TrackingHeaderDto GetByIncidentNo(string TrackingNo)
        {
            string sql = "sp_PwaIncident_Tracking";
            List<SqlParameter> paramList = new List<SqlParameter>();
            TrackingHeaderDto result = null;


            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {
                paramList = new List<SqlParameter>();
                string[] strTrack = TrackingNo.Split('-');
                if (strTrack.Count() > 1)
                {
                    paramList.Add(new SqlParameter("@PwaIncidentNo", strTrack[0].Trim()));
                    paramList.Add(new SqlParameter("@RandomNo", strTrack[1].Trim()));
                }
                else
                {
                    paramList.Add(new SqlParameter("@PwaIncidentNo", strTrack[0].Trim()));
                    paramList.Add(new SqlParameter("@RandomNo", ""));
                }
               
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                reader = sqlCommand.ExecuteReader();

                result = Dto.Utils.Converting.ConvertDataReaderToObjList<TrackingHeaderDto>(reader).FirstOrDefault();
                reader.NextResult();

                if(result != null)
                result.TrackingDetail = Dto.Utils.Converting.ConvertDataReaderToObjList<TrackingDetailDto>(reader);
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
