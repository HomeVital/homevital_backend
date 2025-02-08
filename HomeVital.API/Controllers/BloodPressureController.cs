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
    [Route("api/bloodpressure")]
    public class BloodPressureController : ControllerBase
    {
        private readonly HomeVitalDbContext _context;

        public BloodPressureController(HomeVitalDbContext context)
        {
            _context = context;
        }

        // get bloodpressures by patient id
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetBloodPressuresByPatientId(int patientId)
        {
            var bloodpressures = await _context.BloodPressures.Where(b => b.PatientID == patientId).ToListAsync();
            var bloodpressureDtos = bloodpressures.Select(bloodpressure => new BloodPressureDto
            {
                ID = bloodpressure.ID,
                PatientID = bloodpressure.PatientID,
                Systolic = bloodpressure.Systolic,
                Diastolic = bloodpressure.Diastolic,
                Date = bloodpressure.Date
            });

            return Ok(bloodpressureDtos);
        }

        // make a new bloodpressure record
        [HttpPost]
        public async Task<IActionResult> CreateBloodPressure([FromBody] BloodPressureInputModel bloodpressureInputModel)
        {
            var bloodpressure = new BloodPressure
            {
                PatientID = bloodpressureInputModel.PatientID,
                Systolic = bloodpressureInputModel.Systolic,
                Diastolic = bloodpressureInputModel.Diastolic,
                Date = bloodpressureInputModel.Date
            };

            _context.BloodPressures.Add(bloodpressure);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBloodPressuresByPatientId), new { patientId = bloodpressure.PatientID }, bloodpressure);
        }

        // modify a bloodpressure record with a specific  id
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBloodPressure(int id, [FromBody] BloodPressureInputModel bloodpressureInputModel)
        {
            var bloodpressure = await _context.BloodPressures.FindAsync(id);
            if (bloodpressure == null)
            {
                return NotFound();
            }

            bloodpressure.Systolic = bloodpressureInputModel.Systolic;
            bloodpressure.Diastolic = bloodpressureInputModel.Diastolic;
            bloodpressure.Date = bloodpressureInputModel.Date;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // delete a bloodpressure record with a specific id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodPressure(int id)
        {
            var bloodpressure = await _context.BloodPressures.FindAsync(id);
            if (bloodpressure == null)
            {
                return NotFound();
            }

            _context.BloodPressures.Remove(bloodpressure);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}