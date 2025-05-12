using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Exceptions;
using HomeVital.API.Extensions;
using Microsoft.AspNetCore.Authorization;


namespace HomeVital.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patientplans")]
    public class PatientPlanController : ControllerBase
    {
        private readonly IPatientPlanService _patientPlanService;

        public PatientPlanController(IPatientPlanService patientPlanService)
        {
            _patientPlanService = patientPlanService;
        }

        [Authorize(Roles = "HealthcareWorker")]
        [HttpPost]
        public async Task<ActionResult<PatientPlanDto>> CreatePatientPlanAsync(PatientPlanInputModel patientPlanInputModel)
        {
            
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            
            var createdPlan = await _patientPlanService.CreatePatientPlanAsync(patientPlanInputModel.PatientID, patientPlanInputModel);

            if (createdPlan == null)
            {
                throw new HomeVitalInvalidOperationException("Failed to create patient plan.");
            }

            return Ok(createdPlan);
        }

        [Authorize(Roles = "Patient, HealthcareWorker")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientPlanDto>> GetPatientPlanByIdAsync(int id)
        {
            var plan = await _patientPlanService.GetPatientPlanByIdAsync(id);
            if (plan == null)
            {
                throw new ResourceNotFoundException("Patient plan not found with this ID: " + id);
            }
            return Ok(plan);
        }


        [Authorize(Roles = "Patient, HealthcareWorker")]
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<PatientPlanDto>>> GetPatientPlansByPatientIdAsync(int patientId)
        {
            var plans = await _patientPlanService.GetPatientPlansByPatientIdAsync(patientId);
            if (plans == null)
            {
                throw new ResourceNotFoundException("No patient plans found for this patient ID: " + patientId);
            }
            return Ok(plans);
        }
    }
}