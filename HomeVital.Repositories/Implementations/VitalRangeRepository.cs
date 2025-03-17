using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Models.Entities;

namespace HomeVital.Repositories.Implementations;

public class VitalRangeRepository : IVitalRangeRepository
{
    private readonly HomeVitalDbContext _dbContext;
    private readonly IMapper _mapper;
    public VitalRangeRepository(HomeVitalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // update the body temperature range for the patient with the given id    
    public async Task<BodyTemperatureRangeDto> UpdateBodyTemperatureRangeAsync(int patientId, BodyTemperatureRangeInputModel bodyTemperatureRangeInputModel)
    {
        // update the body temperature range for the patient with the given id
        var patientsBodyTemperatureRange = await _dbContext.BodyTemperatureRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        
        // patient has a body temperature range
        if (patientsBodyTemperatureRange != null)
        {
            BodyTempRangesHelper(bodyTemperatureRangeInputModel, patientsBodyTemperatureRange);
            // save the changes
            await _dbContext.SaveChangesAsync();
        }
        
        return _mapper.Map<BodyTemperatureRangeDto>(patientsBodyTemperatureRange);
    }

    // update the body weight range for the patient with the given id
    public async Task<BodyWeightRangeDto> UpdateBodyWeightRangeAsync(int patientId, BodyWeightRangeInputModel bodyWeightRangeInputModel)
    {
        // update the body weight range for the patient with the given id
        var patientsBodyWeightRange = await _dbContext.BodyWeightRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        
        // patient has a body weight range
        if (patientsBodyWeightRange != null)
        {
            BodyWeightHelpter(bodyWeightRangeInputModel, patientsBodyWeightRange);
            // save the changes
            await _dbContext.SaveChangesAsync();
        }
        
        return _mapper.Map<BodyWeightRangeDto>(patientsBodyWeightRange);
    }

    // update the blood pressure range for the patient with the given id
    public async Task<BloodPressureRangeDto> UpdateBloodPressureRangeAsync(int patientId, BloodPressureRangeInputModel bloodPressureRangeInputModel)
    {
        // update the blood pressure range for the patient with the given id
        var patientsBloodPressureRange = await _dbContext.BloodPressureRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        
        // patient has a blood pressure range
        if (patientsBloodPressureRange != null)
        {
            BloodPressureRangeHelpger(bloodPressureRangeInputModel, patientsBloodPressureRange);
            // save the changes
            await _dbContext.SaveChangesAsync();
        }
        
        return _mapper.Map<BloodPressureRangeDto>(patientsBloodPressureRange);
    }

    // update the blood sugar range for the patient with the given id
    public async Task<BloodSugarRangeDto> UpdateBloodSugarRangeAsync(int patientId, BloodSugarRangeInputModel bloodSugarRangeInputModel)
    {
        // update the blood sugar range for the patient with the given id
        var patientsBloodSugarRange = await _dbContext.BloodSugarRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        
        // patient has a blood sugar range
        if (patientsBloodSugarRange != null)
        {
            BloodSugarRangeHelper(bloodSugarRangeInputModel, patientsBloodSugarRange);
            // save the changes
            await _dbContext.SaveChangesAsync();
        }
        
        return _mapper.Map<BloodSugarRangeDto>(patientsBloodSugarRange);
    }

    // update the oxygen saturation range for the patient with the given id
    public async Task<OxygenSaturationRangeDto> UpdateOxygenSaturationRangeAsync(int patientId, OxygenSaturationRangeInputModel oxygenSaturationRangeInputModel)
    {
        // update the oxygen saturation range for the patient with the given id
        var patientsOxygenSaturationRange = await _dbContext.OxygenSaturationRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        
        // patient has a oxygen saturation range
        if (patientsOxygenSaturationRange != null)
        {
            OxygenSaturationRangeHelper(oxygenSaturationRangeInputModel, patientsOxygenSaturationRange);
            // save the changes
            await _dbContext.SaveChangesAsync();
        }
        
        return _mapper.Map<OxygenSaturationRangeDto>(patientsOxygenSaturationRange);
    }

    
    public async Task<VitalRangeDto> GetVitalRangesByPatientIdAsync(int patientId)
    {
        var bodyTemperatureRange = await _dbContext.BodyTemperatureRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        var bloodPressureRange = await _dbContext.BloodPressureRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        var bloodSugarRange = await _dbContext.BloodSugarRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        var bodyWeightRange = await _dbContext.BodyWeightRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);
        var oxygenSaturationRange = await _dbContext.OxygenSaturationRanges.FirstOrDefaultAsync(x => x.PatientID == patientId);

        if (bodyTemperatureRange != null) bodyTemperatureRange.Id = 1;
        if (bloodPressureRange != null) bloodPressureRange.Id = 2;
        if (bloodSugarRange != null) bloodSugarRange.Id = 3;
        if (bodyWeightRange != null) bodyWeightRange.Id = 4;
        if (oxygenSaturationRange != null) oxygenSaturationRange.Id = 5;

        return new VitalRangeDto
        {
            BodyTemperatureRange = _mapper.Map<BodyTemperatureRangeDto>(bodyTemperatureRange),
            BloodPressureRange = _mapper.Map<BloodPressureRangeDto>(bloodPressureRange),
            BloodSugarRange = _mapper.Map<BloodSugarRangeDto>(bloodSugarRange),
            BodyWeightRange = _mapper.Map<BodyWeightRangeDto>(bodyWeightRange),
            OxygenSaturationRange = _mapper.Map<OxygenSaturationRangeDto>(oxygenSaturationRange)
        };
    }



