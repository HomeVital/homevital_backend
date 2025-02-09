
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
       

    }
}