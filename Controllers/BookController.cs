using Microsoft.AspNetCore.Mvc;

namespace ComFlight.Controllers
{
    public class BookController : Controller
    {

        public IActionResult Booking() => View();
        [HttpGet]
        public IActionResult Index() => View();


        [HttpPost("/Book/BookTicket")]
        public IActionResult BookTicket()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "User is not authenticated" });
            }

            // Логика бронирования билета

            return Ok(new { message = "Ticket booked successfully" });
        }

    }
}
