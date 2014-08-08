using System;

namespace LateSeatsMailSender.Watchlist
{
    public class Flight
    {
        public Airport departure_airport;
        public Airport destination_airport;
        public DateTime departure_date;
        public DateTime arrival_date;

        public string DepartureAirport
        {
            get { return departure_airport.name; }
            private set { }
        }

        public string DepartureDate
        {
            get { return arrival_date.ToString("dd MMMM, yyyy"); }
            private set { }
        }

        public string ArrivalAirport
        {
            get { return destination_airport.name; }
            private set { }
        }

        public string DepartureTime
        {
            get { return arrival_date.TimeOfDay.ToString(); }
            private set { }
        }

        public string ReturnDate
        {
            get { return departure_date.ToString("dd MMMM, yyyy"); }
            private set { }
        }

        public string ReturnTime
        {
            get { return departure_date.TimeOfDay.ToString(); }
            private set { }
        }
    }
}