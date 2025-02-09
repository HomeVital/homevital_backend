using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IHealthcareWorkerRepository
    {
        // GetHealthcareWorkers
        Task<IEnumerable<HealthcareWorkerDto>> GetHealthcareWorkers();
        // GetHealthcareWorkerById
        Task<HealthcareWorkerDto> GetHealthcareWorkerById(int id);
        // DeleteHealthcareWorker
        Task<HealthcareWorkerDto> DeleteHealthcareWorker(int id);
        // CreateHealthcareWorker
        Task<HealthcareWorkerDto> CreateHealthcareWorker(HealthcareWorkerInputModel healthcareWorker);
        // UpdateHealthcareWorker
        Task<HealthcareWorkerDto> UpdateHealthcareWorker(int id, HealthcareWorkerInputModel healthcareWorker);
    }
}