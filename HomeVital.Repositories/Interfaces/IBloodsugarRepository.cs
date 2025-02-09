using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces;

public interface IBloodsugarRepository
{
    Task<BloodsugarDto> CreateBloodsugarAsync(BloodsugarInputModel bloodsugar);
    Task<BloodsugarDto[]> GetBloodsugarsByPatientId(int patientId);
    Task<BloodsugarDto?> GetBloodsugarByIdAsync(int id); 
    Task<bool> DeleteBloodsugarAsync(int id);
    Task<BloodsugarDto?> UpdateBloodsugarAsync(int id, BloodsugarInputModel updatedBloodsugar);
    Task<BloodsugarDto?> GetBloodsugarByUserIdAsync(int userId);
    
}