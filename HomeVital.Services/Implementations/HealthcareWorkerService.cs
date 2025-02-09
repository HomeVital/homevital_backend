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
        
    }
}
   