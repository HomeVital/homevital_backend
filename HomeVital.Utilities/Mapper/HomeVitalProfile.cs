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
            CreateMap<PatientInputModel, Patient>();
            CreateMap<HealthcareWorker, HealthcareWorkerDto>()
                .ForMember(dest => dest.TeamIDs, opt => opt.MapFrom(src => src.Teams.Select(t => t.ID).ToList()));
            CreateMap<HealthcareWorkerInputModel, HealthcareWorker>();
            CreateMap<BloodPressureInputModel, BloodPressure>();
            CreateMap<BloodsugarInputModel, Bloodsugar>();
            // CreateMap<MeasurementDto, Measurement>();
            // CreateMap<Measurement, MeasurementDto>();
            CreateMap<BloodPressure, MeasurementDto>();
            CreateMap<Bloodsugar, MeasurementDto>();
            // CreateMap<Measurement, MeasurementDto>();

            CreateMap<BodyWeight, BodyWeightDto>();
            CreateMap<BodyWeightInputModel, BodyWeight>();
            CreateMap<BodyTemperature, BodyTemperatureDto>();
            CreateMap<BodyTemperatureInputModel, BodyTemperature>();

            CreateMap<OxygenSaturation, OxygenSaturationDto>();
            CreateMap<OxygenSaturationInputModel, OxygenSaturation>();

            CreateMap<BodyTemperatureRange, BodyTemperatureRangeDto>();
            CreateMap<BodyTemperatureRangeInputModel, BodyTemperatureRange>();
            CreateMap<BloodPressureRange, BloodPressureRangeDto>();
            CreateMap<BloodPressureRangeInputModel, BloodPressureRange>();
            CreateMap<BloodSugarRange, BloodSugarRangeDto>();
            CreateMap<BloodSugarRangeInputModel, BloodSugarRange>();
            CreateMap<BodyWeightRange, BodyWeightRangeDto>();
            CreateMap<BodyWeightRangeInputModel, BodyWeightRange>();
            CreateMap<OxygenSaturationRange, OxygenSaturationRangeDto>();
            CreateMap<OxygenSaturationRangeInputModel, OxygenSaturationRange>();
            
            CreateMap<Team, TeamDto>();
            CreateMap<TeamInputModel, Team>();
            CreateMap<TeamDto, Team>();
        
            CreateMap<PatientPlan, PatientPlanDto>();
            CreateMap<PatientPlanInputModel, PatientPlan>();
            CreateMap<PatientPlanDto, PatientPlan>();
            
            }
    }
}