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

        public async Task<IEnumerable<BloodPressureDto>> GetBloodPressuresByPatientId(int patientId)
        {
            var bloodPressures = await _bloodPressureRepository.GetBloodPressuresByPatientId(patientId);
            return _mapper.Map<IEnumerable<BloodPressureDto>>(bloodPressures);
        }

        public async Task<BloodPressureDto> CreateBloodPressure(int patientId, BloodPressureInputModel bloodPressureInputModel)
        {
            return await _bloodPressureRepository.CreateBloodPressure(patientId, bloodPressureInputModel);
        }

        public async Task<BloodPressureDto> UpdateBloodPressure(int id, BloodPressureInputModel bloodPressureInputModel)
        {
            // get the blood pressure record by id 
            var bloodPressure = await _bloodPressureRepository.GetBloodPressureById(id);
            // if the blood pressure record is not found, throw an exception
            if (bloodPressure == null)
            {
                throw new ArgumentException("Blood pressure record not found");
            }
            
            return await _bloodPressureRepository.UpdateBloodPressure(id, bloodPressureInputModel);
        }

        public async Task<BloodPressureDto> DeleteBloodPressure(int id)
        {
            return await _bloodPressureRepository.DeleteBloodPressure(id);
        }

    }
}