using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Pwa.FrameWork.ShortUrl.Generator
{
    public class ShortUrlCaseSensitiveDissociateGenerator :  ShortUrlDissociateGenerator, IShortUrlGenerator
    {

        protected override string GenerateTokenSource()
        {
            


            string urlsafe = string.Empty;
            Enumerable.Range(48, 75)
              .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
              //.OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => urlsafe += Convert.ToChar(i));
            Random random = new Random();
            urlsafe = RandomString(random,urlsafe, urlsafe.Length);
           
            return urlsafe;
        }

        //private  Random random = new Random();
        public  string RandomString(Random random, string chars, int length)
        {
             
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
