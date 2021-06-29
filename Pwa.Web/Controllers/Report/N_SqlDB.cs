using System;
using System.Data;
using System.Data.SqlClient;

namespace Pwa.Component.Database
{
    /// <summary>
    /// ฟังชั่นเกี่ยวกับการ Connect Database
    /// </summary>
    public class N_SqlDB
    {
        private void Error(Exception ex)
        {
            _lastError = ex;
        }

        private string _connString;
        private SqlConnection _sqlConn;

        /// <summary>
        /// Constructor method
        /// </summary>
        public N_SqlDB()
        { }

        private Exception _lastError;
        public Exception LastError { get { return _lastError; } }
        
        /// <summary>
        /// Constructor method with connectionstring
        /// </summary>
        /// <param name="connectionString">String of connectionstring</param>
        public N_SqlDB(string connectionString)
        {
            _connString = connectionString;
        }

        /// <summary>
        /// Close connection with database
        /// </summary>
        public void Close()
        {
            if (_sqlConn.State == System.Data.ConnectionState.Open)
            {
                _sqlConn.Close();
                _sqlConn.Dispose();
            }
        }

        /// <summary>
        /// Open connection with database
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                _sqlConn = new SqlConnection(_connString);
                _sqlConn.Open();
                return _sqlConn.State == System.Data.ConnectionState.Open;
            }
            catch (Exception ex)
            {
                Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Return datareader of query
        /// </summary>
        /// <param name="sql">String sql statement</param>
        public SqlDataReader ExecuteDataReader(string sql)
        {
            try
            {
                SqlCommand sqlComm = new SqlCommand(sql, _sqlConn);
                sqlComm.CommandTimeout = 600;
                return sqlComm.ExecuteReader();
            }
            catch (Exception ex)
            {
                SqlCommand sqlComm = new SqlCommand("select null", _sqlConn);
                SqlDataReader oleReader = sqlComm.ExecuteReader();
                Error(ex);
                return oleReader;
            }
        }

        /// <summary>
        /// Return datareader of query
        /// </summary>
        /// <param name="sql">String sql statement</param>
        /// <param name="parameter">Object parameter of sql statement</param>
        public SqlDataReader ExecuteDataReader(string sql, params object[] parameter)
        {
            try
            {
                SqlCommand sqlComm = new SqlCommand(sql, _sqlConn);
                sqlComm.CommandTimeout = 600;
                for (int i = 0; i < parameter.Length; i++)
                {
                    SqlParameter op = new SqlParameter("@p" + i.ToString(), parameter[i]);
                    if (op.Value == null)
                    {
                        sql = sql.Replace("= @p" + i.ToString(), "is null");
                        sqlComm.CommandText = sql;
                        continue;
                    }
                    sqlComm.Parameters.Add(op);
                }
                return sqlComm.ExecuteReader();
            }
            catch (Exception ex)
            {
                SqlCommand sqlComm = new SqlCommand("select null", _sqlConn);
                SqlDataReader oleReader = sqlComm.ExecuteReader();
                Error(ex);
                return oleReader;
            }
        }

        /// <summary>
        /// Return datatable of query
        /// </summary>
        /// <param name="sql">String sql statement</param>
        public DataTable ExecuteDataTable(string sql)
        {
            DataTable dt = new DataTable();
            dt.Load(ExecuteDataReader(sql));
            return dt;
        }

        /// <summary>
        /// Return datatable of query
        /// </summary>
        /// <param name="sql">String sql statement</param>
        /// <param name="parameter">Object parameter of sql statement</param>
        public DataTable ExecuteDataTable(string sql, params object[] parameter)
        {
            DataTable dt = new DataTable();
            dt.Load(ExecuteDataReader(sql, parameter));
            return dt;
        }

        /// <summary>
        /// Return num of rows effect
        /// </summary>
        /// <param name="sql">String sql statement</param>
        public int ExecuteNonQuery(string sql)
        {
            SqlCommand sqlComm = new SqlCommand(sql, _sqlConn);
            sqlComm.CommandTimeout = 600;
            return sqlComm.ExecuteNonQuery();
        }

        /// <summary>
        /// Return num of rows effect
        /// </summary>
        /// <param name="sql">String sql statement</param>
        /// <param name="parameter">Object parameter of sql statement</param>
        public int ExecuteNonQuery(string sql, params object[] parameter)
        {
            try
            {
                SqlCommand sqlComm = new SqlCommand(sql, _sqlConn);
                sqlComm.CommandTimeout = 600;
                for (int i = 0; i < parameter.Length; i++)
                {
                    SqlParameter op = new SqlParameter("@p" + i.ToString(), parameter[i]);
                    if (op.Value == null)
                        op.Value = DBNull.Value;
                    sqlComm.Parameters.Add(op);
                }
                return sqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Error(ex);
                return 0;
            }
        }

        /// <summary>
        /// Return num of rows effect
        /// </summary>
        /// <param name="sql">String sql statement</param>
        public int ExecuteNonQueryTran(string sql)
        {
            SqlCommand sqlComm = new SqlCommand(sql, _sqlConn, _sqlConn.BeginTransaction());
            sqlComm.CommandTimeout = 600;
            int result = 0;
            try
            {
                result = sqlComm.ExecuteNonQuery();
                sqlComm.Transaction.Commit();
            }
            catch
            { sqlComm.Transaction.Rollback(); }
            return result;
        }

        /// <summary>
        /// Return num of rows effect
        /// </summary>
        /// <param name="sql">String sql statement</param>
        /// <param name="parameter">Object parameter of sql statement</param>
        public int ExecuteNonQueryTran(string sql, params object[] parameter)
        {
            SqlCommand sqlComm = new SqlCommand(sql, _sqlConn, _sqlConn.BeginTransaction());
            sqlComm.CommandTimeout = 600;
            int result = 0;
            for (int i = 0; i < parameter.Length; i++)
            {
                SqlParameter op = new SqlParameter("@p" + i.ToString(), parameter[i]);
                if (op.Value == null)
                    op.Value = DBNull.Value;
                sqlComm.Parameters.Add(op);
            }
            try
            {
                result = sqlComm.ExecuteNonQuery();
                sqlComm.Transaction.Commit();
            }
            catch
            { sqlComm.Transaction.Rollback(); }
            return result;
        }

        /// <summary>
        /// Use for insert data and return data of returnField (Not support data type "binary", "ntext", "text")
        /// </summary>
        /// <param name="sql">String sql statement</param>
        /// <param name="returnField">String field to need return data</param>
        /// <param name="parameter">Object parameter of sql statement</param>
		public object ExecuteReturnId(string sql, string returnField, params object[] parameter)
		{
			return ExecuteReturnId(sql, returnField, false, parameter);
		}

        public object ExecuteReturnId(string sql, string returnField, bool showError, params object[] parameter)
        {
            SqlCommand sqlComm = new SqlCommand(sql, _sqlConn, _sqlConn.BeginTransaction());
            sqlComm.CommandTimeout = 600;
            int result = 0;
            for (int i = 0; i < parameter.Length; i++)
            {
                SqlParameter op = new SqlParameter("@p" + i.ToString(), parameter[i]);
                if (op.Value == null)
                    op.Value = DBNull.Value;
                sqlComm.Parameters.Add(op);
            }
            try
            {
                result = sqlComm.ExecuteNonQuery();
                sqlComm.Transaction.Commit();
            }
            catch (Exception ex)
            {
				sqlComm.Transaction.Rollback();
				if (showError) return ex;
			}
            if (result > 0)
            {
                string tableName = sql.Split(' ')[2].Substring(0, sql.Split(' ')[2].IndexOf("("));
                string[] fieldName = sql.Substring(sql.IndexOf("(") + 1, sql.IndexOf(")") - (sql.IndexOf("(") + 1)).Split(',');
                string[] paramName = sql.Substring(sql.LastIndexOf("(") + 1, sql.LastIndexOf(")") - (sql.LastIndexOf("(") + 1)).Split(',');
                sql = string.Empty;
                sql += "select " + returnField + " from " + tableName + " where ";
                for (int i = 0; i < fieldName.Length; i++)
                {
                    if (i != 0)
                        sql += " and ";
                    sql += fieldName[i].Trim() + " = " + paramName[i].Trim();
                }
                DataTable dt = ExecuteDataTable(sql, parameter);
                if (dt.Rows.Count > 0 && dt.Columns.Count > 0)
                    return dt.Rows[0][0];
            }
            return null;
        }

        /// <summary>
        /// Set/Get database's connectionstring
        /// </summary>
        public string ConnectionString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        /// <summary>
        /// Get only database's connectionstate
        /// </summary>
        public ConnectionState ConnectionState
        {
            get { return _sqlConn.State; }
        }
    }
}
