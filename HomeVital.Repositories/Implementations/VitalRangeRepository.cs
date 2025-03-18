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
            UpdateEntity(bodyTemperatureRangeInputModel, patientsBodyTemperatureRange);
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
            UpdateEntity(bodyWeightRangeInputModel, patientsBodyWeightRange);
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
            UpdateEntity(bloodPressureRangeInputModel, patientsBloodPressureRange);
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
            UpdateEntity(bloodSugarRangeInputModel, patientsBloodSugarRange);
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
            UpdateEntity(oxygenSaturationRangeInputModel, patientsOxygenSaturationRange);
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

    private void UpdateEntity<TInputModel, TEntity>(TInputModel inputModel, TEntity entity)
    {
        var inputProperties = typeof(TInputModel).GetProperties();
        var entityProperties = typeof(TEntity).GetProperties();

        foreach (var inputProperty in inputProperties)
        {
            if (inputProperty.Name == "PatientID") continue;
            var inputValue = inputProperty.GetValue(inputModel);
            if (inputValue != null)
            {
                var entityProperty = entityProperties.FirstOrDefault(p => p.Name == inputProperty.Name);
                if (entityProperty != null)
                {
                    entityProperty.SetValue(entity, inputValue);
                }
            }
        }
    }
}