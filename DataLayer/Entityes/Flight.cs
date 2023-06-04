using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Flight
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Airplane))]
        public int IdAirplane { get; set; }
        public Airplane? Airplane { get; set;}
        [ForeignKey(nameof(Airline_Route))]
        public int IdAirline_Route { get; set; }
        public Airline_Route Airline_Route { get; set; }
        [Required]
        public int freePlaces { get; set; }
        public DateTime DateOfDepature { get; set; }
        public DateTime DateOfDestination { get; set; }
    }
}
