
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

    public async Task<IEnumerable<BloodPressureDto>> GetBloodPressuresByPatientId(int patientId)
    {
        var bloodPressures = await _dbContext.BloodPressures
            .Where(b => b.PatientID == patientId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BloodPressureDto>>(bloodPressures);
    }

    public async Task<BloodPressureDto> CreateBloodPressure(int patientId, BloodPressureInputModel bloodPressureInputModel)
    {
        var newMeasurement = new Measurement
        {
            PatientID = patientId,
            
        };
        _dbContext.Measurements.Add(newMeasurement);
        await _dbContext.SaveChangesAsync();

        var bloodPressure = _mapper.Map<BloodPressure>(bloodPressureInputModel);
        bloodPressure.PatientID = patientId;
        bloodPressure.MeasurementID = newMeasurement.ID;
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




}