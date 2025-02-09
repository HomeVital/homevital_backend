using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthcareWorkersController : ControllerBase
    {
        private readonly IHealthcareWorkerService _healthcareWorkerService;

        public HealthcareWorkersController(IHealthcareWorkerService healthcareWorkerService)
        {
            _healthcareWorkerService = healthcareWorkerService;
        }

        [HttpGet]
        public ActionResult GetHealthcareWorkerByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var healthcareWorker = _healthcareWorkerService.GetHealthcareWorkerByIdAsync(id);
            return Ok(healthcareWorker);
        }

        [HttpPost]
        public ActionResult CreateHealthcareWorkerAsync(HealthcareWorkerInputModel healthcareWorkerInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var newHealthcareWorker = _healthcareWorkerService.CreateHealthcareWorkerAsync(healthcareWorkerInputModel);
            return Ok(newHealthcareWorker);
        }

        [HttpDelete]
        public ActionResult DeleteHealthcareWorkerAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var healthcareWorker = _healthcareWorkerService.DeleteHealthcareWorkerAsync(id);
            return Ok(healthcareWorker);
        }

        [HttpPatch]
        public ActionResult UpdateHealthcareWorkerAsync(int id, HealthcareWorkerInputModel healthcareWorkerInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var updatedHealthcareWorker = _healthcareWorkerService.UpdateHealthcareWorkerAsync(id, healthcareWorkerInputModel);
            return Ok(updatedHealthcareWorker);
        }

    }
}