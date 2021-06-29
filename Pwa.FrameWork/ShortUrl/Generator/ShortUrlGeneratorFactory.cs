using System;
using System.Collections.Generic;
using System.Text;

namespace Pwa.FrameWork.ShortUrl.Generator
{
    public class ShortUrlGeneratorFactory
    {
        public static IShortUrlGenerator GetShortUrlGenerator()
        {
            //return new ShortUrlDissociateGenerator();
            return new ShortUrlCaseSensitiveDissociateGenerator();
        }
    }
}
