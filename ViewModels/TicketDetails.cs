namespace ComFlight.ViewModels
{
    public class TicketDetails
    {
        public int Id { get; set; }
        public string? LoginUser { get; set; }
        public string DepartureCity { get; set; }
        public string DepartureAirport { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationAirport { get; set; }
        public string Seat { get; set; }
        public string Airline { get; set; }
        public decimal Price { get; set; }
        public TimeSpan FlightDuration { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime DestinationDate { get; set; }

    }
}