    private void BodyTempRangesHelper(BodyTemperatureRangeInputModel bodyTemperatureRangeInputModel, BodyTemperatureRange patientsBodyTemperatureRange)
    {
        if (bodyTemperatureRangeInputModel.BodyTemperatureGoodMin.HasValue)
        {
            patientsBodyTemperatureRange.TemperatureGoodMin = bodyTemperatureRangeInputModel.BodyTemperatureGoodMin.Value;
        }
        if (bodyTemperatureRangeInputModel.BodyTemperatureGoodMax.HasValue)
        {
            patientsBodyTemperatureRange.TemperatureGoodMax = bodyTemperatureRangeInputModel.BodyTemperatureGoodMax.Value;
        }
        if (bodyTemperatureRangeInputModel.BodyTemperatureUnderAverage.HasValue)
        {
            patientsBodyTemperatureRange.TemperatureUnderAverage = bodyTemperatureRangeInputModel.BodyTemperatureUnderAverage.Value;
        }
        if (bodyTemperatureRangeInputModel.BodyTemperatureNotOkMin.HasValue)
        {
            patientsBodyTemperatureRange.TemperatureNotOkMin = bodyTemperatureRangeInputModel.BodyTemperatureNotOkMin.Value;
        }
        if (bodyTemperatureRangeInputModel.BodyTemperatureNotOkMax.HasValue)
        {
            patientsBodyTemperatureRange.TemperatureNotOkMax = bodyTemperatureRangeInputModel.BodyTemperatureNotOkMax.Value;
        }
        if (bodyTemperatureRangeInputModel.BodyTemperatureCriticalMin.HasValue)
        {
            patientsBodyTemperatureRange.TemperatureCriticalMin = bodyTemperatureRangeInputModel.BodyTemperatureCriticalMin.Value;
        }
        if (bodyTemperatureRangeInputModel.BodyTemperatureCriticalMax.HasValue)
        {
            patientsBodyTemperatureRange.TemperatureCriticalMax = bodyTemperatureRangeInputModel.BodyTemperatureCriticalMax.Value;
        }
    }

    private void BodyWeightHelpter(BodyWeightRangeInputModel bodyWeightRangeInputModel, BodyWeightRange patientsBodyWeightRange)
    {
        if (bodyWeightRangeInputModel.WeightGainFluctuationPercentageGood.HasValue)
        {
            patientsBodyWeightRange.WeightGainFluctuationPercentageGood = bodyWeightRangeInputModel.WeightGainFluctuationPercentageGood.Value;
        }
        if (bodyWeightRangeInputModel.WeightGainPercentageGoodMax.HasValue)
        {
            patientsBodyWeightRange.WeightGainPercentageGoodMax = bodyWeightRangeInputModel.WeightGainPercentageGoodMax.Value;
        }
        if (bodyWeightRangeInputModel.WeightLossFluctuationPercentageGood.HasValue)
        {
            patientsBodyWeightRange.WeightLossFluctuationPercentageGood = bodyWeightRangeInputModel.WeightLossFluctuationPercentageGood.Value;
        }
    }

