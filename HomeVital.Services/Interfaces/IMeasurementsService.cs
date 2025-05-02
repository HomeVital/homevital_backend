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
        Task<List<Measurements>> GetXMeasurementsByPatientId(int patientId, int count);
        Task<List<Measurements>> GetMeasurementsWithWarnings(string status);
        Task<List<Measurements>> GetPatientWarnings(int patientId, bool onlyUnacknowledged = true);
        Task<bool> AcknowledgeMeasurement(MeasurementAckInputModel input);
        
    }
}