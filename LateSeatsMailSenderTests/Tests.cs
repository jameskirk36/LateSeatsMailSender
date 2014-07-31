using LateSeatsMailSender;
using NUnit.Framework;
using Newtonsoft.Json;

namespace LateSeatsMailSenderTests
{
    [TestFixture]
    public class Tests
    {
        private string _body = @"
Yo james kirk, 

Here are your flight details:

Outbound Flight: 31/07/2014 10:00:00 from Manchester Airport

Return Flight: 31/08/2014 10:00:00 from Palma Mallorca Airport

Thanks, 
The Late Seats Finder Team 
";

        [Test]
        public void GivenSomeJsonSendAnEmail()
        {
            var fakeMailClient = new FakeMailClient();

            LateSeatMailAlerter.SendMailWithAttachment(CreateTestJSON(), fakeMailClient);

            Assert.That(fakeMailClient.MailMessage.To[0].ToString(), Is.EqualTo("james.kirk@laterooms.com"));
            Assert.That(fakeMailClient.MailMessage.From.ToString(), Is.EqualTo("lateseatalerts@laterooms.com"));
            Assert.That(fakeMailClient.MailMessage.Subject, Is.EqualTo("Your late seat can be booked!"));
            Assert.That(fakeMailClient.MailMessage.Body, Is.EqualTo(_body));

            Assert.True(fakeMailClient.MailMessage.Attachments.Count == 1);
            Assert.That(fakeMailClient.MailMessage.Attachments[0].Name,Is.EqualTo("request_form.docx"));
        }

        private static string CreateTestJSON()
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
    }
}
