using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using HomeVital.Models.Entities;

namespace HomeVital.Repositories.Implementations
{
    public class MeasurementsRepository : IMeasurementsRepository
    {
        private readonly HomeVitalDbContext _dbContext;
        private readonly IMapper _mapper;

        public MeasurementsRepository(HomeVitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<MeasurementDto>> GetMeasurementsById(int id)
        {
            // Query the database for measurements with the given patient id
            var bloodPressures = await _dbContext.BloodPressures
            .Where(m => m.PatientID == id)
            .ToListAsync();

            var bloodSugars = await _dbContext.Bloodsugars
            .Where(m => m.PatientID == id)
            .ToListAsync();

            var bodyWeights = await _dbContext.BodyWeights
            .Where(m => m.PatientID == id)
            .ToListAsync();
        

            // Map the measurements to the Dto object
            var measurementDto = new MeasurementDto
            {
                BloodPressure = _mapper.Map<List<BloodPressureDto>>(bloodPressures),
                BloodSugar = _mapper.Map<List<BloodsugarDto>>(bloodSugars),
                BodyWeight = _mapper.Map<List<BodyWeightDto>>(bodyWeights)

            };

            return new List<MeasurementDto> { measurementDto };
        }
    }
}