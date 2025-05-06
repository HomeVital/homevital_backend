using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using HomeVital.API.Extensions;




namespace HomeVital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/vitalrange")]
    public class VitalRangeController : ControllerBase
    {
        
        private readonly IVitalRangeService _vitalRangeService;

        public VitalRangeController(IVitalRangeService vitalRangeService)
        {
            _vitalRangeService = vitalRangeService;
        }

        // Create a new vital range record for a patient
        

        // Update a BodyTempVitalRange record for a patient
        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("bodytemperature/{patientId}")]
        public async Task<ActionResult<BodyTemperatureRangeDto>> UpdateBodyTemperatureRangeAsync(int patientId, BodyTemperatureRangeInputModel bodyTemperatureRangeInputModel)
        {
            var bodyTemperatureRangeDto = await _vitalRangeService.UpdateBodyTemperatureRangeAsync(patientId, bodyTemperatureRangeInputModel);
            return Ok(bodyTemperatureRangeDto);
        }

        // Update a BloodPressureVitalRange record for a patient
        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("bloodpressure/{patientId}")]
        public async Task<ActionResult<BloodPressureRangeDto>> UpdateBloodPressureRangeAsync(int patientId, BloodPressureRangeInputModel bloodPressureRangeInputModel)
        {
            var bloodPressureRangeDto = await _vitalRangeService.UpdateBloodPressureRangeAsync(patientId, bloodPressureRangeInputModel);
            return Ok(bloodPressureRangeDto);
        }

        // Update a BloodSugarVitalRange record for a patient
        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("bloodsugar/{patientId}")]
        public async Task<ActionResult<BloodSugarRangeDto>> UpdateBloodSugarRangeAsync(int patientId, BloodSugarRangeInputModel bloodSugarRangeInputModel)
        {
            var bloodSugarRangeDto = await _vitalRangeService.UpdateBloodSugarRangeAsync(patientId, bloodSugarRangeInputModel);
            return Ok(bloodSugarRangeDto);
        }

        // Update a OxygenSaturationVitalRange record for a patient
        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("oxygensaturation/{patientId}")]
        public async Task<ActionResult<OxygenSaturationRangeDto>> UpdateOxygenSaturationRangeAsync(int patientId, OxygenSaturationRangeInputModel oxygenSaturationRangeInputModel)
        {
            var oxygenSaturationRangeDto = await _vitalRangeService.UpdateOxygenSaturationRangeAsync(patientId, oxygenSaturationRangeInputModel);
            return Ok(oxygenSaturationRangeDto);
        }

        // Update a BodyWeightVitalRange record for a patient
        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("bodyweight/{patientId}")]
        public async Task<ActionResult<BodyWeightRangeDto>> UpdateBodyWeightRangeAsync(int patientId, BodyWeightRangeInputModel bodyWeightRangeInputModel)
        {
            var bodyWeightRangeDto = await _vitalRangeService.UpdateBodyWeightRangeAsync(patientId, bodyWeightRangeInputModel);

            if (bodyWeightRangeDto == null)
            {
                return NotFound("No body weight range records found for this patient.");
            }

            return Ok(bodyWeightRangeDto);
        }

        // Get vital range records for a patient
        // [Authorize(Roles = "HealthcareWorker")]
        [HttpGet("{patientId}")]
        public async Task<ActionResult<VitalRangeDto>> GetVitalRangeAsync(int patientId)
        {
            var vitalRangeDto = await _vitalRangeService.GetVitalRangeAsync(patientId);
            if (vitalRangeDto == null)
            {
                return NotFound("No vital range records found for this patient.");
            }

            return Ok(vitalRangeDto);
        }
    
    }
}