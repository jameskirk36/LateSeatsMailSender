using System.Net.Mail;
using Newtonsoft.Json;

namespace LateSeatsMailSender
{
    public class MailSender
    {
        private const string _from = "lateseatalerts@laterooms.com";
        private const string _subject = "Your LateSeat Alerts";

        public void SendMail(string json, IMailClient mailClient)
        {
            var watchlist = JsonConvert.DeserializeObject<Watchlist>(json);
            var body = CreateBody(watchlist);
            var mail = CreateMailWithAttachment(watchlist, body);
            
            mailClient.SendMail(mail);
        }

        private static MailMessage CreateMailWithAttachment(Watchlist watchlist, string body)
        {
            var mail = new MailMessage(_from, watchlist.email, _subject, body);
            mail.Attachments.Add(new Attachment("form1.docx"));
            return mail;
        }

        private static string CreateBody(Watchlist watchlist)
        {
            return "Hi Joe";
        }
    }
}