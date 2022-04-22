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
        public async Task<IActionResult> MovieFavorite(int movieId, int userId)
        {
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
        public async Task<IActionResult> RemoveMovieFavorite(int movieId, int userId)
        {
            await _userService.RemoveFavorite(userId, movieId);
            return Ok();
        }
    }
}
