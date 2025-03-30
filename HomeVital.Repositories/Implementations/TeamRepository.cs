
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HomeVital.Repositories.dbContext;
using HomeVital.Models.Entities;
using HomeVital.Repositories.Interfaces;
using HomeVital.Models.InputModels;


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

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            var team = await _dbContext.Teams.FindAsync(id);
            if (team == null)
            {
                throw new KeyNotFoundException();
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
            var team = await _dbContext.Teams.FindAsync(id);
            if (team != null)
            {
                _dbContext.Teams.Remove(team);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}