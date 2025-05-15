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

        public async Task<IEnumerable<HealthcareWorkerDto>> GetHealthcareWorkers()
        {
            var healthcareWorkers = await _healthcareWorkerRepository.GetHealthcareWorkers();
            return _mapper.Map<IEnumerable<HealthcareWorkerDto>>(healthcareWorkers);
        }

        public async Task<HealthcareWorkerDto> GetHealthcareWorkerById(int id)
        {
            var healthcareWorker = await _healthcareWorkerRepository.GetHealthcareWorkerById(id);
            return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);
        }

        public async Task<HealthcareWorkerDto> DeleteHealthcareWorker(int id)
        {
            var healthcareWorker = await _healthcareWorkerRepository.GetHealthcareWorkerById(id);
            await _healthcareWorkerRepository.DeleteHealthcareWorker(id);
            return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);

        }

        public async Task<HealthcareWorkerDto> CreateHealthcareWorker(HealthcareWorkerInputModel healthcareWorker)
        {
            return await _healthcareWorkerRepository.CreateHealthcareWorker(healthcareWorker);
        }

        public async Task<HealthcareWorkerDto> UpdateHealthcareWorker(int id, HealthcareWorkerInputModel healthcareWorker)
        {
            return await _healthcareWorkerRepository.UpdateHealthcareWorker(id, healthcareWorker);
        }
        
    }
}
   