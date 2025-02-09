using HomeVital.Models.Dtos;
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
        Task<PatientDto> UpdatePatient(int id, PatientInputModel patient);
    }
}