using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

using HomeVital.Services.Interfaces;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterInputModel registerInputModel)
        {
            if (!ModelState.IsValid)
            {
                // breyta þessu, nota custom exceptions
                throw new Exception("Not valid input model");
            }

            UserDto createdUser = await _userService.Register(registerInputModel);

            return Ok(createdUser);

        }
    }
}