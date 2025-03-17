using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            var bodyTemperatures = await _dbContext.BodyTemperatures
                .Where(m => m.PatientID == id)
                .ToListAsync();

            // Map the measurements to the Dto object
            var measurementDto = new MeasurementDto
            {
                PatientID = id,
                Measurements = new List<Measurements>()
            };

            measurementDto.Measurements.AddRange(bloodPressures.Select(bp => new Measurements
            {
                MeasurementType = "BloodPressure",
                MeasurementID = bp.ID,
                MeasurementValues = new MeasurementValues
                {
                    Systolic = bp.Systolic,
                    Diastolic = bp.Diastolic,
                    BPM = bp.Pulse,
                    MeasureHand = bp.MeasureHand,
                    BodyPosition = bp.BodyPosition
                },
                MeasurementDate = bp.Date
            }));

            measurementDto.Measurements.AddRange(bloodSugars.Select(bs => new Measurements
            {
                MeasurementType = "BloodSugar",
                MeasurementID = bs.ID,
                MeasurementValues = new MeasurementValues
                {
                    BloodSugar = bs.BloodsugarLevel
                },
                
                MeasurementDate = bs.Date
            }));

            measurementDto.Measurements.AddRange(bodyWeights.Select(bw => new Measurements
            {
                MeasurementType = "BodyWeight",
                MeasurementID = bw.ID,
                MeasurementValues = new MeasurementValues
                {
                    Weight = bw.Weight
                }
                ,
                MeasurementDate = bw.Date
            }));

            measurementDto.Measurements.AddRange(bodyTemperatures.Select(bt => new Measurements
            {
                MeasurementType = "BodyTemperature",
                MeasurementID = bt.ID,
                MeasurementValues = new MeasurementValues
                {
                    Temperature = bt.Temperature
                }
                
            }));

            // sort the measurements by date
            measurementDto.Measurements = measurementDto.Measurements.OrderByDescending(m => m.MeasurementDate).ToList();

            return new List<MeasurementDto> { measurementDto };
        }
    }
}