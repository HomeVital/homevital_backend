using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IHealthcareWorkerRepository
    {
        // input models
        Task<HealthcareWorkerDto> CreateHealthcareWorkerAsync(HealthcareWorkerInputModel healthcareWorker);
        Task<HealthcareWorkerDto[]> GetHealthcareWorkersAsync();
        Task<HealthcareWorkerDto?> GetHealthcareWorkerByIdAsync(int id);
        Task<bool> DeleteHealthcareWorkerAsync(int id);
        Task<HealthcareWorkerDto?> UpdateHealthcareWorkerAsync(int id, HealthcareWorkerInputModel updatedHealthcareWorker);
    }
}