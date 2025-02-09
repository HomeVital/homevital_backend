using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<PatientDto> CreatePatient(PatientInputModel patient);
        
    }
}