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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) {
            this._userService = userService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> GetAsync() {
            return Ok("Ok");
        }

        [HttpGet()]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery]Guid userId) {
            var result = await _userService.FindUserByIdAsync(userId);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
