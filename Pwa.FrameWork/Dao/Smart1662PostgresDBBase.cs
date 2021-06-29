using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
namespace Pwa.FrameWork.Dao
{
    public abstract class Smart1662PostgresDBBase
    {
        #region Private variables
        private NpgsqlConnection connect = null;
        private NpgsqlCommand _command = null;
        public NpgsqlDataAdapter _dataAdapter = null;
        private NpgsqlDataReader _dataReader = null;


        private string _conStr = System.Configuration.ConfigurationSettings.AppSettings["GISDB"];
        #endregion


        #region Abstract Method

        public abstract bool Insert(object obj);
        public abstract bool Update(object obj);
        public abstract bool Delete(object obj);
        public abstract object FindByPK(object obj);
        public abstract List<object> FinByAll();

        #endregion

        public Smart1662PostgresDBBase()
        {
            connect = new NpgsqlConnection(conStr);
        }

        public Smart1662PostgresDBBase(string connctionStr)
        {
            conStr = connctionStr;
            connect = new NpgsqlConnection(conStr);
        }

        public NpgsqlConnection Connection
        {
            get { return connect; }
            set { connect = value; }
        }
        public string conStr
        {
            get { return _conStr; }
            set { _conStr = value; }

        }

        public NpgsqlCommand NpgsqlCommand
        {
            get { return _command; }
            set { _command = value; }

        }

        public NpgsqlDataAdapter sqlAdapter
        {
            get { return _dataAdapter; }
            set { _dataAdapter = value; }

        }

        public NpgsqlDataReader sqlReader
        {
            get { return _dataReader; }
            set { _dataReader = value; }

        }

        public bool OpenConnection()
        {
            bool isCan = false;
           
            connect.Open();
            isCan = true;
            return isCan;
        }
        public bool CloseConnection()
        {
            bool isCan = false;
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
            isCan = true;
            return isCan;
        }

        public DataTable ExcecuteProcToDataTable(string procName, List<SqlParameter> param)
        {
            bool isCan = false;
            DataTable table = new DataTable();
            //connect.Open();
            NpgsqlCommand = new NpgsqlCommand();
            NpgsqlCommand.CommandText = procName;
            NpgsqlCommand.CommandType = CommandType.StoredProcedure;
            NpgsqlCommand.Connection = connect;
            if (param != null)
                NpgsqlCommand.Parameters.AddRange(param.ToArray());

            sqlAdapter = new NpgsqlDataAdapter(NpgsqlCommand);

            sqlAdapter.Fill(table);


            return table;
        }

        public DataTable ExcecuteToDataTable(string queryName)
        {
            bool isCan = false;
            DataTable table = new DataTable();

            NpgsqlCommand = new NpgsqlCommand();
            NpgsqlCommand.CommandText = queryName;
            NpgsqlCommand.CommandType = CommandType.Text;
            NpgsqlCommand.Connection = connect;
            sqlAdapter = new NpgsqlDataAdapter(NpgsqlCommand);

            sqlAdapter.Fill(table);


            return table;
        }


        public bool ExcecuteNonQuery(string procName, List<SqlParameter> param)
        {
            bool isCan = false;
            DataTable table = new DataTable();

            try
            {
                connect.Open();
                NpgsqlCommand = new NpgsqlCommand();
                NpgsqlCommand.CommandText = procName;
                NpgsqlCommand.CommandType = CommandType.StoredProcedure;
                NpgsqlCommand.Connection = connect;
                NpgsqlCommand.CommandTimeout = 0;
                NpgsqlCommand.Parameters.AddRange(param.ToArray());
                NpgsqlCommand.ExecuteNonQuery();
                CloseConnection();
                isCan = true;
            }
            catch (Exception ex)
            {
                throw new Exception("ExcecuteNonQuery.Error:"+ex.ToString());
              
            }
            return isCan;
        }

        public bool ExcecuteNonQuery(string procName, NpgsqlTransaction trasaction, List<SqlParameter> param)
        {
            bool isCan = false;
            DataTable table = new DataTable();
            //connect.Open();
            NpgsqlCommand = new NpgsqlCommand();
            NpgsqlCommand.CommandText = procName;
            NpgsqlCommand.CommandType = CommandType.StoredProcedure;
            NpgsqlCommand.Connection = connect;
            NpgsqlCommand.Transaction = trasaction;
            NpgsqlCommand.Parameters.AddRange(param.ToArray());
            NpgsqlCommand.ExecuteNonQuery();

            return true;
        }


