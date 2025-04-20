using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IPatientPlanRepository
    {
        // Create a new patient plan
        Task<PatientPlanDto> CreatePatientPlanAsync(PatientPlanInputModel patientPlanInputModel, int patientId);
        Task<PatientPlanDto> GetPatientPlanByIdAsync(int id);

        // Get all patient plans for a specific patient
        Task<List<PatientPlanDto>> GetPatientPlansByPatientIdAsync(int patientId);
    }
}