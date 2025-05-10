using AutoMapper;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Dtos;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Models.Entities;
using Microsoft.EntityFrameworkCore;
using HomeVital.Models.Exceptions;

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
        // get healthcare workers by ids
        // query the database for healthcare workers with the specified IDs
        // using Include to load related entities if necessary
        // return the list of healthcare workers
        var healthcareWorkers = await _dbContext.HealthcareWorkers
            .Where(hw => ids.Contains(hw.ID))
            // .Include(hw => hw.Teams)
            .ToListAsync();
        if (healthcareWorkers == null || !healthcareWorkers.Any())
        {
            throw new ResourceNotFoundException("No healthcare workers found with the provided IDs.");
        }
        return healthcareWorkers;
    }
    public async Task<IEnumerable<HealthcareWorkerDto>> GetHealthcareWorkers()
    {
        // get all healthcare workers and their teams
        // return the list of healthcare workers
        var healthcareWorkers = await _dbContext.HealthcareWorkers
            .Include(h => h.Teams)
            .ToListAsync();

        if (healthcareWorkers == null || !healthcareWorkers.Any())
        {
            throw new ResourceNotFoundException("No healthcare workers found.");
        }

        return _mapper.Map<IEnumerable<HealthcareWorkerDto>>(healthcareWorkers);
    }

    public async Task<HealthcareWorkerDto> GetHealthcareWorkerById(int id)
    {
        // get healthcare worker by id and their teams
        // return the healthcare worker
        var healthcareWorker = await _dbContext.HealthcareWorkers
            .Include(h => h.Teams)
            .FirstOrDefaultAsync(x => x.ID == id);

        if (healthcareWorker == null)
        {
            throw new ResourceNotFoundException("Healthcare worker not found with ID: " + id);
        }

        return _mapper.Map<HealthcareWorkerDto>(healthcareWorker);
    }

    public async Task<HealthcareWorkerDto> DeleteHealthcareWorker(int id)
    {
        // delete healthcare worker by id remove from teams and remove from user
        // return the deleted healthcare worker
        // check if the healthcare worker exists
        // if not, throw an exception
        var healthcareWorkerToDelete = await _dbContext.HealthcareWorkers
            .Include(h => h.Teams)
            .FirstOrDefaultAsync(x => x.ID == id);

        if (healthcareWorkerToDelete == null)
        {
            throw new ResourceNotFoundException("Healthcare worker not found with ID: " + id);
        }

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
            Kennitala = healthcareWorker.Kennitala ?? throw new VarArgumentException("Kennitala cannot be null.")
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

        if (healthcareWorkerToUpdate == null)
        {
            throw new ResourceNotFoundException("Healthcare worker not found with ID: " + id);
        }
            
        if (healthcareWorkerToUpdate != null)
        {
            
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