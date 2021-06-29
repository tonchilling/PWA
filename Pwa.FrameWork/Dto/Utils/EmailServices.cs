using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;
namespace Pwa.FrameWork.Dto.Utils
{
    public class EmailServices
    {
        static string mailServer =  ConfigurationSettings.AppSettings["SMTPServer"].ToString();
        static string senderEmailId = ConfigurationSettings.AppSettings["SMTPUserName"].ToString();
        static string password = ConfigurationSettings.AppSettings["SMTPPasssword"].ToString();
        static string ToEmail = ConfigurationSettings.AppSettings["ToEmail"].ToString();
        static string fromAdd = ConfigurationSettings.AppSettings["From"].ToString();
        static int port = Convert.ToInt32(ConfigurationSettings.AppSettings["SMTPPort"].ToString());

        public static bool SendMail(string To,string Subject,string Body )
        {
            bool result = true;
            try
            {
                var fromAddress = new MailAddress(senderEmailId, fromAdd);
                var toAddress = new MailAddress(senderEmailId);

                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
                var client = new SmtpClient(mailServer, 587)
                {
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(senderEmailId, password),
                    EnableSsl = true,

                };
                MailMessage mail = null;

                mail = new MailMessage(fromAdd, To)
                {
                    Subject = Subject,
                Body = Body
                };
                mail.IsBodyHtml = true;
           
                    client.Send(mail);
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception(ex.Message);
           
            }

            return result;
        }

        static bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            Console.WriteLine(certificate);

            return true;
        }
    }
}
