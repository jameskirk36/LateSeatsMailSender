using System;
using System.Net.Mail;
using Newtonsoft.Json;

namespace LateSeatsMailSender
{
    public class MailSender
    {
        private const string _from = "lateseatalerts@laterooms.com";
        private const string _subject = "Your late seat can be booked!";


        public void SendMail(string json, IMailClient mailClient)
        {
            var watchlist = DeserialiseJSON(json);
            var body = CreateBody(watchlist);
            var mail = CreateMailWithAttachment(watchlist, body);
            
            mailClient.SendMail(mail);
        }

        private static Watchlist DeserialiseJSON(string json)
        {
            return JsonConvert.DeserializeObject<Watchlist>(json);
        }

        private static MailMessage CreateMailWithAttachment(Watchlist watchlist, string body)
        {
            var mail = new MailMessage(_from, watchlist.email, _subject, body);
            mail.Attachments.Add(new Attachment(@"C:\temp\request_form.docx"));
            return mail;
        }

        private static string CreateBody(Watchlist watchlist)
        {
            return String.Format(@"
Yo {0}, 

Here are your flight details:

Outbound Flight: {1} {2} from {3}

Return Flight: {4} {5} from {6}

Thanks, 
The Late Seats Finder Team 
",watchlist.name,
 watchlist.FirstFlight.DepartureDate,
 watchlist.FirstFlight.DepartureTime,
 watchlist.FirstFlight.DepartureAirport,
 watchlist.FirstFlight.ReturnDate,
 watchlist.FirstFlight.ReturnTime,
 watchlist.FirstFlight.ArrivalAirport


 );
        }
    }
}