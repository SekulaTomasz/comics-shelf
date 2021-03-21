using comics_shelf_api.core.Dtos;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) {
            this._userService = userService;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery]Guid userId) {
            var result = await _userService.FindUserByIdAsync(userId);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getByLogin")]
        public async Task<IActionResult> GetUserByLogin([FromQuery] string userName)
        {
            var result = await _userService.FindUserByLoginAsync(userName);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDto userDto)
        {
            var result = await _userService.LoginUserAsync(userDto);
            if (result.StatusCode == System.Net.HttpStatusCode.OK || result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserLoginDto userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            if (result.StatusCode == System.Net.HttpStatusCode.OK || result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
