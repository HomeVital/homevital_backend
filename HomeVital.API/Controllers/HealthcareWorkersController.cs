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


    }
}