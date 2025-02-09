using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using System.Threading.Tasks;
using AutoMapper;

namespace HomeVital.Services
{
    public class BloodPressureService : IBloodPressureService
    {
        private readonly IBloodPressureRepository _bloodPressureRepository;
        private readonly IMapper _mapper;

        public BloodPressureService(IBloodPressureRepository bloodPressureRepository, IMapper mapper)
        {
            _bloodPressureRepository = bloodPressureRepository;
            _mapper = mapper;
        }

        public async Task<BloodPressure?> GetBloodPressureByIdAsync(int id)
        {
            var bloodPressureDto = await _bloodPressureRepository.GetBloodPressureByIdAsync(id);
            return _mapper.Map<BloodPressure?>(bloodPressureDto);
        }

        public async Task<BloodPressure> CreateBloodPressureAsync(BloodPressureInputModel bloodPressureInputModel)
        {
            var bloodPressureDto = await _bloodPressureRepository.CreateBloodPressure(bloodPressureInputModel);
            return _mapper.Map<BloodPressure>(bloodPressureDto);
        }

        public async Task<BloodPressure?> UpdateBloodPressureAsync(int id, BloodPressureInputModel updatedBloodPressureInputModel)
        {
            var bloodPressureDto = await _bloodPressureRepository.UpdateBloodPressureAsync(id, updatedBloodPressureInputModel);
            return _mapper.Map<BloodPressure?>(bloodPressureDto);
        }

        public async Task<bool> DeleteBloodPressureAsync(int id)
        {
            return await _bloodPressureRepository.DeleteBloodPressureAsync(id);
        }

        
        public async Task<BloodPressure?> GetBloodPressureByUserIdAsync(int userId)
        {
            var bloodPressureDto = await _bloodPressureRepository.GetBloodPressureByUserIdAsync(userId);
            return _mapper.Map<BloodPressure?>(bloodPressureDto);
        }
    }
}