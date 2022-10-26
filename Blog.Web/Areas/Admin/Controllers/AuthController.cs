using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet] //Login ekranını ilk açtığımızda karşımıza çıkan ekran HttpGet ile işaretlenir.
        public IActionResult Login()
        {
            return View();
        }
    }
}