        public List<SqlParameter> AssignObjectToParameter(List<SqlParameter> parameterList, object obj)
        {
            Type t = obj.GetType();
            List<System.Reflection.PropertyInfo> properties = t.GetProperties().ToList();
            SqlParameter param = null;
            foreach (System.Reflection.PropertyInfo pi in obj.GetType().GetProperties())
            {
                string objName = pi.Name.ToLower().Split('_')[0] + (pi.Name.ToLower().Split('_').Length > 1 ? pi.Name.ToLower().Split('_')[1] : "");
                param = parameterList.Find(item => item.ParameterName.ToLower().Split('_')[0].Replace("@", "") + (item.ParameterName.ToLower().Split('_').Length > 2 ? item.ParameterName.ToLower().Split('_')[1] : "") == objName);
                if (param == null)
                {
                    param = parameterList.Find(item => item.ParameterName.Split('_')[0].Replace("@", "")
                                                                   .ToLower().Trim() == pi.Name.ToLower().Trim());


                }

                if (param != null)
                {
                    param.SqlValue = pi.GetValue(obj, null);
                }
            }

            return parameterList;
        }



        public List<SqlParameter> GetAllParameter(string procName)
        {
            List<SqlParameter> resultParam = new List<SqlParameter>();
            NpgsqlCommand = new NpgsqlCommand();
            NpgsqlCommand.CommandText = procName;
            NpgsqlCommand.CommandType = CommandType.StoredProcedure;
            NpgsqlCommand.Connection = connect;
            NpgsqlCommandBuilder.DeriveParameters(NpgsqlCommand);
            foreach (SqlParameter param in NpgsqlCommand.Parameters)
            {
                resultParam.Add(param);
            }

            return resultParam;
        }

        public List<SqlParameter> GetAllParameter(string procName, NpgsqlTransaction tran)
        {
            List<SqlParameter> resultParam = new List<SqlParameter>();
            NpgsqlCommand = new NpgsqlCommand();
            NpgsqlCommand.CommandText = procName;
            NpgsqlCommand.Transaction = tran;
            NpgsqlCommand.CommandType = CommandType.StoredProcedure;
            NpgsqlCommand.Connection = connect;
            NpgsqlCommandBuilder.DeriveParameters(NpgsqlCommand);
            foreach (SqlParameter param in NpgsqlCommand.Parameters)
            {
                if (param.ParameterName.ToLower().IndexOf("return_value") == -1)
                {
                    resultParam.Add(param);
                }
            }

            NpgsqlCommand.Parameters.Clear();
            NpgsqlCommand.Dispose();

            return resultParam;
        }

        public IList<T> FindByColumn<T>(string procName, List<SqlParameter> param)
        {
            string sql = procName;
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<T> list = new List<T>();
            
            NpgsqlDataReader reader = null;

            try
            {
                OpenConnection();
                NpgsqlCommand = new NpgsqlCommand();
                NpgsqlCommand.CommandText = sql;
                NpgsqlCommand.CommandType = CommandType.StoredProcedure;
                NpgsqlCommand.Connection = this.Connection;
                NpgsqlCommand.CommandTimeout = 0;
                NpgsqlCommand.Parameters.AddRange(param.ToArray());
            
                reader = NpgsqlCommand.ExecuteReader();
             //   if (reader.Read())
              //  {

                    list = Dto.Utils.Converting.ConvertDataReaderToObjList<T>(reader);

               // }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally {
                CloseConnection();
            }

           


            return list;
        }

        public T FindByPK<T>(string procName, List<SqlParameter> param)
        {
            string sql = procName;
            List<SqlParameter> paramList = new List<SqlParameter>();
            T list ;

            NpgsqlDataReader reader = null;

            try
            {
                OpenConnection();
                NpgsqlCommand = new NpgsqlCommand();
                NpgsqlCommand.CommandText = sql;
                NpgsqlCommand.CommandType = CommandType.StoredProcedure;
                NpgsqlCommand.Connection = this.Connection;
                NpgsqlCommand.Parameters.AddRange(param.ToArray());

                reader = NpgsqlCommand.ExecuteReader();
                //   if (reader.Read())
                //  {

                list = Dto.Utils.Converting.ConvertDataReaderToObjList<T>(reader).FirstOrDefault();

                // }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                CloseConnection();
            }

            


            return list;
        }
    }
}
