using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
            _patientService = patientService;
        }

        [HttpGet] // Get all patients
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatientsAsync()
        {
            var patients = await _patientService.GetPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")] // Get a patient by ID
        public async Task<ActionResult<PatientDto>> GetPatientByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var patient = await _patientService.GetPatientById(id);
            if (patient == null)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            return Ok(patient);
        }

        [HttpDelete("{id}")] // Delete a patient by ID
        public async Task<ActionResult<PatientDto>> DeletePatientAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var patient = await _patientService.DeletePatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost] // Create a new patient
        public async Task<ActionResult<PatientDto>> CreatePatientAsync(PatientInputModel patientInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var newPatient = await _patientService.CreatePatient(patientInputModel);
            return Ok(newPatient);
        }

        [HttpPatch("{id}")] // Update a patient by ID
        public async Task<ActionResult<PatientDto>> UpdatePatientAsync(int id, PatientInputModel patientInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var updatedPatient = await _patientService.UpdatePatient(id, patientInputModel);
            return Ok(updatedPatient);
        }
    }
}