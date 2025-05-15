using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using AutoMapper;
using HomeVital.Models.Entities;
using HomeVital.Models.Exceptions;

namespace HomeVital.Services
{
    public class OxygenSaturationService : IOxygenSaturationService
    {
        private readonly IOxygenSaturationRepository _oxygenSaturationRepository;
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;

        public OxygenSaturationService(IOxygenSaturationRepository oxygenSaturationRepository, IMapper mapper, IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            _oxygenSaturationRepository = oxygenSaturationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OxygenSaturationDto>> GetOxygenSaturationsByPatientId(int patientId)
        {
            // check if the patient exists
            var patient = await _patientRepository.GetPatientById(patientId);
            if (patient == null)
            {
                throw new ResourceNotFoundException("Patient not found with ID: " + patientId);
            }
            var oxygenSaturations = await _oxygenSaturationRepository.GetOxygenSaturationsByPatientId(patientId);
            return _mapper.Map<IEnumerable<OxygenSaturationDto>>(oxygenSaturations);
        }

        public async Task<OxygenSaturationDto> CreateOxygenSaturation(int patientId, OxygenSaturationInputModel oxygenSaturationInputModel)
        {
            return await _oxygenSaturationRepository.CreateOxygenSaturation(patientId, oxygenSaturationInputModel);
        }

        public async Task<OxygenSaturationDto> UpdateOxygenSaturation(int id, OxygenSaturationInputModel oxygenSaturationInputModel)
        {
            var oxygenSaturation = await _oxygenSaturationRepository.GetOxygenSaturationById(id);

            if (oxygenSaturation == null)
            {
                throw new ResourceNotFoundException("Oxygen saturation record not found with ID: " + id);
            }
            return await _oxygenSaturationRepository.UpdateOxygenSaturation(id, oxygenSaturationInputModel);
        }

        public async Task<OxygenSaturationDto> DeleteOxygenSaturation(int id)
        {
            return await _oxygenSaturationRepository.DeleteOxygenSaturation(id);
        }

        public async Task<OxygenSaturationDto> GetOxygenSaturationById(int id)
        {
            var oxygenSaturation = await _oxygenSaturationRepository.GetOxygenSaturationById(id);
            if (oxygenSaturation == null)
            {
                throw new ResourceNotFoundException("Oxygen saturation record not found with ID: " + id);
            }
            return _mapper.Map<OxygenSaturationDto>(oxygenSaturation);
        }
    }
}