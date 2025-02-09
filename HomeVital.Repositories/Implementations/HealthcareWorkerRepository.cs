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

    public async Task<HealthcareWorkerDto> CreateHealthcareWorkerAsync(HealthcareWorkerInputModel healthcareWorker)
    {
        var healthcareWorker_ = new HealthcareWorker
        {
            Name = healthcareWorker.Name,
            Phone = healthcareWorker.Phone,
            TeamID = healthcareWorker.TeamID,
            Status = healthcareWorker.Status
        };

        _dbContext.HealthcareWorkers.Add(healthcareWorker_);
        await _dbContext.SaveChangesAsync();

        var healthcareWorkerDto = new HealthcareWorkerDto
        {
            ID = healthcareWorker_.ID,
            Name = healthcareWorker_.Name,
            Phone = healthcareWorker_.Phone,
            TeamID = healthcareWorker_.TeamID,
            Status = healthcareWorker_.Status
        };

        return healthcareWorkerDto;
    }

    public async Task<HealthcareWorkerDto?> GetHealthcareWorkerByIdAsync(int id)
    {
        HealthcareWorker? healthcareWorker = await _dbContext.HealthcareWorkers.FindAsync(id);
        if (healthcareWorker == null)
        {
            return null;
        }

        var healthcareWorkerDto = _mapper.Map<HealthcareWorkerDto>(healthcareWorker);

        return healthcareWorkerDto;
    }

    public async Task<bool> DeleteHealthcareWorkerAsync(int id)
    {
        HealthcareWorker? healthcareWorker = await _dbContext.HealthcareWorkers.FindAsync(id);
        if (healthcareWorker == null)
        {
            return false;
        }

        _dbContext.HealthcareWorkers.Remove(healthcareWorker);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<HealthcareWorkerDto?> UpdateHealthcareWorkerAsync(int id, HealthcareWorkerInputModel updatedHealthcareWorker)
    {
        HealthcareWorker? healthcareWorker = await _dbContext.HealthcareWorkers.FirstOrDefaultAsync(h => h.ID == id);
        if (healthcareWorker == null)
        {
            return null;
        }

        _mapper.Map(updatedHealthcareWorker, healthcareWorker);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);
    }

    public async Task<HealthcareWorkerDto[]> GetHealthcareWorkersAsync()
    {
        var healthcareWorkers = await _dbContext.HealthcareWorkers.ToArrayAsync();
        var healthcareWorkerDtos = _mapper.Map<HealthcareWorkerDto[]>(healthcareWorkers);

        return healthcareWorkerDtos;
    }
}