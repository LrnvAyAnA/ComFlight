using System.ComponentModel.DataAnnotations;


namespace ComFlight.ViewModels
{
    public class SearchModel
    {
        [Required(ErrorMessage = "Укажите пункт отправления")]

        public string From { get; set; }

        [Required(ErrorMessage = "Укажите пункт прибытия")]
        public string To { get; set; }

        [Required(ErrorMessage ="Укажите дату отправки")]
        public DateOnly Date { get; set; }
    }
}
