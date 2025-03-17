
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/measurements")]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementsController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        // [HttpGet("getById")]
        // public async Task<ActionResult<List<MeasurementDto>>> GetMeasurementsById([FromQuery] int id)
        // {
        //     var measurements = await _measurementService.GetMeasurementsById(id);
        //     return Ok(measurements);
        // }
        // Post method to get measurements by id so that we can use the id in the body of the request and keep security in mind
        // [HttpPost("getById")]
        // public async Task<ActionResult<List<MeasurementDto>>> GetMeasurementsById([FromBody] int id)
        // {
        //     var measurements = await _measurementService.GetMeasurementsById(id);
        //     return Ok(measurements);
        // }


        [HttpGet("{id}")]
        public async Task<ActionResult<Measurements>> GetMeasurementsByPatientId([FromQuery] int patientId)
        {
            var measurements = await _measurementService.GetMeasurementsByPatientId(patientId);
            return Ok(measurements);
        }
    }
}