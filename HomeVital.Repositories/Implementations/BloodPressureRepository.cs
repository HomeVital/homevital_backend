
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

        if (vitalRangeBloodpressure == null)
        {
            throw new System.ArgumentException("BloodPressure range not found");
        }
        // check blood pressure range
        bloodPressureInputModel.Status = CheckBloodPressureRange(bloodPressureInputModel, vitalRangeBloodpressure);

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

            bloodPressure.MeasureHand = bloodPressureInputModel.MeasureHand;
            bloodPressure.Systolic = bloodPressureInputModel.Systolic;
            bloodPressure.Diastolic = bloodPressureInputModel.Diastolic;
            bloodPressure.Pulse = bloodPressureInputModel.Pulse;
            bloodPressure.Status = bloodPressureInputModel.Status;
            bloodPressure.Date = DateTime.UtcNow;

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
            throw new System.ArgumentException("BloodPressure record not found");
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
    private static string CheckBloodPressureRange(BloodPressureInputModel bloodPressureInputModel, BloodPressureRange vitalRangeBloodpressure)
    {
        // SYS < 120 and DIA < 80
        if (bloodPressureInputModel.Systolic < vitalRangeBloodpressure.SystolicGoodMax 
        && bloodPressureInputModel.Diastolic < vitalRangeBloodpressure.DiastolicGoodMax
        )
        {
            return VitalStatus.Normal.ToString();
        }
        // SYS 120-129 and DIA < 80
        else if (bloodPressureInputModel.Systolic >= vitalRangeBloodpressure.SystolicOkMin 
        && bloodPressureInputModel.Systolic <= vitalRangeBloodpressure.SystolicOkMax 
        && bloodPressureInputModel.Diastolic <= vitalRangeBloodpressure.DiastolicOkMax)
        {
            return  VitalStatus.Raised.ToString();
        }
        // SYS 130-139 or DIA 80-89
        else if (bloodPressureInputModel.Systolic >= vitalRangeBloodpressure.SystolicNotOkMin 
        && bloodPressureInputModel.Systolic <= vitalRangeBloodpressure.SystolicNotOkMax 
        && bloodPressureInputModel.Diastolic >= vitalRangeBloodpressure.DiastolicNotOkMin 
        && bloodPressureInputModel.Diastolic <= vitalRangeBloodpressure.DiastolicNotOkMax)
        {
            return VitalStatus.High.ToString();
        }
        // SYS > 140 - SystolicCriticalStage3Min or DIA > 90 - DiastolicCriticalStage3Min
        else if (bloodPressureInputModel.Systolic >= vitalRangeBloodpressure.SystolicCriticalMin
        && bloodPressureInputModel.Systolic <= vitalRangeBloodpressure.SystolicCriticalStage3Min
        && bloodPressureInputModel.Diastolic >= vitalRangeBloodpressure.DiastolicCriticalMin
        && bloodPressureInputModel.Diastolic <= vitalRangeBloodpressure.DiastolicCriticalStage3Min)
        {
            return VitalStatus.Critical.ToString();
        }
        // SYS > 180  or DIA > 120 
        else if (bloodPressureInputModel.Systolic > vitalRangeBloodpressure.SystolicCriticalStage3Min
        && bloodPressureInputModel.Diastolic > vitalRangeBloodpressure.DiastolicCriticalStage3Min)
        {
            return VitalStatus.CriticalHigh.ToString();
        }
        else
        {
            return VitalStatus.Invalid.ToString();
        }
    }


}