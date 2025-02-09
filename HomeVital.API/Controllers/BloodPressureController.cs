
using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using Microsoft.EntityFrameworkCore;
using HomeVital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;



namespace HomeVital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/bloodpressure")]
    public class BloodPressureController : ControllerBase
    {
        
        private readonly IBloodPressureService _bloodpressureService;

        public BloodPressureController(IBloodPressureService bloodpressureService)
        {
            _bloodpressureService = bloodpressureService;
        }
        
        [HttpGet]
        public ActionResult GetBloodPressureByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var bloodpressure = _bloodpressureService.GetBloodPressureByIdAsync(id);
            return Ok(bloodpressure);
        }
        [HttpPost]
        public ActionResult CreateBloodPressureAsync(BloodPressureInputModel bloodPressureInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var bloodpressure = _bloodpressureService.CreateBloodPressureAsync(bloodPressureInputModel);
            return Ok(bloodpressure);
        }

        [HttpDelete]
        public ActionResult DeleteBloodPressureAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var bloodpressure = _bloodpressureService.DeleteBloodPressureAsync(id);
            return Ok(bloodpressure);
        }

        [HttpPatch]
        public ActionResult UpdateBloodPressureAsync(int id, BloodPressureInputModel updatedBloodPressureInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var bloodpressure = _bloodpressureService.UpdateBloodPressureAsync(id, updatedBloodPressureInputModel);
            return Ok(bloodpressure);
        }

    }
}