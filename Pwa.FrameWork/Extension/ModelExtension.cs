using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Extension
{
    public static class ModelExtension
    {
        public static T CopyTo<T>(this object from ,T to)
            where T : new() 
        {
            Type fromType = from.GetType();
            Type toType = to.GetType();

            foreach (var f in fromType.GetFields())
            {
                var tField = toType.GetField(f.Name);
                if(tField!=null && (tField.FieldType == f.FieldType))
                {
                    tField.SetValue(to, f.GetValue(from));
                }

            }

            foreach (var p in fromType.GetProperties())
            {
                var tProp = toType.GetProperty(p.Name);
                if (tProp != null && (tProp.PropertyType == p.PropertyType))
                {
                    tProp.SetValue(to, p.GetValue(from));
                }

            }

            return to;
        }



    }

    public static class ClientExtension
    {
        public static string ToClientDateTime(this DateTime? datetime)
        {
            string result = "";
            if(datetime!=null && datetime.HasValue)
            {
                result = datetime.Value.ToString("dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"));
            }
            return result;
        }

        public static string ToClientDate(this DateTime? datetime)
        {
            string result = "";
            if (datetime != null && datetime.HasValue)
            {
                result = datetime.Value.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
            }
            return result;
        }

        public static string ToClientDateTime(this DateTime datetime)
        {
            return datetime.ToString("dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US")); ;
        }

        public static string ToClientDate(this DateTime datetime)
        {
            
            return datetime.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")); ;
        }


        public static DateTime ToDateTime(this string datetime)
        {
            var result = DateTime.Now;
            if (datetime != null && datetime.Trim() !="")
            {
                var dateAndTime = datetime.Split(" ".ToCharArray());
                var date = dateAndTime[0];
                var time = dateAndTime[1];

                string[] ds;
                if (date.IndexOf("/") != -1)
                { ds = date.Split("/".ToCharArray()); }
                else if (date.IndexOf("-") != -1)
                { ds = date.Split("-".ToCharArray()); }
                else { ds = date.Split(".".ToCharArray()); }
                //= date.Split("/".ToCharArray());

                if ( int.Parse((ds[2]))> 2540)
                {
                    ds[2] =( int.Parse((ds[2])) - 543).ToString();
                }

                var newDateTime = $"{ds[2]}/{ds[1]}/{ds[0]}";

               

                result = DateTime.Parse($"{newDateTime} {time}", new System.Globalization.CultureInfo("en-US"));
            }
            return result;
        }

        public static DateTime ToDate(this string datetime)
        {
            var result = DateTime.Now;
            if (datetime != null && datetime.Trim() != "")
            {
                
                var date = datetime;
                

                var ds = date.Split("/".ToCharArray());

                if (int.Parse((ds[2])) > 2540)
                {
                    ds[2] = (int.Parse((ds[2])) - 543).ToString();
                }

                //var newDateTime = $"{ds[1]}/{ds[0]}/{ds[2]}";

                result = new DateTime(int.Parse(ds[2]), int.Parse(ds[1]), int.Parse(ds[0]));

                //result = DateTime.Parse($"{newDateTime}", new System.Globalization.CultureInfo("en-US"));
            }
            return result;
        }
    }


 }
