using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Services.Interfaces
{
    public interface IMeasurementService
    {
        // GetallMeasurements
        Task<MeasurementDto> GetMeasurementsById(int id);
        // GetMeasurementByPatientId
        // Task<MeasurementDto> GetMeasurementsByPatientId(int id);
        Task<List<Measurements>> GetMeasurementsByPatientId(int id);
        
    }
}