using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;


namespace HomeVital.Services.Interfaces
{
    public interface IBloodsugarService
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
}