using ComFlight.ViewModels;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ComFlight.Controllers
{
    public class SearchTicket : Controller
    {
        private readonly Context _context;

        public SearchTicket(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetCities(string searchTerm)
        {
            var cities = _context.Cities
                .Where(c => c.Name.Contains(searchTerm))
                .Select(c => c.Name)
                .Take(10) // Ограничьте подсказок до 10
                .ToList();
            return Json(cities);
        }
        public ActionResult GetAirports(string searchTerm)
        {
            var airportResults = (from a in _context.Airports
                                 join c in _context.Cities on a.IdCity equals c.Id
                                 where a.Name.Contains(searchTerm)
                                 select new CombinedModel
                                 {
                                     AirportModel = new AirportModel
                                     {
                                         Name = a.Name,
                                         City = c.Name
                                     }
                                 }).ToList();
            return Json(airportResults);
        }
        public async Task<ActionResult> GetPlanes()
        {
            var planeResults = await _context.Airplanes.ToListAsync();
            return Json(planeResults);
        }
        public async Task<ActionResult> GetAirlines()
        {
            var planeResults = await _context.Airlines.ToListAsync();
            return Json(planeResults);
        }
        public async Task<ActionResult> GetRoutes()
        {
            var routes = new List<string>();
            var planeResults = await _context.Routes.ToListAsync();
            foreach (var route in planeResults)
            {
                Airport airportF = _context.Airports.FirstOrDefault(a => a.Id == route.IdAirportOfDepature);
                Airport airportT = _context.Airports.FirstOrDefault(a => a.Id == route.IdAirportOfDestination);

                routes.Add(airportF.Name+"_"+airportT.Name);
            }
            return Json(routes);
        }
        public ActionResult Search(string From, string To, DateTime date)
        {
            var ticketDetails = (from ticket in _context.Tickets
                                 join flight in _context.Flights on ticket.IdFlight equals flight.Id
                                 join airlineRoute in _context.Airline_Routes on flight.IdAirline_Route equals airlineRoute.Id
                                 join airline in _context.Airlines on airlineRoute.IdAirline equals airline.Id
                                 join route in _context.Routes on airlineRoute.IdRoute equals route.Id
                                 join airportDeparture in _context.Airports on route.IdAirportOfDepature equals airportDeparture.Id
                                 join airportDestination in _context.Airports on route.IdAirportOfDestination equals airportDestination.Id
                                 join cityDeparture in _context.Cities on airportDeparture.IdCity equals cityDeparture.Id
                                 join cityDestination in _context.Cities on airportDestination.IdCity equals cityDestination.Id
                                 where cityDeparture.Name == From
                                       && cityDestination.Name == To
                                       && flight.DateOfDepature.Date == date.Date
                                       && ticket.LoginUser==null
                                 select new TicketDetails
                                 {
                                     DepartureCity = cityDeparture.Name,
                                     DepartureAirport = airportDeparture.Name,
                                     DestinationCity = cityDestination.Name,
                                     DestinationAirport = airportDestination.Name,
                                     FlightDuration = airlineRoute.FlightDuration,
                                     Price = ticket.TotalPrice,
                                     Seat = ticket.Seat,
                                     Airline = airline.Name,
                                     DepartureDate = flight.DateOfDepature,
                                     DestinationDate = flight.DateOfDestination
                                 }).ToList();

            return Json(ticketDetails);
        }

        public ActionResult SortByPrice(string ticketList)
        {
            List<TicketDetails> tickets = ParseTicketList(ticketList);
            tickets = tickets.OrderBy(t => t.Price).ToList(); // Сортировка по цене от меньшего к большему
            return Json(tickets);
        }
        private List<TicketDetails> ParseTicketList(string ticketList)
        {
            List<TicketDetails> tickets = new List<TicketDetails>();
            string pattern = "<div class=\"ticket\">.*?<div class=\"header\">(.*?)<\\/div>.*?<p>From:\\s*(.*?)<\\/p>.*?<p>To:\\s*(.*?)<\\/p>.*?<p>Price:\\s*(.*?)<\\/p>.*?<\\/div>";
            MatchCollection matches = Regex.Matches(ticketList, pattern, RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                TicketDetails ticket = new TicketDetails();

                ticket.Airline = match.Groups[1].Value.Trim();
                ticket.DepartureAirport = match.Groups[2].Value.Trim();
                ticket.DestinationAirport = match.Groups[3].Value.Trim();
                ticket.Price = decimal.Parse(match.Groups[4].Value.Trim());

                tickets.Add(ticket);
            }
            return tickets;
        }
        //private List<Ticket> GetTickets(string fromCity, string toCity, DateTime date)
        //{
        //    // Здесь выполняется запрос к базе данных или другой источник данных
        //    // для получения билетов на основе введенных параметров

        //    // Пример:
        //    var tickets = _context.Tickets
        //        .Where(t => t. == fromCity && t.ToCity == toCity && t.Date == date)
        //        .ToList();

        //    return tickets;
        //}
    }
}
