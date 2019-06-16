using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestJob.Core.Data;
using TestJob.Core.Requests;

namespace TestJob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Core.Services.IUserService _userService;

        public UserController(Core.Services.IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetUsersRequest request)
        {
            await Task.Delay(500);
            var users = await _userService.GetUsersAsync(request.Page, request.PageSize, request.Sort, request.Filter);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Delay(500);
            var user = await _userService.GetUserAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserEntity user)
        {
            return Ok(await _userService.SaveAsync(user));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserEntity user)
        {
            return Ok(await _userService.SaveAsync(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _userService.DeleteAsync(id));
        }
    }
}