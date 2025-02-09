// interface for the healthcare worker service
using System.Threading.Tasks;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Services.Interfaces
{
    public interface IHealthcareWorkerService
    {
        Task<HealthcareWorkerDto> CreateHealthcareWorkerAsync(HealthcareWorkerInputModel healthcareWorker);
        Task<HealthcareWorkerDto[]> GetHealthcareWorkersByPatientId(int patientId);
        Task<HealthcareWorkerDto?> UpdateHealthcareWorkerAsync(int id, HealthcareWorkerInputModel updatedHealthcareWorker);
        Task<HealthcareWorkerDto?> GetHealthcareWorkerByIdAsync(int id);
        Task<bool> DeleteHealthcareWorkerAsync(int id);
    }
}