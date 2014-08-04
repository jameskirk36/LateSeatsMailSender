using System.Net.Mail;
using DocumentFormat.OpenXml.Packaging;
using LateSeatsMailSender;
using NUnit.Framework;
using Newtonsoft.Json;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace LateSeatsMailSenderTests
{
    [TestFixture]
    public class Tests : DocumentWrapper
    {
        private string _body = @"
Yo james kirk, 

Here are your flight details:

Outbound Flight: 31 July, 2014 10:00:00 from Manchester Airport

Return Flight: 31 August, 2014 10:00:00 from Palma Mallorca Airport

Thanks, 
The Late Seats Finder Team 
";

        [Test]
        public void GivenAFlightWatchlistSendAnEmailWithAttachedForm()
        {
            var fakeMailClient = new FakeMailClient();
            var json = CreateTestJSON();
            LateSeatMailAlerter.SendMailWithAttachment(json, fakeMailClient);

            Assert.That(fakeMailClient.MailMessage.To[0].ToString(), Is.EqualTo("james.kirk@laterooms.com"));
            Assert.That(fakeMailClient.MailMessage.From.ToString(), Is.EqualTo("lateseatalerts@laterooms.com"));
            Assert.That(fakeMailClient.MailMessage.Subject, Is.EqualTo("Your late seat can be booked!"));
            Assert.That(fakeMailClient.MailMessage.Body, Is.EqualTo(_body));

            Assert.True(fakeMailClient.MailMessage.Attachments.Count == 1);

            var attachment = fakeMailClient.MailMessage.Attachments[0];
           CheckAttachment(json, attachment);
        }

        private void CheckAttachment(string json, Attachment attachment)
        {
            var watchlist = JsonConvert.DeserializeObject<Watchlist>(json);
            var flight = watchlist.FirstFlight;
            Assert.That(attachment.Name, Is.EqualTo("request_form_31_July.docx"));
            using (var wordDoc = WordprocessingDocument.Open(attachment.ContentStream, true))
            {
                CheckMatches(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.ReturnTime, 1), flight.ReturnTime);
                CheckMatches(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.ReturnDate, 1), flight.ReturnDate);
                CheckMatches(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.DepartureTime, 1), flight.DepartureTime);
                CheckMatches(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.ArrivalAirport, 1), flight.ArrivalAirport);
                CheckMatches(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.DepartureDate, 1), flight.DepartureDate);
                CheckMatches(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.DepartureAirport, 1), flight.DepartureAirport);
            }
        }

        private void CheckMatches(Text cell, string expectedText)
        {
            Assert.That(cell.Text, Is.EqualTo(expectedText));
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
