using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;

using HomeVital.API.Extensions;
using HomeVital.Models.Exceptions;





namespace HomeVital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/oxygensaturation")]
    public class OxygenSaturationController : ControllerBase
    {
        
        private readonly IOxygenSaturationService _oxygensaturationService;
        private readonly IPatientService _patientService;

        public OxygenSaturationController(IOxygenSaturationService oxygensaturationService, IPatientService patientService)
        {
            _patientService = patientService;
            _oxygensaturationService = oxygensaturationService;
        }

        // Get all oxygensaturations by patient ID
        // [Authorize(Roles = "Patient")]
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<OxygenSaturationDto>>> GetOxygenSaturationsByPatientIdAsync(int patientId)
        {

            var oxygensaturations = await _oxygensaturationService.GetOxygenSaturationsByPatientId(patientId);
            if (oxygensaturations == null || oxygensaturations.Count() == 0)
            {
                throw new ResourceNotFoundException("No oxygen saturation records found for this patient.");
            }
            

            return Ok(oxygensaturations);
        }

        // Create a new oxygensaturation record for a patient
        // [Authorize(Roles = "Patient")]
        [HttpPost("{patientId}")]
        public async Task<ActionResult<OxygenSaturationDto>> CreateOxygenSaturationAsync(int patientId, OxygenSaturationInputModel oxygensaturationInputModel)
        {
            var newOxygenSaturation = await _oxygensaturationService.CreateOxygenSaturation(patientId, oxygensaturationInputModel);

            if (newOxygenSaturation == null)
            {
                throw new ResourceNotFoundException("Failed to create oxygen saturation record.");
            }

            return Ok(newOxygenSaturation);
        }

        // Update a oxygensaturation record by ID
        // [Authorize(Roles = "Patient")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<OxygenSaturationDto>> UpdateOxygenSaturationAsync(int id, OxygenSaturationInputModel oxygensaturationInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            var existingOxygenSaturation = await _oxygensaturationService.GetOxygenSaturationById(id);
            if (existingOxygenSaturation == null)
            {
                throw new ResourceNotFoundException("Oxygen saturation record not found with ID:" + id);
            }

            var updatedOxygenSaturation = await _oxygensaturationService.UpdateOxygenSaturation(id, oxygensaturationInputModel);
            return Ok(updatedOxygenSaturation);
        }

        // Delete a oxygensaturation record by ID
        // [Authorize(Roles = "Patient")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOxygenSaturationAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var existingOxygenSaturation = await _oxygensaturationService.GetOxygenSaturationById(id);
            if (existingOxygenSaturation == null)
            {
                throw new ResourceNotFoundException("Oxygen saturation record with this ID:" + id + " not found.");
            }

            await _oxygensaturationService.DeleteOxygenSaturation(id);
            return Ok("Oxygen saturation record deleted successfully.");
        }
    }
}