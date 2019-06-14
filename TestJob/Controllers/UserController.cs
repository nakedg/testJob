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
        public async Task<IActionResult> Get(GetUsersRequest request)
        {
            var users = await _userService.GetUsersAsync(request.Page, request.PageSize, request.Sort, request.Filter);
            return Ok(users);
        }
    }
}