
using HomeVital.Models.Entities;

namespace HomeVital.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<Team> CreateTeamAsync(Team team);
        Task<Team> GetTeamByIdAsync(int id);
        Task<Team> UpdateTeamAsync(Team team);
        Task DeleteTeamAsync(int id);
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamWithRelationsAsync(int id);
    }
}