    private void BloodPressureRangeHelpger(BloodPressureRangeInputModel bloodPressureRangeInputModel, BloodPressureRange patientsBloodPressureRange)
    {
        if (bloodPressureRangeInputModel.SystolicCriticalMax.HasValue)
        {
            patientsBloodPressureRange.SystolicCriticalMax = bloodPressureRangeInputModel.SystolicCriticalMax.Value;
        } 
        if (bloodPressureRangeInputModel.SystolicCriticalMin.HasValue)
        {
            patientsBloodPressureRange.SystolicCriticalMin = bloodPressureRangeInputModel.SystolicCriticalMin.Value;
        }
        if (bloodPressureRangeInputModel.SystolicCriticalStage3Max.HasValue)
        {
            patientsBloodPressureRange.SystolicCriticalStage3Max = bloodPressureRangeInputModel.SystolicCriticalStage3Max.Value;
        }
        if (bloodPressureRangeInputModel.SystolicCriticalStage3Min.HasValue)
        {
            patientsBloodPressureRange.SystolicCriticalStage3Min = bloodPressureRangeInputModel.SystolicCriticalStage3Min.Value;
        }
        if (bloodPressureRangeInputModel.SystolicNotOkMax.HasValue)
        {
            patientsBloodPressureRange.SystolicNotOkMax = bloodPressureRangeInputModel.SystolicNotOkMax.Value;
        }
        if (bloodPressureRangeInputModel.SystolicNotOkMin.HasValue)
        {
            patientsBloodPressureRange.SystolicNotOkMin = bloodPressureRangeInputModel.SystolicNotOkMin.Value;
        }
        if (bloodPressureRangeInputModel.SystolicOkMax.HasValue)
        {
            patientsBloodPressureRange.SystolicOkMax = bloodPressureRangeInputModel.SystolicOkMax.Value;
        }
        if (bloodPressureRangeInputModel.SystolicOkMin.HasValue)
        {
            patientsBloodPressureRange.SystolicOkMin = bloodPressureRangeInputModel.SystolicOkMin.Value;
        }
        if (bloodPressureRangeInputModel.SystolicGoodMax.HasValue)
        {
            patientsBloodPressureRange.SystolicGoodMax = bloodPressureRangeInputModel.SystolicGoodMax.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicCriticalMax.HasValue)
        {
            patientsBloodPressureRange.DiastolicCriticalMax = bloodPressureRangeInputModel.DiastolicCriticalMax.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicCriticalMin.HasValue)
        {
            patientsBloodPressureRange.DiastolicCriticalMin = bloodPressureRangeInputModel.DiastolicCriticalMin.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicCriticalStage3Max.HasValue)
        {
            patientsBloodPressureRange.DiastolicCriticalStage3Max = bloodPressureRangeInputModel.DiastolicCriticalStage3Max.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicCriticalStage3Min.HasValue)
        {
            patientsBloodPressureRange.DiastolicCriticalStage3Min = bloodPressureRangeInputModel.DiastolicCriticalStage3Min.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicNotOkMax.HasValue)
        {
            patientsBloodPressureRange.DiastolicNotOkMax = bloodPressureRangeInputModel.DiastolicNotOkMax.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicNotOkMin.HasValue)
        {
            patientsBloodPressureRange.DiastolicNotOkMin = bloodPressureRangeInputModel.DiastolicNotOkMin.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicOkMax.HasValue)
        {
            patientsBloodPressureRange.DiastolicOkMax = bloodPressureRangeInputModel.DiastolicOkMax.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicOkMin.HasValue)
        {
            patientsBloodPressureRange.DiastolicOkMin = bloodPressureRangeInputModel.DiastolicOkMin.Value;
        }
        if (bloodPressureRangeInputModel.DiastolicGoodMax.HasValue)
        {
            patientsBloodPressureRange.DiastolicGoodMax = bloodPressureRangeInputModel.DiastolicGoodMax.Value;
        }

    }

