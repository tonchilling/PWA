using Pwa.FrameWork.Dao.EF.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Pwa.FrameWork.Dao.Repository.Smart1662
{
    public class ShortUrlResponsitory
    {
        public ShotUrlTokenSource GetTokenSource(string tokenSource)
        {
            using (var context = new Smart1662Entities())
            {
                
                return context.ShotUrlTokenSource.Where(s => s.Source == tokenSource).FirstOrDefault();
            }
        }

        public Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl GetShortUrlByToken(string token)
        {
            using (var context = new Smart1662Entities())
            {
                return context.ShortUrl.Where(s => s.Token == token).FirstOrDefault();
            }

        }

        public ShortUrlToken NextToken()
        {
            using (var context = new Smart1662Entities())
            {
                return context.ShortUrlToken.Where(s => s.Status == 1).FirstOrDefault();
            }

        }
        public void MarkTokenAsUsed(string token)
        {

            using (var context = new Smart1662Entities())
            {
                var target = context.ShortUrlToken.Where(s => s.Token == token).FirstOrDefault();
                if (target != null)
                {
                    target.Status = 2;
                }
                context.SaveChanges();
            }

        }

        public int CountAvailableToken()
        {

            using (var context = new Smart1662Entities())
            {
                return context.ShortUrlToken.Where(s => s.Status == 1).Count();

            }

        }

        public void AddTokens(ShotUrlTokenSource tokenSource, List<ShortUrlToken> tokens)
        {
            using (var scope = new TransactionScope())
            {
                var context = new Smart1662Entities();
                context.ShotUrlTokenSource.Add(tokenSource);
                context.SaveChanges();

                var sourceID = tokenSource.TokenSourceID;
                tokens.ForEach(t => t.TokenSourceID = sourceID);
                context.ShortUrlToken.AddRange(tokens);

                context.SaveChanges();

                scope.Complete();
            }
        }

        public void SaveShortUrl(Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl shortUrl)
        {

            using (var scope = new TransactionScope())
            {
                var context = new Smart1662Entities();
                context.ShortUrl.Add(shortUrl);

                var target = context.ShortUrlToken.Where(s => s.Token == shortUrl.Token).FirstOrDefault();
                if (target != null)
                {
                    target.Status = 2;
                }
                context.SaveChanges();

                scope.Complete();
            }

        }

        public Pwa.FrameWork.Dao.EF.Smart1662.ShortUrl GetShortUrl(string token)
        {
            using (var context = new Smart1662Entities())
            {
                return context.ShortUrl.Where(s => s.Token == token).FirstOrDefault();

            }
        }

         
    }
}
