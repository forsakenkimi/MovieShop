using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
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
        private int GetUserId()
        {
            var userId = Convert.ToInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userId;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            int userId = GetUserId();
            var purchaseMovieCard = await _userService.PurchaseMovie(42);
            return View(purchaseMovieCard);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            //int userId = GetUserId();
            int userId = 1;
            var movieCards = await _userService.GetAllFavoritesMovieCard(userId);
            return View(movieCards);
        }

        [HttpPost]
        public async Task<IActionResult> MoviePurchase(PurchaseRequestModel purchaseRequest)
        {
            int id = purchaseRequest.UserId;
            var purchase = await _userService.PurchaseMovie(purchaseRequest, id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MovieFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _userService.AddFavorite(favoriteRequest);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MovieReview(string reviewText, int rating, int userId, int movieId)
        {
            var reviewRequest = new ReviewRequestModel()
            {
                MovieId = movieId,
                UserId = userId,
                ReviewText = reviewText,
                Rating = rating,
            };
            var review = await _userService.AddMovieReview(reviewRequest);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

    }
}
