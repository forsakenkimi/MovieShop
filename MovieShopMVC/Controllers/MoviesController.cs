using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        //localhost//movies/detail/Id
        private readonly IMovieService _movieService;
        //we are going to use methods in movieService hence DI here
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);
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
