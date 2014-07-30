using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;

namespace LateSeatsMailSender
{
    public class FormGenerator
    {
        public static void GenerateForm()
        {
            string sourceFile = @"Templates\Lates_Request_Form_Template.docx";
            string destinationFile = "form1.docx";

            File.Copy(sourceFile, destinationFile);

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(destinationFile, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                Regex regexText = new Regex("##dep##");
                docText = regexText.Replace(docText, "boo");

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
        }
    }
}
