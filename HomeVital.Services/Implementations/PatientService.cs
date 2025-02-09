// service for patient

using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;
using AutoMapper;

namespace HomeVital.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PatientDto> CreatePatientAsync(PatientInputModel patient)
        {
            var newPatient = await _patientRepository.CreatePatientAsync(patient);
            return _mapper.Map<PatientDto>(newPatient);
        }

        public async Task<PatientDto[]> GetPatientsAsync()
        {
            var patients = await _patientRepository.GetPatientsAsync();
            return _mapper.Map<PatientDto[]>(patients);
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(id);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto?> UpdatePatientAsync(int id, PatientInputModel updatedPatient)
        {
            var patient = await _patientRepository.UpdatePatientAsync(id, updatedPatient);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            return await _patientRepository.DeletePatientAsync(id);
        }
    }
}
