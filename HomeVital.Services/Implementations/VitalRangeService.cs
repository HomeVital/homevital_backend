using HomeVital.Services.Interfaces;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
using HomeVital.Repositories.Interfaces;

namespace HomeVital.Services.Implementations;

public class VitalRangeService: IVitalRangeService
{
    private readonly IVitalRangeRepository _vitalRangeRepository;

    public VitalRangeService(IVitalRangeRepository vitalRangeRepository)
    {
        _vitalRangeRepository = vitalRangeRepository;
    }


    public async Task<BodyTemperatureRangeDto> UpdateBodyTemperatureRangeAsync(int patientId, BodyTemperatureRangeInputModel bodyTemperatureRangeInputModel)
    {
        // check if any of the input values are less than 0 or 0
        if (bodyTemperatureRangeInputModel.TemperatureGoodMin < 0 || bodyTemperatureRangeInputModel.TemperatureGoodMax < 0 ||
            bodyTemperatureRangeInputModel.TemperatureUnderAverage < 0 || bodyTemperatureRangeInputModel.TemperatureNotOkMin < 0 ||
            bodyTemperatureRangeInputModel.TemperatureNotOkMax < 0 || bodyTemperatureRangeInputModel.TemperatureCriticalMin < 0 ||
            bodyTemperatureRangeInputModel.TemperatureCriticalMax < 0)
        {
            throw new ArgumentException("Body temperature values cannot be less than 0");
        }

        return await _vitalRangeRepository.UpdateBodyTemperatureRangeAsync(patientId, bodyTemperatureRangeInputModel);
    }

    public async Task<BloodPressureRangeDto> UpdateBloodPressureRangeAsync(int patientId, BloodPressureRangeInputModel bloodPressureRangeInputModel)
    {
        // check if any of the input values are less than 0 or 0
        if (bloodPressureRangeInputModel.DiastolicCriticalMax < 0 || bloodPressureRangeInputModel.DiastolicCriticalMin < 0 ||
            bloodPressureRangeInputModel.DiastolicCriticalStage3Max < 0 || bloodPressureRangeInputModel.DiastolicCriticalStage3Min < 0 ||
            bloodPressureRangeInputModel.DiastolicGoodMax < 0 || bloodPressureRangeInputModel.DiastolicNotOkMax < 0 ||
            bloodPressureRangeInputModel.DiastolicNotOkMin < 0 || bloodPressureRangeInputModel.DiastolicOkMax < 0 ||
            bloodPressureRangeInputModel.DiastolicOkMin < 0 || bloodPressureRangeInputModel.SystolicCriticalMax < 0 ||
            bloodPressureRangeInputModel.SystolicCriticalMin < 0 || bloodPressureRangeInputModel.SystolicCriticalStage3Max < 0 ||
            bloodPressureRangeInputModel.SystolicCriticalStage3Min < 0 || bloodPressureRangeInputModel.SystolicGoodMax < 0 ||
            bloodPressureRangeInputModel.SystolicNotOkMax < 0 || bloodPressureRangeInputModel.SystolicNotOkMin < 0 ||
            bloodPressureRangeInputModel.SystolicOkMax < 0 || bloodPressureRangeInputModel.SystolicOkMin < 0)
        {
            // return with error message
            throw new ArgumentException("Blood pressure values cannot be less than 0");
        }

        return await _vitalRangeRepository.UpdateBloodPressureRangeAsync(patientId, bloodPressureRangeInputModel);
    }

    public async Task<BloodSugarRangeDto> UpdateBloodSugarRangeAsync(int patientId, BloodSugarRangeInputModel bloodSugarRangeInputModel)
    {
        // check if any of the input values are less than 0 or 0
        if (bloodSugarRangeInputModel.BloodSugarCriticalMax < 0 || bloodSugarRangeInputModel.BloodSugarCriticalMin < 0 ||
            bloodSugarRangeInputModel.BloodSugarGoodMax < 0 || bloodSugarRangeInputModel.BloodSugarGoodMin < 0 ||
            bloodSugarRangeInputModel.BloodSugarlowMax < 0 || bloodSugarRangeInputModel.BloodSugarlowMin < 0 ||
            bloodSugarRangeInputModel.BloodSugarNotOkMax < 0 || bloodSugarRangeInputModel.BloodSugarNotOkMin < 0)
        {
            throw new ArgumentException("Blood sugar values cannot be less than 0");
        }

        // check for gaps or overlaps between the ranges
        if (bloodSugarRangeInputModel.BloodSugarGoodMax >= bloodSugarRangeInputModel.BloodSugarNotOkMin ||
            bloodSugarRangeInputModel.BloodSugarNotOkMax >= bloodSugarRangeInputModel.BloodSugarCriticalMin ||
            bloodSugarRangeInputModel.BloodSugarlowMax <= bloodSugarRangeInputModel.BloodSugarGoodMin)
        {
            throw new ArgumentException("Blood sugar ranges are not aligned properly");
        }


        return await _vitalRangeRepository.UpdateBloodSugarRangeAsync(patientId, bloodSugarRangeInputModel);
    }

    public async Task<BodyWeightRangeDto> UpdateBodyWeightRangeAsync(int patientId, BodyWeightRangeInputModel bodyWeightRangeInputModel)
    {
        // check if any of the input values are less than 0 or 0
        if (bodyWeightRangeInputModel.WeightGainFluctuationPercentageGood < 0 || bodyWeightRangeInputModel.WeightGainPercentageGoodMax < 0 ||
            bodyWeightRangeInputModel.WeightLossFluctuationPercentageGood < 0 )
        {
            throw new ArgumentException("Body weight values cannot be less than 0");
        }

        return await _vitalRangeRepository.UpdateBodyWeightRangeAsync(patientId, bodyWeightRangeInputModel);
    }

    public async Task<OxygenSaturationRangeDto> UpdateOxygenSaturationRangeAsync(int patientId, OxygenSaturationRangeInputModel oxygenSaturationRangeInputModel)
    {
        // check if any of the input values are less than 0 or 0
        if (oxygenSaturationRangeInputModel.OxygenSaturationCriticalMax < 0 || oxygenSaturationRangeInputModel.OxygenSaturationCriticalMin < 0 ||
            oxygenSaturationRangeInputModel.OxygenSaturationGood < 0 || oxygenSaturationRangeInputModel.OxygenSaturationOkMax < 0 ||
            oxygenSaturationRangeInputModel.OxygenSaturationOkMin < 0 || oxygenSaturationRangeInputModel.OxygenSaturationNotOkMax < 0 ||
            oxygenSaturationRangeInputModel.OxygenSaturationNotOkMin < 0)
        {
            throw new ArgumentException("Oxygen saturation values cannot be less than 0");
        }

        return await _vitalRangeRepository.UpdateOxygenSaturationRangeAsync(patientId, oxygenSaturationRangeInputModel);
    }

    public async Task<VitalRangeDto> GetVitalRangeAsync(int patientId)
    {
        return await _vitalRangeRepository.GetVitalRangesByPatientIdAsync(patientId);
    }


}
