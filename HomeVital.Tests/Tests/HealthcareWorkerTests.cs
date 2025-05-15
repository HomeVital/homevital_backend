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
    public class WorkerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public WorkerTest(WebApplicationFactory<Program> factory)
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
                    var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                    var response = await client.GetAsync("/api/healthcareworkers");
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

        [Fact]
        public async Task TestWorkersControllerGetById()
        {
            await WaitForDatabaseAsync();
            
            // Arrange
            var client = _factory.CreateClient();
            
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            // Debug: Ensure token is not null or empty
            Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync("/api/healthcareworkers/2");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            // Assert
            Assert.NotNull(actualWorker);
            Assert.IsType<HealthcareWorker>(actualWorker);
            Assert.Equal(2, actualWorker.ID);
        }

        [Fact]
        public async Task TestWorkersControllerGet()
        {
            await WaitForDatabaseAsync();
    
            // Arrange
            var client = _factory.CreateClient();

            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            // Debug: Ensure token is not null or empty
            Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            
            // Act
            var response = await client.GetAsync("/api/healthcareworkers");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<HealthcareWorker>>>(responseString);
            var actualWorkers = apiResponse.Data;

            // Assert
            Assert.NotNull(actualWorkers);
            Assert.IsType<List<HealthcareWorker>>(actualWorkers);
            Assert.NotEmpty(actualWorkers);
        }

        [Fact]
        public async Task TestWorkersControllerPost()
        {
            await WaitForDatabaseAsync();
            
            // Arrange
            var client = _factory.CreateClient();

            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            // Debug: Ensure token is not null or empty
            Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var newWorker = new HealthcareWorkerInputModel
            {
                Name = "Test Worker",
                Phone = "123456789",
                Kennitala = "1231231231",
                Status = "Active",
                TeamIDs = new List<int> { 1, 2 }
            };
            var content = new StringContent(JsonConvert.SerializeObject(newWorker), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/healthcareworkers", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            // Assert
            Assert.NotNull(actualWorker);
            Assert.IsType<HealthcareWorker>(actualWorker);
            Assert.Equal("Test Worker", actualWorker.Name);
        }

        [Fact]
        public async Task TestWorkersControllerPatch()
        {
            await WaitForDatabaseAsync();
            
            // get worker with id 2
            var client = _factory.CreateClient();

            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            // Debug: Ensure token is not null or empty
            Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var response = await client.GetAsync("/api/healthcareworkers/2");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var worker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            Assert.NotNull(worker);
            Assert.IsType<HealthcareWorker>(worker);
            Assert.Equal("Freyr Björgvin", worker.Name);

            // Arrange
            var updatedWorker = worker;
            updatedWorker.Name = "Updated Worker";
            var content = new StringContent(JsonConvert.SerializeObject(updatedWorker), System.Text.Encoding.UTF8, "application/json");

            // Act
            response = await client.PatchAsync("/api/healthcareworkers/2", content);
            response.EnsureSuccessStatusCode();

            responseString = await response.Content.ReadAsStringAsync();
            var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            // Assert
            Assert.NotNull(actualWorker);
            Assert.IsType<HealthcareWorker>(actualWorker);

            // get the updated worker to check if the changes were saved
            response = await client.GetAsync("/api/healthcareworkers/2");
            response.EnsureSuccessStatusCode();

            responseString = await response.Content.ReadAsStringAsync();
            actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            Assert.NotNull(actualWorker);
            Assert.IsType<HealthcareWorker>(actualWorker);
            Assert.Equal("Updated Worker", actualWorker.Name);
        }

        [Fact]
        public async Task TestWorkersControllerDelete()
        {
            await WaitForDatabaseAsync();
            
            // Arrange
            var client = _factory.CreateClient();

            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            // Debug: Ensure token is not null or empty
            Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            
            // Act
            var response = await client.DeleteAsync("/api/healthcareworkers/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            // Assert
            Assert.NotNull(actualWorker);
            Assert.IsType<HealthcareWorker>(actualWorker);
            Assert.Equal("Sigurmundur Davíð", actualWorker.Name);

            // get the deleted worker to check if the worker was deleted
            response = await client.GetAsync("/api/healthcareworkers/1");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
            responseString = await response.Content.ReadAsStringAsync();
            
            var expectedError = new { error = "Healthcare worker not found with ID: 1" };
            var actualError = JsonConvert.DeserializeObject<dynamic>(responseString);

            Assert.Equal(expectedError.error, (string)actualError.error);
        }
    }
}        