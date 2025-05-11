using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models.Exceptions;
using HomeVital.API.Extensions;
using Microsoft.AspNetCore.Authorization;





namespace HomeVital.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [Authorize(Roles = "HealthcareWorker")]
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
                throw new ResourceNotFoundException("No patients found.");
            }

            // Pagination logic
            var totalCount = patients.Count();
            var paginatedData = patients
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Wrap in envelope
            var envelope = new Envelope<IEnumerable<PatientDto>>(
                paginatedData!,
                totalCount,
                pageSize,
                pageNumber
            );

            return Ok(envelope);
        }
        
        [Authorize(Roles = "Patient, HealthcareWorker")]
        [HttpGet("{id}")] // Get a patient by ID
        public async Task<ActionResult<PatientDto>> GetPatientByIdAsync(int id)
        {
            // Check if the patient exists
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var patient = await _patientService.GetPatientById(id)!;
           
            return Ok(patient);
        }

        [Authorize(Roles = "HealthcareWorker")]
        [HttpDelete("{id}")] // Delete a patient by ID
        public async Task<ActionResult<PatientDto>> DeletePatientAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            var patient = await _patientService.DeletePatient(id);
            if (patient == null)
            {
                throw new ResourceNotFoundException("Patient not found with this ID: " + id);
            }
            return Ok(patient);
        }

        [Authorize(Roles = "HealthcareWorker")]
        [HttpPost] // Create a new patient
        public async Task<ActionResult<PatientDto>> CreatePatientAsync(PatientInputModel patientInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var newPatient = await _patientService.CreatePatient(patientInputModel);
            if (newPatient == null)
            {
                return BadRequest("Failed to create patient");
            }

            return Ok(newPatient);
        }


        [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("{id}")] // Update a patient by ID
        public async Task<ActionResult<PatientDto>> UpdatePatientAsync(int id, PatientInputModel patientInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            // Check if the patient exists
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var existingPatient = await _patientService.GetPatientById(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (existingPatient == null)
            {
                throw new ResourceNotFoundException("Patient not found with this ID: " + id);
            }

            var updatedPatient = await _patientService.UpdatePatient(id, patientInputModel);
            return Ok(updatedPatient);
        }        

    }
}