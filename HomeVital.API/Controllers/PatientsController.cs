
using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;


// namespace HomeVital.API.Controllers
// {
//     [ApiController]
//     [Route("api/patients")]
//     public class PatientsController : ControllerBase
//     {
//         private readonly IPatientService _patientService;

//         public PatientsController(IPatientService patientService)
//         {
//             _patientService = patientService;
//         }

//         [HttpGet]
//         public ActionResult GetPatients()
//         {
//             var patients = _patientService.GetPatientsAsync();
//             return Ok(patients);    
//         }

//         [HttpGet]
//         public ActionResult GetPatientByIdAsync(int id)
//         {
//             if (!ModelState.IsValid)
//             {
//                 throw new System.ArgumentException("Invalid input model");
//             }
//             var patient = _patientService.GetPatientByIdAsync(id);
//             return Ok(patient);
//         }

//         [HttpPost]
//         public ActionResult CreatePatientAsync(PatientInputModel patientInputModel)
//         {
//             if (!ModelState.IsValid)
//             {
//                 throw new System.ArgumentException("Invalid input model");
//             }

//             var newPatient = _patientService.CreatePatientAsync(patientInputModel);
//             return Ok(newPatient);
//         }

//         [HttpDelete]
//         public ActionResult DeletePatientAsync(int id)
//         {
//             if (!ModelState.IsValid)
//             {
//                 throw new System.ArgumentException("Invalid input model");
//             }
//             var patient = _patientService.DeletePatientAsync(id);
//             return Ok(patient);
//         }

//         [HttpPatch]
//         public ActionResult UpdatePatientAsync(int id, PatientInputModel patientInputModel)
//         {
//             if (!ModelState.IsValid)
//             {
//                 throw new System.ArgumentException("Invalid input model");
//             }
//             var updatedPatient = _patientService.UpdatePatientAsync(id, patientInputModel);
//             return Ok(updatedPatient);
//         }

//     }
// }

namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatientAsync(PatientInputModel patientInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var newPatient = await _patientService.CreatePatient(patientInputModel);
            return Ok(newPatient);
        }
    }
}