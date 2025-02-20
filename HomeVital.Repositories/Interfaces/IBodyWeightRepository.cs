using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces;
public interface IBodyWeightRepository
{
    // GetBodyWeightsByPatientId
    Task<IEnumerable<BodyWeightDto>> GetBodyWeightsByPatientId(int patientId);
    // CreateBodyWeight
    Task<BodyWeightDto> CreateBodyWeight(int patientId, BodyWeightInputModel bodyWeightInputModel);
    // UpdateBodyWeight
    Task<BodyWeightDto> UpdateBodyWeight(int id, BodyWeightInputModel bodyWeightInputModel);
    // DeleteBodyWeight
    Task<BodyWeightDto> DeleteBodyWeight(int id);
    // GetBodyWeightById
    Task<BodyWeightDto> GetBodyWeightById(int id);
    
}