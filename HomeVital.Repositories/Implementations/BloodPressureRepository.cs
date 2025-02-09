
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;



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

    public async Task<BloodPressureDto> CreateBloodPressure(BloodPressureInputModel inputModel)
    {
        BloodPressure? bloodPressure = new BloodPressure
        {
            PatientID = inputModel.PatientID,
            Systolic = inputModel.Systolic,
            Diastolic = inputModel.Diastolic,
            Date = inputModel.Date
        };

        _dbContext.BloodPressures.Add(bloodPressure);
        await _dbContext.SaveChangesAsync();

        BloodPressure? storedBloodPressure = await _dbContext.BloodPressures
            .FirstOrDefaultAsync(b => b.ID == bloodPressure.ID);

        var bloodPressureDto = _mapper.Map<BloodPressureDto>(storedBloodPressure);

        return bloodPressureDto;
    }

    public async Task<BloodPressureDto?> GetBloodPressureByIdAsync(int id)
    {
        BloodPressure? bloodPressure = await _dbContext.BloodPressures.FindAsync(id);
        if (bloodPressure == null)
        {
            return null;
        }

        var bloodPressureDto = _mapper.Map<BloodPressureDto>(bloodPressure);

        return bloodPressureDto;
    }

    public async Task<bool> DeleteBloodPressureAsync(int id)
    {
        BloodPressure? bloodPressure = await _dbContext.BloodPressures.FindAsync(id);
        if (bloodPressure == null)
        {
            return false;
        }

        _dbContext.BloodPressures.Remove(bloodPressure);
        await _dbContext.SaveChangesAsync();

        return true;
    }

   
    public async Task<BloodPressureDto?> UpdateBloodPressureAsync(int id, BloodPressureInputModel updatedBloodPressure)
    {
        BloodPressure? bloodPressure = await _dbContext.BloodPressures.FindAsync(id);
        if (bloodPressure == null)
        {
            return null;
        }

        bloodPressure.PatientID = updatedBloodPressure.PatientID;
        bloodPressure.Systolic = updatedBloodPressure.Systolic;
        bloodPressure.Diastolic = updatedBloodPressure.Diastolic;
        bloodPressure.Date = updatedBloodPressure.Date;

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }

    public async Task<BloodPressureDto?> GetBloodPressureByUserIdAsync(int userId)
    {
        var bloodPressure = await _dbContext.BloodPressures
            .FirstOrDefaultAsync(bp => bp.PatientID == userId);

        if (bloodPressure == null)
        {
            return null;
        }

        return _mapper.Map<BloodPressureDto>(bloodPressure);
    }
}