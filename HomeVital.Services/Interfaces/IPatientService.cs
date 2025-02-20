using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using HomeVital.Models.InputModels;

namespace HomeVital.Services.Interfaces
{
    public interface IPatientService
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