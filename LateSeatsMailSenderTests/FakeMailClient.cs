using LateSeatsMailSender;

namespace LateSeatsMailSenderTests
{
    public class FakeMailClient : IMailClient
    {
        public string MailTo { get; set; }
        public string MailBody { get; set; }
        public string MailFrom { get; set; }
        public object MailSubject { get; set; }


        public void SendMail(string mailTo, string mailFrom, string mailSubject, string body)
        {
            MailTo = mailTo;
            MailFrom = mailFrom;
            MailSubject = mailSubject;
            MailBody = body;
        }

    }
}