using System.Net.Mail;
using LateSeatsMailSender;

namespace LateSeatsMailSenderTests
{
    public class FakeMailClient : IMailClient
    {
        private MailMessage _mailMessage = null;
        public MailMessage MailMessage {
            get
            {
                return _mailMessage;
            }
            set
            {
                
            } 
        }

        public void SendMail(string mailTo, string mailFrom, string mailSubject, string body)
        {
          
            _mailMessage = new MailMessage(mailFrom, mailTo, mailSubject, body);
        }

        public void SendMail(MailMessage mail)
        {
            _mailMessage = mail;
        }
    }
}