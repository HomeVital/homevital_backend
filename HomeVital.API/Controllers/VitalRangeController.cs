using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using HomeVital.API.Extensions;
using HomeVital.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;



namespace HomeVital.API.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("bodytemperature/{patientId}")]
        public async Task<ActionResult<BodyTemperatureRangeDto>> UpdateBodyTemperatureRangeAsync(int patientId, BodyTemperatureRangeInputModel bodyTemperatureRangeInputModel)
        {
            var bodyTemperatureRangeDto = await _vitalRangeService.UpdateBodyTemperatureRangeAsync(patientId, bodyTemperatureRangeInputModel);
            
            if (bodyTemperatureRangeDto == null)
            {
                throw new ResourceNotFoundException("No body temperature range records found for this patient.");
            }
            return Ok(bodyTemperatureRangeDto);
        }

        // Update a BloodPressureVitalRange record for a patient
        [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("bloodpressure/{patientId}")]
        public async Task<ActionResult<BloodPressureRangeDto>> UpdateBloodPressureRangeAsync(int patientId, BloodPressureRangeInputModel bloodPressureRangeInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var bloodPressureRangeDto = await _vitalRangeService.UpdateBloodPressureRangeAsync(patientId, bloodPressureRangeInputModel);
            
            return Ok(bloodPressureRangeDto);
        }

        // Update a BloodSugarVitalRange record for a patient
        [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("bloodsugar/{patientId}")]
        public async Task<ActionResult<BloodSugarRangeDto>> UpdateBloodSugarRangeAsync(int patientId, BloodSugarRangeInputModel bloodSugarRangeInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var bloodSugarRangeDto = await _vitalRangeService.UpdateBloodSugarRangeAsync(patientId, bloodSugarRangeInputModel);
            return Ok(bloodSugarRangeDto);
        }

        // Update a OxygenSaturationVitalRange record for a patient
        [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("oxygensaturation/{patientId}")]
        public async Task<ActionResult<OxygenSaturationRangeDto>> UpdateOxygenSaturationRangeAsync(int patientId, OxygenSaturationRangeInputModel oxygenSaturationRangeInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            var oxygenSaturationRangeDto = await _vitalRangeService.UpdateOxygenSaturationRangeAsync(patientId, oxygenSaturationRangeInputModel);
            return Ok(oxygenSaturationRangeDto);
        }

        // Update a BodyWeightVitalRange record for a patient
        [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("bodyweight/{patientId}")]
        public async Task<ActionResult<BodyWeightRangeDto>> UpdateBodyWeightRangeAsync(int patientId, BodyWeightRangeInputModel bodyWeightRangeInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            var bodyWeightRangeDto = await _vitalRangeService.UpdateBodyWeightRangeAsync(patientId, bodyWeightRangeInputModel);

            
            return Ok(bodyWeightRangeDto);
        }

        // Get vital range records for a patient
        [Authorize(Roles = "HealthcareWorker")]
        [HttpGet("{patientId}")]
        public async Task<ActionResult<VitalRangeDto>> GetVitalRangeAsync(int patientId)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            var vitalRangeDto = await _vitalRangeService.GetVitalRangeAsync(patientId);
            return Ok(vitalRangeDto);
        }
    
    }
}