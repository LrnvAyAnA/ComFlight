using ComFlight.Helpers;
using ComFlight.ViewModels;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComFlight.Controllers
{
    public class AdminController : Controller
    {
        private Context db;

        public AdminController(Context context )
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Add()=>View();
        public IActionResult Delete() => View();
        public IActionResult Update() => View();
        public IActionResult Read() => View();

        [HttpPost]
        public  IActionResult AddCity([FromBody] CombinedModel model)
        {
            City city =  db.Cities.FirstOrDefault(u => u.Name == model.CityModel.Name); ;
            if (model.CityModel.Name!=null)
            {
                if(city == null)
                {
                    db.Cities.Add(new City { Name=model.CityModel.Name});
                     db.SaveChanges();
                    return Ok((new { message = "Город успешно добавлен в базу данных." }));
                }
                else
                {
                    return BadRequest("Город с таким названием уже существует.");
                }                
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult AddAirport([FromBody] CombinedModel model)
        {
            
            if (model.AirportModel.Name!=null&&model.AirportModel.City!=null)
            {
                City city = db.Cities.FirstOrDefault(u => u.Name == model.AirportModel.City); ;
                Airport airport = db.Airports.FirstOrDefault(u => u.Name == model.AirportModel.Name); ;
                if(city == null)
                {
                    return BadRequest("Города с таким названием не существует.");
                }
                else
                {
                    if (airport == null)
                    {
                        db.Airports.Add(new Airport { Name = model.AirportModel.Name, IdCity = city.Id });
                        db.SaveChanges();
                        return Ok((new { message = "Аэропорт успешно добавлен в базу данных." }));
                    }
                    else
                    {
                        return BadRequest("Аэропорт с таким названием уже существует.");
                    }
                }
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult DeleteAirport([FromBody] CombinedModel model)
        {
            
            if(model.AirportModel.Name != null && model.AirportModel.City != null){
                var airport = db.Airports
            .Include(a => a.City)
            .SingleOrDefault(a => a.Name == model.AirportModel.Name && a.City.Name == model.AirportModel.City);
                if (airport != null)
                {
                    db.Airports.Remove(airport);
                    db.SaveChanges();
                    return Ok((new { message = "Аэропорт успешно удален из базы данных." }));
                }
                else
                {
                    return BadRequest("Такого аэропорта в данном городе не существует.");
                }
            }           
            return Ok();
        }

        [HttpPost]
        public IActionResult AddAirline([FromBody] CombinedModel model)
        {

            if (model.AirlineModel.Name != null)
            {
                Airline airline = db.Airlines.FirstOrDefault(u => u.Name == model.AirlineModel.Name); ;
                if (airline == null)
                {
                    db.Airlines.Add(new Airline { Name = model.AirlineModel.Name});
                    db.SaveChanges();
                    return Ok((new { message = "Авиакомпания успешно добавлена в базу данных." }));
                }
                else
                {
                    return BadRequest("Авиакомпания с таким названием уже существует.");
                }
            }
            return Ok();
        }
        public IActionResult AddRoute([FromBody] CombinedModel model)
        {

            if (model.RouteModel.AirportFrom != null && model.RouteModel.AirportTo != null)
            {
                var airportFrom = model.RouteModel.AirportFrom.Split(',')[0].Trim();
                var cityFrom = model.RouteModel.AirportFrom.Split(',')[1].Trim();
                var airportTo = model.RouteModel.AirportTo.Split(',')[0].Trim();
                var cityTo = model.RouteModel.AirportTo.Split(',')[1].Trim();
                var cityF = db.Cities.FirstOrDefault(u => u.Name == cityFrom); ;
                var cityT = db.Cities.FirstOrDefault(u => u.Name == cityTo); ;
                var From = db.Airports.FirstOrDefault(u => u.Name == airportFrom && u.IdCity==cityF.Id); ;
                var To = db.Airports.FirstOrDefault(u => u.Name == airportTo && u.IdCity==cityT.Id); ;
                DataLayer.Entityes.Route route = db.Routes.FirstOrDefault(u => u.IdAirportOfDepature==From.Id && u.IdAirportOfDestination==To.Id); ;
                if (route == null)
                {
                    db.Routes.Add(new DataLayer.Entityes.Route { IdAirportOfDepature=From.Id,IdAirportOfDestination=To.Id });
                    db.SaveChanges();
                    return Ok((new { message = "Маршрут успешно добавлен в базу данных." }));
                }
                else
                {
                    return BadRequest("Такой маршрут уже существует.");
                }
            }
            return Ok();
        }
        public IActionResult AddFlight([FromBody] CombinedModel model)
        {
            if (model.FlightModel.Plane != null && model.FlightModel.dateOfDestination != null && model.FlightModel.dateOfDeparture != null && 
                model.FlightModel.Airline != null && model.FlightModel.Route != null)
            {
                int price;
                if (int.TryParse(model.FlightModel.Price, out int p))
                {
                    price = p;
                }
                else
                {
                    return BadRequest("Некорректная цена");
                }
                var airplane = model.FlightModel.Plane;
                var airportFrom = model.FlightModel.Route.Split('_')[0].Trim();
                var airportTo = model.FlightModel.Route.Split('_')[1].Trim();
                DateTime dateFrom = DateTime.Parse(model.FlightModel.dateOfDeparture);
                DateTime dateTo = DateTime.Parse(model.FlightModel.dateOfDestination);
                TimeSpan duration = dateTo - dateFrom;
                DataLayer.Entityes.Route route = db.Routes.FirstOrDefault(u => u.AirportOfDepature.Name == airportFrom && u.AirportOfDestination.Name==airportTo);
                Airplane plane = db.Airplanes.FirstOrDefault(u => u.Model == airplane);
                Airline airline = db.Airlines.FirstOrDefault(u => u.Name == model.FlightModel.Airline);
                Airline_Route airline_Route = db.Airline_Routes.FirstOrDefault(u => u.IdAirline == airline.Id && u.IdRoute == route.Id);
                if (airline_Route == null)
                {
                    db.Airline_Routes.Add(new Airline_Route { IdAirline = airline.Id, IdRoute = route.Id,FlightDuration= duration});
                    db.SaveChanges();
                    airline_Route = db.Airline_Routes.FirstOrDefault(u => u.IdAirline == airline.Id && u.IdRoute == route.Id);
                }
                Flight flight = db.Flights.FirstOrDefault(u=>u.IdAirplane == plane.Id && u.IdAirline_Route == airline_Route.Id && u.freePlaces == plane.Seats && u.DateOfDepature == dateFrom && u.DateOfDestination == dateTo);
                if (flight == null)
                {
                    db.Flights.Add(new DataLayer.Entityes.Flight { IdAirplane = plane.Id, IdAirline_Route = airline_Route.Id, freePlaces = plane.Seats, DateOfDepature = dateFrom, DateOfDestination = dateTo,Price=price });
                    db.SaveChanges();
                    GenerateTickets(db.Flights.FirstOrDefault(u => u.IdAirplane == plane.Id && u.IdAirline_Route == airline_Route.Id && u.freePlaces == plane.Seats && u.DateOfDepature == dateFrom && u.DateOfDestination == dateTo));
                    return Ok((new { message = "Рейс успешно добавлен в базу данных." }));
                }
                else
                {
                    return BadRequest("Такой рейс уже существует.");
                }
            }
            return Ok();
        }
        public void GenerateTickets(Flight flight)
        {
            string seat;
            int seatsPerRow = (int)Math.Ceiling((double)flight.Airplane.Seats / flight.Airplane.numOfRow);
            DataLayer.Entityes.Klass p_class = db.Klasses.FirstOrDefault(u=>u.Name == "Премиум-класс");
            DataLayer.Entityes.Klass b_class = db.Klasses.FirstOrDefault(u=>u.Name == "Бизнес-класс");
            DataLayer.Entityes.Klass e_class = db.Klasses.FirstOrDefault(u=>u.Name == "Эконом-класс");
            int economic =Convert.ToInt32(flight.Airplane.Seats * 0.8);
            int business = Convert.ToInt32(flight.Airplane.Seats * 0.15);
            int premium = flight.Airplane.Seats-economic-business;
            int forE = (int)Math.Ceiling((double)economic / seatsPerRow);
            int forB = (int)Math.Ceiling((double)business / seatsPerRow);
            int forP = (int)Math.Ceiling((double)premium / seatsPerRow);
            char letter = 'A';
            int count = flight.Airplane.Seats;
            for (int j = 0; j < forP; j++)
            {
                if (premium <= 0)
                {
                    break;
                }
                for (int i = 1; i <= seatsPerRow; i++)
                {
                    if (premium <= 0)
                    {
                        break;
                    }
                    db.Tickets.Add(new Ticket { IdClass = p_class.Id, Seat = $"{letter}{i}", IdFlight = flight.Id, TotalPrice = flight.Price + p_class.Price });
                    premium--;                    
                }
                letter = (char)(letter + 1);
            }
            letter = (char)(letter + 1);

            for (int j = 0; j < forB; j++)
            {
                if (business <= 0)
                {
                    break;
                }
                for (int i = 1; i <= seatsPerRow; i++)
                {
                    if (business <= 0)
                    {
                        break;
                    }
                    db.Tickets.Add(new Ticket { IdClass = b_class.Id, Seat = $"{letter}{i}", IdFlight = flight.Id, TotalPrice = flight.Price + b_class.Price });
                    business--;
                }
                letter = (char)(letter + 1);
            }
            letter = (char)(letter + 1);

            for (int j = 0; j < forE; j++)
            {
                if (economic <= 0)
                {
                    break;
                }
                for (int i = 1; i <= seatsPerRow; i++)
                {
                    if (economic <= 0)
                    {
                        break;
                    }
                    db.Tickets.Add(new Ticket { IdClass = e_class.Id, Seat = $"{letter}{i}", IdFlight = flight.Id, TotalPrice = flight.Price + e_class.Price });
                    economic--;
                }
                letter = (char)(letter + 1);
            }
            db.SaveChanges();
        }
        public IActionResult AddPlane([FromBody] CombinedModel model)
        {
            if (model.AirplaneModel.Model != null && model.AirplaneModel.Seats != null && model.AirplaneModel.DateOfManufacture != null)
            {
                int Seats,Rows;
                var Model = model.AirplaneModel.Model;
                if(int.TryParse(model.AirplaneModel.Seats, out int parsedNumber)&& int.TryParse(model.AirplaneModel.numOfRow, out int rows))
                {
                    Seats = parsedNumber;
                    Rows = rows;
                }
                else
                {
                    return BadRequest("Введено неверное значение. Пожалуйста, введите число.");
                }
                var datetime = DateTime.Parse(model.AirplaneModel.DateOfManufacture);
                DateOnly date = new DateOnly(datetime.Year, datetime.Month, datetime.Day);
                DataLayer.Entityes.Airplane plane = db.Airplanes.FirstOrDefault(u => u.Model == model.AirplaneModel.Model&& u.DateOfManufacture==date);
                if(plane == null)
                {
                    db.Airplanes.Add(new DataLayer.Entityes.Airplane { Model = Model, DateOfManufacture = date, Seats = Seats,numOfRow=Rows});
                    db.SaveChanges();
                    return Ok(new { message = "Самолет успешно добавлен в базу данных." });
                }
                else
                {
                    return BadRequest("Такой самолет уже существует.");
                }
            }
            return Ok();
        }

        public IActionResult UpdateAirline(string curName,[FromBody] CombinedModel model)
        {
            return Ok(new { message = "Авиакомпания успешно отредактирована." });
        }
        public JsonResult GetAirplanesData()
        {
            List<Airplane> airplanes = db.Airplanes.ToList();
            return Json(airplanes);
        }



    }
}
