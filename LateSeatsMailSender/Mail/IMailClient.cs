using System.Net.Mail;

namespace LateSeatsMailSender.Mail
{
    public interface IMailClient
    {
        void SendMail(string mailTo, string mailFrom, string mailSubject, string body);
        void SendMail(MailMessage mailTo);
    }
}
