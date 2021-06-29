using System;
using System.Collections.Generic;
using System.Text;


namespace Pwa.FrameWork.ShortUrl.Generator
{
    public interface IShortUrlGenerator
    {
        string NextToken();
        string BaseUrl { get; set; }


        Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl CreateShortUrl(Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl shortUrl);

        Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl GetShortUrl(string token);


    }
}