    private void BloodSugarRangeHelper(BloodSugarRangeInputModel bloodSugarRangeInputModel, BloodSugarRange patientsBloodSugarRange)
    {
        if (bloodSugarRangeInputModel.BloodSugarCriticalMax.HasValue)
        {
            patientsBloodSugarRange.BloodSugarCriticalMax = bloodSugarRangeInputModel.BloodSugarCriticalMax.Value;
        }
        if (bloodSugarRangeInputModel.BloodSugarCriticalMin.HasValue)
        {
            patientsBloodSugarRange.BloodSugarCriticalMin = bloodSugarRangeInputModel.BloodSugarCriticalMin.Value;
        }
        if (bloodSugarRangeInputModel.BloodSugarNotOkMax.HasValue)
        {
            patientsBloodSugarRange.BloodSugarNotOkMax = bloodSugarRangeInputModel.BloodSugarNotOkMax.Value;
        }
        if (bloodSugarRangeInputModel.BloodSugarNotOkMin.HasValue)
        {
            patientsBloodSugarRange.BloodSugarNotOkMin = bloodSugarRangeInputModel.BloodSugarNotOkMin.Value;
        }
        if (bloodSugarRangeInputModel.BloodSugarGoodMax.HasValue)
        {
            patientsBloodSugarRange.BloodSugarGoodMax = bloodSugarRangeInputModel.BloodSugarGoodMax.Value;
        }
        if (bloodSugarRangeInputModel.BloodSugarGoodMin.HasValue)
        {
            patientsBloodSugarRange.BloodSugarGoodMin = bloodSugarRangeInputModel.BloodSugarGoodMin.Value;
        }
        if (bloodSugarRangeInputModel.BloodSugarlowMax.HasValue)
        {
            patientsBloodSugarRange.BloodSugarlowMax = bloodSugarRangeInputModel.BloodSugarlowMax.Value;
        }
        if (bloodSugarRangeInputModel.BloodSugarlowMin.HasValue)
        {
            patientsBloodSugarRange.BloodSugarlowMin = bloodSugarRangeInputModel.BloodSugarlowMin.Value;
        }
        
    }

    private void OxygenSaturationRangeHelper(OxygenSaturationRangeInputModel oxygenSaturationRangeInputModel, OxygenSaturationRange patientsOxygenSaturationRange)
    {
        if (oxygenSaturationRangeInputModel.OxygenSaturationCriticalMax.HasValue)
        {
            patientsOxygenSaturationRange.OxygenSaturationCriticalMax = oxygenSaturationRangeInputModel.OxygenSaturationCriticalMax.Value;
        }
        if (oxygenSaturationRangeInputModel.OxygenSaturationCriticalMin.HasValue)
        {
            patientsOxygenSaturationRange.OxygenSaturationCriticalMin = oxygenSaturationRangeInputModel.OxygenSaturationCriticalMin.Value;
        }
        if (oxygenSaturationRangeInputModel.OxygenSaturationNotOkMax.HasValue)
        {
            patientsOxygenSaturationRange.OxygenSaturationNotOkMax = oxygenSaturationRangeInputModel.OxygenSaturationNotOkMax.Value;
        }
        if (oxygenSaturationRangeInputModel.OxygenSaturationNotOkMin.HasValue)
        {
            patientsOxygenSaturationRange.OxygenSaturationNotOkMin = oxygenSaturationRangeInputModel.OxygenSaturationNotOkMin.Value;
        }
        if (oxygenSaturationRangeInputModel.OxygenSaturationGood.HasValue)
        {
            patientsOxygenSaturationRange.OxygenSaturationGood = oxygenSaturationRangeInputModel.OxygenSaturationGood.Value;
        }
        if (oxygenSaturationRangeInputModel.OxygenSaturationOkMin.HasValue)
        {
            patientsOxygenSaturationRange.OxygenSaturationOkMin = oxygenSaturationRangeInputModel.OxygenSaturationOkMin.Value;
        }
        if (oxygenSaturationRangeInputModel.OxygenSaturationOkMax.HasValue)
        {
            patientsOxygenSaturationRange.OxygenSaturationOkMax = oxygenSaturationRangeInputModel.OxygenSaturationOkMax.Value;
        }
    }
}