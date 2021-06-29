using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.Smart1662
{
    public class IncidentNotifyOtherSystem
    {
        public void SendEmailRejectNotify(int incidentID)
        {

            string toEmail = System.Configuration.ConfigurationManager.AppSettings["OtherSys.EmailNotify.To"];

            var resp = RespositoryFactory.GetPwaIncidentResponsitory();
            var inc = resp.GetIncidentWithInfo(incidentID);

            var rejectHis = resp.GetRejectHistory(incidentID);
            string rejectReason = "";
            if(rejectHis!=null && rejectHis.Count() > 0)
            {
                rejectReason = rejectHis.OrderByDescending(o => o.CreatedDate).First().IncidentStageDetail;
            }

            var content = this.GetEmailRejectNotifyContent(toEmail,inc.PwaIncidentNo,inc.RequestType, rejectReason);

            EmailServices.SendMail(toEmail, "ผลการตรวจสอบเรื่องร้องเรียน", content);


        }


        public string GetEmailRejectNotifyContent(string toEmail,string incidentNo,string incTypeName,string rejectReason)
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}Content\\EmailIncidentNoti_OthSys.html";

            var fileContents = System.IO.File.ReadAllText(path);
            var html = fileContents.Replace("{{0}}", toEmail);
            html = html.Replace("{{1}}", incidentNo);
            html = html.Replace("{{2}}", incTypeName);
            html = html.Replace("{{3}}", rejectReason);
           

            return html;


        }
    }
}
