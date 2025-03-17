
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
    }
}