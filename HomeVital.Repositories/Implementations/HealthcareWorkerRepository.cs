using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeVital.Repositories.Implementations;

public class HealthcareWorkerRepository : IHealthcareWorkerRepository
{
    private readonly HomeVitalDbContext _dbContext;
    private readonly IMapper _mapper;
    public HealthcareWorkerRepository(HomeVitalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HealthcareWorkerDto>> GetHealthcareWorkers()
    {
        var healthcareWorkers = await _dbContext.HealthcareWorkers.ToListAsync();
        return _mapper.Map<IEnumerable<HealthcareWorkerDto>>(healthcareWorkers);
    }

    public async Task<HealthcareWorkerDto> GetHealthcareWorkerById(int id)
    {
        var healthcareWorker = await _dbContext.HealthcareWorkers.FirstOrDefaultAsync(x => x.ID == id);
        return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);
    }

    public async Task<HealthcareWorkerDto> DeleteHealthcareWorker(int id)
    {
        var healthcareWorker = await _dbContext.HealthcareWorkers.FirstOrDefaultAsync(x => x.ID == id);
        if (healthcareWorker != null)
        {
            _dbContext.HealthcareWorkers.Remove(healthcareWorker);
            await _dbContext.SaveChangesAsync();
        }
        return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);
    }

    public async Task<HealthcareWorkerDto> CreateHealthcareWorker(HealthcareWorkerInputModel healthcareWorker)
    {
        var newHealthcareWorker = _mapper.Map<HealthcareWorker>(healthcareWorker);
        await _dbContext.HealthcareWorkers.AddAsync(newHealthcareWorker);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<HealthcareWorkerDto>(newHealthcareWorker);
    }

    // UpdateHealthcareWorker
    public async Task<HealthcareWorkerDto> UpdateHealthcareWorker(int id, HealthcareWorkerInputModel healthcareWorker)
    {
        var healthcareWorkerToUpdate = await _dbContext.HealthcareWorkers.FirstOrDefaultAsync(x => x.ID == id);
        if (healthcareWorkerToUpdate != null)
        {
            healthcareWorkerToUpdate.Name = healthcareWorker.Name;
            healthcareWorkerToUpdate.Phone = healthcareWorker.Phone;
            healthcareWorkerToUpdate.TeamID = healthcareWorker.TeamID;
            healthcareWorkerToUpdate.Status = healthcareWorker.Status;
            await _dbContext.SaveChangesAsync();
        }
        return _mapper.Map<HealthcareWorkerDto>(healthcareWorkerToUpdate);
    }
    
}