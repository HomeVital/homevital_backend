using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using HomeVital.Models.Entities;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Implementations
{
    public class PatientPlanRepository : IPatientPlanRepository
    {
        private readonly HomeVitalDbContext _dbContext;
        private readonly IMapper _mapper;

        public PatientPlanRepository(HomeVitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PatientPlanDto> CreatePatientPlanAsync(PatientPlanInputModel patientPlanInputModel, int patientId)
        {
            var patientPlan = new PatientPlan
            {
                Name = patientPlanInputModel.Name,
                StartDate = patientPlanInputModel.StartDate,
                EndDate = patientPlanInputModel.EndDate,
                PatientID = patientId,
                Instructions = patientPlanInputModel.Instructions,
                TeamID = patientPlanInputModel.TeamID,
                WeightMeasurementDays = patientPlanInputModel.WeightMeasurementDays,
                BloodPressureMeasurementDays = patientPlanInputModel.BloodPressureMeasurementDays,
                BloodSugarMeasurementDays = patientPlanInputModel.BloodSugarMeasurementDays,
                OxygenSaturationMeasurementDays = patientPlanInputModel.OxygenSaturationMeasurementDays,
                BodyTemperatureMeasurementDays = patientPlanInputModel.BodyTemperatureMeasurementDays
            };

            var teamExists = await _dbContext.Teams.AnyAsync(t => t.ID == patientPlanInputModel.TeamID);
            if (!teamExists)
            {
                throw new ArgumentException("Invalid TeamID provided.");
            }

            var patient = await _dbContext.Patients
                .Include(p => p.PatientPlans)
                .FirstOrDefaultAsync(p => p.ID == patientId);
            if (patient == null)
            {
                throw new ArgumentException($"Patient with ID {patientId} does not exist.");
            }
            

            _dbContext.PatientPlans.Add(patientPlan);

            patient.UpdateStatusBasedOnPlans();

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PatientPlanDto>(patientPlan);
        }

        public async Task<PatientPlanDto> GetPatientPlanByIdAsync(int id)
        {
            var patientPlan = await _dbContext.PatientPlans
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (patientPlan == null)
            {
                throw new KeyNotFoundException($"Patient plan with ID {id} was not found.");
            }

            return _mapper.Map<PatientPlanDto>(patientPlan);
        }
        public async Task<List<PatientPlanDto>> GetPatientPlansByPatientIdAsync(int patientId)
        {
            var patientPlans = await _dbContext.PatientPlans
                .Include(p => p.Patient)
                .Where(p => p.PatientID == patientId)
                .ToListAsync();

            return _mapper.Map<List<PatientPlanDto>>(patientPlans);
        }


    }
}