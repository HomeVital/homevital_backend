using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces;

public interface IBloodsugarRepository
{
    // GetBloodsugarsByPatientId
    Task<IEnumerable<BloodsugarDto>> GetBloodsugarsByPatientId(int patientId);
    // CreateBloodsugar
    Task<BloodsugarDto> CreateBloodsugar(int patientId, BloodsugarInputModel bloodsugarInputModel);
    // UpdateBloodsugar
    Task<BloodsugarDto> UpdateBloodsugar(int id, BloodsugarInputModel bloodsugarInputModel);
    // DeleteBloodsugar
    Task<BloodsugarDto> DeleteBloodsugar(int id);
    
}