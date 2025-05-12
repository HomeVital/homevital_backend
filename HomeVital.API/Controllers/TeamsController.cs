

using Microsoft.AspNetCore.Mvc;
using HomeVital.Models.InputModels;
using HomeVital.Models.Dtos;
using HomeVital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using HomeVital.Models.Exceptions;
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

        [Authorize(Roles = "HealthcareWorker")]
        [HttpPost] // Create a new team
        public async Task<ActionResult<TeamDto>> CreateTeamAsync(TeamInputModel teamInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            var newTeam = await _teamService.CreateTeamAsync(teamInputModel);

            if (newTeam == null)
            {
                throw new HomeVitalInvalidOperationException("Failed to create team.");
            }
            return Ok(newTeam);
        }

        [Authorize(Roles = "HealthcareWorker, Patient")]
        [HttpGet("{id}")] // Get a team by ID
        public async Task<ActionResult<TeamDto>> GetTeamByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }

            // Check if the team exists
            var existingTeam = await _teamService.GetTeamByIdAsync(id);
            if (existingTeam == null)
            {
                throw new ResourceNotFoundException("Team not found with this ID: " + id);
            }
            return Ok(existingTeam);
        }

        [Authorize(Roles = "HealthcareWorker")]
        [HttpGet] // Get all teams
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAllTeamsAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
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
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            // Check if the team exists
            var existingTeam = await _teamService.GetTeamByIdAsync(id);
            if (existingTeam == null)
            {
                throw new ResourceNotFoundException("Team not found with this ID: " + id);
            }

            var updatedTeam = await _teamService.UpdateTeamAsync(id, teamInputModel);
            return Ok(updatedTeam);
        }

        [Authorize(Roles = "HealthcareWorker")]
        [HttpDelete("{id}")] // Delete a team by ID
        public async Task<ActionResult> DeleteTeamAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException(ModelState.RetrieveErrorString());
            }
            // Check if the team exists before attempting to delete
            var existingTeam = await _teamService.GetTeamByIdAsync(id);
            if (existingTeam == null)
            {
                throw new ResourceNotFoundException("Team not found with this ID: " + id);
            }

            // Call the service to delete the team and retrieve the deleted team's info
            await _teamService.DeleteTeamAsync(id);
            // Return the deleted team's info
            // Note: You might want to return a specific response or the deleted team info
            return Ok(existingTeam);
        }
    }
}