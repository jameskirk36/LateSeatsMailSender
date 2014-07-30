using System.Collections.Generic;
using LateSeatsMailSender;
using Newtonsoft.Json;

namespace LateSeatsMailSenderTests
{
    public class Mail
    {
        public string name;
        public string email;
        public object[] flights;
    }
    public class MailSender
    {
        public void SendMail(string json, IMailClient mailClient)
        {
            var mail = JsonConvert.DeserializeObject<Mail>(json);
            mailClient.SendMail(mail.email);
        }
    }
}