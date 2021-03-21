using comics_shelf_api.core.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comics_shelf_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ComicsController : ControllerBase
    {
        private readonly IComicsService _comicsService;
        private readonly IPurchaseComicsService _purchaseComicsService;

        public ComicsController(IComicsService comicsService, IPurchaseComicsService purchaseComicsService)
        {
            this._comicsService = comicsService;
            this._purchaseComicsService = purchaseComicsService;
        }

        [HttpGet("avaliable")]
        public async Task<IActionResult> GetAvailableComics([FromQuery] int currentPage = 1)
        {
            var result = await _comicsService.GetAvailableComicsAsync(currentPage);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("byTitle")]
        public async Task<IActionResult> GetComicsByTitle([FromQuery] string title) {
            var result = await _comicsService.GetComicsByTitle(title);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUserComicsAsync([FromQuery] Guid userId)
        {
            var result = await _purchaseComicsService.GetUserComicsAsync(userId);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
