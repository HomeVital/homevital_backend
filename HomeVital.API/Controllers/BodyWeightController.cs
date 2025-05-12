using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Exceptions;
using HomeVital.API.Extensions;
using Microsoft.AspNetCore.Authorization;



namespace HomeVital.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/bodyweight")]

    public class BodyWeightController : ControllerBase
    {
        private readonly IBodyWeightService _bodyWeightService;
        public BodyWeightController(IBodyWeightService bodyWeightService)
        {
            _bodyWeightService = bodyWeightService;
        }
    

    // Get all bodyweights by patient ID
    [Authorize(Roles = "Patient")]
    [HttpGet("{patientId}")]
    public async Task<ActionResult<IEnumerable<BodyWeightDto>>> GetBodyWeightsByPatientIdAsync(int patientId)
    {
        var bodyWeights = await _bodyWeightService.GetBodyWeightsByPatientId(patientId);
        if (bodyWeights == null || !bodyWeights.Any())
        {
            throw new ResourceNotFoundException("No bodyweights found for this patient.");
        }
        return Ok(bodyWeights);
    }

    // Create a new bodyweight record for a patient
    [Authorize(Roles = "Patient")]
    [HttpPost("{patientId}")]
    public async Task<ActionResult<BodyWeightDto>> CreateBodyWeightAsync(int patientId, BodyWeightInputModel bodyWeightInputModel)
    {
        if (!ModelState.IsValid)
        {
            throw new ModelFormatException(ModelState.RetrieveErrorString());
        } 

        var newBodyWeight = await _bodyWeightService.CreateBodyWeight(patientId, bodyWeightInputModel);

        if (newBodyWeight == null)
        {
            throw new HomeVitalInvalidOperationException("Failed to create body weight record.");
        }
        return Ok(newBodyWeight);
    }

    // Update a bodyweight record by ID
    [Authorize(Roles = "Patient")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<BodyWeightDto>> UpdateBodyWeightAsync(int id, BodyWeightInputModel bodyWeightInputModel)
    {
        if (!ModelState.IsValid)
        {
            throw new ModelFormatException(ModelState.RetrieveErrorString());
        }

        var updatedBodyWeight = await _bodyWeightService.UpdateBodyWeight(id, bodyWeightInputModel);
        return Ok(updatedBodyWeight);
    }

    // Delete a bodyweight record by ID
    [Authorize(Roles = "Patient")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BodyWeightDto>> DeleteBodyWeightAsync(int id)
    {
        var deletedBodyWeight = await _bodyWeightService.DeleteBodyWeight(id);
        
        return Ok(deletedBodyWeight);
    }

    
    }
}

