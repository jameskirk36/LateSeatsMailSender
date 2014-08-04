using System;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace LateSeatsMailSender
{
    public class FormGenerator
    {
        public static RequestForm GenerateForm(Flight flight)
        {
            var stream = ReadResourceIntoMemory();

            PopulateForm(stream, flight);

            EnsureStreamIsReadable(stream);
            var formName = CreateFormNameFromDepartureDate(flight);

            return new RequestForm(formName, stream);
        }

        private static string CreateFormNameFromDepartureDate(Flight flight)
        {
            return String.Format("request_form_{0}_{1}.docx",
                flight.departure_date.Date.Day,
                flight.departure_date.ToString("MMMM"));
        }

        private static void EnsureStreamIsReadable(MemoryStream stream)
        {
            stream.Position = 0;
        }

        private static MemoryStream ReadResourceIntoMemory()
        {
            return new MemoryStream(Properties.Resources.Lates_Request_Form_Template);
        }

        private static void PopulateForm(MemoryStream stream, Flight flight)
        {
            using (var wordDoc = WordprocessingDocument.Open(stream, true))
            {
                PopulateTable(wordDoc, flight);
            }
        }

        private static void PopulateTable(WordprocessingDocument wordDoc, Flight flight)
        {
            PopulateDepartureDate(wordDoc, flight.DepartureDate);
            PopulateDepartureAirport(wordDoc, flight.DepartureAirport);
            PopulateArrivalAirport(wordDoc, flight.ArrivalAirport);
            PopulateDepartureTime(wordDoc, flight.DepartureTime);
            PopulateReturnDate(wordDoc, flight.ReturnDate);
            PopulateReturnTime(wordDoc, flight.ReturnTime);
        }

        private static void PopulateReturnTime(WordprocessingDocument wordDoc, string returnTime)
        {
            ChangeTextInCell(wordDoc, 7, 1, returnTime);
        }

        private static void PopulateReturnDate(WordprocessingDocument wordDoc, string returnDate)
        {
            ChangeTextInCell(wordDoc, 6, 1, returnDate);
        }

        private static void PopulateDepartureTime(WordprocessingDocument wordDoc, string departureTime)
        {
            ChangeTextInCell(wordDoc, 5, 1, departureTime);
        }

        private static void PopulateArrivalAirport(WordprocessingDocument wordDoc, string arrivalAirport)
        {
            ChangeTextInCell(wordDoc, 4, 1, arrivalAirport);

        }

        private static void PopulateDepartureDate(WordprocessingDocument wordDoc, string departureDate)
        {
            ChangeTextInCell(wordDoc, 2, 1, departureDate);

        }

        private static void PopulateDepartureAirport(WordprocessingDocument wordDoc, string departureAirport)
        {
            ChangeTextInCell(wordDoc, 3, 1, departureAirport);
        }

       private static void ChangeTextInCell(WordprocessingDocument wordDoc, int rowIndex, int colIndex, string txt)
        {
            Table table = 
                wordDoc.MainDocumentPart.Document.Body.Elements<Table>().First();

            // Find the second row in the table.
            TableRow row = table.Elements<TableRow>().ElementAt(rowIndex);

            // Find the third cell in the row.
            TableCell cell = row.Elements<TableCell>().ElementAt(colIndex);

            // Find the first paragraph in the table cell.
            Paragraph p = cell.Elements<Paragraph>().First();

            // Find the first run in the paragraph.
            Run r = p.Elements<Run>().First();

            // Set the text for the run.
            Text t = r.Elements<Text>().First();
            t.Text = txt;
        }
    }
}
