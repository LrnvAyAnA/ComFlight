using System.ComponentModel.DataAnnotations;

namespace ComFlight.ViewModels
{
    public class AirlineModel
    {
		[Required(ErrorMessage = "Не указан аэропорт")]
		public string Name { get; set; }
    }
}
