using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao.ADO
{
    public class IC360Dao
    {
        public List<VwIC360> GetImportData()
        {
            List<VwIC360> result = new List<VwIC360>(0);
            VwIC360 item = null;
            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Pwa.Pwa1662.ConnectionString"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(connStr))
            {
                //var sql = @"select  * from [dbo].[view_smart_1662]";
                var sql = @"select  * from [dbo].[V_SMART_1662]";

                var cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;

                cnn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        item = new VwIC360();
                        foreach (var prop in typeof(VwIC360).GetProperties())
                        {

                            Debug.WriteLine(prop.Name);
                            if (rdr[prop.Name] != DBNull.Value)
                            {
                                if ((rdr.GetFieldType(rdr.GetOrdinal(prop.Name)).Name == "String") && (rdr.GetString(rdr.GetOrdinal(prop.Name)) == "NULL"))
                                {
                                    prop.SetValue(item, null);
                                }
                                else
                                {
                                    prop.SetValue(item, rdr[prop.Name]);
                                }

                            }
                        }

                        result.Add(item);
                    }
                }
            }
            return result;
        }

    }


    public class VwIC360
    {
        public string KC_TITLE { get; set; }

        public string KS_C_CODE_DESC { get; set; }

        public string KS_C_FIRST_NAME { get; set; }

        public string KS_C_LAST_NAME { get; set; }

        public string GENDER { get; set; }

        public string CONTACT_CHANNEL_ID { get; set; }

        public string CALLBACK_PHONE { get; set; }

        public string EMAIL { get; set; }

        public string CALLER_NAME { get; set; }

        public int? OPEN_USER_ID { get; set; }

        public string AGO_TITLE { get; set; }

        public string AG_O_FIRST_NAME { get; set; }

        public string AG_O_LAST_NAME { get; set; }

        public int? CLOSED_BY { get; set; }

        public string AGC_TITLE { get; set; }

        public string AG_C_FIRST_NAME { get; set; }

        public string AG_C_LAST_NAME { get; set; }

        public string INCIDENT_ID { get; set; }

        public DateTime? INCIDENT_DT { get; set; }

        public DateTime? CLOSED_DT { get; set; }

        public DateTime? RESOLVED_BEF_DT { get; set; }

        public int? WORKTIME_DAY { get; set; }

        public int? WORKTIME_HOUR { get; set; }

        public int? WORKTIME_MINUTES { get; set; }

        public string SLA { get; set; }

        public Byte? IS_KNOWN_ERROR { get; set; }

        public string HOLD_COMMENTS { get; set; }

        public string KS_I_CODE_DESC { get; set; }

        public string CATEGORY_TREE { get; set; }

        public string CATEGORY_DESC { get; set; }

        public string SUBJECT { get; set; }

        public string SOLUTION_DETAIL { get; set; }

        public DateTime? LAST_UPD_DT { get; set; }

        public DateTime? ASSIGN_DATE { get; set; }

        public DateTime? REPLY_DATE { get; set; }

        public string KS_IA_E_CODE_DESC { get; set; }

        public string ASSIGN_BRANCH_CODE { get; set; }

        public string KS_IA_CODE_DESC { get; set; }

    }

}
