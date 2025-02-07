using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        [HttpGet("")]
        // get patient by id
        public async Task<IActionResult> GetPatientById([FromQuery] int id)
        {
            // get patient by id
            await Task.Delay(1000);
            
            return Ok("Patient with id " + id);
        }
        
    }

}