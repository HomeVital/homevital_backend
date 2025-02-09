using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IBloodPressureRepository
    {
        Task<BloodPressureDto> CreateBloodPressure(BloodPressureInputModel bloodPressureInputModel);
        Task<BloodPressureDto?> GetBloodPressureByIdAsync(int id);
        Task<bool> DeleteBloodPressureAsync(int id);
        Task<BloodPressureDto?> UpdateBloodPressureAsync(int id, BloodPressureInputModel updatedBloodPressure);
        Task<BloodPressureDto?> GetBloodPressureByUserIdAsync(int userId);
    }
}