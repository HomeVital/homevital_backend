using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Exceptions;
using HomeVital.API.Extensions;

namespace HomeVital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/bodytemperature")]

    public class BodyTemperatureController : ControllerBase
    {
        private readonly IBodyTemperatureService _bodyTemperatureService;

        public BodyTemperatureController(IBodyTemperatureService bodyTemperatureService)
        {
            _bodyTemperatureService = bodyTemperatureService;
        }

        // Get all body temperatures by patient ID
        // [Authorize(Roles = "Patient")]
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<BodyTemperatureDto>>> GetBodyTemperaturesByPatientIdAsync(int patientId)
        {
            var bodyTemperatures = await _bodyTemperatureService.GetBodyTemperaturesByPatientId(patientId);
            if (bodyTemperatures == null || !bodyTemperatures.Any())
            {
                throw new ResourceNotFoundException("No body temperatures found for the specified patient.");
            }
            return Ok(bodyTemperatures);
        }

        // Create a new body temperature record for a patient
        // [Authorize(Roles = "Patient")]
        [HttpPost("{patientId}")]
        public async Task<ActionResult<BodyTemperatureDto>> CreateBodyTemperatureAsync(int patientId, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var newBodyTemperature = await _bodyTemperatureService.CreateBodyTemperature(patientId, bodyTemperatureInputModel);
            return Ok(newBodyTemperature);
        }

        // Update a body temperature record by ID
        // [Authorize(Roles = "Patient")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<BodyTemperatureDto>> UpdateBodyTemperatureAsync(int id, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var updatedBodyTemperature = await _bodyTemperatureService.UpdateBodyTemperature(id, bodyTemperatureInputModel);
            return Ok(updatedBodyTemperature);
        }

        // Delete a body temperature record by ID
        // [Authorize(Roles = "Patient")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<BodyTemperatureDto>> DeleteBodyTemperatureAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var deletedBodyTemperature = await _bodyTemperatureService.DeleteBodyTemperature(id);
            return Ok(deletedBodyTemperature);
        }

    }
}