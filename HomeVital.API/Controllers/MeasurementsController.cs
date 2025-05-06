
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
        private readonly IPatientService _patientService;

        public MeasurementsController(IMeasurementService measurementService, IPatientService patientService)
        {
            _patientService = patientService;
            _measurementService = measurementService;
        }

        // get X number of measurements for a patient by patient id
        [HttpGet("{patientId}/latest/{count}")]
        public async Task<ActionResult<IEnumerable<Measurements>>> GetXMeasurementsByPatientId(int patientId, int count)
        {
            // check if the patient exists
            var patient = await _patientService.GetPatientById(patientId);
            if (patient == null)
            {
                return NotFound("Patient does not exist.");
            }
            var measurements = await _measurementService.GetXMeasurementsByPatientId(patientId, count);
            return Ok(measurements);
        }
        
        [HttpGet("{patientId}")]
        public async Task<ActionResult<Envelope<List<MeasurementDto>>>> GetMeasurementsByPatientId(
            int patientId,
            [FromQuery] int pageSize = 25,
            [FromQuery] int pageNumber = 1
        )
        {
            // Check if the patient exists
            var patient = await _patientService.GetPatientById(patientId);
            if (patient == null)
            {
                return NotFound("Patient does not exist.");
            }

            // Get measurements for the patient
            var measurements = await _measurementService.GetMeasurementsByPatientId(patientId);
            if (measurements == null || !measurements.Any())
            {
                return NotFound("No measurements found for this patient.");
            }

            // Pagination logic
            var totalCount = measurements.Count();
            var paginatedData = measurements
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Wrap in envelope
            var envelope = new Envelope<List<Measurements>>(
                paginatedData,
                totalCount,
                pageSize,
                pageNumber
            );

            return Ok(envelope);
        }
        
        [HttpGet("warnings")]
        public async Task<ActionResult<Envelope<List<MeasurementDto>>>> GetMeasurementsWithWarnings(
            [FromQuery] int pageSize = 25,
            [FromQuery] int pageNumber = 1
        )
        {
            // Fetch measurements with warnings
            var measurements = await _measurementService.GetMeasurementsWithWarnings();
            if (measurements == null || !measurements.Any())
            {
                return NotFound("No measurements found.");
            }

            // Pagination logic
            var totalCount = measurements.Count();
            var paginatedData = measurements
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList(); 

            // Wrap in envelope
            var envelope = new Envelope<List<Measurements>>(
                paginatedData,
                totalCount,
                pageSize,
                pageNumber
            );

            return Ok(envelope);
        }

        // Get unacknowledged warnings for a specific patient
        [HttpGet("patient/{patientId}/warnings")]
        public async Task<ActionResult<List<Measurements>>> GetPatientWarnings(int patientId, bool onlyUnacknowledged = true)
        {
            // check if the patient exists
            var patient = await _patientService.GetPatientById(patientId);
            if (patient == null)
            {
                return NotFound("Patient does not exist.");
            }
            var measurements = await _measurementService.GetPatientWarnings(patientId, onlyUnacknowledged);
            return Ok(measurements);
        }

        [HttpPost("acknowledge")]
        public async Task<ActionResult> AcknowledgeMeasurement(MeasurementAckInputModel input)
        {
            var result = await _measurementService.AcknowledgeMeasurement(input);
            if (!result)
                return NotFound(result);
                
            return Ok(result);
        }
    }
}