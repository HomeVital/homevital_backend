using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using AutoMapper;
using HomeVital.Models.Entities;

namespace HomeVital.Services
{
    public class OxygenSaturationService : IOxygenSaturationService
    {
        private readonly IOxygenSaturationRepository _oxygenSaturationRepository;
        private readonly IMapper _mapper;

        public OxygenSaturationService(IOxygenSaturationRepository oxygenSaturationRepository, IMapper mapper)
        {
            _oxygenSaturationRepository = oxygenSaturationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OxygenSaturationDto>> GetOxygenSaturationsByPatientId(int patientId)
        {
            var oxygenSaturations = await _oxygenSaturationRepository.GetOxygenSaturationsByPatientId(patientId);
            return _mapper.Map<IEnumerable<OxygenSaturationDto>>(oxygenSaturations);
        }

        public async Task<OxygenSaturationDto> CreateOxygenSaturation(int patientId, OxygenSaturationInputModel oxygenSaturationInputModel)
        {
            var oxygenSaturation = _mapper.Map<OxygenSaturation>(oxygenSaturationInputModel);
            oxygenSaturation.PatientID = patientId;
            var createdOxygenSaturation = await _oxygenSaturationRepository.CreateOxygenSaturation(patientId, oxygenSaturationInputModel);
            return _mapper.Map<OxygenSaturationDto>(createdOxygenSaturation);
        }

        public async Task<OxygenSaturationDto> UpdateOxygenSaturation(int id, OxygenSaturationInputModel oxygenSaturationInputModel)
        {
            var oxygenSaturation = _mapper.Map<OxygenSaturation>(oxygenSaturationInputModel);
            oxygenSaturation.ID = id;
            var updatedOxygenSaturation = await _oxygenSaturationRepository.UpdateOxygenSaturation(id, oxygenSaturationInputModel);
            return _mapper.Map<OxygenSaturationDto>(updatedOxygenSaturation);
        }

        public async Task<OxygenSaturationDto> DeleteOxygenSaturation(int id)
        {
            var deletedOxygenSaturation = await _oxygenSaturationRepository.DeleteOxygenSaturation(id);
            return _mapper.Map<OxygenSaturationDto>(deletedOxygenSaturation);
        }
    }
}