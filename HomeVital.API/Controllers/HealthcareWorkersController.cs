using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models.Exceptions;
using HomeVital.API.Extensions;


namespace HomeVital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/healthcareworkers")]

    public class HealthcareWorkersController : ControllerBase
    {
        private readonly IHealthcareWorkerService _healthcareWorkerService;

        public HealthcareWorkersController(IHealthcareWorkerService healthcareWorkerService)
        {
            _healthcareWorkerService = healthcareWorkerService;
        }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpGet] // Get all healthcare workers
        public async Task<ActionResult<Envelope<IEnumerable<HealthcareWorkerDto>>>> GetHealthcareWorkersAsync(
            [FromQuery] int pageSize = 25,
            [FromQuery] int pageNumber = 1
        )
        {
            var healthcareWorkers = await _healthcareWorkerService.GetHealthcareWorkers();
            
            if (healthcareWorkers == null || !healthcareWorkers.Any())
            {
                throw new ResourceNotFoundException("No healthcare workers found");
            }

            // Pagination logic
            var totalCount = healthcareWorkers.Count();
            var paginatedData = healthcareWorkers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            // Wrap in envelope
            var envelope = new Envelope<IEnumerable<HealthcareWorkerDto>>(
                paginatedData,
                totalCount,
                pageSize,
                pageNumber
            );
            // Return the paginated data
            return Ok(envelope);

            // return Ok(healthcareWorkers);
        }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpGet("{id}")] // Get a healthcare worker by ID
        public async Task<ActionResult<HealthcareWorkerDto>> GetHealthcareWorkerByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            var healthcareWorker = await _healthcareWorkerService.GetHealthcareWorkerById(id);
            return Ok(healthcareWorker);
        }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpDelete("{id}")] // Delete a healthcare worker by ID
        public async Task<ActionResult<HealthcareWorkerDto>> DeleteHealthcareWorkerAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            var healthcareWorker = await _healthcareWorkerService.DeleteHealthcareWorker(id);
            return Ok(healthcareWorker);
        }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPost] // Create a new healthcare worker
        public async Task<ActionResult<HealthcareWorkerDto>> CreateHealthcareWorkerAsync(HealthcareWorkerInputModel healthcareWorkerInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var newHealthcareWorker = await _healthcareWorkerService.CreateHealthcareWorker(healthcareWorkerInputModel);
            return Ok(newHealthcareWorker);
        }

        // [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("{id}")] // Update a healthcare worker by ID
        public async Task<ActionResult<HealthcareWorkerDto>> UpdateHealthcareWorkerAsync(int id, HealthcareWorkerInputModel healthcareWorkerInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var updatedHealthcareWorker = await _healthcareWorkerService.UpdateHealthcareWorker(id, healthcareWorkerInputModel);
            return Ok(updatedHealthcareWorker);
        }

        


    }
}