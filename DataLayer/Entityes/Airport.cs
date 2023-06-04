using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Airport
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [ForeignKey(nameof(City))]
        [Required]
        public int IdCity { get; set; }
        public City City { get; set; }
    }
}
