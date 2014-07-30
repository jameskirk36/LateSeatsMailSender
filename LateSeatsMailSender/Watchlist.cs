namespace LateSeatsMailSender
{
    /*
     *  ""departure_airport"" : {
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
     * */

    public class Airport
    {
        public string code;
        public string name;
    }
    public class Flight
    {
        public Airport departure_airport;
        public Airport destination_airport;
    }
    public class Watchlist
    {
        public string name;
        public string email;
        public Flight[] flights;

        public string DepartureAirport
        {
            get { return flights[0].departure_airport.name; }
            private set { } 
        }
    }
}