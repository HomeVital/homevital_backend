using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Services.Interfaces
{
    public interface IBloodPressureService
    {
        // GetBloodPressuresByPatientId
        Task<IEnumerable<BloodPressureDto>> GetBloodPressuresByPatientId(int patientId); 
        // CreateBloodPressure
        Task<BloodPressureDto> CreateBloodPressure(int patientId, BloodPressureInputModel bloodPressureInputModel);
        // UpdateBloodPressure
        Task<BloodPressureDto> UpdateBloodPressure(int id, BloodPressureInputModel bloodPressureInputModel);
        // DeleteBloodPressure
        Task<BloodPressureDto> DeleteBloodPressure(int id);
    }
    
}
