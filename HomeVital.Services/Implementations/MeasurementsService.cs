using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Services.Interfaces;
using HomeVital.Models.InputModels;
// mapper
using AutoMapper;
namespace HomeVital.Services.Implementations
{
    public class MeasurementsService : IMeasurementService
    {
        private readonly IMeasurementsRepository _measurementsRepository;
        private readonly IMapper _mapper;

        public MeasurementsService(IMeasurementsRepository measurementsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _measurementsRepository = measurementsRepository;
        }

        // public async Task<MeasurementDto> GetMeasurementsById(int id)
        // {
        //     return await _measurementsRepository.GetMeasurementsById(id);
        // }

        public async Task<List<Measurements>> GetMeasurementsByPatientId(int id)
        {
            // Get all measurements for the patient
            var measurements = await _measurementsRepository.GetMeasurementsByPatientId(id);
            if (measurements == null || !measurements.Any())
            {
                return Enumerable.Empty<Measurements>().ToList();
            }

            // Return the measurements directly
            return measurements;
        }

        public async Task<List<Measurements>> GetXMeasurementsByPatientId(int patientId, int count)
        {
            return await _measurementsRepository.GetXMeasurementsByPatientId(patientId, count);
        }
        public async Task<List<Measurements>> GetMeasurementsWithWarnings()
        {
            // Get all measurements with warnings
            var measurements = await _measurementsRepository.GetMeasurementsWithWarnings();
            if (measurements == null || !measurements.Any())
            {
                return Enumerable.Empty<Measurements>().ToList();
            }

            // Return the measurements directly
            return measurements;
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