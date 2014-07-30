using LateSeatsMailSender;
using NUnit.Framework;
using Newtonsoft.Json;

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

            Assert.That(fakeMailClient.MailMessage.To[0].ToString(), Is.EqualTo("joe@laterooms.com"));
            Assert.That(fakeMailClient.MailMessage.From.ToString(), Is.EqualTo("lateseatalerts@laterooms.com"));
            Assert.That(fakeMailClient.MailMessage.Subject, Is.EqualTo("Your LateSeat Alerts"));
            Assert.That(fakeMailClient.MailMessage.Body, Is.EqualTo("Hi Joe"));

            Assert.True(fakeMailClient.MailMessage.Attachments.Count == 1);
            Assert.That(fakeMailClient.MailMessage.Attachments[0].Name,Is.EqualTo("form1.docx"));
        }

        private static string CreateTestJSON()
        {
            return @"
            {
	            ""name"": ""joe bloggs"",
	            ""email"": ""joe@laterooms.com"",
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



        [Test]
        public void OpenXMLTest()
        {
            var watchlist = JsonConvert.DeserializeObject<Watchlist>(CreateTestJSON());
            
            FormGenerator.GenerateForm(watchlist);
        }
    }
}
