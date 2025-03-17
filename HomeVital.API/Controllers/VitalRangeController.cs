using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;




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
        [HttpPatch("bodytemperature/{patientId}")]
        public async Task<ActionResult<BodyTemperatureRangeDto>> UpdateBodyTemperatureRangeAsync(int patientId, BodyTemperatureRangeInputModel bodyTemperatureRangeInputModel)
        {
            var bodyTemperatureRangeDto = await _vitalRangeService.UpdateBodyTemperatureRangeAsync(patientId, bodyTemperatureRangeInputModel);
            return Ok(bodyTemperatureRangeDto);
        }

        // Update a BloodPressureVitalRange record for a patient
        [HttpPatch("bloodpressure/{patientId}")]
        public async Task<ActionResult<BloodPressureRangeDto>> UpdateBloodPressureRangeAsync(int patientId, BloodPressureRangeInputModel bloodPressureRangeInputModel)
        {
            var bloodPressureRangeDto = await _vitalRangeService.UpdateBloodPressureRangeAsync(patientId, bloodPressureRangeInputModel);
            return Ok(bloodPressureRangeDto);
        }

        // Update a BloodSugarVitalRange record for a patient
        [HttpPatch("bloodsugar/{patientId}")]
        public async Task<ActionResult<BloodSugarRangeDto>> UpdateBloodSugarRangeAsync(int patientId, BloodSugarRangeInputModel bloodSugarRangeInputModel)
        {
            var bloodSugarRangeDto = await _vitalRangeService.UpdateBloodSugarRangeAsync(patientId, bloodSugarRangeInputModel);
            return Ok(bloodSugarRangeDto);
        }

        // Update a OxygenSaturationVitalRange record for a patient
        [HttpPatch("oxygensaturation/{patientId}")]
        public async Task<ActionResult<OxygenSaturationRangeDto>> UpdateOxygenSaturationRangeAsync(int patientId, OxygenSaturationRangeInputModel oxygenSaturationRangeInputModel)
        {
            var oxygenSaturationRangeDto = await _vitalRangeService.UpdateOxygenSaturationRangeAsync(patientId, oxygenSaturationRangeInputModel);
            return Ok(oxygenSaturationRangeDto);
        }

        // Update a BodyWeightVitalRange record for a patient
        [HttpPatch("bodyweight/{patientId}")]
        public async Task<ActionResult<BodyWeightRangeDto>> UpdateBodyWeightRangeAsync(int patientId, BodyWeightRangeInputModel bodyWeightRangeInputModel)
        {
            var bodyWeightRangeDto = await _vitalRangeService.UpdateBodyWeightRangeAsync(patientId, bodyWeightRangeInputModel);
            return Ok(bodyWeightRangeDto);
        }

        // Get vital range records for a patient
        [HttpGet("{patientId}")]
        public async Task<ActionResult<VitalRangeDto>> GetVitalRangeAsync(int patientId)
        {
            var vitalRangeDto = await _vitalRangeService.GetVitalRangeAsync(patientId);
            return Ok(vitalRangeDto);
        }
    
    }
}