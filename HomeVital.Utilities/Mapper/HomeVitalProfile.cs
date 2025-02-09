using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using HomeVital.Models.InputModels;


namespace HomeVital.Utilities.Mapper
{
    public class HomeVitalProfile : Profile
    {
        public HomeVitalProfile()
        {
            // Make the mapping
            CreateMap<User, UserDto>();
            CreateMap<BloodPressure, BloodPressureDto>();
            CreateMap<Bloodsugar, BloodsugarDto>();
            CreateMap<Patient, PatientDto>();
            CreateMap<HealthcareWorker, HealthcareWorkerDto>();
            CreateMap<HealthcareWorkerInputModel, HealthcareWorker>();


        }

    }
}