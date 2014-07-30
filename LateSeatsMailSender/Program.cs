using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LateSeatsMailSender
{
    class Program
    {
        private static string ReceiveJSON()
        {
            return @"
            {
	            ""name"": ""james kirk"",
	            ""email"": ""james.kirk@laterooms.com"",
	            ""flight"": [
		            {
			            ""departure_airport"" : {
				            ""code"": ""MIA"",
				            ""name"": ""Manchester Airport"" 
			            },
			            ""destination_airport"" : {
				            ""code"": ""PMI"",
				            ""name"": ""Palma Mallorca Airport""
			            },
			            ""departure_date"" : ""2014-07-31T10:00:00"",
			            ""arrival_date"" : ""2014-07-31T13:30:00"",
			            ""flight_number"" : ""TOM1234"",
		            }
	            ]
            }";
        }

        static void Main(string[] args)
        {
            var mailSender = new MailSender();
            var realMailSender = new SMPTClientWrapper(args[0], Convert.ToInt32(args[1]));

            mailSender.SendMail(ReceiveJSON(), realMailSender);
        }
    }

    internal class SMPTClientWrapper : IMailClient
    {
        private readonly SmtpClient _mailClient;

        public SMPTClientWrapper(string smtpHost, int port)
        {
            _mailClient = new SmtpClient(smtpHost, port);
        }

        public void SendMail(string mailTo, string mailFrom, string mailSubject, string body)
        {
            _mailClient.Send(new MailMessage(mailFrom, mailTo, mailSubject, body));
        }
    }
}
