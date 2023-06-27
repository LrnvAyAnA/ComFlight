using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Mvc;

namespace ComFlight.Controllers
{
    public class UserController : Controller
    {
        private Context db;

        public UserController(Context context)
        {
            db = context;
        }
        public IActionResult GetName()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                User user = db.Users.FirstOrDefault(u => u.LoginUser == userName);

                return Json(user.Name);
            }
            else
            {
                return Json(null);
            }
        }
        public bool GetStateInfo()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                User user = db.Users.FirstOrDefault(u => u.LoginUser == userName);
                if(user.Passport!=null)
                    return true;
                else
                    return false;
            }
            return false;
        }

    }
}
