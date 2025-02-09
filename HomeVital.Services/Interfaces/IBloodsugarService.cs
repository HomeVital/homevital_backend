using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
using System.Threading.Tasks;

namespace HomeVital.Services.Interfaces
{
    public interface IBloodsugarService
    {
        Task<BloodsugarDto> CreateBloodsugarAsync(BloodsugarInputModel bloodsugar);
        Task<BloodsugarDto[]> GetBloodsugarsByPatientId(int patientId);
        Task<BloodsugarDto?> UpdateBloodsugarAsync(int id, BloodsugarInputModel updatedBloodsugar);
        Task<BloodsugarDto?> GetBloodsugarByIdAsync(int id);
        Task<bool> DeleteBloodsugarAsync(int id);
    }
}