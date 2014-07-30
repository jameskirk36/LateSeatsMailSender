using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LateSeatsMailSender;
using NUnit.Framework;

namespace LateSeatsMailSenderTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ReceivesEventSendsEmail()

        {
            var fakeMailClient = new FakeMailClient();
            var json = CreateTestJSON();

            var mailSender = new MailSender();
            mailSender.SendMail(json, fakeMailClient);

            Assert.That(fakeMailClient.MailAddress, Is.EqualTo("joe@laterooms.com"));

        }

        

        private static string CreateTestJSON()
        {
            return @"
            {
	            ""name"": ""joe bloggs"",
	            ""email"": ""joe@laterooms.com"",
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
    }

    public class FakeMailClient : IMailClient
    {
        public string MailAddress { get; set; }
        public void SendMail(string email)
        {
            MailAddress = email;
        }
    }
}
