using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using HomeVital.API;
using HomeVital.Models.Entities;
using HomeVital.Models.Dtos;
using HomeVital.Models.InputModels;
using HomeVital.Tests.Utils;


namespace HomeVital.Tests
{
    public class TeamTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TeamTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        public class ApiResponse<T>
        {
            public T Data { get; set; }
        }

        private async Task WaitForDatabaseAsync(int timeout = 10000, int pollingInterval = 500)
        {
            var startTime = DateTime.UtcNow;
            while ((DateTime.UtcNow - startTime).TotalMilliseconds < timeout)
            {
                try
                {
                    var client = _factory.CreateClient();
                    // get auth token
                    var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                    var response = await client.GetAsync("/api/patients");
                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                }
                catch
                {
                    // Ignore exceptions and retry
                }
                await Task.Delay(pollingInterval);
            }
            throw new TimeoutException("Database was not ready within the timeout period.");
        }

    
        // Test an endpoint that returns a list of teams
        [Fact]
        public async Task TestTeamControllerGetAllTeams()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            // Debug: Ensure token is not null or empty
            Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync("/api/teams");

            // Debug: Log response status code if not successful
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Response status code: {response.StatusCode}, Content: {errorContent}");
            }

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<TeamDto>>(content);
            Assert.NotNull(teams);
            Assert.NotEmpty(teams);
            // teams should be 5 at startup with data initialization
            Assert.Equal(5, teams.Count);
        }

        // Test an endpoint that returns a team by ID
        [Fact]

        public async Task TestTeamControllerGetById()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync("/api/teams/1");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<TeamDto>(content);

            // Assert
            Assert.NotNull(team);
            Assert.IsType<TeamDto>(team);
            Assert.Equal(1, team.ID);

        }

        // Test an Post endpoint that creates a new team (input model needs name and team leader)
        [Fact]
        public async Task TestTeamControllerPost()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var newTeam = new TeamInputModel
            {
                Name = "Test Team",
                TeamLeaderID = 1,
            };

            var jsonContent = JsonConvert.SerializeObject(newTeam);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/teams", content);
            response.EnsureSuccessStatusCode();

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var createdTeam = JsonConvert.DeserializeObject<TeamDto>(responseString);
            Assert.NotNull(createdTeam);
            Assert.IsType<TeamDto>(createdTeam);
            Assert.Equal("Test Team", createdTeam.Name);
            Assert.Equal(1, createdTeam.TeamLeaderID);

        }

        // Test an endpoint that updates a team (patch) (update name or team leader or both)
        [Fact]
        public async Task TestTeamControllerPatch()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var updatedTeam = new TeamInputModel
            {
                Name = "Updated Team",
                TeamLeaderID = 2,
            };

            var jsonContent = JsonConvert.SerializeObject(updatedTeam);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PatchAsync("/api/teams/1", content);
            response.EnsureSuccessStatusCode();

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var updatedTeamResponse = JsonConvert.DeserializeObject<TeamDto>(responseString);
            Assert.NotNull(updatedTeamResponse);
            Assert.IsType<TeamDto>(updatedTeamResponse);
            Assert.Equal("Updated Team", updatedTeamResponse.Name);
            Assert.Equal(2, updatedTeamResponse.TeamLeaderID);
        }

        // Test an endpoint that deletes a team
        // [Fact]
        // public async Task TestTeamControllerDelete()
        // {
        //     await WaitForDatabaseAsync();

        //     // Arrange
        //     var client = _factory.CreateClient();
        //     var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
        //     client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

        //     // Get the team to be deleted
        //     var getResponse = await client.GetAsync("/api/teams/1");
        //     getResponse.EnsureSuccessStatusCode();
        //     var getContent = await getResponse.Content.ReadAsStringAsync();
        //     var teamToDelete = JsonConvert.DeserializeObject<TeamDto>(getContent);
        //     Assert.NotNull(teamToDelete);
        //     // check what workers are in the team and what patients are in the team and check if the status for team is updated
        //     var workersInTeam = teamToDelete.WorkerIDs;
        //     var patientsInTeam = teamToDelete.PatientIDs;
        //     Assert.NotNull(workersInTeam);
        //     Assert.NotNull(patientsInTeam);

        //     // Act
        //     var response = await client.DeleteAsync("/api/teams/1");
        //     response.EnsureSuccessStatusCode();

        //     // Assert
        //     var content = await response.Content.ReadAsStringAsync();
        //     Assert.Equal("{}", content);

        //     // check if the team is deleted
        //     var checkResponse = await client.GetAsync("/api/teams/1");
        //     Assert.Equal(System.Net.HttpStatusCode.NotFound, checkResponse.StatusCode);
            
        //     // check if the workers in the team are updated
        //     var checkWorkersResponse = await client.GetAsync("/api/healthcareworkers/1");
        //     checkWorkersResponse.EnsureSuccessStatusCode();
        //     var checkWorkersContent = await checkWorkersResponse.Content.ReadAsStringAsync();
        //     var updatedWorker = JsonConvert.DeserializeObject<HealthcareWorker>(checkWorkersContent);
        //     // check that the workers teamID's list does not contain the deleted teamID 1 
            
        //     // Extract the IDs from the worker's Teams collection
        //     var teamIds = updatedWorker.Teams.Select(team => team.ID).ToHashSet();

        //     // Check that the TeamID 1 is not in the worker's Teams
        //     Assert.DoesNotContain(1, teamIds);

        //     // check if the patients in the team are updated and that their TeamID is set to 0
        //     var checkPatientsResponse = await client.GetAsync("/api/patients/1");
        //     checkPatientsResponse.EnsureSuccessStatusCode();
        //     var checkPatientsContent = await checkPatientsResponse.Content.ReadAsStringAsync();
        //     var updatedPatient = JsonConvert.DeserializeObject<Patient>(checkPatientsContent);
        //     // check that the patients TeamID is set to 0
        //     Assert.Equal(0, updatedPatient.TeamID);

        // }

    }
}
