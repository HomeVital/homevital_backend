
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Models.Enums;
using HomeVital.Models.Exceptions;



namespace HomeVital.Repositories.Implementations;

public class BloodPressureRepository : IBloodPressureRepository
{
    private readonly HomeVitalDbContext _dbContext;
    private readonly IMapper _mapper;

    public BloodPressureRepository(HomeVitalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BloodPressureDto>> GetBloodPressuresByPatientId(int patientId)
    {
        // query the database for blood pressure records for the given patient ID
        var bloodPressures = await _dbContext.BloodPressures
            .Where(b => b.PatientID == patientId)
            .OrderByDescending(b => b.Date)
            .ToListAsync();

        if (bloodPressures == null || !bloodPressures.Any())
        {
            throw new ResourceNotFoundException("No blood pressure records found for the patient with ID: " + patientId);
        }
        // order by date
        bloodPressures = bloodPressures.OrderByDescending(b => b.Date).ToList();

        return _mapper.Map<IEnumerable<BloodPressureDto>>(bloodPressures);
    }

    public async Task<BloodPressureDto> CreateBloodPressure(int patientId, BloodPressureInputModel bloodPressureInputModel)
    {
        // query the database for blood pressure records for the given patient ID
        var vitalRangeBloodpressure = await _dbContext.BloodPressureRanges
            .FirstOrDefaultAsync(b => b.PatientID == patientId);

        if (vitalRangeBloodpressure == null)
        {
            throw new ResourceNotFoundException("Blood pressure range not found for the patient with ID: " + patientId);
        }
        // check blood pressure range
        bloodPressureInputModel.Status = CheckBloodPressureRange(bloodPressureInputModel, vitalRangeBloodpressure);
        // make sure measure hand and body position are not null

        // map the input model to the entity
        var bloodPressure = _mapper.Map<BloodPressure>(bloodPressureInputModel);
        bloodPressure.PatientID = patientId;
        bloodPressure.Date = DateTime.UtcNow;

        // add the blood pressure record to the database
        _dbContext.BloodPressures.Add(bloodPressure);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }
  

    public async Task<BloodPressureDto> UpdateBloodPressure(int id, BloodPressureInputModel bloodPressureInputModel)
    {
        // query the database for blood pressure records for the given patient ID
        var bloodPressure = await _dbContext.BloodPressures
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bloodPressure == null)
        {
            throw new ResourceNotFoundException("Blood pressure not found with ID: " + id);
        }

        // check the date, if the date on the measurement is older than 1 day, throw an exception
        if (bloodPressure.Date < DateTime.UtcNow.AddDays(-1))
        {
            throw new MethodNotAllowedException("Blood pressure measurement is older than 1 day with ID: " + id);
        }

        if (bloodPressure != null)
        {
            // check blood pressure range using CheckBloodPressureRange
            var vitalRangeBloodpressure = await _dbContext.BloodPressureRanges
                .FirstOrDefaultAsync(b => b.PatientID == bloodPressure.PatientID);

            if (vitalRangeBloodpressure != null)
            {
                bloodPressureInputModel.Status = CheckBloodPressureRange(bloodPressureInputModel, vitalRangeBloodpressure);
            }
            
            // Update only non-default values
            // Convert enum to string when setting the MeasureHand property
            bloodPressure.MeasuredHand = bloodPressureInputModel.MeasuredHand.ToString();
            
            // Convert enum to string when setting the BodyPosition property
            bloodPressure.BodyPosition = bloodPressureInputModel.BodyPosition.ToString();
            if (bloodPressureInputModel.Systolic > 0)
            {
                bloodPressure.Systolic = bloodPressureInputModel.Systolic;
            }
            if (bloodPressureInputModel.Diastolic > 0)
            {
                bloodPressure.Diastolic = bloodPressureInputModel.Diastolic;
            }
            if (bloodPressureInputModel.Pulse > 0)
            {
                bloodPressure.Pulse = bloodPressureInputModel.Pulse;
            }
            if (!string.IsNullOrEmpty(bloodPressureInputModel.Status))
            {
                bloodPressure.Status = bloodPressureInputModel.Status;
            }
            // save the changes to the database
            await _dbContext.SaveChangesAsync();
        }
        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }

    public async Task<BloodPressureDto> DeleteBloodPressure(int id)
    {
        // query the database for blood pressure records for the given patient ID
        var bloodPressure = await _dbContext.BloodPressures
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bloodPressure == null)
        {
            throw new ResourceNotFoundException("Blood pressure not found with ID: " + id);
        }
        // check the date, if the date on the measurement is older than 1 day, throw an exception
        if (bloodPressure.Date < DateTime.UtcNow.AddDays(-1))
        {
            throw new MethodNotAllowedException("Blood pressure measurement is older than 1 day with ID: " + id);
        }

