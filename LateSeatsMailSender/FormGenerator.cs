using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using BookmarkStart = DocumentFormat.OpenXml.Wordprocessing.BookmarkStart;


namespace LateSeatsMailSender
{
    public class FormGenerator
    {
        public static void GenerateForm(Watchlist watchlist)
        {
            string sourceFile = @"Templates\Lates_Request_Form_Template.docx";
            string destinationFile = "form1.docx";

            if (File.Exists(destinationFile))
            {
                File.Delete(destinationFile);
            }
            File.Copy(sourceFile, destinationFile);

            PopulateForm(destinationFile, watchlist);
        }

        private static void PopulateForm(string destinationFile, Watchlist watchlist)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(destinationFile, true))
            {
                PopulateTable(wordDoc, watchlist);
            }
        }

        private static void PopulateTable(WordprocessingDocument wordDoc, Watchlist watchlist)
        {
            PopulateDepartureDate(wordDoc, watchlist.DepartureDate);
            PopulateDepartureAirport(wordDoc, watchlist.DepartureAirport);
            PopulateArrivalAirport(wordDoc, watchlist.ArrivalAirport);
            PopulateDepartureTime(wordDoc, watchlist.DepartureTime);
            PopulateReturnDate(wordDoc, watchlist.ReturnDate);
            PopulateReturnTime(wordDoc, watchlist.ReturnTime);
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
