using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("movie")]
        //half done 
        //not sure how to add genre
        public async Task<IActionResult> AddMovieByAdmin(MovieCreateRequestModel model)
        {
            var user = await _adminService.AddMovieByAdmin(model);
            return Ok(user);
        }

        [HttpPut]
        [Route("movie")]
        //half done 
        //not sure how to add genre
        public async Task<IActionResult> UpdateMovieByAdmin(MovieCreateRequestModel model)
        {
            var user = await _adminService.UpdateMovieByAdmin(model);
            return Ok(user);
        }

        [HttpGet]
        [Route("top-purchased-movies")]
        public async Task<IActionResult> GetTopPurchases([FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null, [FromQuery] int pageSize = 30, [FromQuery] int pageIndex = 1)
        {
            IEnumerable<MoviesReportModel> Movies = await _adminService.GetTopPurchasedMovies(fromDate, toDate, pageSize, pageIndex);
            if (Movies == null)
            {
                return NotFound(new { errorMessage = "No Cast Found" });
            }
            return Ok(Movies);
        }
    }
}
