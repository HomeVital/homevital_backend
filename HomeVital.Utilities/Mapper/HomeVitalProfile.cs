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
            CreateMap<PatientDto, Patient>();
            CreateMap<HealthcareWorker, HealthcareWorkerDto>()
                .ForMember(dest => dest.TeamIDs, opt => opt.MapFrom(src => src.Teams.Select(t => t.ID).ToList()));
            CreateMap<HealthcareWorkerInputModel, HealthcareWorker>();
            CreateMap<HealthcareWorkerDto, HealthcareWorker>();
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
            CreateMap<TeamDto, Team>();
            // CreateMap<TeamInputModel, Team>()
            // .ForMember(dest => dest.WorkerIDs, opt => opt.MapFrom(src => (src.WorkerIDs ?? Enumerable.Empty<int>()).ToList()))
            // .ForMember(dest => dest.Patients, opt => opt.Ignore()) // Adjust as needed
            // .ForMember(dest => dest.HealthcareWorkers, opt => opt.Ignore()); // Adjust as needed
            CreateMap<TeamInputModel, Team>()
                .ForMember(dest => dest.WorkerIDs, opt => opt.Ignore()) // Ignore computed property
                .ForMember(dest => dest.PatientIDs, opt => opt.Ignore()) // Ignore computed property
                .ForMember(dest => dest.HealthcareWorkers, opt => opt.Ignore()) // Explicitly ignore navigation property
                .ForMember(dest => dest.Patients, opt => opt.Ignore());  
            CreateMap<PatientPlan, PatientPlanDto>();
            CreateMap<PatientPlanInputModel, PatientPlan>();
            CreateMap<PatientPlanDto, PatientPlan>();

            
            }
    }
}