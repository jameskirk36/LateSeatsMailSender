using System.Net.Mail;

namespace LateSeatsMailSender
{
    internal class SmtpClientWrapper : IMailClient
    {
        private readonly SmtpClient _mailClient;

        public SmtpClientWrapper(string smtpHost, int port)
        {
            _mailClient = new SmtpClient(smtpHost, port);
        }

        public void SendMail(string mailTo, string mailFrom, string mailSubject, string body)
        {
            _mailClient.Send(new MailMessage(mailFrom, mailTo, mailSubject, body));
        }

        public void SendMail(MailMessage mailTo)
        {
            _mailClient.Send(mailTo);
        }
    }
}