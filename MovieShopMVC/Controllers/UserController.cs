using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
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
            var purchaseMovieCard = await _userService.PurchaseMovie(userId);
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
        public async Task<IActionResult> MoviePurchase(int userId, int movieId, string purchaseNumber, decimal totalPrice,
            DateTime purchaseDateTime)
        {
            var purchaseRequest = new PurchaseRequestModel()
            {
                UserId = userId,
                PurchaseDateTime = purchaseDateTime,
                PurchaseNumber = purchaseNumber,
                TotalPrice = totalPrice,
                MovieId = movieId,
            };
            int id = userId;
            var purchase = await _userService.PurchaseMovie(purchaseRequest, id);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        [HttpPost]
        public async Task<IActionResult> MovieFavorite(int movieId, int userId)
        {
            var favoriteRequest = new FavoriteRequestModel()
            {
                MovieId = movieId,
                UserId = userId
            };

            var favorite = await _userService.AddFavorite(favoriteRequest);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMovieFavorite(int movieId, int userId)
        {
            await _userService.RemoveFavorite(userId, movieId);
            return RedirectToAction("Details", "Movies", new { id = movieId });
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

        [HttpPost]
        public async Task<IActionResult> DeleteMovieReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovieReview(string reviewText, int rating, int userId, int movieId)
        {
            var reviewRequest = new ReviewRequestModel()
            {
                MovieId = movieId,
                UserId = userId,
                ReviewText = reviewText,
                Rating = rating,
            };
            var review = await _userService.UpdateMovieReview(reviewRequest);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        public async Task<IActionResult> TwoSubmitsForm(string reviewText, int rating, int userId, int movieId, string command)
        {
            if(command == "Delete")
            {
                await DeleteMovieReview(userId, movieId);

            }
            else
            {
                await UpdateMovieReview(reviewText, rating, userId, movieId);
            }

            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

    }
}
