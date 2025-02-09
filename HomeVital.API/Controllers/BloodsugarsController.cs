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
            
            private readonly BloodsugarService _bloodsugarService;
    
            public BloodsugarsController(BloodsugarService bloodsugarService)
            {
                _bloodsugarService = bloodsugarService;
            }
            

    }
}