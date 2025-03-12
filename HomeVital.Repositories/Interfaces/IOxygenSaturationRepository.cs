using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;

namespace HomeVital.Repositories.Interfaces;

public interface IOxygenSaturationRepository
{
    // GetOxygenSaturationsByPatientId
    Task<IEnumerable<OxygenSaturationDto>> GetOxygenSaturationsByPatientId(int patientId);
    // CreateOxygenSaturation
    Task<OxygenSaturationDto> CreateOxygenSaturation(int patientId, OxygenSaturationInputModel oxygensaturationInputModel);
    // UpdateOxygenSaturation
    Task<OxygenSaturationDto> UpdateOxygenSaturation(int id, OxygenSaturationInputModel oxygensaturationInputModel);
    // DeleteOxygenSaturation
    Task<OxygenSaturationDto> DeleteOxygenSaturation(int id);

}