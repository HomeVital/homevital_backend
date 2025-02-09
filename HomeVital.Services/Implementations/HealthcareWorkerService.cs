using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using AutoMapper;

namespace HomeVital.Services
{
    public class HealthcareWorkerService : IHealthcareWorkerService
    {
        private readonly IMapper _mapper;
        private readonly IHealthcareWorkerRepository _healthcareWorkerRepository;

        public HealthcareWorkerService(IMapper mapper, IHealthcareWorkerRepository healthcareWorkerRepository)
        {
            _mapper = mapper;
            _healthcareWorkerRepository = healthcareWorkerRepository;
        }
        
        public async Task<HealthcareWorkerDto> CreateHealthcareWorkerAsync(HealthcareWorkerInputModel healthcareWorker)
        {
            return  await _healthcareWorkerRepository.CreateHealthcareWorkerAsync(healthcareWorker);
        }

        public async Task<HealthcareWorkerDto[]> GetHealthcareWorkersByPatientId(int patientId)
        {
            var healthcareWorkers = await _healthcareWorkerRepository.GetHealthcareWorkerByIdAsync(patientId);
            return _mapper.Map<HealthcareWorkerDto[]>(healthcareWorkers);
        }

        public async Task<HealthcareWorkerDto?> UpdateHealthcareWorkerAsync(int id, HealthcareWorkerInputModel updatedHealthcareWorker)
        {
            var healthcareWorker = await _healthcareWorkerRepository.UpdateHealthcareWorkerAsync(id, updatedHealthcareWorker);
            return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);
        }

        public async Task<HealthcareWorkerDto?> GetHealthcareWorkerByIdAsync(int id)
        {
            var healthcareWorker = await _healthcareWorkerRepository.GetHealthcareWorkerByIdAsync(id);
            return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);
        }

        public async Task<bool> DeleteHealthcareWorkerAsync(int id)
        {
            return await _healthcareWorkerRepository.DeleteHealthcareWorkerAsync(id);
        }
    }
}