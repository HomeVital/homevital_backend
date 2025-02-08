using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using HomeVital.Repositories.dbContext;
using Microsoft.EntityFrameworkCore;

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

        // get all healthcare workers
        [HttpGet]
        public async Task<IActionResult> GetHealthcareWorkers()
        {
            var workers = await _context.HealthcareWorkers.ToListAsync();
            var workerDtos = workers.Select(worker => new HealthcareWorkerDto
            {
                ID = worker.ID,
                Name = worker.Name,
                Phone = worker.Phone,
                TeamID = worker.TeamID,
                Status = worker.Status
            });

            return Ok(workerDtos);
        }

        // get a healthcare worker by id

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
                TeamID = worker.TeamID,
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
                TeamID = inputModel.TeamID,
                Status = inputModel.Status
            };

            _context.HealthcareWorkers.Add(worker);
            await _context.SaveChangesAsync();

            var workerDto = new HealthcareWorkerDto
            {
                ID = worker.ID,
                Name = worker.Name,
                Phone = worker.Phone,
                TeamID = worker.TeamID,
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
            worker.TeamID = inputModel.TeamID;
            worker.Status = inputModel.Status;

            await _context.SaveChangesAsync();

            var workerDto = new HealthcareWorkerDto
            {
                ID = worker.ID,
                Name = worker.Name,
                Phone = worker.Phone,
                TeamID = worker.TeamID,
                Status = worker.Status
            };

            return Ok(workerDto);
        }

        // delete a healthcare worker by id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteHealthcareWorker(int id)
        {
            var worker = await _context.HealthcareWorkers.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.HealthcareWorkers.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}