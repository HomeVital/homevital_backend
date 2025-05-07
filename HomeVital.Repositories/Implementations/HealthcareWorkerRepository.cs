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

    public async Task<List<HealthcareWorker>> GetHealthcareWorkersByIdsAsync(IEnumerable<int> ids)
    {
        return await _dbContext.HealthcareWorkers
            .Where(hw => ids.Contains(hw.ID))
            .ToListAsync();
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
        var healthcareWorker = await _dbContext.HealthcareWorkers
            .Include(h => h.Teams)
            .FirstOrDefaultAsync(x => x.ID == id);
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
        
        // Fetch actual team entities if any team IDs provided
        if (healthcareWorker.TeamIDs != null && healthcareWorker.TeamIDs.Any())
        {
            var teams = await _dbContext.Teams
                .Where(t => healthcareWorker.TeamIDs.Contains(t.ID))
                .ToListAsync();
                
            foreach (var team in teams)
            {
                newHealthcareWorker.Teams.Add(team);
            }
        }

        // Create associated user
        var newUser = new User
        {
            HealthcareWorkerID = newHealthcareWorker.ID,
            Kennitala = healthcareWorker.Kennitala
        };
        
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.HealthcareWorkers.AddAsync(newHealthcareWorker);
        await _dbContext.SaveChangesAsync();
        
        return _mapper.Map<HealthcareWorkerDto>(newHealthcareWorker);
    }

    public async Task<HealthcareWorkerDto> UpdateHealthcareWorker(int id, HealthcareWorkerInputModel healthcareWorker)
    {
        var healthcareWorkerToUpdate = await _dbContext.HealthcareWorkers
            .Include(hw => hw.Teams)
            .FirstOrDefaultAsync(x => x.ID == id);
            
        if (healthcareWorkerToUpdate != null)
        {
            // Update basic properties
            healthcareWorkerToUpdate.Name = healthcareWorker.Name;
            healthcareWorkerToUpdate.Phone = healthcareWorker.Phone;
            healthcareWorkerToUpdate.Status = healthcareWorker.Status;
            
            // Handle teams relationship properly
            if (healthcareWorker.TeamIDs != null)
            {
                // Clear current teams and add the new ones
                healthcareWorkerToUpdate.Teams.Clear();
                
                // Fetch actual team entities from the database
                var teams = await _dbContext.Teams
                    .Where(t => healthcareWorker.TeamIDs.Contains(t.ID))
                    .ToListAsync();
                    
                foreach (var team in teams)
                {
                    healthcareWorkerToUpdate.Teams.Add(team);
                }
            }
            
            await _dbContext.SaveChangesAsync();
        }
        
        return _mapper.Map<HealthcareWorkerDto>(healthcareWorkerToUpdate);
    }
    
}