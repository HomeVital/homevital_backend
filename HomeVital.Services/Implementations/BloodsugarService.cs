using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using AutoMapper;

namespace HomeVital.Services
{
    public class BloodsugarService : IBloodsugarService
    {
        private readonly IBloodsugarRepository _bloodsugarRepository;
        private readonly IMapper _mapper;

        public BloodsugarService(IBloodsugarRepository bloodsugarRepository, IMapper mapper)
        {
            _bloodsugarRepository = bloodsugarRepository;
            _mapper = mapper;
        }

        public async Task<BloodsugarDto> CreateBloodsugarAsync(BloodsugarInputModel bloodsugar)
        {
            var newBloodsugar = await _bloodsugarRepository.CreateBloodsugarAsync(bloodsugar);
            return _mapper.Map<BloodsugarDto>(newBloodsugar);
        }

        public async Task<BloodsugarDto[]> GetBloodsugarsByPatientId(int patientId)
        {
            var bloodsugars = await _bloodsugarRepository.GetBloodsugarsByPatientId(patientId);
            return _mapper.Map<BloodsugarDto[]>(bloodsugars);
        }

        public async Task<BloodsugarDto?> UpdateBloodsugarAsync(int id, BloodsugarInputModel updatedBloodsugar)
        {
            var bloodsugar = await _bloodsugarRepository.UpdateBloodsugarAsync(id, updatedBloodsugar);
            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        public async Task<BloodsugarDto?> GetBloodsugarByIdAsync(int id)
        {
            var bloodsugar = await _bloodsugarRepository.GetBloodsugarByIdAsync(id);
            return _mapper.Map<BloodsugarDto>(bloodsugar);
        }

        public async Task<bool> DeleteBloodsugarAsync(int id)
        {
            return await _bloodsugarRepository.DeleteBloodsugarAsync(id);
        }
    }
}