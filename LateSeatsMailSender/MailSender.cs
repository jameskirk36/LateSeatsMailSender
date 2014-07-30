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
            mailClient.SendMail(watchlist.email, _from, _subject, body);
        }

        private static string CreateBody(Watchlist watchlist)
        {
            return "Hi Joe";
        }
    }
}