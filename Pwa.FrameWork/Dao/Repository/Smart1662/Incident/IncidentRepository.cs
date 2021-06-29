using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.EF.Smart1662;

namespace Pwa.FrameWork.Dao.Repository.Smart1662.Incident
{
    public class IncidentRepository : IIncidentRepository
    {
        

        public async Task Add(SysConplainant comlain)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                context.SysConplainant.Add(comlain);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(SysConplainant comlain)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = await context.SysConplainant.FirstOrDefaultAsync(c => c.complainant_id == comlain.complainant_id);
                context.SysConplainant.Remove(target);
                await context.SaveChangesAsync();
            }
        }

        public async Task<SysConplainant> GetComplain(string id)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = await context.SysConplainant.FirstOrDefaultAsync(c => c.complainant_id == id);
                return target;
            }
        }

        public async Task<IEnumerable<SysConplainant>> GetComplains()
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                var target = await context.SysConplainant.ToListAsync();
                return target.AsQueryable();

            }
        }

        public async Task Update(SysConplainant comlain)
        {
            using (Smart1662Entities context = new Smart1662Entities())
            {
                context.Entry(comlain).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
