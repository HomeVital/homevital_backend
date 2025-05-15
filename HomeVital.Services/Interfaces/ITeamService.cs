
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;


namespace HomeVital.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> CreateTeamAsync(TeamInputModel teamInputModel);
        Task<TeamDto> GetTeamByIdAsync(int id);
        Task<TeamDto> UpdateTeamAsync(int id, TeamInputModel teamInputModel);
        Task DeleteTeamAsync(int id);
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
    }
}