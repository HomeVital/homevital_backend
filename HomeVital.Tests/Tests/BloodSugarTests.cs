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
using System.Net.Http.Json;

namespace HomeVital.Tests
{
    public class BloodSugarTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BloodSugarTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private async Task WaitForDatabaseAsync(int timeout = 10000, int pollingInterval = 1500)
        {
            var startTime = DateTime.UtcNow;
            while ((DateTime.UtcNow - startTime).TotalMilliseconds < timeout)
            {
                try
                {
                    var client = _factory.CreateClient();
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

        // get blood sugar measurements by patient id
        [Fact]
        public async Task TestBloodSugarMeasurementsByPatientId()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync("/api/bloodsugar/5");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var bloodSugarMeasurements = JsonConvert.DeserializeObject<List<BloodsugarDto>>(content);
            Assert.NotNull(bloodSugarMeasurements);
        }

        // POST blood sugar measurement for each Measurement Status "Normal", "Raised", "High"
        [Theory]
        [InlineData(3.9f, "Normal")]
        [InlineData(5.6f, "Raised")]
        [InlineData(10.0f, "High")]
        public async Task TestPostBloodSugarMeasurement(float bloodSugarLevel, string expectedStatus)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var bloodSugarMeasurement = new BloodsugarInputModel
            {
                BloodsugarLevel = bloodSugarLevel,
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/bloodsugar/5", bloodSugarMeasurement);
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BloodsugarDto>(content);
            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
        }

        // PATCH blood sugar measurement for each Measurement Status "Normal", "Raised", "High"
        [Theory]
        [InlineData(3.9f, "Normal")]
        [InlineData(1.6f, "Raised")]
        [InlineData(6.0f, "Raised")]
        [InlineData(9.0f, "High")]
        public async Task TestPatchBloodSugarMeasurement(float bloodSugarLevel, string expectedStatus)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            
            // get the newest blood sugar measurement id
            var response = await client.GetAsync("/api/bloodsugar/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var bloodSugarMeasurements = JsonConvert.DeserializeObject<List<BloodsugarDto>>(content);
            Assert.NotNull(bloodSugarMeasurements);
            var measurementToUpdate = bloodSugarMeasurements[0];
            var id = measurementToUpdate.ID;
            var bloodSugarMeasurementToUpdate = new BloodsugarInputModel
            {
                BloodsugarLevel = bloodSugarLevel,
            };

            var patchResponse = await client.PatchAsJsonAsync($"/api/bloodsugar/{id}", bloodSugarMeasurementToUpdate);
            patchResponse.EnsureSuccessStatusCode();
            var patchContent = await patchResponse.Content.ReadAsStringAsync();
            var bloodSugarMeasurement = JsonConvert.DeserializeObject<BloodsugarDto>(patchContent);
            Assert.NotNull(bloodSugarMeasurement);
            Assert.Equal(expectedStatus, bloodSugarMeasurement.Status);
            Assert.Equal(bloodSugarLevel, bloodSugarMeasurement.BloodsugarLevel);
        }

        // DELETE blood sugar measurement 
        [Fact]
        public async Task TestDeleteBloodSugarMeasurement()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // get the newest blood sugar measurement id
            var response = await client.GetAsync("/api/bloodsugar/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var bloodSugarMeasurements = JsonConvert.DeserializeObject<List<BloodsugarDto>>(content);
            Assert.NotNull(bloodSugarMeasurements);
            var measurementToDelete = bloodSugarMeasurements[0];
            var id = measurementToDelete.ID;
            // Act
            var deleteResponse = await client.DeleteAsync($"/api/bloodsugar/{id}");
            deleteResponse.EnsureSuccessStatusCode();
            // Assert
            var deleteContent = await deleteResponse.Content.ReadAsStringAsync();
            var deleteResult = JsonConvert.DeserializeObject<BloodsugarDto>(deleteContent);
            Assert.NotNull(deleteResult);
            Assert.Equal(measurementToDelete.ID, deleteResult.ID);
            Assert.Equal(measurementToDelete.BloodsugarLevel, deleteResult.BloodsugarLevel);
        }
    }
}