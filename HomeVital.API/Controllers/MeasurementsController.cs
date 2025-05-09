
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
namespace HomeVital.API.Controllers
{
    // [Authorize]
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
        // [Authorize(Roles = "HealthcareWorker, Patient")]
        [HttpGet("{patientId}/latest/{count}")]
        public async Task<ActionResult<IEnumerable<Measurements>>> GetXMeasurementsByPatientId(int patientId, int count)
        {
            // check if the patient exists
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var patient = await _patientService.GetPatientById(patientId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (patient == null)
            {
                return NotFound("Patient does not exist.");
            }
            var measurements = await _measurementService.GetXMeasurementsByPatientId(patientId, count);
            return Ok(measurements);
        }
        
        // [Authorize(Roles = "HealthcareWorker, Patient")]
        [HttpGet("{patientId}")]
        public async Task<ActionResult<Envelope<List<MeasurementDto>>>> GetMeasurementsByPatientId(
            int patientId,
            [FromQuery] int pageSize = 25,
            [FromQuery] int pageNumber = 1
        )
        {
            // Check if the patient exists
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var patient = await _patientService.GetPatientById(patientId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (patient == null)
            {
                return NotFound("Patient does not exist.");
            }

            // Get measurements for the patient
            var measurements = await _measurementService.GetMeasurementsByPatientId(patientId);
            // if (measurements == null)
            // {
                
            // }

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
        
        // [Authorize(Roles = "HealthcareWorker")]
        [HttpGet("warnings")]
        public async Task<ActionResult<Envelope<List<MeasurementDto>>>> GetMeasurementsWithWarnings(
            [FromQuery] List<int> teamIDs,
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
            // Filter by teamIDs if provided
            if (teamIDs != null && teamIDs.Any())
            {
                measurements = measurements.Where(m => teamIDs.Contains(m.TeamID)).ToList();
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
        // [Authorize(Roles = "HealthcareWorker")]
        [HttpGet("patient/{patientId}/warnings")]
        public async Task<ActionResult<List<Measurements>>> GetPatientWarnings(int patientId, bool onlyUnacknowledged = true)
        {
            // check if the patient exists
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var patient = await _patientService.GetPatientById(patientId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (patient == null)
            {
                return NotFound("Patient does not exist.");
            }
            var measurements = await _measurementService.GetPatientWarnings(patientId, onlyUnacknowledged);
            return Ok(measurements);
        }

        // [Authorize(Roles = "HealthcareWorker")]
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