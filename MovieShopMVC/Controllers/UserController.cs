using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]

    public class UserController : Controller
    {
        //created by myself
        private int GetUserId() {
            var userId = Convert.ToInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            int userId = GetUserId();
            System.Diagnostics.Debug.WriteLine(userId);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            int userId = GetUserId();
            return View();
        }
    }
}
