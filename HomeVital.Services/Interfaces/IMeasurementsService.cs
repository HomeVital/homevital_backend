using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Services.Interfaces
{
    public interface IMeasurementService
    {
        // GetallMeasurements
        Task<List<MeasurementDto>> GetMeasurementsById(int id);
        // GetMeasurementByPatientId
        Task<List<MeasurementDto>> GetMeasurementsByPatientId(int id);
        
    }
}