        // delete the blood pressure record from the database
        _dbContext.BloodPressures.Remove(bloodPressure);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }

    public async Task<BloodPressureDto> GetBloodPressureById(int id)
    {
        // query the database for blood pressure records for the given patient ID
        var bloodPressure = await _dbContext.BloodPressures
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bloodPressure == null)
        {
            throw new ResourceNotFoundException("Blood pressure not found with ID: " + id);
        }

        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }

    /// <summary>
    /// Checks the blood pressure range and returns the status.
    /// Systolic and diastolic values are checked against the ranges defined in the BloodPressureRange entity.
    /// The systolic value is given more weight in determining the final status.
    /// </summary>
    /// <param name="bloodPressureInputModel">The input model containing the blood pressure values.</param>
    /// <param name="BloodPressureRange">The range of blood pressure values.</param>
    /// <returns>The status of the blood pressure.</returns>
    /// <remarks>
    /// The method checks the systolic and diastolic values against the defined ranges.
    /// - If the systolic value is below the "SystolicLowered", the status is "Raised".
    /// - If the systolic value is below the "SystolicGood", the status is "Normal".
    /// - If the systolic value is below the "SystolicRaised", the status is "Raised".
    /// - If the systolic value is below the "SystolicHigh" or above the "SystolicHigh", the status is "High".
    /// - If the systolic value is invalid, the status is "Invalid".
    /// - If the diastolic value is below the "DiastolicLowered", the status is "Raised".
    /// - If the diastolic value is below the "DiastolicGood", the status is "Normal".
    /// - If the diastolic value is below the "DiastolicRaised", the status is "Raised".
    /// - If the diastolic value is below the "DiastolicHigh" or above the "DiastolicHigh", the status is "High".
    /// - If the diastolic value is invalid, the status is "Invalid".
    /// - The final status is determined based on the systolic value having more weight.
    /// </summary>
    private static string CheckBloodPressureRange(BloodPressureInputModel bloodPressureInputModel, BloodPressureRange BloodPressureRange)
    {
        // Initialize the status variables
        var sysStatus = string.Empty;
        var diaStatus = string.Empty;

        // Check if the blood pressure is within the normal range, start with checking the systolic values
        if (bloodPressureInputModel.Systolic < BloodPressureRange.SystolicLowered)
        {
            sysStatus = VitalStatus.Raised.ToString();
        }
        else if (bloodPressureInputModel.Systolic < BloodPressureRange.SystolicGood)
        {
            sysStatus = VitalStatus.Normal.ToString();
        }
        else if (bloodPressureInputModel.Systolic < BloodPressureRange.SystolicRaised)
        {
            sysStatus = VitalStatus.Raised.ToString();
        }
        // Systolic high if now less than the systolic high value or greater than the systolic high value
        else if (bloodPressureInputModel.Systolic < BloodPressureRange.SystolicHigh || bloodPressureInputModel.Systolic > BloodPressureRange.SystolicHigh)
        {
            sysStatus = VitalStatus.High.ToString();
        }
        else
        {
            sysStatus = VitalStatus.Invalid.ToString();
        }

        if (bloodPressureInputModel.Diastolic < BloodPressureRange.DiastolicLowered)
        {
            diaStatus = VitalStatus.Raised.ToString();
        }
        else if (bloodPressureInputModel.Diastolic < BloodPressureRange.DiastolicGood)
        {
            diaStatus = VitalStatus.Normal.ToString();
        }
        else if (bloodPressureInputModel.Diastolic < BloodPressureRange.DiastolicRaised)
        {
            diaStatus = VitalStatus.Raised.ToString();
        }
        else if (bloodPressureInputModel.Diastolic < BloodPressureRange.DiastolicHigh || bloodPressureInputModel.Diastolic > BloodPressureRange.DiastolicHigh)
        {
            diaStatus = VitalStatus.High.ToString();
        }
        // Determine final status with systolic having more weight
        string finalStatus;
        if (sysStatus == VitalStatus.High.ToString() || sysStatus == VitalStatus.Raised.ToString())
        {
            finalStatus = sysStatus; // Prioritize systolic status
        }
        else if (sysStatus == VitalStatus.Normal.ToString())
        {
            finalStatus = diaStatus; // Consider diastolic status only if systolic is normal
        }
        else
        {
            finalStatus = VitalStatus.Invalid.ToString(); // Handle invalid cases
        }

        // Return or set the final status
        return finalStatus;        
    }

}