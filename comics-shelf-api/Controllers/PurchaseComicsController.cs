using comics_shelf_api.core.Dtos;
using comics_shelf_api.core.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace comics_shelf_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
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

        [HttpGet("download")]
        public async Task<FileStreamResult> Download()
        {
            var path = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "Static/Zadanie rekrutacyjne na stanowisko Fullstack.pdf");
            FileStream uploadFileStream = System.IO.File.OpenRead(path);
            return File(uploadFileStream, "application/octet-stream");
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
