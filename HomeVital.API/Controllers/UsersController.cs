using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
    

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] string test)
        {
            // Register user
            await Task.Delay(1000);
            
            return Ok("wohoow " + test);
        }

    }
}