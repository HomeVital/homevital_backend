using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;


namespace HomeVital.API.Controllers
{

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
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<BodyTemperatureDto>>> GetBodyTemperaturesByPatientIdAsync(int patientId)
        {
            var bodyTemperatures = await _bodyTemperatureService.GetBodyTemperaturesByPatientId(patientId);
            return Ok(bodyTemperatures);
        }

        // Create a new body temperature record for a patient
        [HttpPost("{patientId}")]
        public async Task<ActionResult<BodyTemperatureDto>> CreateBodyTemperatureAsync(int patientId, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var newBodyTemperature = await _bodyTemperatureService.CreateBodyTemperature(patientId, bodyTemperatureInputModel);
            return Ok(newBodyTemperature);
        }

        // Update a body temperature record by ID
        [HttpPatch("{id}")]
        public async Task<ActionResult<BodyTemperatureDto>> UpdateBodyTemperatureAsync(int id, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var updatedBodyTemperature = await _bodyTemperatureService.UpdateBodyTemperature(id, bodyTemperatureInputModel);
            return Ok(updatedBodyTemperature);
        }

        // Delete a body temperature record by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<BodyTemperatureDto>> DeleteBodyTemperatureAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var deletedBodyTemperature = await _bodyTemperatureService.DeleteBodyTemperature(id);
            return Ok(deletedBodyTemperature);
        }

    }
}