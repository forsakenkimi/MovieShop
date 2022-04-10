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
        public IActionResult Details(int id)
        {
            var movieDetails = _movieService.GetMovieDetails(1); //need to be fixed 3 for test only
            return View(movieDetails);
        }
    }
}
