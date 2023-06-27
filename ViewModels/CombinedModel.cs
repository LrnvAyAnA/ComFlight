namespace ComFlight.ViewModels
{
    public class CombinedModel
    {
        public CityModel CityModel { get; set; }
        public AirportModel AirportModel { get; set; }
        public AirlineModel AirlineModel { get; set; }
        public RouteModel RouteModel { get; set; }
        public AirplaneModel AirplaneModel { get; set; }
        public FlightModel FlightModel { get; set; }
        public CombinedModel()
        {
            CityModel = new CityModel();
            AirportModel = new AirportModel();
            AirlineModel = new AirlineModel();
            RouteModel = new RouteModel();
            FlightModel = new FlightModel();
            AirplaneModel = new AirplaneModel();
        }
    }
}
