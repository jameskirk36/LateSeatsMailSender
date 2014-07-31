using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace LateSeatsMailSender
{
    public class FormGenerator
    {
        public static Stream GenerateForm(Watchlist watchlist)
        {
            var stream = ReadResourceIntoMemory();

            PopulateForm(stream, watchlist);

            EnsureStreamIsReadable(stream);

            return stream;
        }

        private static void EnsureStreamIsReadable(MemoryStream stream)
        {
            stream.Position = 0;
        }

        private static MemoryStream ReadResourceIntoMemory()
        {
            return new MemoryStream(Properties.Resources.Lates_Request_Form_Template);
        }

        private static void PopulateForm(MemoryStream stream, Watchlist watchlist)
        {
            using (var wordDoc = WordprocessingDocument.Open(stream, true))
            {
                PopulateTable(wordDoc, watchlist);
            }
        }

        private static void PopulateTable(WordprocessingDocument wordDoc, Watchlist watchlist)
        {
            PopulateDepartureDate(wordDoc, watchlist.FirstFlight.DepartureDate);
            PopulateDepartureAirport(wordDoc, watchlist.FirstFlight.DepartureAirport);
            PopulateArrivalAirport(wordDoc, watchlist.FirstFlight.ArrivalAirport);
            PopulateDepartureTime(wordDoc, watchlist.FirstFlight.DepartureTime);
            PopulateReturnDate(wordDoc, watchlist.FirstFlight.ReturnDate);
            PopulateReturnTime(wordDoc, watchlist.FirstFlight.ReturnTime);
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
