
using Pwa.FrameWork.Dao.Repository.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.Incident
{
    public class SlaManager
    {

        public List<Dao.EF.Smart1662.SysHoliday> GetHolidays(DateTime start,DateTime end)
        {
            var holidayResp = RespositoryFactory.GetSysHolidayResponsitory();

            return holidayResp.GetHolidates(start, end);
        }
        public SlaCaculateInfo CalculateSla(bool sla, int processStatus,string categorySubjec ,DateTime? startSla,DateTime? endSla)
        {
            SlaCaculateInfo result = null;
            /*if not ignore sla*/
            if (!sla && processStatus == 10 && endSla.HasValue)
            {
                var cateSubjectResp = Pwa.FrameWork.Dao.Repository.Smart1662.RespositoryFactory.GetSysRequestCategorySubjectResponsitory();
                var categorySubject = cateSubjectResp.GetById(int.Parse((string.IsNullOrEmpty(categorySubjec) ? "0" : categorySubjec)));
                var slaWorkingMinute = Convert.ToInt32( (endSla.Value - startSla.Value).TotalMinutes);

                var holidays = GetHolidays(startSla.Value, endSla.Value);
                var holidaysMinute = (((holidays!=null)? holidays.Count():0) * (24 * 60));

                slaWorkingMinute -= holidaysMinute;

                int slaDay = (categorySubject != null && categorySubject.SLA != null && categorySubject.SLA != "") ? int.Parse(categorySubject.SLA) : 0;
                int slaMinute = (slaDay * (24 * 60));

                result = new SlaCaculateInfo()
                {
                    SLAStatus = slaWorkingMinute > slaMinute ,
                    MinuteWorking = slaWorkingMinute
                };
            }

            return result;
        }
    }

    public class SlaCaculateInfo
    {
        public bool? SLAStatus { get; set; }
        public int MinuteWorking { get; set; }
    }

    
}
