using System.ComponentModel.DataAnnotations;
namespace ComFlight.ViewModels
{
    public class AirportModel
    {
        [Required(ErrorMessage = "Не указан аэропорт")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан город")]

        public string City { get; set; }
    }
}
