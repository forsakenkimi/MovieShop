using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Genres()
        {
            var genres = await _genreService.GetAllGenres();
            if (genres == null)
            {
                return NotFound(new { errorMessage = "No Genre Found" });
            }
            return Ok(genres);
        }
    }
}
