using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.ShortUrl.Configuration
{
    public class ShortUrlSettings
    {
        public static string BaseShortUrl =>  ConfigurationManager.AppSettings["SHOTURL.BASEURL"];

        public static int TokenMinDigit
        {
            get
            {

                return 3;
            }
        }

        public static int TokenMaxDigit
        {
            get
            {
                 
                return 10;
            }
        }

        public static int SourceTokenLotCount
        {
            get
            {
                return 1;
            }
        }

        public static int MinAvailableToken
        {
            get
            {
                
                return   500;
            }
        }

        public static int MaxRequireAvailableToken
        {
            get
            {
                
                return 1000;
            }
        }
    }
}
