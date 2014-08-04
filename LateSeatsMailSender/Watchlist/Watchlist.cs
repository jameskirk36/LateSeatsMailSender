namespace LateSeatsMailSender.Watchlist
{
    public class Watchlist
    {
        public string name;
        public string email;
        public Flight[] flights;

        public Flight FirstFlight
        {
            get
            {
                return flights[0];
            }
        }
    }
}