using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    
    public class MoviesController : Controller
    {
        //localhost//movies/detail/Id
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        //we are going to use methods in movieService hence DI here
        public MoviesController(IMovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }


        [HttpGet]       
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);   
            if (this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
            }else
            {
                int userId = Convert.ToInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                ViewData["userId"] = userId;
                ViewData["movieIds"] = await _userService.GetAllPurchasesMovieId(userId);
                
            }
             
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> genres(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovieCard = await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber);
            return View(pagedMovieCard);
        }
    }
}
