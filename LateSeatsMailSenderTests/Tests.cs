using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LateSeatsMailSenderTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ReceivesJSONSendsEmail()
        {
            var fakeMailClient = new FakeMailClient();
            var mailSender = new MailSender();
            mailSender.SendMail(CreateTestJSON(), fakeMailClient);

            Assert.That(fakeMailClient.MailTo, Is.EqualTo("joe@laterooms.com"));
            Assert.That(fakeMailClient.MailFrom, Is.EqualTo("lateseatalerts@laterooms.com"));
            Assert.That(fakeMailClient.MailSubject, Is.EqualTo("Your LateSeat Alerts"));
            Assert.That(fakeMailClient.MailBody, Is.EqualTo("Hi Joe"));
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
}
