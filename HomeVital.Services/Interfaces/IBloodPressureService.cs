using System.Threading.Tasks;
using HomeVital.Models.Entities;
using HomeVital.Models.InputModels;
namespace HomeVital.Services.Interfaces


{
    public interface IBloodPressureService
    {
        Task<BloodPressure?> GetBloodPressureByIdAsync(int id);
        Task<BloodPressure> CreateBloodPressureAsync(BloodPressureInputModel bloodPressureInputModel);
            
        Task<bool> DeleteBloodPressureAsync(int id);
        Task<BloodPressure?> UpdateBloodPressureAsync(int id, BloodPressureInputModel updatedBloodPressureInputModel);
    }
}
