using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace HomeVital.Services
{
    public class PatientPlanService : IPatientPlanService
    {
        private readonly IPatientPlanRepository _patientPlanRepository;
        private readonly IMapper _mapper;

        public PatientPlanService(IPatientPlanRepository patientPlanRepository, IMapper mapper)
        {
            _patientPlanRepository = patientPlanRepository;
            _mapper = mapper;
        }

        public async Task<PatientPlanDto> CreatePatientPlanAsync( int patientId, PatientPlanInputModel patientPlanInputModel)
        {
            return await _patientPlanRepository.CreatePatientPlanAsync(patientPlanInputModel, patientId);
        }
    

        public async Task<PatientPlanDto> GetPatientPlanByIdAsync(int id)
        {
            var patientPlan = await _patientPlanRepository.GetPatientPlanByIdAsync(id);
            return _mapper.Map<PatientPlanDto>(patientPlan);
        }

        public async Task<List<PatientPlanDto>> GetPatientPlansByPatientIdAsync(int patientId)
        {
            var patientPlans = await _patientPlanRepository.GetPatientPlansByPatientIdAsync(patientId);
            return _mapper.Map<List<PatientPlanDto>>(patientPlans);
        }

    }
}