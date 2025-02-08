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
    [Route("api/bloodsugar")]
    public class BloodsugarsController : ControllerBase
    {
        private readonly HomeVitalDbContext _context;

        public BloodsugarsController(HomeVitalDbContext context)
        {
            _context = context;
        }

        // get bloodsugars by patient id
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetBloodsugarsByPatientId(int patientId)
        {
            var bloodsugars = await _context.Bloodsugars.Where(b => b.PatientID == patientId).ToListAsync();
            var bloodsugarDtos = bloodsugars.Select(bloodsugar => new BloodsugarDto
            {
                ID = bloodsugar.ID,
                PatientID = bloodsugar.PatientID,
                BloodsugarLevel = bloodsugar.BloodsugarLevel,
                Date = bloodsugar.Date
            });

            return Ok(bloodsugarDtos);
        }

        // make a new bloodsugar record
        [HttpPost]
        public async Task<IActionResult> CreateBloodsugar([FromBody] BloodsugarInputModel bloodsugarInputModel)
        {
            var bloodsugar = new Bloodsugar
            {
                PatientID = bloodsugarInputModel.PatientID,
                BloodsugarLevel = bloodsugarInputModel.BloodsugarLevel,
                Date = bloodsugarInputModel.Date
            };

            _context.Bloodsugars.Add(bloodsugar);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBloodsugarsByPatientId), new { patientId = bloodsugar.PatientID }, bloodsugar);
        }

        // modify a bloodsugar record with a specific  id
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBloodsugar(int id, [FromBody] BloodsugarInputModel bloodsugarInputModel)
        {
            var bloodsugar = await _context.Bloodsugars.FindAsync(id);
            if (bloodsugar == null)
            {
                return NotFound();
            }

            bloodsugar.PatientID = bloodsugarInputModel.PatientID;
            bloodsugar.BloodsugarLevel = bloodsugarInputModel.BloodsugarLevel;
            bloodsugar.Date = bloodsugarInputModel.Date;

            await _context.SaveChangesAsync();

            return Ok(bloodsugar);
        }

    }
}