using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/measurements")]
    public class MeasurementsController : ControllerBase
    {
        [HttpGet("bloodsugar")]
        // get measurement by id

        public async Task<IActionResult> GetMeasurementById([FromQuery] int id)
        {
            // get measurement by id
            await Task.Delay(1000);
            
            return Ok("blood sugar is 8" );
        }

        // body temperature
        [HttpGet("bodytemperature")]
        public async Task<IActionResult> GetBodyTemperatureById([FromQuery] int id)
        {
            // get measurement by id
            await Task.Delay(1000);
            
            return Ok("body temperature is 36.6" );
        }

        // blood pressure
        [HttpGet("bloodpressure")]
        public async Task<IActionResult> GetBloodPressureById([FromQuery] int id)
        {
            // get measurement by id
            await Task.Delay(1000);
            
            return Ok("blood pressure is 120/80" );
        }

        // weight
        [HttpGet("weight")]
        public async Task<IActionResult> GetWeightById([FromQuery] int id)
        {
            // get measurement by id
            await Task.Delay(1000);
            
            return Ok("weight is 80" );
        }

    }
}