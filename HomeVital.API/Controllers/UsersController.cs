using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
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
            return Ok(user_.Kennitala);
        }

        // [HttpPost("Login")]
        // // POST api/user/Login
        // // take in Kennitala and password and returns the user id if the user exists in the database
        // public async Task<ActionResult<string>> Login([FromBody] RegisterInputModel registerInputModel)
        // {
        //     if(!ModelState.IsValid)
        //     {
        //         throw new System.ArgumentException("Invalid input model");
        //     }
        //     var user_ = await _userService.Login(registerInputModel);
        //     if (user_ == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(user_.Id);
        // }
        
        // [HttpPost("Login")]
        // public async Task<ActionResult<string>> Login([FromBody] RegisterInputModel registerInputModel)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         throw new System.ArgumentException("Invalid input model");
        //     }
        //     var user_ = await _userService.Login(registerInputModel);
        //     if (user_ == null)
        //     {
        //         return NotFound();
        //     }
        //     var token = _tokenService.GenerateToken(user_.Kennitala, user_.Roles?? new string[0]);
        //     return Ok(token);
        // }

        [HttpGet("GetPatientData")]
        [Authorize(Roles = "Patient")]
        public IActionResult GetAdminData()
        {
            // Only accessible by users with the "Admin" role
            return Ok("This is admin data.");
        }

        [HttpGet("GetHealthcareWorkerData")]
        [Authorize(Roles = "HealthcareWorker, Patient")]
        public IActionResult GetUserData()
        {
            // Only accessible by users with the "User" role
            return Ok("This is HealthcareWorker data.");
        }

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] RegisterInputModel registerInputModel)
        {
            var user = await _userService.Login(registerInputModel);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}