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
            PopulateDepartureAirport(wordDoc, watchlist.DepartureAirport);
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
