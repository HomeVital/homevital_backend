// interface for the healthcare worker service
using System.Threading.Tasks;
using HomeVital.Models.Entities;

namespace HomeVital.Services.Interfaces
{
    public interface IHealthcareWorkerService
    {
        Task<HealthcareWorker?> GetHealthcareWorkerByIdAsync(int id);
        Task<HealthcareWorker> CreateHealthcareWorkerAsync(HealthcareWorker worker);
        Task<bool> DeleteHealthcareWorkerAsync(int id);
        Task<HealthcareWorker?> UpdateHealthcareWorkerAsync(int id, HealthcareWorker updatedWorker);
    }
}