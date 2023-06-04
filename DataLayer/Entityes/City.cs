using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
