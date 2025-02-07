using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using HomeVital.Repositories.dbContext;

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/healthcareworkers")]
    public class HealthcareWorkersController : ControllerBase
    {
        private readonly HomeVitalDbContext _context;

        public HealthcareWorkersController(HomeVitalDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthcareWorkerById(int id)
        {
            var worker = await _context.HealthcareWorkers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            var workerDto = new HealthcareWorkerDto
            {
                ID = worker.ID,
                Name = worker.Name,
                Phone = worker.Phone,
                Team = worker.Team,
                Status = worker.Status
            };

            return Ok(workerDto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHealthcareWorker([FromBody] HealthcareWorkerInputModel inputModel)
        {
            var worker = new HealthcareWorker
            {
                Name = inputModel.Name,
                Phone = inputModel.Phone,
                Team = inputModel.Team,
                Status = inputModel.Status
            };

            _context.HealthcareWorkers.Add(worker);
            await _context.SaveChangesAsync();

            var workerDto = new HealthcareWorkerDto
            {
                ID = worker.ID,
                Name = worker.Name,
                Phone = worker.Phone,
                Team = worker.Team,
                Status = worker.Status
            };

            return CreatedAtAction(nameof(GetHealthcareWorkerById), new { id = worker.ID }, workerDto);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateHealthcareWorker(int id, [FromBody] HealthcareWorkerInputModel inputModel)
        {
            var worker = await _context.HealthcareWorkers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            worker.Name = inputModel.Name;
            worker.Phone = inputModel.Phone;
            worker.Team = inputModel.Team;
            worker.Status = inputModel.Status;

            await _context.SaveChangesAsync();

            var workerDto = new HealthcareWorkerDto
            {
                ID = worker.ID,
                Name = worker.Name,
                Phone = worker.Phone,
                Team = worker.Team,
                Status = worker.Status
            };

            return Ok(workerDto);
        }
    }
}