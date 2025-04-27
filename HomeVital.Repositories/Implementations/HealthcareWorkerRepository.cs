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
        // get all healthcare workers and their teams
        var healthcareWorkers = await _dbContext.HealthcareWorkers
            .Include(h => h.Teams)
            .ToListAsync();

        return _mapper.Map<IEnumerable<HealthcareWorkerDto>>(healthcareWorkers);
    }

    public async Task<HealthcareWorkerDto> GetHealthcareWorkerById(int id)
    {
        var healthcareWorker = await _dbContext.HealthcareWorkers.FirstOrDefaultAsync(x => x.ID == id);
        return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);
    }

    public async Task<HealthcareWorkerDto> DeleteHealthcareWorker(int id)
    {
        // delete healthcare worker by id remove from teams and remove from user
        var healthcareWorkerToDelete = await _dbContext.HealthcareWorkers
            .Include(h => h.Teams)
            .FirstOrDefaultAsync(x => x.ID == id);
        if (healthcareWorkerToDelete != null)
        {
            // remove from teams
            foreach (var team in healthcareWorkerToDelete.Teams)
            {
                team.HealthcareWorkers.Remove(healthcareWorkerToDelete);
            }

            _dbContext.Users.RemoveRange(_dbContext.Users.Where(u => u.HealthcareWorkerID == id));

            _dbContext.HealthcareWorkers.Remove(healthcareWorkerToDelete);
            await _dbContext.SaveChangesAsync();
        }

        return _mapper.Map<HealthcareWorkerDto>(healthcareWorkerToDelete);
    }

    public async Task<HealthcareWorkerDto> CreateHealthcareWorker(HealthcareWorkerInputModel healthcareWorker)
    {
        var newHealthcareWorker = _mapper.Map<HealthcareWorker>(healthcareWorker);

        // add date to when the healthcare worker was created???
        // newHealthcareWorker.DateCreated = DateTime.UtcNow;

        // init UserID for healthcare worker
        var newUser = new User
        {
            HealthcareWorkerID = newHealthcareWorker.ID,
            Kennitala = healthcareWorker.Kennitala
        };
        _dbContext.Users.Add(newUser);
        await _dbContext.SaveChangesAsync();

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
            healthcareWorkerToUpdate.Teams = healthcareWorker.TeamIDs.Select(teamId => new Team { ID = teamId }).ToList();
            healthcareWorkerToUpdate.Status = healthcareWorker.Status;
            await _dbContext.SaveChangesAsync();
        }
        return _mapper.Map<HealthcareWorkerDto>(healthcareWorkerToUpdate);
    }
    
}