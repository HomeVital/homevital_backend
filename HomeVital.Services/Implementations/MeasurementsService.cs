using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Services.Interfaces;

namespace HomeVital.Services.Implementations
{
    public class MeasurementsService : IMeasurementService
    {
        private readonly IMeasurementsRepository _measurementsRepository;

        public MeasurementsService(IMeasurementsRepository measurementsRepository)
        {
            _measurementsRepository = measurementsRepository;
        }

        public async Task<List<MeasurementDto>> GetMeasurementsById(int id)
        {
            return await _measurementsRepository.GetMeasurementsById(id);
        }
    }
}