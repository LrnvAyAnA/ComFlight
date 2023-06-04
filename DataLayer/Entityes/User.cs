using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Введите имя")]
        public string? LoginUser { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string? Pass { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public string? Passport { get; set; }

        public DateOnly? Birthday { get; set; }
		[ForeignKey(nameof(Role))]
        [Required]
		public int IdRole { get; set; }
        public Role Role { get; set; }

    }
}
