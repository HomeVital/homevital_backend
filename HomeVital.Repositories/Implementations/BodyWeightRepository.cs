using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using HomeVital.Models.Entities;

namespace HomeVital.Repositories.Implementations;

public class BodyWeightRepository : IBodyWeightRepository
{

    private readonly HomeVitalDbContext _dbContext;
    private readonly IMapper _mapper;

    public BodyWeightRepository(HomeVitalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BodyWeightDto>> GetBodyWeightsByPatientId(int patientId)
    {
        var bodyWeights = await _dbContext.BodyWeights
            .Where(b => b.PatientID == patientId)
            .OrderByDescending(b => b.Date)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BodyWeightDto>>(bodyWeights);
    }

    public async Task<BodyWeightDto> CreateBodyWeight(int patientId, BodyWeightInputModel bodyWeightInputModel)
    {
        var bodyWeight = _mapper.Map<BodyWeight>(bodyWeightInputModel);
        bodyWeight.PatientID = patientId;
        bodyWeight.Date = DateTime.UtcNow;

        _dbContext.BodyWeights.Add(bodyWeight);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<BodyWeightDto>(bodyWeight);
    }

    public async Task<BodyWeightDto> UpdateBodyWeight(int id, BodyWeightInputModel bodyWeightInputModel)
    {
        var bodyWeight = await _dbContext.BodyWeights
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bodyWeight != null)
        {
            bodyWeight.Weight = bodyWeightInputModel.Weight;
            bodyWeight.Date = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        return _mapper.Map<BodyWeightDto>(bodyWeight);
    }

    public async Task<BodyWeightDto> DeleteBodyWeight(int id)
    {
        var bodyWeight = await _dbContext.BodyWeights
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bodyWeight != null)
        {
            _dbContext.BodyWeights.Remove(bodyWeight);
            await _dbContext.SaveChangesAsync();
        }

        return _mapper.Map<BodyWeightDto>(bodyWeight);
    }

    public async Task<BodyWeightDto> GetBodyWeightById(int id)
    {
        var bodyWeight = await _dbContext.BodyWeights
            .FirstOrDefaultAsync(b => b.ID == id);
    
        if (bodyWeight == null)
        {
            throw new KeyNotFoundException($"BodyWeight with ID {id} not found.");
        }
    
        return _mapper.Map<BodyWeightDto>(bodyWeight);
    }


}