using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

using HomeVital.Models.Exceptions;
using HomeVital.API.Extensions;

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
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            var user_ = await _userService.MockLogin(registerInputModel);
            if (user_ == null)
            {
                throw new ResourceNotFoundException("User not found with this Kennitala: " + registerInputModel.Kennitala);
            }
            return Ok(user_.Kennitala);
        }


        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] RegisterInputModel registerInputModel)
        {
            var user = await _userService.Login(registerInputModel);
            if (user == null)
            {
                throw new ResourceNotFoundException("User not found with this Kennitala: " + registerInputModel.Kennitala);
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}