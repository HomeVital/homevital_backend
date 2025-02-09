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

        public async Task<IEnumerable<BloodsugarDto>> GetBloodsugarsByPatientId(int patientId)
        {
            var bloodsugars = await _bloodsugarRepository.GetBloodsugarsByPatientId(patientId);
            return _mapper.Map<IEnumerable<BloodsugarDto>>(bloodsugars);
        }

        public async Task<BloodsugarDto> CreateBloodsugar(int patientId, BloodsugarInputModel bloodsugarInputModel)
        {
            return await _bloodsugarRepository.CreateBloodsugar(patientId, bloodsugarInputModel);
        }

        public async Task<BloodsugarDto> UpdateBloodsugar(int id, BloodsugarInputModel bloodsugarInputModel)
        {
            return await _bloodsugarRepository.UpdateBloodsugar(id, bloodsugarInputModel);
        }

        public async Task<BloodsugarDto> DeleteBloodsugar(int id)
        {
            return await _bloodsugarRepository.DeleteBloodsugar(id);
        }



    }
}