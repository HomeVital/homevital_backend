using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;



namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/patientplans")]
    public class PatientPlanController : ControllerBase
    {
        private readonly IPatientPlanService _patientPlanService;

        public PatientPlanController(IPatientPlanService patientPlanService)
        {
            _patientPlanService = patientPlanService;
        }

        [HttpPost]
        public async Task<ActionResult<PatientPlanDto>> CreatePatientPlanAsync(PatientPlanInputModel patientPlanInputModel)
        {
            
            var createdPlan = await _patientPlanService.CreatePatientPlanAsync(patientPlanInputModel.PatientID, patientPlanInputModel);
            if (createdPlan == null)
            {
                return BadRequest("Failed to create patient plan.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("input model is not valid");
            }
            
            return Ok(createdPlan);
        }

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

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<PatientPlanDto>>> GetPatientPlansByPatientIdAsync(int patientId)
        {
            var plans = await _patientPlanService.GetPatientPlansByPatientIdAsync(patientId);
            if (plans == null || plans.Count == 0)
            {
                return NotFound();
            }
            return Ok(plans);
        }
    }
}