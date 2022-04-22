using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //created by myself
        private async Task<int> GetUserId()
        {
            string tokenEncoded = await HttpContext.GetTokenAsync("access_token");
            var stream = tokenEncoded;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenDecoded = jsonToken as JwtSecurityToken;
            var userId = Convert.ToInt32(tokenDecoded.Claims.First(claim => claim.Type == "nameid").Value);           
            return userId;
        }


        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> Purchases()
        {
            int userId = await GetUserId();
            var purchaseMovieCard = await _userService.PurchaseMovie(userId);
            if (purchaseMovieCard == null)
            {
                return NotFound(new { errorMessage = "No Purchase Found For UserId" });
            }
            return Ok(purchaseMovieCard);
        }

        [HttpGet]
        [Route("purchaseDetails/{movieId:int}")]
        public async Task<IActionResult> PurchasesDetails(int movieId)
        {
            int userId = await GetUserId();
            var purchaseMovieCard = await _userService.PurchaseMovieByMovieId(movieId, userId);
            if (purchaseMovieCard == null)
            {
                return NotFound(new { errorMessage = "No Purchase Found For UserId" });
            }
            return Ok(purchaseMovieCard);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> MovieFavorite(int movieId)
        {
            int userId = await GetUserId();
            var favoriteRequest = new FavoriteRequestModel()
            {
                MovieId = movieId,
                UserId = userId
            };

            var favorite = await _userService.AddFavorite(favoriteRequest);
            return Ok(favorite);
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveMovieFavorite(int movieId)
        {
            int userId = await GetUserId();
            await _userService.RemoveFavorite(userId, movieId);
            return Ok();
        }

        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> IsMovieFavored(int movieId)
        {
            int userId = await GetUserId();
            bool isFavored = await _userService.IsMovieFavored(userId, movieId);
            return Ok(isFavored);
        }

        //[HttpPost]
        //[Route("add-review")]
        //public async Task<IActionResult> MovieReview(string reviewText, int rating, int movieId)
        //{
        //    int userId = await GetUserId();
        //    var reviewRequest = new ReviewRequestModel()
        //    {
        //        MovieId = movieId,
        //        UserId = userId,
        //        ReviewText = reviewText,
        //        Rating = rating,
        //    };
        //    var review = await _userService.AddMovieReview(reviewRequest);
        //    return Ok(review);
        //}

        [HttpDelete]
        [Route("delete-review/{moviedId:int}")]
        public async Task<IActionResult> DeleteMovieReview(int movieId)
        {
            int userId = await GetUserId();
            await _userService.DeleteMovieReview(userId, movieId);
            return Ok();
        }

        //[HttpPut]
        //[Route("edit-review}")]
        //public async Task<IActionResult> UpdateMovieReview(string reviewText, int rating, int movieId)
        //{
        //    int userId = await GetUserId();
        //    var reviewRequest = new ReviewRequestModel()
        //    {
        //        MovieId = movieId,
        //        UserId = userId,
        //        ReviewText = reviewText,
        //        Rating = rating,
        //    };
        //    var review = await _userService.UpdateMovieReview(reviewRequest);
        //    return Ok(review);
        //}

        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> IsMoviePurchased(int movieId)
        {
            var userId = await GetUserId();
            var isPurchased = await _userService.IsMoviePurchased(userId, movieId);
            return Ok(isPurchased);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> Favorites()
        {
            int userId = await GetUserId();
            var movieCards = await _userService.GetAllFavoritesMovieCard(userId);
            if (movieCards == null)
            {
                return NotFound(new { errorMessage = "No Favorite Movies Found For UserId" });
            }
            return Ok(movieCards);
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> Reviews()
        {
            int userId = await GetUserId();
            var reviews = await _userService.GetAllReviewsByUserId(userId);
            if (reviews == null)
            {
                return NotFound(new { errorMessage = "No Reviews Found For UserId" });
            }
            return Ok(reviews);
        }
    }
}
