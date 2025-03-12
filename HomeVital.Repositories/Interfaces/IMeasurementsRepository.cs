using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces
{
    public interface IMeasurementsRepository
    {
        // GetAllMeasurements
        Task<List<MeasurementDto>> GetMeasurementsById(int id);
        // GetMeasurementBypatientId
        Task<List<MeasurementDto>> GetMeasurementsByPatientId(int id);
    }
}