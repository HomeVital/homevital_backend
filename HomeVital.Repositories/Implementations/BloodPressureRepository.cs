
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Models.Enums;


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
        var bloodPressures = await _dbContext.BloodPressures
            .Where(b => b.PatientID == patientId)
            .OrderByDescending(b => b.Date)
            .ToListAsync();

        // order by date
        bloodPressures = bloodPressures.OrderByDescending(b => b.Date).ToList();

        return _mapper.Map<IEnumerable<BloodPressureDto>>(bloodPressures);
    }

    public async Task<BloodPressureDto> CreateBloodPressure(int patientId, BloodPressureInputModel bloodPressureInputModel)
    {
        var vitalRangeBloodpressure = await _dbContext.BloodPressureRanges
            .FirstOrDefaultAsync(b => b.PatientID == patientId);

        // if (vitalRangeBloodpressure == null)
        // {
        //     return null;
        // }
        // check blood pressure range
        bloodPressureInputModel.Status = CheckBloodPressureRange(bloodPressureInputModel, vitalRangeBloodpressure);
        // make sure measure hand and body position are not null


        var bloodPressure = _mapper.Map<BloodPressure>(bloodPressureInputModel);
        bloodPressure.PatientID = patientId;
        bloodPressure.Date = DateTime.UtcNow;

        _dbContext.BloodPressures.Add(bloodPressure);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }
  

    public async Task<BloodPressureDto> UpdateBloodPressure(int id, BloodPressureInputModel bloodPressureInputModel)
    {
        var bloodPressure = await _dbContext.BloodPressures
            .FirstOrDefaultAsync(b => b.ID == id);

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

            await _dbContext.SaveChangesAsync();
        }
        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }

    public async Task<BloodPressureDto> DeleteBloodPressure(int id)
    {
        var bloodPressure = await _dbContext.BloodPressures
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bloodPressure == null)
        {
            return null;
        }

        _dbContext.BloodPressures.Remove(bloodPressure);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }

    public async Task<BloodPressureDto> GetBloodPressureById(int id)
    {
        var bloodPressure = await _dbContext.BloodPressures
            .FirstOrDefaultAsync(b => b.ID == id);

        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }

    // private function to check blood pressure range
    private static string CheckBloodPressureRange(BloodPressureInputModel bloodPressureInputModel, BloodPressureRange BloodPressureRange)
    {
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