// interface for the healthcare worker service
using System.Threading.Tasks;
using HomeVital.Models.Entities;

// namespace HomeVital.Services.Interfaces
// {
//     public interface IHealthcareWorkerService
//     {
//         Task<HealthcareWorker> GetHealthcareWorkerByIdAsync(int id);
//         Task<HealthcareWorker> CreateHealthcareWorkerAsync(HealthcareWorker worker);
//     }
// }


namespace HomeVital.Services.Interfaces
{
    public interface IHealthcareWorkerService
    {
        Task<HealthcareWorker?> GetHealthcareWorkerByIdAsync(int id);
        Task<HealthcareWorker> CreateHealthcareWorkerAsync(HealthcareWorker worker);
    }
}