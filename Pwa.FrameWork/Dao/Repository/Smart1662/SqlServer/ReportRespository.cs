using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dto;
using Pwa.FrameWork.Dto.Utils;
namespace Pwa.FrameWork.Dao.Repository.Smart1662.SqlServer
{


  public  class ReportRespository
    {

        private readonly Smart1662Entities dbEntity;
        protected Smart1662ADO db;
        List<SqlParameter> paramList = null;
        SqlDataReader reader = null;
        SqlCommand sqlCommand = null;
        SqlDataAdapter adapter = null;

        public ReportRespository()
        {
            dbEntity = new Smart1662Entities();
            db = new Smart1662ADO();

        }

        List<SqlParameter> ConvertReportParam(CriterialDto dto)
        {
            string fromDateOfyyyyMMdd ="", toDateOfyyyyMMdd ="";
          

            if (dto.PeriodType == "1")
            {
                fromDateOfyyyyMMdd = Converting.DateOfyyyyMMdd(dto.FromDateTH);
                toDateOfyyyyMMdd = Converting.DateOfyyyyMMdd(dto.ToDateTH);
            }
            if (dto.PeriodType == "2")
            {
                fromDateOfyyyyMMdd = Converting.MMyyToyyyyMMdd(dto.FromMonthYearTH, FirstDayOrLastDay.FirstDay);
                toDateOfyyyyMMdd = Converting.MMyyToyyyyMMdd(dto.ToMonthYearTH, FirstDayOrLastDay.FirstDay);
            }
            if (dto.PeriodType == "3")
            {
                fromDateOfyyyyMMdd = Converting.QuarterToyyyyMMdd(dto.Year, dto.Quarter, FirstDayOrLastDay.FirstDay);
                toDateOfyyyyMMdd = Converting.QuarterToyyyyMMdd(dto.Year, dto.Quarter, FirstDayOrLastDay.LastDay);
            }


            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@AreaCode", dto.AreaId));
            paramList.Add(new SqlParameter("@BranchCode", dto.BranchId));
        //    paramList.Add(new SqlParameter("@PeriodType", dto.PeriodType));
            paramList.Add(new SqlParameter("@FromDate", fromDateOfyyyyMMdd));
            paramList.Add(new SqlParameter("@ToDate", toDateOfyyyyMMdd));
            paramList.Add(new SqlParameter("@RequestType", dto.RequestType));
            paramList.Add(new SqlParameter("@RequestCategory", dto.RequestCategory));
            paramList.Add(new SqlParameter("@RequestCategorySubject", dto.RequestCategorySubject));
            paramList.Add(new SqlParameter("@ChannelType", dto.ChannelType));

            return paramList;
        }
        public DataSet GetGUI001(CriterialDto dto)
        {
            string sql = "sp_Report_GUI001";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(ConvertReportParam(dto).ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI002(CriterialDto dto)
        {
            string sql = "sp_Report_GUI002";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI004(CriterialDto dto)
        {
            string sql = "sp_Report_GUI004";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }
        public DataSet GetGUI006(CriterialDto dto)
        {
            string sql = "sp_Report_GUI006";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }
        public DataSet GetGUI007(CriterialDto dto)
        {
            string sql = "sp_Report_GUI007";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI008(CriterialDto dto)
        {
            string sql = "sp_Report_GUI008";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI009(CriterialDto dto)
        {
            string sql = "sp_Report_GUI009";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }
        public DataSet GetGUI010(CriterialDto dto)
        {
            string sql = "sp_Report_GUI010";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }
        public DataSet GetGUI011(CriterialDto dto)
        {
            string sql = "sp_Report_GUI011";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI012(CriterialDto dto)
        {
            string sql = "sp_Report_GUI012";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI013(CriterialDto dto)
        {
            string sql = "sp_Report_GUI013";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI015(CriterialDto dto)
        {
            string sql = "sp_Report_GUI015";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {

                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI016(CriterialDto dto)
        {
            string sql = "sp_Report_GUI016";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");


            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;


            try
            {

                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);

            

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }



            return dsResult;

        }

        public DataSet GetGUI018(CriterialDto dto)
        {
            string sql = "sp_Report_GUI018";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }
        public DataSet GetGUI019(CriterialDto dto)
        {
            string sql = "sp_Report_GUI019";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }
        public DataSet GetGUI020(CriterialDto dto)
        {
            string sql = "sp_Report_GUI020";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }

        public DataSet GetGUI021(CriterialDto dto)
        {
            string sql = "sp_Report_GUI021";
            List<SqlParameter> paramList = new List<SqlParameter>();
            DataSet dsResult = new DataSet("Data");
            SqlDataReader reader = null;
            SqlCommand sqlCommand = null;
            try
            {
                db.OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = db.Connection;
                sqlCommand.Parameters.AddRange(paramList.ToArray());

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCommand;

                adapter.Fill(dsResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                adapter.Dispose();
                db.CloseConnection();
            }
            return dsResult;
        }
    }
}
