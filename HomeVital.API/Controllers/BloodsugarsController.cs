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
            
            [HttpGet]
            public ActionResult GetBloodSugarByIdAsync(int id)
            {
                if (!ModelState.IsValid)
                {
                    throw new System.ArgumentException("Invalid input model");
                }
                var bloodsugar = _bloodsugarService.GetBloodsugarByIdAsync(id);
                return Ok(bloodsugar);
            }
            [HttpPost]
            public ActionResult CreateBloodSugarAsync(BloodsugarInputModel bloodSugarInputModel)
            {
                if (!ModelState.IsValid)
                {
                    throw new System.ArgumentException("Invalid input model");
                }
                
                var newBloodsugar = _bloodsugarService.CreateBloodsugarAsync(bloodSugarInputModel);
                return Ok(newBloodsugar);
            }
    
            [HttpDelete]
            public ActionResult DeleteBloodSugarAsync(int id)
            {
                if (!ModelState.IsValid)
                {
                    throw new System.ArgumentException("Invalid input model");
                }
                var bloodsugar = _bloodsugarService.DeleteBloodsugarAsync(id);
                return Ok(bloodsugar);
            }
    
            [HttpPatch]
            public ActionResult UpdateBloodSugarAsync(int id, BloodsugarInputModel bloodSugarInputModel)
            {
                if (!ModelState.IsValid)
                {
                    throw new System.ArgumentException("Invalid input model");
                }
                
                var updatedBloodsugar = _bloodsugarService.UpdateBloodsugarAsync(id, bloodSugarInputModel);
                return Ok(updatedBloodsugar);
            }
        
        

    }
}