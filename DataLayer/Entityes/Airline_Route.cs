using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Airline_Route
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Airline))]
        public int IdAirline { get; set; }
        public Airline Airline { get; set; }
        [ForeignKey(nameof(Route))]
        public int IdRoute { get; set; }
        public Route Route { get; set; }
        public TimeSpan FlightDuration { get; set; }
    }
}
