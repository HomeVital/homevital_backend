

using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using HomeVital.API.Extensions;



namespace HomeVital.API.Controllers
{
    [Authorize]
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

            // Check if the team exists
            var existingTeam = await _teamService.GetTeamByIdAsync(id);
            if (existingTeam == null)
            {
                return NotFound();
            }
            return Ok(existingTeam);
        }

        [HttpGet] // Get all teams
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAllTeamsAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [Authorize(Roles = "HealthcareWorker")]
        [HttpPatch("{id}")] // Update a team by ID
        public async Task<ActionResult<TeamDto>> UpdateTeamAsync(int id, [FromBody] TeamInputModel teamInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new System.ArgumentException("Invalid input model");
            }
            // Check if the team exists
            var existingTeam = await _teamService.GetTeamByIdAsync(id);
            if (existingTeam == null)
            {
                return NotFound();
            }

            var updatedTeam = await _teamService.UpdateTeamAsync(id, teamInputModel);
            return Ok(updatedTeam);
        }

        [HttpDelete("{id}")] // Delete a team by ID
        public async Task<ActionResult> DeleteTeamAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Input model is not valid");
            }
            // Check if the team exists before attempting to delete
            var existingTeam = await _teamService.GetTeamByIdAsync(id);
            if (existingTeam == null)
            {
                return NotFound();
            }

            // Call the service to delete the team and retrieve the deleted team's info
            await _teamService.DeleteTeamAsync(id);
            // Return the deleted team's info
            // Note: You might want to return a specific response or the deleted team info
            return Ok(existingTeam);
        }
    }
}