using System;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LateSeatsMailSender.Watchlist;

namespace LateSeatsMailSender.Form
{
    public class FormGenerator : DocumentWrapper
    {
        public RequestForm GenerateForm(Flight flight)
        {
            var stream = ReadResourceIntoMemory();

            PopulateForm(stream, flight);

            EnsureStreamIsReadable(stream);
            var formName = CreateFormNameFromDepartureDate(flight);

            return new RequestForm(formName, stream);
        }

        private string CreateFormNameFromDepartureDate(Flight flight)
        {
            return String.Format("request_form_{0}_{1}.docx",
                flight.arrival_date.Date.Day,
                flight.arrival_date.ToString("MMMM"));
        }

        private void EnsureStreamIsReadable(MemoryStream stream)
        {
            stream.Position = 0;
        }

        private MemoryStream ReadResourceIntoMemory()
        {
            return new MemoryStream(Properties.Resources.Lates_Request_Form_Template);
        }

        private void PopulateForm(MemoryStream stream, Flight flight)
        {
            using (var wordDoc = WordprocessingDocument.Open(stream, true))
            {
                PopulateTable(wordDoc, flight);
            }
        }

        private void PopulateTable(WordprocessingDocument wordDoc, Flight flight)
        {
            PopulateDepartureDate(wordDoc, flight.DepartureDate);
            PopulateDepartureAirport(wordDoc, flight.DepartureAirport);
            PopulateArrivalAirport(wordDoc, flight.ArrivalAirport);
            PopulateDepartureTime(wordDoc, flight.DepartureTime);
            PopulateReturnDate(wordDoc, flight.ReturnDate);
            PopulateReturnTime(wordDoc, flight.ReturnTime);
        }

        private void PopulateReturnTime(WordprocessingDocument wordDoc, string returnTime)
        {
            ChangeText(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.ReturnTime, 1), returnTime);
        }

        private void PopulateReturnDate(WordprocessingDocument wordDoc, string returnDate)
        {
            ChangeText(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.ReturnDate, 1), returnDate);
        }

        private void PopulateDepartureTime(WordprocessingDocument wordDoc, string departureTime)
        {
            ChangeText(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.DepartureTime, 1), departureTime);
        }

        private void PopulateArrivalAirport(WordprocessingDocument wordDoc, string arrivalAirport)
        {
            ChangeText(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.ArrivalAirport, 1), arrivalAirport);
        }

        private void PopulateDepartureDate(WordprocessingDocument wordDoc, string departureDate)
        {
            ChangeText(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.DepartureDate, 1), departureDate);
        }

        private void PopulateDepartureAirport(WordprocessingDocument wordDoc, string departureAirport)
        {
            ChangeText(GetTextInCell(wordDoc, (int)CellIndexHelper.Indices.DepartureAirport, 1), departureAirport);
        }

       private void ChangeText(Text cellText, string txt)
        {
            cellText.Text = txt;
        }

        public int CellIndexForObject(object o)
        {
            return 1;
        }
       
    }

    
}
