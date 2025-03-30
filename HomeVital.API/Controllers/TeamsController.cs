

using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;



namespace HomeVital.API.Controllers
{
    [ApiController]
    [Route("api/teams")]

    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost] // Create a new team
        public async Task<ActionResult<TeamDto>> CreateTeamAsync(TeamInputModel teamInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var newTeam = await _teamService.CreateTeamAsync(teamInputModel);
            return Ok(newTeam);
        }

        [HttpGet("{id}")] // Get a team by ID
        public async Task<ActionResult<TeamDto>> GetTeamByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var team = await _teamService.GetTeamByIdAsync(id);
            return Ok(team);
        }

        [HttpPatch("{id}")] // Update a team by ID
        public async Task<ActionResult<TeamDto>> UpdateTeamAsync(int id, [FromBody] TeamInputModel teamInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }

            var updatedTeam = await _teamService.UpdateTeamAsync(id, teamInputModel);
            return Ok(updatedTeam);
        }

        [HttpDelete("{id}")] // Delete a team by ID
        public async Task<ActionResult> DeleteTeamAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            await _teamService.DeleteTeamAsync(id);
            return Ok();
        }
    }
}