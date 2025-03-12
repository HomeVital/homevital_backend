using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using AutoMapper;

namespace HomeVital.Services
{
    public class OxygenSaturationService : IOxygenSaturationService
    {
        private readonly IOxygenSaturationRepository _oxygensaturationRepository;
        private readonly IMapper _mapper;

        public OxygenSaturationService(IOxygenSaturationRepository oxygensaturationRepository, IMapper mapper)
        {
            _oxygensaturationRepository = oxygensaturationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OxygenSaturationDto>> GetOxygenSaturationsByPatientId(int patientId)
        {
            var oxygensaturations = await _oxygensaturationRepository.GetOxygenSaturationsByPatientId(patientId);
            return _mapper.Map<IEnumerable<OxygenSaturationDto>>(oxygensaturations);
        }

        public async Task<OxygenSaturationDto> CreateOxygenSaturation(int patientId, OxygenSaturationInputModel oxygensaturationInputModel)
        {
            return await _oxygensaturationRepository.CreateOxygenSaturation(patientId, oxygensaturationInputModel);
        }

        public async Task<OxygenSaturationDto> UpdateOxygenSaturation(int id, OxygenSaturationInputModel oxygensaturationInputModel)
        {
            return await _oxygensaturationRepository.UpdateOxygenSaturation(id, oxygensaturationInputModel);
        }

        public async Task<OxygenSaturationDto> DeleteOxygenSaturation(int id)
        {
            return await _oxygensaturationRepository.DeleteOxygenSaturation(id);
        }



    }
}