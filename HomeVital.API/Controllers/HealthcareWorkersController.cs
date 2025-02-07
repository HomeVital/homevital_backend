using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/healthcareworkers")]
    public class HealthcareWorkersController : ControllerBase
    {
        [HttpGet("")]
        // get healthcare worker by id
        public async Task<IActionResult> GetHealthcareWorkerById([FromQuery] int id)
        {
            // get healthcare worker by id
            await Task.Delay(1000);
            
            return Ok("Healthcare worker with id " + id);
        }

        // create healthcare worker
        [HttpPost("create")]
        public async Task<IActionResult> CreateHealthcareWorker([FromBody] string test)
        {
            // create healthcare worker
            await Task.Delay(1000);
            
            return Ok("Testing : Healthcare worker created " + test);
        }

        // update healthcare worker
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateHealthcareWorker([FromBody] string test)
        {
            // update healthcare worker
            await Task.Delay(1000);
            
            return Ok("Testing : Healthcare worker updated " + test);
        }


    }
}