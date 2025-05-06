using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IMeasurementsRepository
    {
        // GetAllMeasurements
        // Task<MeasurementDto> GetMeasurementsById(int id);
        // GetMeasurementBypatientId
        // Task<MeasurementDto> GetMeasurementsByPatientId(int id);
        Task<List<Measurements>> GetMeasurementsByPatientId(int id);
        Task<List<Measurements>> GetXMeasurementsByPatientId(int patientId, int count);
        Task<List<Measurements>> GetMeasurementsWithWarnings();
        Task<List<Measurements>> GetPatientWarnings(int patientId, bool onlyUnacknowledged = true);
        Task<bool> AcknowledgeMeasurement(MeasurementAckInputModel input);
    }
}