using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //question how to make route /api/movies only
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Movies(int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovieCard = await _movieService.GetMoviesPagination(pageSize, pageNumber);
            if (pagedMovieCard == null)
            {
                return NotFound(new { errorMessage = "No Movie Found For GenreId" });
            }
            return Ok(pagedMovieCard);
        }


        //Get30HighestRatedMovies()
        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> Get30HighestRatedMovies()
        {
            var movies = await _movieService.Get30HighestRatedMovies();
            if (!movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            return Ok(movies);
        }


        [HttpGet]
        [Route("top-grossing")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.Get30HighestGrossingMovies();
            if (!movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            return Ok(movies);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = "No Movie Found For id" });
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> genres(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovieCard = await _movieService.GetMoviesByGenrePagination(genreId, pageSize, pageNumber);
            if (pagedMovieCard == null)
            {
                return NotFound(new { errorMessage = "No Movie Found For GenreId" });
            }
            return Ok(pagedMovieCard);
        }
        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> reviews(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedReviews = await _movieService.GetReviewsByMoviePagination(id, pageSize, pageNumber);
            if (pagedReviews == null)
            {
                return NotFound(new { errorMessage = "No Review Found For MovieId" });
            }
            return Ok(pagedReviews);
        }
    }
}
