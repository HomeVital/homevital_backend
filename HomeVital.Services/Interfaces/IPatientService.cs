using HomeVital.Models.Dtos;
using HomeVital.Models.Entities;
using HomeVital.Models.InputModels;

namespace HomeVital.Services.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatient(PatientInputModel patient);
        
    }
        
}