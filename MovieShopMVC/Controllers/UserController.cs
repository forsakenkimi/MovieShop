using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }
    }
}
