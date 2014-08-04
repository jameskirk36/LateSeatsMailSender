using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace LateSeatsMailSender
{
    public class DocumentWrapper
    {
        public static Text GetTextInCell(WordprocessingDocument wordDoc, int rowIndex, int colIndex)
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
            return t;
        }
    }
}