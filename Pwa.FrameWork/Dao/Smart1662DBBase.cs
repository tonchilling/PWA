using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Pwa.FrameWork.Dao
{
    public abstract class Smart1662DBBase
    {
        #region Private variables
        private SqlConnection connect = null;
        private SqlCommand _command = null;
        public SqlDataAdapter _dataAdapter = null;
        private SqlDataReader _dataReader = null;


        private string _conStr = System.Configuration.ConfigurationSettings.AppSettings["Smart1662DB"];
        #endregion


        #region Abstract Method

        public abstract bool Insert(object obj);
        public abstract bool Update(object obj);
        public abstract bool Delete(object obj);
        public abstract object FindByPK(object obj);
        public abstract List<object> FinByAll();

        #endregion

        public Smart1662DBBase()
        {
            connect = new SqlConnection(conStr);
        }


        public SqlConnection Connection
        {
            get { return connect; }
        }
        public string conStr
        {
            get { return _conStr; }
            set { _conStr = value; }

        }

        public SqlCommand sqlCommand
        {
            get { return _command; }
            set { _command = value; }

        }

        public SqlDataAdapter sqlAdapter
        {
            get { return _dataAdapter; }
            set { _dataAdapter = value; }

        }

        public SqlDataReader sqlReader
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
            sqlCommand = new SqlCommand();
            sqlCommand.CommandText = procName;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = connect;
            if (param != null)
                sqlCommand.Parameters.AddRange(param.ToArray());

            sqlAdapter = new SqlDataAdapter(sqlCommand);

            sqlAdapter.Fill(table);


            return table;
        }

        public DataTable ExcecuteToDataTable(string queryName)
        {
            bool isCan = false;
            DataTable table = new DataTable();

            sqlCommand = new SqlCommand();
            sqlCommand.CommandText = queryName;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = connect;
            sqlAdapter = new SqlDataAdapter(sqlCommand);

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
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = procName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = connect;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.AddRange(param.ToArray());
                sqlCommand.ExecuteNonQuery();
                CloseConnection();
                isCan = true;
            }
            catch (Exception ex)
            {
                throw new Exception("ExcecuteNonQuery.Error:"+ex.ToString());
              
            }
            return isCan;
        }

        public bool ExcecuteNonQuery(string procName, SqlTransaction trasaction, List<SqlParameter> param)
        {
            bool isCan = false;
            DataTable table = new DataTable();
            //connect.Open();
            sqlCommand = new SqlCommand();
            sqlCommand.CommandText = procName;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = connect;
            sqlCommand.Transaction = trasaction;
            sqlCommand.Parameters.AddRange(param.ToArray());
            sqlCommand.ExecuteNonQuery();

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
            sqlCommand = new SqlCommand();
            sqlCommand.CommandText = procName;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = connect;
            SqlCommandBuilder.DeriveParameters(sqlCommand);
            foreach (SqlParameter param in sqlCommand.Parameters)
            {
                resultParam.Add(param);
            }

            return resultParam;
        }

        public List<SqlParameter> GetAllParameter(string procName, SqlTransaction tran)
        {
            List<SqlParameter> resultParam = new List<SqlParameter>();
            sqlCommand = new SqlCommand();
            sqlCommand.CommandText = procName;
            sqlCommand.Transaction = tran;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = connect;
            SqlCommandBuilder.DeriveParameters(sqlCommand);
            foreach (SqlParameter param in sqlCommand.Parameters)
            {
                if (param.ParameterName.ToLower().IndexOf("return_value") == -1)
                {
                    resultParam.Add(param);
                }
            }

            sqlCommand.Parameters.Clear();
            sqlCommand.Dispose();

            return resultParam;
        }

        public IList<T> FindByColumn<T>(string procName, List<SqlParameter> param)
        {
            string sql = procName;
            List<SqlParameter> paramList = new List<SqlParameter>();
            List<T> list = new List<T>();
            
            SqlDataReader reader = null;

            try
            {
                OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = this.Connection;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.AddRange(param.ToArray());
            
                reader = sqlCommand.ExecuteReader();
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

            SqlDataReader reader = null;

            try
            {
                OpenConnection();
                sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = this.Connection;
                sqlCommand.Parameters.AddRange(param.ToArray());

                reader = sqlCommand.ExecuteReader();
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
