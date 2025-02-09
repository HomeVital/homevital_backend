using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using Microsoft.EntityFrameworkCore;
using HomeVital.Services.Interfaces;


namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/bloodsugar")]
    public class BloodsugarsController : ControllerBase
    {
            
            private readonly IBloodsugarService _bloodsugarService;
    
            public BloodsugarsController(IBloodsugarService bloodsugarService)
            {
                _bloodsugarService = bloodsugarService;
            }

            // Get all bloodsugars by patient ID
            [HttpGet("{patientId}")]
            public async Task<ActionResult<IEnumerable<BloodsugarDto>>> GetBloodsugarsByPatientIdAsync(int patientId)
            {
                var bloodsugars = await _bloodsugarService.GetBloodsugarsByPatientId(patientId);
                return Ok(bloodsugars);
            }

            // Create a new bloodsugar record for a patient
            [HttpPost("{patientId}")]
            public async Task<ActionResult<BloodsugarDto>> CreateBloodsugarAsync(int patientId, BloodsugarInputModel bloodsugarInputModel)
            {
                if (!ModelState.IsValid)
                {
                    throw new System.ArgumentException("Invalid input model");
                }

                var newBloodsugar = await _bloodsugarService.CreateBloodsugar(patientId, bloodsugarInputModel);
                return Ok(newBloodsugar);
            }

            // Update a bloodsugar record by ID
            [HttpPatch("{id}")]
            public async Task<ActionResult<BloodsugarDto>> UpdateBloodsugarAsync(int id, BloodsugarInputModel bloodsugarInputModel)
            {
                if (!ModelState.IsValid)
                {
                    throw new System.ArgumentException("Invalid input model");
                }

                var updatedBloodsugar = await _bloodsugarService.UpdateBloodsugar(id, bloodsugarInputModel);
                return Ok(updatedBloodsugar);
            }

            // Delete a bloodsugar record by ID
            [HttpDelete("{id}")]
            public async Task<ActionResult<BloodsugarDto>> DeleteBloodsugarAsync(int id)
            {
                if (!ModelState.IsValid)
                {
                    throw new System.ArgumentException("Invalid input model");
                }

                var bloodsugar = await _bloodsugarService.DeleteBloodsugar(id);
                return Ok(bloodsugar);
            }
            

    }
}