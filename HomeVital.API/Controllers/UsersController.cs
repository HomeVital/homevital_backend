using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    

        [HttpPost("MockLogin")]
        // POST api/user/MockLogin 
        // take in Kennitala and returns the kennitala if it exists in the database
        public async Task<ActionResult<string>> MockLogin([FromBody] RegisterInputModel registerInputModel)
        {   
            if(!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var user_ = await _userService.MockLogin(registerInputModel);
            if (user_ == null)
            {
                return NotFound();
            }
            return Ok(user_);
        }

        [HttpPost("Login")]
        // POST api/user/Login
        // take in Kennitala and password and returns the user id if the user exists in the database
        public async Task<ActionResult<string>> Login([FromBody] RegisterInputModel registerInputModel)
        {
            if(!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var user_ = await _userService.Login(registerInputModel);
            if (user_ == null)
            {
                return NotFound();
            }
            return Ok(user_);
        }
        

    }
}