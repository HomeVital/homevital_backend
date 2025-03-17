using System;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;


namespace HomeVital.Services.Interfaces
{
    public interface IOxygenSaturationService
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
}