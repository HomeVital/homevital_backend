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
                },
                MeasurementDate = bw.Date
            }));

            measurementDto.Measurements.AddRange(bodyTemperatures.Select(bt => new Measurements
            {
                MeasurementType = "BodyTemperature",
                MeasurementID = bt.ID,
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

        // get measurements by patient id and use the measurements table to get the measurementsID and then get the measurements values
        // public async Task<List<MeasurementDto>> GetMeasurementsByPatientId(int id)
        // {
        //     // Query the database for measurementsID with the given patient id
        //     var measurements = await _dbContext.Measurements
        //         .Where(m => m.PatientID == id)
        //         .ToListAsync();

        //     // Query the bloodpressure table for matching measurementsID
        //     var bloodPressures = await _dbContext.BloodPressures
        //         .Where(bp => measurements.Any(m => m.ID == bp.MeasurementID))
        //         .ToListAsync();

        //     // Query the bloodsugar table for matching measurementsID
        //     var bloodSugars = await _dbContext.Bloodsugars
        //         .Where(bs => measurements.Any(m => m.ID == bs.MeasurementID))
        //         .ToListAsync();

        //     // Query the bodyweight table for matching measurementsID
        //     var bodyWeights = await _dbContext.BodyWeights
        //         .Where(bw => measurements.Any(m => m.ID == bw.MeasurementID))
        //         .ToListAsync();

        //     // Query the bodytemperature table for matching measurementsID
        //     var bodyTemperatures = await _dbContext.BodyTemperatures
        //         .Where(bt => measurements.Any(m => m.ID == bt.MeasurementID))
        //         .ToListAsync();

        //     // Map the measurements to the Dto object
        //     var measurementDto = new MeasurementDto
        //     {
        //         PatientID = id,
        //         Measurements = new List<Measurements>()
        //     };

        //     measurementDto.Measurements.AddRange(bloodPressures.Select(bp => new Measurements
        //     {
        //         MeasurementType = "BloodPressure",
        //         MeasurementID = bp.ID,
        //         MeasurementValues = new List<MeasurementValues>
        //         {
        //             new MeasurementValues
        //             {
        //                 Systolic = bp.Systolic,
        //                 Diastolic = bp.Diastolic,
        //                 BPM = bp.Pulse,
        //                 MeasureHand = bp.MeasureHand,
        //                 BodyPosition = bp.BodyPosition
        //             }
        //         },
        //         MeasurementDate = bp.Date
        //     }));

        //     measurementDto.Measurements.AddRange(bloodSugars.Select(bs => new Measurements
        //     {
        //         MeasurementType = "BloodSugar",
        //         MeasurementID = bs.ID,
        //         MeasurementValues = new List<MeasurementValues>
        //         {
        //             new MeasurementValues
        //             {
        //                 BloodSugar = bs.BloodsugarLevel
        //             }
        //         },
        //         MeasurementDate = bs.Date
        //     }));

        //     measurementDto.Measurements.AddRange(bodyWeights.Select(bw => new Measurements
        //     {
        //         MeasurementType = "BodyWeight",
        //         MeasurementID = bw.ID,
        //         MeasurementValues = new List<MeasurementValues>
        //         {
        //             new MeasurementValues
        //             {
        //                 Weight = bw.Weight
        //             }
        //         },
        //         MeasurementDate = bw.Date
        //     }));


        //     measurementDto.Measurements.AddRange(bodyTemperatures.Select(bt => new Measurements
        //     {
        //         MeasurementType = "BodyTemperature",
        //         MeasurementID = bt.ID,
        //         MeasurementValues = new List<MeasurementValues>
        //         {
        //             new MeasurementValues
        //             {
        //                 Temperature = bt.Temperature
        //             }
        //         },
        //         MeasurementDate = bt.Date
        //     }));

        //     // sort the measurements by date
        //     measurementDto.Measurements = measurementDto.Measurements.OrderByDescending(m => m.MeasurementDate).ToList();

        //     return new List<MeasurementDto> { measurementDto };
        // }
        // public async Task<List<MeasurementDto>> GetMeasurementsByPatientId(int id)
        // {
        //     var bloodPressures = await _dbContext.BloodPressures
        //         .Where(m => m.PatientID == id)
        //         .ToListAsync();

        //     var bloodSugars = await _dbContext.Bloodsugars
        //         .Where(m => m.PatientID == id)
        //         .ToListAsync();

        //     var bodyWeights = await _dbContext.BodyWeights
        //         .Where(m => m.PatientID == id)
        //         .ToListAsync();

        //     var bodyTemperatures = await _dbContext.BodyTemperatures
        //         .Where(m => m.PatientID == id)
        //         .ToListAsync();

        //     var oxygenSaturations = await _dbContext.OxygenSaturations
        //         .Where(m => m.PatientID == id)
        //         .ToListAsync();

        //     // Combine all measurements into a single list
        //     var measurements = bloodPressures.Cast<Measurement>()
        //         .Concat(bloodSugars)
        //         .Concat(bodyWeights)
        //         .Concat(bodyTemperatures)
        //         .Concat(oxygenSaturations)
        //         .ToList();

        //     // Map the measurements to the Dto object
        //     var measurementDto = new MeasurementDto
        //     {
        //         PatientID = id,
        //         Measurements = measurements
        //     };

        //     return new List<MeasurementDto> { measurementDto };
        // }

        // public async Task<MeasurementDto> GetMeasurementsByPatientId(int id)
        // {
        //     var bloodPressures = await _dbContext.BloodPressures
        //         .Where(m => m.PatientID == id)
        //         .Select(bp => new Measurements
        //         {
        //             MeasurementType = "BloodPressure",
        //             MeasurementID = (int)bp.MeasurementID,
        //             MeasurementValues = new MeasurementValues
        //             {
        //                 Systolic = bp.Systolic,
        //                 Diastolic = bp.Diastolic,
        //                 BPM = bp.Pulse,
        //                 MeasureHand = bp.MeasureHand,
        //                 BodyPosition = bp.BodyPosition
        //             },
        //             MeasurementDate = bp.Date
        //         })
        //         .ToListAsync();

        //     var bloodSugars = await _dbContext.Bloodsugars
        //         .Where(m => m.PatientID == id)
        //         .Select(bs => new Measurements
        //         {
        //             MeasurementType = "BloodSugar",
        //             MeasurementID = (int)bs.MeasurementID,
        //             MeasurementValues = new MeasurementValues
        //             {
        //                 BloodSugar = bs.BloodsugarLevel
        //             },
        //             MeasurementDate = bs.Date
        //         })
        //         .ToListAsync();

        //     var bodyWeights = await _dbContext.BodyWeights
        //         .Where(m => m.PatientID == id)
        //         .Select(bw => new Measurements
        //         {
        //             MeasurementType = "BodyWeight",
        //             MeasurementID = (int)bw.MeasurementID,
        //             MeasurementValues = new MeasurementValues
        //             {
        //                 Weight = bw.Weight
        //             },
        //             MeasurementDate = bw.Date
        //         })
        //         .ToListAsync();

        //     var bodyTemperatures = await _dbContext.BodyTemperatures
        //         .Where(m => m.PatientID == id)
        //         .Select(bt => new Measurements
        //         {
                    
        //             MeasurementType = "BodyTemperature",
        //             MeasurementID = (int)bt.MeasurementID,
        //             MeasurementValues = new MeasurementValues
        //             {
        //                 Temperature = bt.Temperature
        //             },
        //             MeasurementDate = bt.Date
        //         })
        //         .ToListAsync();

        //     var oxygenSaturations = await _dbContext.OxygenSaturations
        //         .Where(m => m.PatientID == id)
        //         .Select(os => new Measurements
        //         {
        //             MeasurementType = "OxygenSaturation",
        //             MeasurementID = os.MeasurementID,
        //             MeasurementValues = new MeasurementValues 
        //             {
        //                 OxygenSaturation = os.OxygenSaturationValue
        //             },
        //             MeasurementDate = os.Date
        //         })
        //         .ToListAsync();

        //     // Combine all measurements into a single list
        //     var measurements = bloodPressures
        //         .Concat(bloodSugars)
        //         .Concat(bodyWeights)
        //         .Concat(bodyTemperatures)
        //         .Concat(oxygenSaturations)
        //         .ToList();

        //     // Map the measurements to the Dto object
        //     // var measurementDto = new MeasurementDto
        //     // {
        //     //     PatientID = id,
        //     //     Measurements = measurements
        //     // };   

        //     // return the measurements use map to map the measurements to the Dto object
        //     return new MeasurementDto { Measurements = measurements };

        
        // }
        public async Task<List<Measurements>> GetMeasurementsByPatientId(int id)
        {
            var bloodPressures = await _dbContext.BloodPressures
                .Where(m => m.PatientID == id)
                .Select(bp => new Measurements
                {
                    MeasurementType = "BloodPressure",
                    MeasurementID = bp.MeasurementID ?? 0,
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
                    MeasurementType = "BloodSugar",
                    MeasurementID = bs.MeasurementID ?? 0,
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
                    MeasurementType = "BodyWeight",
                    MeasurementID = bw.MeasurementID ?? 0,
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
                    MeasurementType = "BodyTemperature",
                    MeasurementID = bt.MeasurementID ?? 0,
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
                    MeasurementType = "OxygenSaturation",
                    MeasurementID = os.MeasurementID,
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

            // Assign sequential UIDs
            for (int i = 0; i < measurements.Count; i++)
            {
                measurements[i].UID = i + 1;
            }
            // Return the list of measurements
            return measurements;
        }
    }
}