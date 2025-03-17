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

        public async Task<MeasurementDto> GetMeasurementsById(int id)
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
                Measurements = new List<Measurements>()
            };

            measurementDto.Measurements.AddRange(bloodPressures.Select(bp => new Measurements
            {
                MeasurementType = "BloodPressure",
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
                MeasurementValues = new MeasurementValues
                {
                    BloodSugar = bs.BloodsugarLevel
                },
                MeasurementDate = bs.Date
            }));

            measurementDto.Measurements.AddRange(bodyWeights.Select(bw => new Measurements
            {
                MeasurementType = "BodyWeight",
                MeasurementValues = new MeasurementValues
                {
                    Weight = bw.Weight
                },
                MeasurementDate = bw.Date
            }));

            measurementDto.Measurements.AddRange(bodyTemperatures.Select(bt => new Measurements
            {
                MeasurementType = "BodyTemperature",
                MeasurementValues = new MeasurementValues
                {
                    Temperature = bt.Temperature
                },
                MeasurementDate = bt.Date
            }));

            // sort the measurements by date
            measurementDto.Measurements = measurementDto.Measurements.OrderByDescending(m => m.MeasurementDate).ToList();

            return new MeasurementDto { Measurements = measurementDto.Measurements };
        }
        public async Task<List<Measurements>> GetMeasurementsByPatientId(int id)
        {
            var bloodPressures = await _dbContext.BloodPressures
                .Where(m => m.PatientID == id)
                .Select(bp => new Measurements
                {
                    ID = bp.ID,
                    MeasurementType = "BloodPressure",
                    MeasurementDate = bp.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        Systolic = bp.Systolic,
                        Diastolic = bp.Diastolic,
                        BPM = bp.Pulse,
                        MeasureHand = bp.MeasureHand,
                        BodyPosition = bp.BodyPosition
                    },
                    
                })
                .ToListAsync();

            var bloodSugars = await _dbContext.Bloodsugars
                .Where(m => m.PatientID == id)
                .Select(bs => new Measurements
                {
                    ID = bs.ID,
                    MeasurementType = "BloodSugar",
                    MeasurementDate = bs.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        BloodSugar = bs.BloodsugarLevel
                    },
                })
                .ToListAsync();

            var bodyWeights = await _dbContext.BodyWeights
                .Where(m => m.PatientID == id)
                .Select(bw => new Measurements
                {
                    ID = bw.ID,
                    MeasurementType = "BodyWeight",
                    MeasurementDate = bw.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        Weight = bw.Weight
                    },
                    
                })
                .ToListAsync();

            var bodyTemperatures = await _dbContext.BodyTemperatures
                .Where(m => m.PatientID == id)
                .Select(bt => new Measurements
                {
                    ID = bt.ID,
                    MeasurementType = "BodyTemperature",
                    MeasurementDate = bt.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        Temperature = bt.Temperature
                    },
                })
                .ToListAsync();

            var oxygenSaturations = await _dbContext.OxygenSaturations
                .Where(m => m.PatientID == id)
                .Select(os => new Measurements
                {
                    ID = os.ID,
                    MeasurementType = "OxygenSaturation",
                    MeasurementDate = os.Date,
                    MeasurementValues = new MeasurementValues 
                    {
                        OxygenSaturation = os.OxygenSaturationValue
                    },
                })
                .ToListAsync();

            // Combine all measurements into a single list
            var measurements = bloodPressures
                .Concat(bloodSugars)
                .Concat(bodyWeights)
                .Concat(bodyTemperatures)
                .Concat(oxygenSaturations)
                .ToList();

            // Sort the measurements by date
            measurements = measurements.OrderByDescending(m => m.MeasurementDate).ToList();

            // Assign sequential UIDs
            for (int i = 0; i < measurements.Count; i++)
            {
                measurements[i].UID = i + 1;
            }
            // Return the list of measurements
            return measurements;
        }

        public async Task<List<Measurements>> GetXMeasurementsByPatientId(int patientId, int count)
        {
            // call the GetMeasurementsByPatientId method to get all measurements for the patient
            var measurements = await GetMeasurementsByPatientId(patientId);

            // return the first 'count' measurements

            measurements =  measurements.Take(count).ToList();

            return _mapper.Map<List<Measurements>>(measurements);
        }
    }
}