using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;



namespace HomeVital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/patientplans")]
    public class PatientPlanController : ControllerBase
    {
        private readonly IPatientPlanService _patientPlanService;

        public PatientPlanController(IPatientPlanService patientPlanService)
        {
            _patientPlanService = patientPlanService;
        }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPost]
        public async Task<ActionResult<PatientPlanDto>> CreatePatientPlanAsync(PatientPlanInputModel patientPlanInputModel)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest("input model is not valid");
            }
            
            var createdPlan = await _patientPlanService.CreatePatientPlanAsync(patientPlanInputModel.PatientID, patientPlanInputModel);

            if (createdPlan == null)
            {
                return BadRequest("Failed to create patient plan");
            }

            return Ok(createdPlan);
        }

        // [Authorize(Roles = "Patient, HealthcareWorker")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientPlanDto>> GetPatientPlanByIdAsync(int id)
        {
            var plan = await _patientPlanService.GetPatientPlanByIdAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return Ok(plan);
        }


        // [Authorize(Roles = "Patient, HealthcareWorker")]
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<PatientPlanDto>>> GetPatientPlansByPatientIdAsync(int patientId)
        {
            var plans = await _patientPlanService.GetPatientPlansByPatientIdAsync(patientId);
            if (plans == null)
            {
                return NotFound();
            }
            return Ok(plans);
        }
    }
}