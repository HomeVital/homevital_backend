using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces;

public interface IBodyTemperatureRepository
{
    // GetBodyTemperaturesByPatientId
    Task<IEnumerable<BodyTemperatureDto>> GetBodyTemperaturesByPatientId(int patientId);
    // CreateBodyTemperature
    Task<BodyTemperatureDto> CreateBodyTemperature(int patientId, BodyTemperatureInputModel bodyTemperatureInputModel);
    // UpdateBodyTemperature
    Task<BodyTemperatureDto> UpdateBodyTemperature(int id, BodyTemperatureInputModel bodyTemperatureInputModel);
    // DeleteBodyTemperature
    Task<BodyTemperatureDto> DeleteBodyTemperature(int id);
    
}