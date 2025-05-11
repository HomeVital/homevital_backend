
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
    [Route("api/bloodpressure")]
    public class BloodPressureController : ControllerBase
    {
        
        private readonly IBloodPressureService _bloodpressureService;

        public BloodPressureController(IBloodPressureService bloodpressureService)
        {
            _bloodpressureService = bloodpressureService;
        }

        // Get all bloodpressures by patient ID
        [Authorize(Roles = "Patient")]
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<BloodPressureDto>>> GetBloodPressuresByPatientIdAsync(int patientId)
        {
            var bloodpressures = await _bloodpressureService.GetBloodPressuresByPatientId(patientId);
            return Ok(bloodpressures);
        }

        // Create a new bloodpressure record for a patient
        [Authorize(Roles = "Patient")]
        [HttpPost("{patientId}")]
        public async Task<ActionResult<BloodPressureDto>> CreateBloodPressureAsync(int patientId, BloodPressureInputModel bloodpressureInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var newBloodPressure = await _bloodpressureService.CreateBloodPressure(patientId, bloodpressureInputModel);
            return Ok(newBloodPressure);
        }

        // Update a bloodpressure record by ID
        [Authorize(Roles = "Patient")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<BloodPressureDto>> UpdateBloodPressureAsync(int id, BloodPressureInputModel bloodpressureInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var updatedBloodPressure = await _bloodpressureService.UpdateBloodPressure(id, bloodpressureInputModel);
            return Ok(updatedBloodPressure);
        }

        // Delete a bloodpressure record by ID
        [Authorize(Roles = "Patient")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BloodPressureDto>> DeleteBloodPressureAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var deletedBloodPressure = await _bloodpressureService.DeleteBloodPressure(id);
            return Ok(deletedBloodPressure);
        }

    }
}