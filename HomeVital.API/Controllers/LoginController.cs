
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        // [HttpPost] // Create a new patient
        // public async Task<ActionResult<PatientDto>> CreatePatientAsync(PatientInputModel patientInputModel)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         throw new System.ArgumentException("Invalid input model");
        //     }

        //     var newPatient = await _patientService.CreatePatient(patientInputModel);
        //     return Ok(newPatient);
        // }

        // [HttpPost] // Login as a patient --> the endpoint takes in Kennitala  and returns a the patient's ID
        // public async Task<ActionResult<LoginDto>> LoginPatientAsync(LoginInputModel loginInputModel)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         throw new System.ArgumentException("Invalid input model");
        //     }

            
        // }


    }
}
