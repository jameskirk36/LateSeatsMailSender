using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
	            ""flights"": [
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
                        ""return_date"":""2014-08-31T10:00:00"",
		            }
	            ]
            }";
        }

        static void Main(string[] args)
        {
            var json = ReceiveJSON();

            var watchlist = JsonConvert.DeserializeObject<Watchlist>(json);
            FormGenerator.GenerateForm(watchlist);

            var mailSender = new MailSender();
            var realMailSender = new SmtpClientWrapper(args[0], Convert.ToInt32(args[1]));

            mailSender.SendMail(json, realMailSender);
        }
    }
}
