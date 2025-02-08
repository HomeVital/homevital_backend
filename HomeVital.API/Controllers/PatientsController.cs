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
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly HomeVitalDbContext _context;

        public PatientsController(HomeVitalDbContext context)
        {
            _context = context;
        }

        // get all patients
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            var patientDtos = patients.Select(patient => new PatientDto
            {
                ID = patient.ID,
                Name = patient.Name,
                Phone = patient.Phone,
                TeamID = patient.TeamID,
                Status = patient.Status
            });

            return Ok(patientDtos);
        }

        // get patient by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById([FromQuery] int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            var patientDto = new PatientDto
            {
                ID = patient.ID,
                Name = patient.Name,
                Phone = patient.Phone,
                TeamID = patient.TeamID,
                Status = patient.Status
            };

            return Ok(patientDto);
        }
        
        // create a patient
        [HttpPost("create")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientInputModel patientInputModel)
        {
            var patient = new Patient
            {
                Name = patientInputModel.Name,
                Phone = patientInputModel.Phone,
                TeamID = patientInputModel.TeamID,
                Status = patientInputModel.Status
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return Ok(patient);
        }

        // update a patient
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatient([FromQuery] int id, [FromBody] PatientInputModel patientInputModel)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            patient.Name = patientInputModel.Name;
            patient.Phone = patientInputModel.Phone;
            patient.TeamID = patientInputModel.TeamID;
            patient.Status = patientInputModel.Status;

            await _context.SaveChangesAsync();

            return Ok(patient);
        }

        // delete a patient
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromQuery] int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}