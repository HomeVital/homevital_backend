using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;


namespace HomeVital.Services.Interfaces
{
    public interface IPatientPlanService
    {
        // Create a new patient plan
        Task<PatientPlanDto> CreatePatientPlanAsync(int patientId, PatientPlanInputModel patientPlanInputModel);

        // Get a patient plan by ID
        Task<PatientPlanDto> GetPatientPlanByIdAsync(int id);

        // Get all patient plans for a specific patient
        Task<List<PatientPlanDto>> GetPatientPlansByPatientIdAsync(int patientId);
    }
}