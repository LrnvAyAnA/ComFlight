using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Booking
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Ticket))]
        [Required]
        public int IdTicket { get; set; }
        public Ticket Ticket { get; set; }
    }
}
