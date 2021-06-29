using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace Pwa.FrameWork.Utilities
{
    public class JsonHelper
    {
        /// <summary>
        /// serialize object to JSON string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            MemoryStream ms = null;
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                ms = new MemoryStream();
                serializer.WriteObject(ms, obj);
                string retVal = Encoding.UTF8.GetString(ms.ToArray());

                return retVal;
            }
            finally
            {   //make sure we can dispose resource even there's some exception in code
                if (ms != null)
                {
                    ms.Dispose();
                }
            }

        }

        /// <summary>
        /// Deserialize JSON string to object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            MemoryStream ms = null;

            try
            {
                T obj = Activator.CreateInstance<T>();
                ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);

                return obj;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }
            }

        }
    }
}
