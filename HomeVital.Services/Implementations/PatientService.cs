// service for patient

using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.Dtos;

namespace HomeVital.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientDto> CreatePatient(PatientInputModel patient)
        {
            return await _patientRepository.CreatePatient(patient);
        }

        public async Task<IEnumerable<PatientDto>> GetPatients()
        {
            return await _patientRepository.GetPatients();
        }

        public async Task<PatientDto> GetPatientById(int id)
        {
            return await _patientRepository.GetPatientById(id);
        }

        public async Task<PatientDto> DeletePatient(int id)
        {
            return await _patientRepository.DeletePatient(id);
        }

        public async Task<PatientDto> UpdatePatient(int id, PatientInputModel patient)
        {
            return await _patientRepository.UpdatePatient(id, patient);
        }

    }
}