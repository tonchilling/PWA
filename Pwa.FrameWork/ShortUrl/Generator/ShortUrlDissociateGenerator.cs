using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.ShortUrl.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Pwa.FrameWork.ShortUrl.Generator
{
    public class ShortUrlDissociateGenerator : IShortUrlGenerator
    {
        protected ShortUrlResponsitory _webAppResp = new ShortUrlResponsitory();
        public string BaseUrl { get; set; }
        public Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl CreateShortUrl(Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl shortUrl)
        {
            Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl result = null;
            if (shortUrl != null)
            {
                var token = (shortUrl.Token != null && shortUrl.Token != "") ? shortUrl.Token : this.NextToken();

                shortUrl.Token = token;
                shortUrl.ShortenedUrl = $"{BaseUrl}/{token}";
                _webAppResp.SaveShortUrl(shortUrl);
                result = _webAppResp.GetShortUrl(token);
            }

            return result;
        }

        public Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl GetShortUrl(string token)
        {
            return _webAppResp.GetShortUrl(token);
        }

        public string NextToken()
        {
            
            if (_webAppResp.CountAvailableToken() < ShortUrlSettings.MinAvailableToken)
            {
                while (_webAppResp.CountAvailableToken() < ShortUrlSettings.MaxRequireAvailableToken)
                {
                    GenerateToken();
                }
            }
           
            var nextToken = _webAppResp.NextToken();
            return (nextToken != null) ? nextToken.Token : null;
           
        }

        private void GenerateToken()
        {
            bool duplicated = true;
            var tokenSource = "";
            var tokenLotCount = ShortUrlSettings.SourceTokenLotCount;
            var minDigit = ShortUrlSettings.TokenMinDigit;
            var maxDigit = ShortUrlSettings.TokenMaxDigit;
            while (duplicated)
            {
                tokenSource = "";
                for (int i = 0; i < tokenLotCount; i++)
                {
                    tokenSource = GenerateTokenSource();
                }
                
                var savedTokenSource = _webAppResp.GetTokenSource(tokenSource);
                duplicated = savedTokenSource != null;
            }
            var targetTokenSource = "";
            var tokens = new List<string>(0);
          

            for (int digit = minDigit; digit <= maxDigit; digit++)
            {
                for (int index = 0; index < tokenSource.Length; index++)
                {
                    if ((tokenSource.Length - index) < digit)
                    {
                        targetTokenSource += tokenSource.Substring(0, digit);
                    }
                    else
                    {
                        targetTokenSource = tokenSource;
                    }
                    tokens.Add(targetTokenSource.Substring(index, digit));
                }
            }
            var rnd = new Random();
            tokens = tokens.OrderBy(o => rnd.Next()).ToList();
            if (tokens.Count() > 0)
            {
                _webAppResp.AddTokens(new ShotUrlTokenSource()
                {
                    Source = tokenSource,
                    MinGeneratedDigit = (short)minDigit,
                    MaxGeneratedDigit = (short)maxDigit,
                    CreatedBy = "A1001",
                    CreatedDate = DateTime.Now,
                    Status = 2

                }, tokens.Select(token => new ShortUrlToken()
                {
                    Token = token,
                    Status = 1,
                    CreatedBy = "A1001",
                    CreatedDate = DateTime.Now
                }).ToList());
            }
        }

        protected virtual string GenerateTokenSource()
        {
            

            string urlsafe = string.Empty;
            Enumerable.Range(48, 75)
              .Where(i => i < 58 || i > 64 && i < 91 )
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => urlsafe += Convert.ToChar(i));

            return urlsafe;
        }
    }
}
