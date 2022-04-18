using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    //[Authorize]

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //created by myself
        //private int GetUserId() {
        //    var userId = Convert.ToInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    return userId;
        //}

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            //int userId = GetUserId();
            var purchaseMovieCard = await _userService.PurchaseMovie(15643);
            return View(purchaseMovieCard);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            //int userId = GetUserId();
            int userId = 1;
            var movieCards = _userService.GetAllFavoritesMovieCard(userId);
            return View(movieCards);
        }
    }
}
