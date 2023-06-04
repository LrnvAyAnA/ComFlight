using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Ticket
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public string? LoginUser { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Flight))]
        [Required]
        public int IdFlight { get; set; }
        public Flight Flight { get; set; }
        [Required]
        public string? Seat { get; set; }
        [ForeignKey(nameof(Class))]
        public int IdClass { get; set; }
        public Klass? Class { get; set; }
        public int TotalPrice { get; set; }
    }
}
