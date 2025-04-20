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
            return CreatedAtAction(nameof(GetPatientPlanByIdAsync), new { id = createdPlan.ID }, createdPlan);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientPlanDto>> GetPatientPlanByIdAsync(int id)
        {
            var plan = await _patientPlanService.GetPatientPlanByIdAsync(id);
            return Ok(plan);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<PatientPlanDto>>> GetPatientPlansByPatientIdAsync(int patientId)
        {
            var plans = await _patientPlanService.GetPatientPlansByPatientIdAsync(patientId);
            return Ok(plans);
        }
    }
}