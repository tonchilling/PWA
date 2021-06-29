using Pwa.FrameWork.Dao.EF.Smart1662;
using Pwa.FrameWork.Dao.Repository.Smart1662;
using Pwa.FrameWork.Dto.Smart1662.RepaireWork;
using Pwa.FrameWork.Dto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pwa.FrameWork.Dao.ADO;
namespace Pwa.FrameWork.Bal.Smart1662
{
    public class IncidentNotify
    {
        private int _incidentID;
        private PwaRepaireWorkHeaderDto _repaireWorkHeaderDto = null;
        LogDAO logDao = null;
        private IncidentNotify()
        {

        }
        public IncidentNotify(int incidentID)
        {
            _incidentID = incidentID;

            var resp = RespositoryFactory.GetPwaRepaireWorkResponsitory(true);
            _repaireWorkHeaderDto = resp.GetById(_incidentID.ToString());


        }

       
        public  string GetNotifyEmailContent(string toEmail)
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}Content\\EmailIncidentNoti.html";

            var fileContents = System.IO.File.ReadAllText(path);
            var html = fileContents.Replace("{{0}}", toEmail);
            html = html.Replace("{{1}}", _repaireWorkHeaderDto.Incidents.First().PwaIncidentNo);
            html = html.Replace("{{2}}", _repaireWorkHeaderDto.RequestTypeName);
            html = html.Replace("{{3}}", _repaireWorkHeaderDto.Incidents.First().RequestCategorySubject);
            html = html.Replace("{{4}}", "ดำเนินการแล้วเสร็จ");
            html = html.Replace("{{5}}", (_repaireWorkHeaderDto.Incidents.First().CompletedCaseDate.HasValue ? _repaireWorkHeaderDto.Incidents.First().CompletedCaseDate.Value.ToString("dd/MM/yy HH:mm",new System.Globalization.CultureInfo("en-US")) : ""));
            html = html.Replace("{{6}}", _repaireWorkHeaderDto.Incidents.First().RequestCategorySubject);

            html = html.Replace("{{7}}", (_repaireWorkHeaderDto.Incidents.First().CompletedCaseDate.HasValue ? _repaireWorkHeaderDto.Incidents.First().CompletedCaseDate.Value.ToString("dd/MM/yy", new System.Globalization.CultureInfo("en-US")) : ""));
            html = html.Replace("{{8}}", (_repaireWorkHeaderDto.Incidents.First().CompletedCaseDate.HasValue ? _repaireWorkHeaderDto.Incidents.First().CompletedCaseDate.Value.ToString("HH:mm", new System.Globalization.CultureInfo("en-US")) : ""));
            html = html.Replace("{{9}}", _repaireWorkHeaderDto.AccountId);
            html = html.Replace("{{10}}", "ที่อยู่");
            html = html.Replace("{{11}}", _repaireWorkHeaderDto.Incidents.First().CaseDetail);
            html = html.Replace("{{12}}", "ผู้แจ้ง");
            html = html.Replace("{{13}}", "เบอร์โทร");

            return html;
        }

        public void SendNotifyEmail(string toEmail)
        {
            var content = this.GetNotifyEmailContent(toEmail);
             logDao = new LogDAO();

            LogDTO logDto = new LogDTO();
          

            try
            {
                EmailServices.SendMail(toEmail, "ผลการตรวจสอบเรื่องร้องเรียน", content);
            }
            catch (Exception ex)
            {
                logDto.Project = "Smart1662";
                logDto.Page = "SendNotifyEmail";
                logDto.CreatedBy = "";
                logDto.IssueDate = DateTime.Now.ToString();
                logDto.ValueField = "To Email" + toEmail;
                logDto.Exception = ex.Message;
                logDao.Log(logDto);
            }
            finally
            { }

        }

    }
}
