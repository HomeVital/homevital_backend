
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
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

        // get X number of measurements for a patient by patient id
        [HttpGet("{patientId}/latest/{count}")]
        public async Task<ActionResult<IEnumerable<Measurements>>> GetXMeasurementsByPatientId(int patientId, int count)
        {
            var measurements = await _measurementService.GetXMeasurementsByPatientId(patientId, count);
            return Ok(measurements);
        }
        

        [HttpGet("{patientId}")]
        public async Task<ActionResult<Measurements>> GetMeasurementsByPatientId(int patientId)
        {
            var measurements = await _measurementService.GetMeasurementsByPatientId(patientId);
            return Ok(measurements);
        }

        [HttpGet("warnings")]
        public async Task<ActionResult<List<Measurements>>> GetMeasurementsWithWarnings(string status)
        {
            var measurements = await _measurementService.GetMeasurementsWithWarnings(status);
            return Ok(measurements);
        }

        // Get unacknowledged warnings for a specific patient
        [HttpGet("patient/{patientId}/warnings")]
        public async Task<ActionResult<List<Measurements>>> GetPatientWarnings(int patientId, bool onlyUnacknowledged = true)
        {
            var measurements = await _measurementService.GetPatientWarnings(patientId, onlyUnacknowledged);
            return Ok(measurements);
        }

        [HttpPost("acknowledge")]
        public async Task<ActionResult> AcknowledgeMeasurement(MeasurementAckInputModel input)
        {
            var result = await _measurementService.AcknowledgeMeasurement(input);
            if (!result)
                return NotFound("Measurement not found");
                
            return Ok();
        }
    }
}