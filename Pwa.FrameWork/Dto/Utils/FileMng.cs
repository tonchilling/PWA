using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Xml;


namespace Pwa.FrameWork.Dto.Utils
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	public class FileMng
	{
        public FileMng()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string FillZero(string number,int totalZero) 
		{
			string result = "";

			for(int i=0;i<totalZero;i++) 
			{
				result+="0";
			}

			return result+number;
		}

		public static string FillZero(int number,int totalZero) 
		{
			return FillZero(Convert.ToString(number),totalZero);
		}

        public static int ConvertToInt(string value)
        {

            int nResult = 0;

            try
            {
                nResult = Convert.ToInt32(value);
            }
            catch
            { }
            finally
            {
            }
            return nResult;
        }

		public static string GetLangCode(string lang) 
		{
			return null;//NokAirWeb.Utility.ConfigXMLManager.getConfigValue("configs/global/LangCodes/LangCode[Lang='"+lang+"']/Code");
		}

        public static MemoryStream Response2Stream(string fullPathName)
        {
            FileStream fStream = null;
            MemoryStream mStream = null;

            try
            {
                FileInfo myFile;

                myFile = new FileInfo(fullPathName);

                if (myFile.Exists == true)
                {
                    //string strDirectory = string.Format("{0}Upload\\{1}\\EXCEL\\", HttpContext.Current.Request.PhysicalApplicationPath, R3OID);

                    fStream = new FileStream(fullPathName, FileMode.Open);
                    byte[] Data = new Byte[fStream.Length];
                    fStream.Read(Data, 0, Data.Length);
                    mStream = new MemoryStream(Data);
                }
                else
                {
                    return null;
                }

                myFile = null;

                return mStream;
            }
            finally
            {
                if (mStream != null)
                {
                    mStream.Close();
                    mStream.Flush();
                    mStream.Dispose();
                }

                if (fStream != null)
                {
                    fStream.Close();
                    //fstream.Flush();
                    fStream.Dispose();
                }

            }
        }


        public static bool HaveDirectory(string dirName)
        {
            if (dirName == null) return false;
            try
            {
                if (!System.IO.Directory.Exists(dirName))
                {
                    System.IO.Directory.CreateDirectory(dirName);
                }
            }
            catch (Exception ex)
            {
                // throw io exception
            }
            return true;
        }

        public static bool HaveDirectory2(string dirName)
        {
            if (dirName == null) return false;
            try
            {
                if (!System.IO.Directory.Exists(dirName))
                {
                    System.IO.Directory.CreateDirectory(dirName);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool HaveFile(string fileName)
        {
            bool bResult = false;
            if (fileName == null) bResult= false;
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    bResult = true;
                    //System.IO.Directory.CreateDirectory(dirName);
                }
            }
            catch (Exception ex)
            {
                // throw io exception
            }
            return bResult;
        }
        public static bool DeleteFile(string fileName)
        {
            bool bResult = false;
            if (fileName == null) bResult = false;
            try
            {
                if (System.IO.File.Exists(fileName))
                {
                    bResult = true;
                    System.IO.File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return bResult;
        }


   
        static bool HaveDirectory(string[] dirName)
        {
            if (dirName == null) return false;
            try
            {
                foreach (string dirNameR in dirName)
                {
                    if (!Directory.Exists(dirNameR))
                    {
                        Directory.CreateDirectory(dirNameR);
                    }
                }
            }
            catch (Exception ex)
            {
                // throw io exception
            }
            return true;
        }

        public static string WriteObject2File(string rootPATH, string fileName, string obj, string getFileType)
        {
            string strXML = "";
            string filename = "";
            string fileName1 = "";
            string strDate = DateTime.Now.ToString("yyyyMMdd_HHmmss",
                                    new System.Globalization.CultureInfo("en-US"));
            string strFolderName = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US"));
            XmlTextWriter writer = null;
            // if (obj != null) 
            try
            {
                strXML = obj;

                if (HaveDirectory(rootPATH))
                {
                    strDate = DateTime.Now.ToString("yyyyMMdd_HHmmss",
                           new System.Globalization.CultureInfo("en-US"));
                    fileName1 = fileName + "." + getFileType.ToString();
                    filename = rootPATH + "\\" + fileName + "." + getFileType;


                    //while (File.Exists(filename))
                    //{ //if found file name, try to generate new
                    //    System.Threading.Thread.Sleep(1000);//sleep for a second.

                    //    //wake-up and generate new name
                    //    strDate = DateTime.Now.ToString("yyyyMMdd_HHmmss",
                    //        new System.Globalization.CultureInfo("en-US"));
                    //    filename = rootPATH + getFileType.ToString();
                    //}

                    writer = new XmlTextWriter(filename, null);
                    writer.WriteRaw(strXML);
                }
                else
                {
                    // should throw can't create directory
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WriteObject2Xml error : " + ex.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    Debug.WriteLine("Close ..ok");
                }
            }

            return fileName1;
        }
        public static string WriteObject2File(string[] rootPATH, string fileName, string obj, string getFileType)
        {
            string strXML = "";
            string filename = "";
            string fileName1 = "";
            string strDate = DateTime.Now.ToString("yyyyMMdd_HHmmss",
                                    new System.Globalization.CultureInfo("en-US"));
            string strFolderName = DateTime.Now.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US"));
            XmlTextWriter writer = null;
            // if (obj != null) 
            try
            {
                strXML = obj;

                if (HaveDirectory(rootPATH))
                {


                    foreach (string realPath in rootPATH)
                    {
                        if (!realPath.Trim().Equals(""))
                        {

                            filename = realPath + "\\" + fileName + "." + getFileType.ToString();
                            writer = new XmlTextWriter(filename, null);
                            writer.WriteRaw(strXML);
                            if (writer != null)
                            {
                                writer.Close();
                                //Debug.WriteLine("Close ..ok");
                            }

                        }
                    }
                }
                else
                {
                    // should throw can't create directory
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WriteObject2Xml error : " + ex.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    Debug.WriteLine("Close ..ok");
                }
            }

            return fileName1;
        }

     

    

      
      
	}
}
