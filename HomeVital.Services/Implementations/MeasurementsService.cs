using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;

namespace HomeVital.Services.Implementations
{
    public class MeasurementsService : IMeasurementService
    {
        private readonly IMeasurementsRepository _measurementsRepository;

        public MeasurementsService(IMeasurementsRepository measurementsRepository)
        {
            _measurementsRepository = measurementsRepository;
        }

        public async Task<MeasurementDto> GetMeasurementsById(int id)
        {
            return await _measurementsRepository.GetMeasurementsById(id);
        }

        public async Task<List<Measurements>> GetMeasurementsByPatientId(int id)
        {
            return await _measurementsRepository.GetMeasurementsByPatientId(id);
        }

        public async Task<List<Measurements>> GetXMeasurementsByPatientId(int patientId, int count)
        {
            return await _measurementsRepository.GetXMeasurementsByPatientId(patientId, count);
        }
        public async Task<List<Measurements>> GetMeasurementsWithWarnings(string status)
        {
            return await _measurementsRepository.GetMeasurementsWithWarnings(status);
        }

        public async Task<List<Measurements>> GetPatientWarnings(int patientId, bool onlyUnacknowledged = true)
        {
            return await _measurementsRepository.GetPatientWarnings(patientId, onlyUnacknowledged);
        }

        public async Task<bool> AcknowledgeMeasurement(MeasurementAckInputModel input)
        {
            return await _measurementsRepository.AcknowledgeMeasurement(input);
        }
    }
}