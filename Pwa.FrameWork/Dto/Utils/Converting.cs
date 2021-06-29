using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Globalization;
using Npgsql;
namespace Pwa.FrameWork.Dto.Utils
{

    public enum FirstDayOrLastDay {
         FirstDay=1,
         LastDay=2
    }
    public static class Converting
    {
      
        static string[] mL = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        static string[] mS = { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
        static string[] mLTh = { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };

       
        public static string DateOfDDMM(string date)
        {
            string result = date;
            DateTime dt ;
            try {
                dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0} {1}", dt.Day.ToString(), mS[dt.Month]);
            }
            catch { }
            finally { }
            return result;
        }
        public static string DateOfDDMMLong(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0} {1}", dt.Day.ToString(), mL[dt.Month-1]);
            }
            catch { }
            finally { }
            return result;
        }


        public static int ToYYYYMM(string mm,string yyyy)
        {
            int result = 0;
         
            try
            {


                result = Convert.ToInt32(string.Format("{0}{1}", yyyy, mm));
            }
            catch { }
            finally { }
            return result;
        }
        public static int ToYYYYMM(string yyyyMM)
        {
            int result = 0;

            try
            {


                result = Convert.ToInt32(yyyyMM);
            }
            catch { }
            finally { }
            return result;
        }

        public static T GetReqeustForm<T>(T obj) where T : new()
        {
            obj = new T();
            var properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {



                var prop = HttpContext.Current.Request.Form.AllKeys.Where(key => key.ToLower().Contains(property.Name.ToLower())).FirstOrDefault();
                var valueAsString = prop == null || prop == "" ? "" : HttpContext.Current.Request.Form[prop.ToString()];
                // var value = Parse(property.GetType() , valueAsString);

                // if (value == null)
                //    continue;
                if (!property.PropertyType.IsGenericType
                    && (property.PropertyType.Name == "String"))
                {
                    property.SetValue(obj, valueAsString, null);
                }
            }
            return obj;
        }

        public static DataTable ConvertObjectToDataTable<T>(IEnumerable<T> list)
        {

            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;

        }
        public static List<T> ConvertDataReaderToObjList<T>(SqlDataReader reader)
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();
            try
            {
                var columnNames = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    columnNames.Add(reader.GetName(i));


                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {



                        if (columnNames.Find(delegate (string colName)
                        {
                            return colName.ToLower().Equals(property.Name.ToLower());
                        }) != null)
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
            }
            catch (Exception ex)
            {

                string err = ex.ToString();

            }
            finally
            {

            }
            return results;
        }

        public static List<T> ConvertDataReaderToObjList<T>(NpgsqlDataReader reader)
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();
            try
            {
                var columnNames = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    columnNames.Add(reader.GetName(i));


                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {



                        if (columnNames.Find(delegate (string colName)
                        {
                            return colName.ToLower().Equals(property.Name.ToLower());
                        }) != null)
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
            }
            catch (Exception ex)
            {

                string err = ex.ToString();

            }
            finally
            {

            }
            return results;
        }
        public static string DateOfyyyyMMdd(DateTime date)
        {

            return string.Format("{0}{1}{2}", date.Year.ToString()
                                            , date.Month.ToString("##00")
                                            , date.Day.ToString("##00"));
        }


        public static string DateOfStartdd_MM_yyyyTH(DateTime date)
        {

            return string.Format("{0}/{1}/{2}", "01", date.Month.ToString("##00"), date.Year.ToString());
        }

        public static string DateOfEnddd_MM_yyyyTH(DateTime date)
        {
            string endday= DateTime.DaysInMonth(date.Year, date.Month).ToString("##00");
            return string.Format("{0}/{1}/{2}", endday, date.Month.ToString("##00"), date.Year.ToString());
        }


        public static string DateOfHHmmTH(DateTime date)
        {
            string res = "";
            try
            {

                if (date == null)
                {
                    return string.Empty;
                }
                string year = (date.Year < 2500 ? (date.Year + 543).ToString() : date.Year.ToString());
                res = string.Format("{0}:{1}", date.Hour.ToString("##00")
                                            , date.Minute.ToString("##00"));
            }
            catch (Exception ex)
            {

            }
            return res;
        }


        public static string DateOfdd_MM_yyyyTH(DateTime date)
        {
            string res = "";
            try
            {
               
                if (date == null)
                {
                    return string.Empty;
                }
                string year = (date.Year < 2500 ? (date.Year + 543).ToString() : date.Year.ToString());
                res = string.Format("{0}/{1}/{2}", date.Day.ToString("##00")
                                            , date.Month.ToString("##00")
                                            , year.ToString());
            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public static string DateOfdd_MM_yyyyTH_withTime(DateTime date)
        {
            string res = "";
            try
            {

                if (date == null)
                {
                    return string.Empty;
                }
                string year = (date.Year < 2500 ? (date.Year + 543).ToString() : date.Year.ToString());
                res = string.Format("{0}/{1}/{2} {3}:{4}", date.Day.ToString("##00")
                                            , date.Month.ToString("##00")
                                            , year.ToString()
                                            ,date.Hour.ToString()
                                            ,date.Minute.ToString());
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public static string FormatCurrency(string data)
        {
            string result = "";
            try {

                result = Convert.ToDecimal(data).ToString("##,##0.00");
            }

            catch  {
                result = data;
            }
            finally {

            }

            return result;


        }

        public static int ToInt(string data)
        {
            int result = 0;
            try
            {

                result = Convert.ToInt32(data);
            }

            catch
            {
               
            }
            finally
            {

            }

            return result;


        }

        public static bool ToBoolean(string data)
        {
            bool result =false;
            try
            {

                result = Convert.ToBoolean(data);
            }

            catch
            {

            }
            finally
            {

            }

            return result;


        }

        public static double ToDoble(string data)
        {
            double result = 0;
            try
            {

                result = Convert.ToDouble(data);
            }

            catch
            {

            }
            finally
            {

            }

            return result;


        }
        public static decimal ToDecimal(string data)
        {
            decimal result = 0;
            try
            {

                result = Convert.ToDecimal(data);
            }

            catch
            {

            }
            finally
            {

            }

            return result;


        }
        public static string FormatInt(string data)
        {
            string result = "";
            try
            {

                result = Convert.ToDecimal(data).ToString("##,##0");
            }

            catch
            {
                result = data;
            }
            finally
            {

            }

            return result;


        }



        /// <summary>
        /// Formate datetime string from dd/MM/yyyy to yyyyMMdd
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateOfyyyyMMdd(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                dt = DateTime.ParseExact(date,
                                   "dd/MM/yyyy",
                                   new System.Globalization.CultureInfo("th-TH"));

                string year =( dt.Year > 2500 ? (dt.Year - 543).ToString() : dt.Year.ToString());
                result = string.Format("{0}{1}{2}", year
                                               , dt.Month.ToString("##00")
                                               , dt.Day.ToString("##00"));
            }
            catch { }
            finally { }
            return result;
        }

        ///from  MM/yyyy to yyyyMMdd
        public static string MMyyToyyyyMMdd(string mmYY, FirstDayOrLastDay firstDayOrLastDay)
        {
            string result = mmYY;
            DateTime dt;
            try
            {
                result = "01/" + mmYY;
                dt = DateTime.ParseExact(result,
                                   "dd/MM/yyyy",
                                   new System.Globalization.CultureInfo("th-TH"));

                if (firstDayOrLastDay == FirstDayOrLastDay.LastDay)
                {
                    dt = dt.AddMonths(1).AddDays(-1);
                }
                string year = (dt.Year > 2500 ? (dt.Year - 543).ToString() : dt.Year.ToString());
                result = string.Format("{0}{1}{2}", year
                                               , dt.Month.ToString("##00")
                                               , dt.Day.ToString("##00"));
            }
            catch { }
            finally { }
            return result;
        }

        /// return fromdate and todate as string array
        public static string QuarterToyyyyMMdd(string year,string quarter, FirstDayOrLastDay firstDayOrLastDay)
        {
            string result="";
            DateTime dt;
            string currentYear = year;
            try
            {
                if (quarter == "1")
                {
                    result = string.Format("01/01/{0}", currentYear);
                }
                else if (quarter == "2") {
                    result = string.Format("01/04/{0}", currentYear);
                }
                else if (quarter == "3")
                {
                    result = string.Format("01/07/{0}", currentYear);
                }
                else if (quarter == "4")
                {
                    result = string.Format("01/10/{0}", currentYear);
                }


                dt = DateTime.ParseExact(result,
                                   "dd/MM/yyyy",
                                   new System.Globalization.CultureInfo("th-TH"));

                if (firstDayOrLastDay == FirstDayOrLastDay.LastDay)
                {
                    dt = dt.AddMonths(1).AddDays(-1);
                }
                string thisyear = (dt.Year > 2500 ? (dt.Year - 543).ToString() : dt.Year.ToString());
                result = string.Format("{0}{1}{2}", thisyear
                                               , dt.Month.ToString("##00")
                                               , dt.Day.ToString("##00"));
            }
            catch { }
            finally { }
            return result;
        }





        /// <summary>
        /// Formate datetime string from dd/MM/yyyy to yyyyMMdd
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateOfyyyyMMddTH(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                dt = DateTime.ParseExact(date,
                                   "dd/MM/yyyy",
                                   new System.Globalization.CultureInfo("th-TH"));

                result = string.Format("{0}{1}{2}", dt.Year.ToString()
                                               , dt.Month.ToString("##00")
                                               , dt.Day.ToString("##00"));
            }
            catch { }
            finally { }
            return result;
        }


        /// <summary>
        /// Formate datetime string from yyyyMMdd to  MM/dd/yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateOfMM_dd_yyyy(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date,
                                  "dd/MM/yyyy",
                                    new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0}/{1}/{2}", dt.Month.ToString("##00")
                                               , dt.Day.ToString("##00")
                                               , dt.Year.ToString());
            }
            catch {
                result = "";
            }
            finally { }
            return result;
        }

        /// <summary>
        /// Formate datetime string from yyyyMMdd to  dd/MM/yyyy
        /// </summary>
        /// <param name="date">dd/MM/yyyy hh:mm:ss</param>
        /// <returns>dd/MM/yyyy TH</returns>
        public static string DateOfdd_MM_yyyyTHLongToShort(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date,
                                  "dd/MM/yyyy hh:mm:ss",
                                    new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0}/{1}/{2}", dt.Day.ToString("##00")
                                               , dt.Month.ToString("##00")
                                               , dt.Year.ToString());
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }


        /// <summary>
        /// Formate datetime string from yyyyMMdd to  dd/MM/yyyy
        /// </summary>
        /// <param name="date">yyyyMMdd</param>
        /// <returns>dd/MM/yyyy TH</returns>
        public static string DateOfdd_MM_yyyyTH(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date,
                                  "yyyyMMdd",
                                    new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0}/{1}/{2}", dt.Day.ToString("##00")
                                               , dt.Month.ToString("##00")
                                               , dt.Year+543);
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }
        ///date='yyyyMMdd'
        public static DateTime ConvertStrToDatetime(string date)
        {
            DateTime dt=new DateTime() ;
            try
            {
                dt= DateTime.ParseExact(date, "yyyyMMdd", new CultureInfo("en-US", false), DateTimeStyles.NoCurrentDateDefault);
            }
            catch
            { }
            finally { }

            return dt;
        }

        /// type : polygon,point,line
        /// coordinates [[[101.560737,13.369669],[101.560737,13.500572],[101.71741,13.500572],[101.71741,13.369669],[101.560737,13.369669]]]

        public static string ToShapeText(string type, string coordinates)
        {
            coordinates = coordinates.Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "");

            string result = "";

            int i = 1;
            try
            {
                foreach (var val in coordinates.Split(','))
                {
                    if (i % 2 == 0)
                    {
                        result += string.Format("{0}, ", val);
                    }
                    else
                    {
                        result += string.Format("{0} ", val);
                    }
                    i++;

                }

                switch (type.ToLower())
                {
                  case "point" :  result = string.Format("{0} ({1})", type, result.Trim().Substring(0, result.Length - 2));  break;
                    case "polygon": result = string.Format("{0} (({1}))", type, result.Trim().Substring(0, result.Length - 2)); break;
                   default: result = string.Format("{0} ({1})", type, result.Trim().Substring(0, result.Length - 2)); break;
                }
            }
            catch
            { }
            finally { }
            return result;

        }

        public static string  ToMultiShapeText(string type, string coordinates)
        {
            //  coordinates = coordinates.Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "");
            coordinates = coordinates.Replace("[", "(").Replace("]", ")");
            coordinates = coordinates.Substring(1, coordinates.Length - 2);
            string result = "";

            int i = 1;
            string[] dataSplit = null;
            int line = 0;
            try
            {
                foreach (var val1 in coordinates.Split(new string[] { "))" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    line++;
                    i = 1;
                    result += "(";

                    dataSplit = val1.Replace(",((", "((").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Split(',');
                    foreach (var val in dataSplit)
                    {
                        if (i % 2 == 0 && i < dataSplit.Length)
                        {
                            result += string.Format("{0}, ", val);
                        }
                        else
                        {
                            result += string.Format("{0} ", val);
                        }
                        i++;

                    }
                    result += "),";
                }

                switch (type.ToLower())
                {
                    case "point": result = string.Format("{0} ({1})", type, result.Trim().Substring(0, result.Length - 2)); break;
                    case "polygon":
                        if (line > 1)
                        {
                            result = string.Format("{0} (({1})))", "MULTIPOLYGON", result.Trim().Substring(0, result.Length - 2));
                        }
                        else
                        {
                            result = string.Format("{0} ({1}))", type, result.Trim().Substring(0, result.Length - 2));
                        }
                        break;
                    default: result = string.Format("{0} ({1})", type, result.Trim().Substring(0, result.Length - 2)); break;
                }
            }
            catch
            { }
            finally { }
            return result;

        }


        //Polygon ((100.186891 14.138351, 100.186891 14.86162, 101.022036 14.86162, 101.022036 14.138351, 100.186891 14.138351))
        //to 
        //coordinates [[[101.560737,13.369669],[101.560737,13.500572],[101.71741,13.500572],[101.71741,13.369669],[101.560737,13.369669]]]
        public static GeojsonFeatureDto2 ToShape( string coordinates)
        {

            GeojsonFeatureDto2 geometryFormat = new GeojsonFeatureDto2();
            string type = coordinates;

            var coordinateList = new List<List<double>>();
            if (coordinates == null)
                return null;
            coordinates = coordinates.ToLower().Replace("(", "").Replace(")", "").Replace("point","").Replace("polygon","").Replace("linestring","");

            string resultCoordinate = "";
            geometryFormat.type = "Feature";
            geometryFormat.properties = new properties();
            geometryFormat.geometry = new geometry2();
           
            if (type.ToLower().IndexOf("point") > -1)
            {
                geometryFormat.geometry.type = "Point";
            }
            else if (type.ToLower().IndexOf("polygon") > -1)
            {
                geometryFormat.geometry.type = "Polygon";
            }
            else if (type.ToLower().IndexOf("linestring") > -1)
            {
                geometryFormat.geometry.type = "LineString";
            }
            int i = 1;
            try
            {
                List<double> coords = null;
            
                foreach (var val in coordinates.Split(','))
                {
                    coords = new List<double>();
                    coords.Add(Converting.ToDoble(val.Split(' ')[0]));
                    coords.Add(Converting.ToDoble(val.Split(' ')[1]));
                    coordinateList.Add(coords);
           

                    i++;

                }
                geometryFormat.geometry.coordinates = coordinateList;

                //{ "type":"Feature","properties":{ },"geometry":{ "type":"Polygon","coordinates":[[[100.186891,14.138351],[100.186891,14.86162],[101.022036,14.86162],[101.022036,14.138351],[100.186891,14.138351]]]}
                //{"type":"Feature","properties":{},"geometry":{"type":"LineString","coordinates":[[100.723761,14.261804],[100.799335,14.264466]]}}


                /*(     if (type.ToLower().IndexOf("point") > -1)
                     {
                         geometryFormat.geometry.type = "Point";
                         geometryFormat.geometry.coordinates = resultCoordinate = string.Format("[{0}]", resultCoordinate);
                     }
                     else if (type.ToLower().IndexOf("polygon") > -1) {
                         geometryFormat.geometry.type = "Polygon";
                         geometryFormat.geometry.coordinates = resultCoordinate = string.Format("[[{0}]]", resultCoordinate);
                     }
                     else if (type.ToLower().IndexOf("linestring") > -1)
                     {
                         geometryFormat.geometry.type = "LineString";
                         geometryFormat.geometry.coordinates = resultCoordinate = string.Format("[[{0}]]", resultCoordinate);
                     }*/


            }
            catch
            { }
            finally { }
            return geometryFormat;

        }

        public static GeojsonFeatureDto2 ToShapeFromGIS(string coordinates)
        {

            GeojsonFeatureDto2 geometryFormat = new GeojsonFeatureDto2();
            string type = coordinates;

            var coordinateList = new List<double[,]>();
            if (coordinates == null)
                return null;
            coordinates = coordinates.ToLower().Replace("(", "").Replace(")", "").Replace("point", "").Replace("polygon", "").Replace("linestring", "");

            string resultCoordinate = "";
            geometryFormat.type = "Feature";
            geometryFormat.properties = new properties();
            geometryFormat.geometry = new geometry2();

            if (type.ToLower().IndexOf("point") > -1)
            {
                geometryFormat.geometry.type = "Point";
            }
            else if (type.ToLower().IndexOf("polygon") > -1)
            {
                geometryFormat.geometry.type = "Polygon";
            }
            else if (type.ToLower().IndexOf("linestring") > -1)
            {
                geometryFormat.geometry.type = "LineString";
            }
            int i = 1;
            try
            {
            //    geometryFormat.geometry.coordinates = new List<double[,]>();
                foreach (var val in coordinates.Split(','))
                {
                    coordinateList.Add(new double[,] { { Converting.ToDoble(val.Split(' ')[0]), Converting.ToDoble(val.Split(' ')[1]) } });
                  
                    // resultCoordinate += string.Format("[{0}],", val.Trim().Replace(" ", ","));
                    i++;

                }
                geometryFormat.geometry.coordinates = null;


                //{ "type":"Feature","properties":{ },"geometry":{ "type":"Polygon","coordinates":[[[100.186891,14.138351],[100.186891,14.86162],[101.022036,14.86162],[101.022036,14.138351],[100.186891,14.138351]]]}
                //{"type":"Feature","properties":{},"geometry":{"type":"LineString","coordinates":[[100.723761,14.261804],[100.799335,14.264466]]}}


                /*(     if (type.ToLower().IndexOf("point") > -1)
                     {
                         geometryFormat.geometry.type = "Point";
                         geometryFormat.geometry.coordinates = resultCoordinate = string.Format("[{0}]", resultCoordinate);
                     }
                     else if (type.ToLower().IndexOf("polygon") > -1) {
                         geometryFormat.geometry.type = "Polygon";
                         geometryFormat.geometry.coordinates = resultCoordinate = string.Format("[[{0}]]", resultCoordinate);
                     }
                     else if (type.ToLower().IndexOf("linestring") > -1)
                     {
                         geometryFormat.geometry.type = "LineString";
                         geometryFormat.geometry.coordinates = resultCoordinate = string.Format("[[{0}]]", resultCoordinate);
                     }*/


            }
            catch
            { }
            finally { }
            return geometryFormat;

        }

        public static string DateOfdd_MM_yyyy(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date,
                                   "MM/dd/yyyy",
                                    new System.Globalization.CultureInfo("en-US"));

                result = string.Format("{0}/{1}/{2}", dt.Day.ToString("##00")
                                               , dt.Month.ToString("##00")
                                               , dt.Year.ToString());
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }

        /// <summary>
        /// Get colum name excel by index
        /// </summary>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public static string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }

        public static string DateOfdd_MM_yyyyToThLongDate(string date)
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date,
                                  "dd/MM/yyyy",
                                    new System.Globalization.CultureInfo("en-US"));
                string year = (dt.Year > 2500 ? (dt.Year).ToString() : (dt.Year+543).ToString());
                result = string.Format("{0} {1} พ.ศ. {2}", dt.Day.ToString()
                                               , mLTh[dt.Month - 1]
                                               , year);
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }
        public static string DateOfyyyyMMddToThLongDate(string date)  //yyyyMMdd
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date, "yyyyMMdd",
                                    new System.Globalization.CultureInfo("en-US"));
                string year = (dt.Year > 2500 ? (dt.Year).ToString() : (dt.Year + 543).ToString());
                result = string.Format("{0} {1} พ.ศ. {2}", dt.Day.ToString()
                                               , mLTh[dt.Month - 1]
                                               , year);
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }
        public static string DateOfDateToThLongDate(DateTime dt)  //yyyyMMdd
        {
            string result = string.Empty; ;
            try
            {
                string year = (dt.Year > 2500 ? (dt.Year).ToString() : (dt.Year + 543).ToString());
                result = string.Format("{0} {1} พ.ศ. {2}", dt.Day.ToString()
                                               , mLTh[dt.Month - 1]
                                               , year);
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }
        public static string DateOfyyyyMMddToThShortDate(string date)  //yyyyMMdd
        {
            string result = date;
            DateTime dt;
            try
            {
                // dt = DateTime.Parse(date, new System.Globalization.CultureInfo("en-US"));
                dt = DateTime.ParseExact(date, "yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
                string year = (dt.Year > 2500 ? (dt.Year).ToString() : (dt.Year + 543).ToString());
                result = string.Format("{0}/{1}/{2}", dt.Day.ToString("##00"), dt.Month.ToString("##00"), year);
            }
            catch
            {
                result = "";
            }
            finally { }
            return result;
        }
    }
}
