using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Route
    {
        public int Id { get; set; }

        [ForeignKey(nameof(AirportOfDepature))]
        public int IdAirportOfDepature { get; set; } // внешний ключ
        public Airport AirportOfDepature { get; set; }
        [ForeignKey(nameof(AirportOfDestination))]
        public int IdAirportOfDestination { get; set; } // внешний ключ
        public Airport AirportOfDestination { get; set; }

    }
}
