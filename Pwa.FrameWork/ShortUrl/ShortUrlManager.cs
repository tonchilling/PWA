using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.ShortUrl.Configuration;
using Pwa.FrameWork.ShortUrl.Generator;
using System;
using System.Collections.Generic;
using System.Text;


namespace Pwa.FrameWork.ShortUrl
{
    public class ShortUrlManager
    {
        private IShortUrlGenerator _generator = null;

        private IShortUrlGenerator ShortUrlGenerator
        {
            get 
            {
                if (_generator == null)
                {
                    _generator = ShortUrlGeneratorFactory.GetShortUrlGenerator();
                }
                return _generator;
            }
        }

        public Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl CreateShortUrl(string fullUrl)
        {
            var generator = ShortUrlGeneratorFactory.GetShortUrlGenerator();
            generator.BaseUrl = ShortUrlSettings.BaseShortUrl;


            return generator.CreateShortUrl(new Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl()
            {
                Url = fullUrl,
                CreatedBy = "A1001",
                CreatedDate = DateTime.Now
            });
        }

        public Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl GetShortUrlByToken(string token)
        {
            ShortUrlResponsitory webAppResp = new ShortUrlResponsitory();
            return webAppResp.GetShortUrlByToken(token);
        }

    }
}
