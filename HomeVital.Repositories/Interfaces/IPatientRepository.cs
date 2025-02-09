using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<PatientDto> CreatePatientAsync(PatientInputModel patient);
        Task<PatientDto[]> GetPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<bool> DeletePatientAsync(int id);
        Task<PatientDto?> UpdatePatientAsync(int id, PatientInputModel updatedPatient);
    }
}