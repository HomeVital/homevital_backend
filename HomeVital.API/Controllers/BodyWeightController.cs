using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;


namespace HomeVital.API.Controllers
{
    // [Authorize]
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
    // [Authorize(Roles = "Patient")]
    [HttpGet("{patientId}")]
    public async Task<ActionResult<IEnumerable<BodyWeightDto>>> GetBodyWeightsByPatientIdAsync(int patientId)
    {
        var bodyWeights = await _bodyWeightService.GetBodyWeightsByPatientId(patientId);
        return Ok(bodyWeights);
    }

    // Create a new bodyweight record for a patient
    // [Authorize(Roles = "Patient")]
    [HttpPost("{patientId}")]
    public async Task<ActionResult<BodyWeightDto>> CreateBodyWeightAsync(int patientId, BodyWeightInputModel bodyWeightInputModel)
    {
        if (!ModelState.IsValid)
        {
            throw new System.ArgumentException("Invalid input model");
        }

        var newBodyWeight = await _bodyWeightService.CreateBodyWeight(patientId, bodyWeightInputModel);
        return Ok(newBodyWeight);
    }

    // Update a bodyweight record by ID
    // [Authorize(Roles = "Patient")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<BodyWeightDto>> UpdateBodyWeightAsync(int id, BodyWeightInputModel bodyWeightInputModel)
    {
        if (!ModelState.IsValid)
        {
            throw new System.ArgumentException("Invalid input model");
        }

        var updatedBodyWeight = await _bodyWeightService.UpdateBodyWeight(id, bodyWeightInputModel);
        return Ok(updatedBodyWeight);
    }

    // Delete a bodyweight record by ID
    // [Authorize(Roles = "Patient")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BodyWeightDto>> DeleteBodyWeightAsync(int id)
    {
        var deletedBodyWeight = await _bodyWeightService.DeleteBodyWeight(id);
        return Ok(deletedBodyWeight);
    }

    // Get a bodyweight record by ID
    // [HttpGet("by-id/{id}")]
    // public async Task<ActionResult<BodyWeightDto>> GetBodyWeightByIdAsync(int id)
    // {
    //     var bodyWeight = await _bodyWeightService.GetBodyWeightById(id);
    //     return Ok(bodyWeight);
    // }
    
    }
}

