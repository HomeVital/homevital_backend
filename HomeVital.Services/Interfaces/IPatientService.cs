using System.Threading.Tasks;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;


namespace HomeVital.Services.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(PatientInputModel patient);
        Task<PatientDto[]> GetPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<PatientDto?> UpdatePatientAsync(int id, PatientInputModel updatedPatient);
        Task<bool> DeletePatientAsync(int id);
    }
}