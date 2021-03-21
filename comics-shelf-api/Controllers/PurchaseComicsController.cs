using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Services.Interfaces;
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
    public class PurchaseComicsController : ControllerBase
    {

        private readonly IPurchaseComicsService _purchaseComicsService;

        public PurchaseComicsController(IPurchaseComicsService purchaseComicsService) {
            this._purchaseComicsService = purchaseComicsService;
        }

        [HttpGet("canUserDownload")]
        public async Task<IActionResult> CanUserDownloadFileAsync([FromQuery] Guid userId, Guid comicsId) {
            var result = await _purchaseComicsService.CanUserDownloadFileAsync(userId, comicsId);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("user/comics")]
        public async Task<IActionResult> GetUserComicsAsync([FromQuery] Guid userId)
        {
            var result = await _purchaseComicsService.GetUserComicsAsync(userId);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseComicsAsync([FromBody] PurchaseComicsDto purchaseComicsDto)
        {
            var result = await _purchaseComicsService.PurchaseComicsAsync(purchaseComicsDto.UserId, purchaseComicsDto.ExternalProviderComicsDto);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnComicsAsync([FromBody] ReturnComicsRequestDto req)
        {
            var result = await _purchaseComicsService.ReturnComicsAsync(req.UserId, req.ComicsId);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
