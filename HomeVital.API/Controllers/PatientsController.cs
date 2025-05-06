using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;

namespace HomeVital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // [HttpGet] // Get all patients
        // public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatientsAsync()
        // {
        //     var patients = await _patientService.GetPatients();

        //     if (patients == null || patients.Count() == 0)
        //     {
        //         return NotFound("No patients found.");
        //     }
            
        //     return Ok(patients);
        // }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpGet] // Get all patients
        public async Task<ActionResult<Envelope<IEnumerable<PatientDto>>>> GetPatientsAsync(
            [FromQuery] int pageSize = 25,
            [FromQuery] int pageNumber = 1
        )
        {
            // Get all patients
            var patients = await _patientService.GetPatients();

            if (patients == null || !patients.Any())
            {
                return NotFound("No patients found.");
            }

            // Pagination logic
            var totalCount = patients.Count();
            var paginatedData = patients
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Wrap in envelope
            var envelope = new Envelope<IEnumerable<PatientDto>>(
                paginatedData,
                totalCount,
                pageSize,
                pageNumber
            );

            return Ok(envelope);
        }
        
        // [Authorize(Roles = "Patient, HealthcareWorker")]
        [HttpGet("{id}")] // Get a patient by ID
        public async Task<ActionResult<PatientDto>> GetPatientByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("input model is not valid");
            }
            var patient = await _patientService.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpDelete("{id}")] // Delete a patient by ID
        public async Task<ActionResult<PatientDto>> DeletePatientAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("input model is not valid");
            }
            var patient = await _patientService.DeletePatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPost] // Create a new patient
        public async Task<ActionResult<PatientDto>> CreatePatientAsync(PatientInputModel patientInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("input model is not valid");
            }

            var newPatient = await _patientService.CreatePatient(patientInputModel);
            return Ok(newPatient);
        }


        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("{id}")] // Update a patient by ID
        public async Task<ActionResult<PatientDto>> UpdatePatientAsync(int id, PatientInputModel patientInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("input model is not valid");
            }
            // Check if the patient exists
            var existingPatient = await _patientService.GetPatientById(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            var updatedPatient = await _patientService.UpdatePatient(id, patientInputModel);
            return Ok(updatedPatient);
        }        

    }
}