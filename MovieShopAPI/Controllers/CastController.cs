﻿using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var castDetails = await _castService.GetCastDetails(id);
            if (castDetails == null)
            {
                return NotFound(new { errorMessage = "No Cast Found" });
            }
            return Ok(castDetails);
        }
    }
}
