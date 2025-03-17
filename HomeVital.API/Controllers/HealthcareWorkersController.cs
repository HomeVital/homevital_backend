using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/healthcareworkers")]
    public class HealthcareWorkersController : ControllerBase
    {
        private readonly IHealthcareWorkerService _healthcareWorkerService;

        public HealthcareWorkersController(IHealthcareWorkerService healthcareWorkerService)
        {
            _healthcareWorkerService = healthcareWorkerService;
        }

        [HttpGet] // Get all healthcare workers
        public async Task<ActionResult<IEnumerable<HealthcareWorkerDto>>> GetHealthcareWorkersAsync()
        {
            var healthcareWorkers = await _healthcareWorkerService.GetHealthcareWorkers();
            return Ok(healthcareWorkers);
        }

        [HttpGet("{id}")] // Get a healthcare worker by ID
        public async Task<ActionResult<HealthcareWorkerDto>> GetHealthcareWorkerByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var healthcareWorker = await _healthcareWorkerService.GetHealthcareWorkerById(id);
            return Ok(healthcareWorker);
        }

        [HttpDelete("{id}")] // Delete a healthcare worker by ID
        public async Task<ActionResult<HealthcareWorkerDto>> DeleteHealthcareWorkerAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var healthcareWorker = await _healthcareWorkerService.DeleteHealthcareWorker(id);
            return Ok(healthcareWorker);
        }

        [HttpPost] // Create a new healthcare worker
        public async Task<ActionResult<HealthcareWorkerDto>> CreateHealthcareWorkerAsync(HealthcareWorkerInputModel healthcareWorkerInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var newHealthcareWorker = await _healthcareWorkerService.CreateHealthcareWorker(healthcareWorkerInputModel);
            return Ok(newHealthcareWorker);
        }

        [HttpPatch("{id}")] // Update a healthcare worker by ID
        public async Task<ActionResult<HealthcareWorkerDto>> UpdateHealthcareWorkerAsync(int id, HealthcareWorkerInputModel healthcareWorkerInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var updatedHealthcareWorker = await _healthcareWorkerService.UpdateHealthcareWorker(id, healthcareWorkerInputModel);
            return Ok(updatedHealthcareWorker);
        }


    }
}