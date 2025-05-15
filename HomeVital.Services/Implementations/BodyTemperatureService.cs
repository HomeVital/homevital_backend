using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using AutoMapper;

namespace HomeVital.Services
{
    public class BodyTemperatureService : IBodyTemperatureService
    {
        private readonly IBodyTemperatureRepository _bodyTemperatureRepository;
        private readonly IMapper _mapper;

        public BodyTemperatureService(IBodyTemperatureRepository bodyTemperatureRepository, IMapper mapper)
        {
            _bodyTemperatureRepository = bodyTemperatureRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BodyTemperatureDto>> GetBodyTemperaturesByPatientId(int patientId)
        {
            var bodyTemperatures = await _bodyTemperatureRepository.GetBodyTemperaturesByPatientId(patientId);
            return _mapper.Map<IEnumerable<BodyTemperatureDto>>(bodyTemperatures);
        }

        public async Task<BodyTemperatureDto> CreateBodyTemperature(int patientId, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            return await _bodyTemperatureRepository.CreateBodyTemperature(patientId, bodyTemperatureInputModel);
        }

        public async Task<BodyTemperatureDto> UpdateBodyTemperature(int id, BodyTemperatureInputModel bodyTemperatureInputModel)
        {
            return await _bodyTemperatureRepository.UpdateBodyTemperature(id, bodyTemperatureInputModel);
        }

        public async Task<BodyTemperatureDto> DeleteBodyTemperature(int id)
        {
            return await _bodyTemperatureRepository.DeleteBodyTemperature(id);
        }
           
    }
    }