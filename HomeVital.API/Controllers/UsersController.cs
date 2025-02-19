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
        public ActionResult<string> MockLogin([FromBody] string kennitala)
        {   
            var user = _userService.MockLogin(new RegisterInputModel { Kennitala = kennitala });
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        

    }
}