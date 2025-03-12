using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;




namespace HomeVital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/oxygensaturation")]
    public class OxygenSaturationController : ControllerBase
    {
        
        private readonly IOxygenSaturationService _oxygensaturationService;

        public OxygenSaturationController(IOxygenSaturationService oxygensaturationService)
        {
            _oxygensaturationService = oxygensaturationService;
        }

        // Get all oxygensaturations by patient ID
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<OxygenSaturationDto>>> GetOxygenSaturationsByPatientIdAsync(int patientId)
        {
            var oxygensaturations = await _oxygensaturationService.GetOxygenSaturationsByPatientId(patientId);
            return Ok(oxygensaturations);
        }

        // Create a new oxygensaturation record for a patient
        [HttpPost("{patientId}")]
        public async Task<ActionResult<OxygenSaturationDto>> CreateOxygenSaturationAsync(int patientId, OxygenSaturationInputModel oxygensaturationInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var newOxygenSaturation = await _oxygensaturationService.CreateOxygenSaturation(patientId, oxygensaturationInputModel);
            return Ok(newOxygenSaturation);
        }

        // Update a oxygensaturation record by ID
        [HttpPatch("{id}")]
        public async Task<ActionResult<OxygenSaturationDto>> UpdateOxygenSaturationAsync(int id, OxygenSaturationInputModel oxygensaturationInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var updatedOxygenSaturation = await _oxygensaturationService.UpdateOxygenSaturation(id, oxygensaturationInputModel);
            return Ok(updatedOxygenSaturation);
        }

        // Delete a oxygensaturation record by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOxygenSaturationAsync(int id)
        {
            await _oxygensaturationService.DeleteOxygenSaturation(id);
            return Ok();
        }
    }
}