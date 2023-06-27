using System.ComponentModel.DataAnnotations;

namespace ComFlight.ViewModels
{
    public class CityModel
    {
        [Required(ErrorMessage = "Не указан город")]
        public string? Name { get; set; }
    }
}
