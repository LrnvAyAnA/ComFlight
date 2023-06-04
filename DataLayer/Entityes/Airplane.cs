using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Airplane
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Model { get; set; }
        [Required]
        public int Seats { get; set; }
        public DateOnly DateOfManufacture { get; set; }
    }
}
