
using Newtonsoft.Json;

namespace LateSeatsMailSender
{
    public class LateSeatMailAlerter
    {
    public static void SendMailWithAttachment(string json, IMailClient mailClient)
        {
            var watchlist = JsonConvert.DeserializeObject<Watchlist>(json);
            var requestForm = FormGenerator.GenerateForm(watchlist.FirstFlight);

            var mailSender = new MailSender();

            mailSender.SendMail(json, mailClient, requestForm);
        }
    }
}
