
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Models.Exceptions;


namespace HomeVital.Repositories.Implementations
{
    public class TeamRepository : ITeamRepository
    {
        private readonly HomeVitalDbContext _dbContext;
        private readonly IMapper _mapper;

        public TeamRepository(HomeVitalDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Team> CreateTeamAsync(Team team)
        {
            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();
            return team;
        }
        public async Task<Team> GetTeamWithRelationsAsync(int id)
        {
            var team = await _dbContext.Teams
                .Include(t => t.HealthcareWorkers)
                .Include(t => t.Patients)
                .FirstOrDefaultAsync(t => t.ID == id);

            if (team == null)
            {
                throw new ResourceNotFoundException("Team not found with ID: " + id);
            }

            return team;
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            // var team = await _dbContext.Teams.FindAsync(id);
            var team = await _dbContext.Teams
                .Include(t => t.HealthcareWorkers)
                .Include(t => t.Patients)
                .FirstOrDefaultAsync(t => t.ID == id);
                
            if (team == null)
            {
                throw new ResourceNotFoundException("Team not found with ID: " + id);
            }
            return team;
        }

        public async Task<Team> UpdateTeamAsync(Team team)
        {
            _dbContext.Teams.Update(team);
            await _dbContext.SaveChangesAsync();
            return team;
        }

       
        public async Task DeleteTeamAsync(int id)
        {
            var team = await _dbContext.Teams
                .Include(t => t.Patients)
                .Include(t => t.HealthcareWorkers)
                .FirstOrDefaultAsync(t => t.ID == id);

            if (team == null)
            {
                throw new ResourceNotFoundException("Team not found with ID: " + id);
            }

            // Remove relationships with patients
            foreach (var patient in team.Patients)
            {
                patient.TeamID = null; // Set TeamID to null to break the relationship
            }

            // Remove relationships with healthcare workers
            foreach (var worker in team.HealthcareWorkers)
            {
                worker.Teams.Remove(team); // Remove the team from the healthcare worker's list of teams
            }

            // Save changes to update the relationships
            await _dbContext.SaveChangesAsync();

            // Now delete the team
            _dbContext.Teams.Remove(team);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            var teams = await _dbContext.Teams
                .Include(t => t.HealthcareWorkers)
                .Include(t => t.Patients)
                .ToListAsync();
            if (teams == null)
            {
                throw new ResourceNotFoundException("No teams found.");
            }
            return teams;
        }

    }
}