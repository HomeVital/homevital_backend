using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<PatientDto> CreatePatient(PatientInputModel patient);
        // GetPatients
        Task<IEnumerable<PatientDto>> GetPatients();
        // GetPatientById
        Task<PatientDto> GetPatientById(int id);
        // DeletePatient
        Task<PatientDto> DeletePatient(int id);
        // UpdatePatient
        Task<Patient> UpdatePatient(int id, PatientInputModel patient);
        // GetPatientsByIds
        Task<List<Patient>> GetPatientsByIdsAsync(IEnumerable<int> ids);
    }
}