using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
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
                        MeasuredHand = bp.MeasuredHand,
                        BodyPosition = bp.BodyPosition,
                        Status = bp.Status,
                        IsAcknowledged = bp.IsAcknowledged,
                        AcknowledgedByWorkerID = bp.AcknowledgedByWorkerID,
                        AcknowledgedDate = bp.AcknowledgedDate,
                        ResolutionNotes = bp.ResolutionNotes,
                        IsStoredInSaga = bp.IsStoredInSaga
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
                        BloodSugar = bs.BloodsugarLevel,
                        Status = bs.Status,
                        IsAcknowledged = bs.IsAcknowledged,
                        AcknowledgedByWorkerID = bs.AcknowledgedByWorkerID,
                        AcknowledgedDate = bs.AcknowledgedDate,
                        ResolutionNotes = bs.ResolutionNotes,
                        IsStoredInSaga = bs.IsStoredInSaga
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
                        Weight = (float?)bw.Weight,
                        Status = bw.Status,
                        IsAcknowledged = bw.IsAcknowledged,
                        AcknowledgedByWorkerID = bw.AcknowledgedByWorkerID,
                        AcknowledgedDate = bw.AcknowledgedDate,
                        ResolutionNotes = bw.ResolutionNotes,
                        IsStoredInSaga = bw.IsStoredInSaga
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
                        Temperature = bt.Temperature,
                        Status = bt.Status,
                        IsAcknowledged = bt.IsAcknowledged,
                        AcknowledgedByWorkerID = bt.AcknowledgedByWorkerID,
                        AcknowledgedDate = bt.AcknowledgedDate,
                        ResolutionNotes = bt.ResolutionNotes,
                        IsStoredInSaga = bt.IsStoredInSaga
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
                        OxygenSaturation = os.OxygenSaturationValue,
                        Status = os.Status,
                        IsAcknowledged = os.IsAcknowledged,
                        AcknowledgedByWorkerID = os.AcknowledgedByWorkerID,
                        AcknowledgedDate = os.AcknowledgedDate,
                        ResolutionNotes = os.ResolutionNotes,
                        IsStoredInSaga = os.IsStoredInSaga

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
            for (int i = 0; i < measurements.Count(); i++)
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

        public async Task<List<Measurements>> GetMeasurementsWithWarnings()
        {
            // Start with empty lists
            List<Measurements> results = new List<Measurements>();
            
            

            // Filter statuses that indicate warnings (not "Normal" or empty)
            var bloodPressures = await _dbContext.BloodPressures
                .Include(bp => bp.Patient)
                .ThenInclude(p => p.Team)
                .Where(m => !string.IsNullOrEmpty(m.Status) && m.Status != "Normal")
                .Select(bp => new Measurements
                {
                    ID = bp.ID,
                    PatientID = bp.PatientID,
                    TeamID =(int)bp.Patient.TeamID!,
                    MeasurementType = "BloodPressure",
                    MeasurementDate = bp.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        Systolic = bp.Systolic,
                        Diastolic = bp.Diastolic,
                        BPM = bp.Pulse,
                        MeasuredHand = bp.MeasuredHand,
                        BodyPosition = bp.BodyPosition,
                        Status = bp.Status,
                        IsAcknowledged = bp.IsAcknowledged,
                        AcknowledgedByWorkerID = bp.AcknowledgedByWorkerID,
                        AcknowledgedDate = bp.AcknowledgedDate,
                        ResolutionNotes = bp.ResolutionNotes
                    },
                })
                .ToListAsync();
            
            results.AddRange(bloodPressures);
            
            // Do the same for other measurement types
            // ...
            var bloodSugars = await _dbContext.Bloodsugars
                .Include(bp => bp.Patient)
                .ThenInclude(p => p.Team)
                .Where(m => !string.IsNullOrEmpty(m.Status) && m.Status != "Normal")
                .Select(bs => new Measurements
                {
                    ID = bs.ID,
                    PatientID = bs.PatientID,
                    TeamID =(int)bs.Patient.TeamID!,
                    MeasurementType = "BloodSugar",
                    MeasurementDate = bs.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        BloodSugar = bs.BloodsugarLevel,
                        Status = bs.Status,
                        IsAcknowledged = bs.IsAcknowledged,
                        AcknowledgedByWorkerID = bs.AcknowledgedByWorkerID,
                        AcknowledgedDate = bs.AcknowledgedDate,
                        ResolutionNotes = bs.ResolutionNotes
                    },
                })
                .ToListAsync();

            results.AddRange(bloodSugars);

            var bodyWeights = await _dbContext.BodyWeights
                .Include(bp => bp.Patient)
                .ThenInclude(p => p.Team)
                .Where(m => !string.IsNullOrEmpty(m.Status) && m.Status != "Normal")
                .Select(bw => new Measurements
                {
                    ID = bw.ID,
                    PatientID = bw.PatientID,
                    TeamID =(int)bw.Patient.TeamID!,
                    MeasurementType = "BodyWeight",
                    MeasurementDate = bw.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        Weight = (float?)bw.Weight,
                        Status = bw.Status,
                        IsAcknowledged = bw.IsAcknowledged,
                        AcknowledgedByWorkerID = bw.AcknowledgedByWorkerID,
                        AcknowledgedDate = bw.AcknowledgedDate,
                        ResolutionNotes = bw.ResolutionNotes
                    },
                })
                .ToListAsync();

            results.AddRange(bodyWeights);

            var bodyTemperatures = await _dbContext.BodyTemperatures
                .Include(bp => bp.Patient)
                .ThenInclude(p => p.Team)
                .Where(m => !string.IsNullOrEmpty(m.Status) && m.Status != "Normal")
                .Select(bt => new Measurements
                {
                    ID = bt.ID,
                    PatientID = bt.PatientID,
                    TeamID =(int)bt.Patient.TeamID!,
                    MeasurementType = "BodyTemperature",
                    MeasurementDate = bt.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        Temperature = bt.Temperature,
                        Status = bt.Status,
                        IsAcknowledged = bt.IsAcknowledged,
                        AcknowledgedByWorkerID = bt.AcknowledgedByWorkerID,
                        AcknowledgedDate = bt.AcknowledgedDate,
                        ResolutionNotes = bt.ResolutionNotes
                    },
                })
                .ToListAsync();

            results.AddRange(bodyTemperatures);

            var oxygenSaturations = await _dbContext.OxygenSaturations
                .Include(bp => bp.Patient)
                .ThenInclude(p => p.Team)
                .Where(m => !string.IsNullOrEmpty(m.Status) && m.Status != "Normal")
                .Select(os => new Measurements
                {
                    ID = os.ID,
                    PatientID = os.PatientID,
                    TeamID =(int)os.Patient.TeamID!,
                    MeasurementType = "OxygenSaturation",
                    MeasurementDate = os.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        OxygenSaturation = os.OxygenSaturationValue,
                        Status = os.Status,
                        IsAcknowledged = os.IsAcknowledged,
                        AcknowledgedByWorkerID = os.AcknowledgedByWorkerID,
                        AcknowledgedDate = os.AcknowledgedDate,
                        ResolutionNotes = os.ResolutionNotes
                    },
                })
                .ToListAsync();

            results.AddRange(oxygenSaturations);
            
            // Sort by date (newest first)
            results = results.OrderByDescending(m => m.MeasurementDate).ToList();
            
            // Assign sequential UIDs
            for (int i = 0; i < results.Count; i++)
            {
                results[i].UID = i + 1;
            }
            
            return results;
        }

        public async Task<List<Measurements>> GetPatientWarnings(int patientId, bool onlyUnacknowledged = true)
        {
            // Start with empty lists
            List<Measurements> results = new List<Measurements>();
            
            // Get blood pressure warnings for this patient
            var bloodPressures = await _dbContext.BloodPressures
                .Where(m => m.PatientID == patientId && 
                    !string.IsNullOrEmpty(m.Status) && 
                    m.Status != "Normal" &&
                    (!onlyUnacknowledged || !m.IsAcknowledged))
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
                        MeasuredHand = bp.MeasuredHand,
                        BodyPosition = bp.BodyPosition,
                        Status = bp.Status,
                        IsAcknowledged = bp.IsAcknowledged,
                        AcknowledgedByWorkerID = bp.AcknowledgedByWorkerID,
                        AcknowledgedDate = bp.AcknowledgedDate,
                        ResolutionNotes = bp.ResolutionNotes
                    },
                })
                .ToListAsync();
            
            results.AddRange(bloodPressures);
            
            // Do the same for other measurement types
            // ...
            var bloodSugars = await _dbContext.Bloodsugars
                .Where(m => m.PatientID == patientId && 
                    !string.IsNullOrEmpty(m.Status) && 
                    m.Status != "Normal" &&
                    (!onlyUnacknowledged || !m.IsAcknowledged))
                .Select(bs => new Measurements
                {
                    ID = bs.ID,
                    MeasurementType = "BloodSugar",
                    MeasurementDate = bs.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        BloodSugar = bs.BloodsugarLevel,
                        Status = bs.Status,
                        IsAcknowledged = bs.IsAcknowledged,
                        AcknowledgedByWorkerID = bs.AcknowledgedByWorkerID,
                        AcknowledgedDate = bs.AcknowledgedDate,
                        ResolutionNotes = bs.ResolutionNotes
                    },
                })
                .ToListAsync();

            results.AddRange(bloodSugars);

            var bodyWeights = await _dbContext.BodyWeights
                .Where(m => m.PatientID == patientId && 
                    !string.IsNullOrEmpty(m.Status) && 
                    m.Status != "Normal" &&
                    (!onlyUnacknowledged || !m.IsAcknowledged))
                .Select(bw => new Measurements
                {
                    ID = bw.ID,
                    MeasurementType = "BodyWeight",
                    MeasurementDate = bw.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        Weight = (float?)bw.Weight,
                        Status = bw.Status,
                        IsAcknowledged = bw.IsAcknowledged,
                        AcknowledgedByWorkerID = bw.AcknowledgedByWorkerID,
                        AcknowledgedDate = bw.AcknowledgedDate,
                        ResolutionNotes = bw.ResolutionNotes
                    },
                })
                .ToListAsync();

            results.AddRange(bodyWeights);

            var bodyTemperatures = await _dbContext.BodyTemperatures
                .Where(m => m.PatientID == patientId && 
                    !string.IsNullOrEmpty(m.Status) && 
                    m.Status != "Normal" &&
                    (!onlyUnacknowledged || !m.IsAcknowledged))
                .Select(bt => new Measurements
                {
                    ID = bt.ID,
                    MeasurementType = "BodyTemperature",
                    MeasurementDate = bt.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        Temperature = bt.Temperature,
                        Status = bt.Status,
                        IsAcknowledged = bt.IsAcknowledged,
                        AcknowledgedByWorkerID = bt.AcknowledgedByWorkerID,
                        AcknowledgedDate = bt.AcknowledgedDate,
                        ResolutionNotes = bt.ResolutionNotes
                    },
                })
                .ToListAsync();

            results.AddRange(bodyTemperatures);

            var oxygenSaturations = await _dbContext.OxygenSaturations
                .Where(m => m.PatientID == patientId && 
                    !string.IsNullOrEmpty(m.Status) && 
                    m.Status != "Normal" &&
                    (!onlyUnacknowledged || !m.IsAcknowledged))
                .Select(os => new Measurements
                {
                    ID = os.ID,
                    MeasurementType = "OxygenSaturation",
                    MeasurementDate = os.Date,
                    MeasurementValues = new MeasurementValues
                    {
                        OxygenSaturation = os.OxygenSaturationValue,
                        Status = os.Status,
                        IsAcknowledged = os.IsAcknowledged,
                        AcknowledgedByWorkerID = os.AcknowledgedByWorkerID,
                        AcknowledgedDate = os.AcknowledgedDate,
                        ResolutionNotes = os.ResolutionNotes
                    },
                })
                .ToListAsync();
            
            // Sort by date (newest first)
            results = results.OrderByDescending(m => m.MeasurementDate).ToList();
            
            // Assign sequential UIDs
            for (int i = 0; i < results.Count; i++)
            {
                results[i].UID = i + 1;
            }
            
            return results;
        }

        public async Task<bool> AcknowledgeMeasurement(MeasurementAckInputModel input)
        {
            switch (input.MeasurementType.ToLower())
            {
                case "bloodpressure":
                    var bp = await _dbContext.BloodPressures.FindAsync(input.MeasurementID);
                    if (bp == null) return false;
                    
                    bp.IsAcknowledged = true;
                    bp.AcknowledgedByWorkerID = input.WorkerID;
                    bp.AcknowledgedDate = DateTime.UtcNow;
                    bp.ResolutionNotes = input.ResolutionNotes;
                    break;
                    
                case "bloodsugar":
                    var bs = await _dbContext.Bloodsugars.FindAsync(input.MeasurementID);
                    if (bs == null) return false;
                    
                    bs.IsAcknowledged = true;
                    bs.AcknowledgedByWorkerID = input.WorkerID;
                    bs.AcknowledgedDate = DateTime.UtcNow;
                    bs.ResolutionNotes = input.ResolutionNotes;
                    break;
                    
                case "bodytemperature":
                    var bt = await _dbContext.BodyTemperatures.FindAsync(input.MeasurementID);
                    if (bt == null) return false;
                    
                    bt.IsAcknowledged = true;
                    bt.AcknowledgedByWorkerID = input.WorkerID;
                    bt.AcknowledgedDate = DateTime.UtcNow;
                    bt.ResolutionNotes = input.ResolutionNotes;
                    break;
                    
                case "bodyweight":
                    var bw = await _dbContext.BodyWeights.FindAsync(input.MeasurementID);
                    if (bw == null) return false;
                    
                    bw.IsAcknowledged = true;
                    bw.AcknowledgedByWorkerID = input.WorkerID;
                    bw.AcknowledgedDate = DateTime.UtcNow;
                    bw.ResolutionNotes = input.ResolutionNotes;
                    break;
                    
                case "oxygensaturation":
                    var os = await _dbContext.OxygenSaturations.FindAsync(input.MeasurementID);
                    if (os == null) return false;
                    
                    os.IsAcknowledged = true;
                    os.AcknowledgedByWorkerID = input.WorkerID;
                    os.AcknowledgedDate = DateTime.UtcNow;
                    os.ResolutionNotes = input.ResolutionNotes;
                    break;
                    
                default:
                    return false;
            }
            
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetSagaStatus(SagaAckInputModel input)
        {
            switch (input.MeasurementType.ToLower())
            {
                case "bloodpressure":
                    var bp = await _dbContext.BloodPressures.FindAsync(input.MeasurementID);
                    if (bp == null) return false;
                    
                    bp.IsStoredInSaga = true;
                    break;
                    
                case "bloodsugar":
                    var bs = await _dbContext.Bloodsugars.FindAsync(input.MeasurementID);
                    if (bs == null) return false;
                    
                    bs.IsStoredInSaga = true;
                    break;
                    
                case "bodytemperature":
                    var bt = await _dbContext.BodyTemperatures.FindAsync(input.MeasurementID);
                    if (bt == null) return false;
                    
                    bt.IsStoredInSaga = true;
                    break;
                    
                case "bodyweight":
                    var bw = await _dbContext.BodyWeights.FindAsync(input.MeasurementID);
                    if (bw == null) return false;
                    
                    bw.IsStoredInSaga = true;
                    break;
                    
                case "oxygensaturation":
                    var os = await _dbContext.OxygenSaturations.FindAsync(input.MeasurementID);
                    if (os == null) return false;
                    
                    os.IsStoredInSaga = true;
                    break;
                    
                default:
                    return false;
            }
            
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}