// using Microsoft.AspNetCore.Mvc;
// using HomeVital.Models.InputModels;
// using HomeVital.Models.Dtos;
// using HomeVital.Services.Interfaces;




// namespace HomeVital.API.Controllers
// {
//     // [Authorize]
//     [ApiController]
//     [Route("api/vitalrange")]
//     public class VitalRangeController : ControllerBase
//     {
        
//         private readonly IVitalRangeService _vitalRangeService;

//         public VitalRangeController(IVitalRangeService vitalRangeService)
//         {
//             _vitalRangeService = vitalRangeService;
//         }

//         // Get all vital ranges by patient ID
//         [HttpGet("{patientId}")]
//         public async Task<ActionResult<IEnumerable<VitalRangeDto>>> GetVitalRangesByPatientIdAsync(int patientId)
//         {
//             var vitalRanges = await _vitalRangeService.GetVitalRangesByPatientId(patientId);
//             return Ok(vitalRanges);
//         }

//         // Create a new vital range record for a patient
        

//         // Update a vital range record by ID
//         [HttpPatch("{id}")]
//         public async Task<ActionResult<VitalRangeDto>> UpdateVitalRangeAsync(int id, VitalRangeInputModel vitalRangeInputModel)
//         {
//             if (!ModelState.IsValid)
//             {
//                 throw new System.ArgumentException("Invalid input model");
//             }

//             var updatedVitalRange = await _vitalRangeService.UpdateVitalRange(id, vitalRangeInputModel);
//             return Ok(updatedVitalRange);
//         }

//         // Delete a vital range record by ID
    
//     }
// }