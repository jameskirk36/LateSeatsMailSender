using System.Net.Mail;
using DocumentFormat.OpenXml.Packaging;
using LSF.Mailer.Controllers;
using LateSeatsMailSender;
using LateSeatsMailSender.Form;
using LateSeatsMailSender.Watchlist;
using NUnit.Framework;
using Newtonsoft.Json;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace LateSeatsMailSenderTests
{
    [TestFixture]
    public class Tests : DocumentWrapper
    {
        private string _expectedBody = @"
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
            var mailClient = CreateFakeMailClient();
            var json = WatchlistJSONWithSingleFlight();

            LateSeatMailAlerter.SendMailWithAttachment(json, mailClient);

            Assert.That(mailClient.MailMessage.To[0].ToString(), Is.EqualTo("james.kirk@laterooms.com"));
            Assert.That(mailClient.MailMessage.From.ToString(), Is.EqualTo("lateseatalerts@laterooms.com"));
            Assert.That(mailClient.MailMessage.Subject, Is.EqualTo("Your late seat can be booked!"));
            Assert.That(mailClient.MailMessage.Body, Is.EqualTo(_expectedBody));

            CheckAttachments(mailClient.MailMessage.Attachments, json);
        }

        private void CheckAttachments(AttachmentCollection attachments, string json)
        {
            Assert.True(attachments.Count == 1);
            CheckAttachment(json, attachments[0]);
        }

        private static FakeMailClient CreateFakeMailClient()
        {
            return new FakeMailClient();
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

        private static string WatchlistJSONWithSingleFlight()
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
       
        [Test]
        [Ignore("Smoke Test")]
        public void SmokeTest()
        {
            var controller = new HomeController();
            controller.Index(WatchlistJSONWithSingleFlight());
        }

    }
}
