using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using HotelManagementSystem.Models;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    public class UserAccessController : Controller
    {
        public IActionResult Login()
        {
            
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("LoginPage");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsersLoginInfo modelUserLogin)
        {
            if (modelUserLogin.Email == "admin@mail.com" &&
                modelUserLogin.Password == "admin")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelUserLogin.Email),
                    new Claim("OtherProperties", "ExampleRole")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }
            ViewData["ValidateMessage"] = "Invalid Credentials";
            return View("LoginPage");
        }
    }
}
