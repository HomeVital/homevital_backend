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
                CreatedBy = "System", // Replace with actual user ID or name
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = "System", // Replace with actual user ID or name
                UpdatedDate = DateTime.UtcNow,

            };

            _dbContext.PatientPlans.Add(patientPlan);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PatientPlanDto>(patientPlan);
        }

        public async Task<PatientPlanDto> GetPatientPlanByIdAsync(int id)
        {
            var patientPlan = await _dbContext.PatientPlans
                .Include(p => p.Patient)
                .Include(p => p.MeasurementPlans)
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
                .Include(p => p.MeasurementPlans)
                .Where(p => p.PatientID == patientId)
                .ToListAsync();

            return _mapper.Map<List<PatientPlanDto>>(patientPlans);
        }


    }
}