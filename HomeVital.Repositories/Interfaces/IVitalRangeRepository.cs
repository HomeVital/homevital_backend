using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces;

public interface IVitalRangeRepository
{
    // update body temperature range
    Task<BodyTemperatureRangeDto> UpdateBodyTemperatureRangeAsync(int patientId, BodyTemperatureRangeInputModel bodyTemperatureRangeInputModel);
    // update blood pressure range
    Task<BloodPressureRangeDto> UpdateBloodPressureRangeAsync(int patientId, BloodPressureRangeInputModel bloodPressureRangeInputModel);
    // update blood sugar range
    Task<BloodSugarRangeDto> UpdateBloodSugarRangeAsync(int patientId, BloodSugarRangeInputModel bloodSugarRangeInputModel);
    // update body weight range
    Task<BodyWeightRangeDto> UpdateBodyWeightRangeAsync(int patientId, BodyWeightRangeInputModel bodyWeightRangeInputModel);
    // update oxygen saturation range
    Task<OxygenSaturationRangeDto> UpdateOxygenSaturationRangeAsync(int patientId, OxygenSaturationRangeInputModel oxygenSaturationRangeInputModel);
    // get vital ranges
    Task<VitalRangeDto> GetVitalRangesByPatientIdAsync(int patientId);
}