using ComFlight.Helpers;
using ComFlight.ViewModels;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ComFlight.Controllers
{
    public class Profile : Controller
    {
        private Context db;

        public Profile(Context context)
        {
            db = context;
        }
        [HttpGet]

        public IActionResult Index()=>View();
        [HttpGet]
        public IActionResult AdminAccount()=>View();

		[HttpGet]
		public IActionResult UserAccount() => View();

		[HttpGet]
        public IActionResult SignUp()=>View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.LoginUser == model.Login);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(new User { Name = model.Name, LoginUser = model.Login, Pass = HashPasswordHelper.HashPassword(model.Password), IdRole = db.Roles.FirstOrDefault(r => r.Name == "Пользователь").Id});
                    await db.SaveChangesAsync();
                    user = db.Users.FirstOrDefault(u => u.LoginUser == model.Login);
                    var role = await db.Roles.FindAsync(user.IdRole);
                    await Authenticate(user.Name, role.Name); // аутентификация

                    return RedirectToAction("Index", "Profile");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            User user = await db.Users.FirstOrDefaultAsync(u => u.LoginUser == model.Email && u.Pass == HashPasswordHelper.HashPassword(model.Password));

            if (user != null)
            {
                var role = await db.Roles.FindAsync(user.IdRole);
                await Authenticate(user.LoginUser, role.Name); // аутентификация
                return RedirectToAction("Index", "Profile");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View(model);
        }

        private async Task Authenticate(string? userName,string role)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
				new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, userName)
			};
            // создаем объект ClaimsIdentity
            var id = new ClaimsIdentity(claims, "ApplicationCookie");
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Profile");
        }


    }
}
