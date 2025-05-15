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
    public class BodyWeightTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BodyWeightTests(WebApplicationFactory<Program> factory)
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

        // get weight measurements by patient id
        [Fact]
        public async Task TestWeightMeasurementsByPatientId()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync("/api/bodyweight/1");
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            var weightMeasurements = JsonConvert.DeserializeObject<List<BodyWeightDto>>(content);
            Assert.NotNull(weightMeasurements);
        }
        // create weight measurement
        [Theory]
        [InlineData(1, 70.0,"Normal")]
        [InlineData(1, 71.0,"Normal")]
        [InlineData(1, 80.0,"High")]
        public async Task TestCreateWeightMeasurement(int patientId, float weight, string status)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var bodyWeightInputModel = new BodyWeightInputModel
            {
                Weight = weight
            };

            // Act
            var response = await client.PostAsJsonAsync($"/api/bodyweight/{patientId}", bodyWeightInputModel);
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            var createdBodyWeight = JsonConvert.DeserializeObject<BodyWeightDto>(content);
            Assert.NotNull(createdBodyWeight);
            Assert.Equal(weight, createdBodyWeight.Weight);
            Assert.Equal(status, createdBodyWeight.Status);
        }

        // PATCH weight measurement for each Measurement Status "Normal", "Raised", "High"
        [Theory]
        [InlineData(60.0f, "High")]
        [InlineData(85.0f, "High")]
        [InlineData(110.0f, "High")]
        public async Task TestPatchWeightMeasurement(float weight, string expectedStatus)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Get the newest weight measurement ID
            var response = await client.GetAsync("/api/bodyweight/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var weightMeasurements = JsonConvert.DeserializeObject<List<BodyWeightDto>>(content);
            Assert.NotNull(weightMeasurements);
            var measurementToUpdate = weightMeasurements[0];
            var id = measurementToUpdate.ID;

            // Prepare the updated weight measurement
            var weightMeasurementToUpdate = new BodyWeightInputModel
            {
                Weight = weight,
            };

            // Act
            var patchResponse = await client.PatchAsJsonAsync($"/api/bodyweight/{id}", weightMeasurementToUpdate);
            patchResponse.EnsureSuccessStatusCode();
            var patchContent = await patchResponse.Content.ReadAsStringAsync();
            var updatedWeightMeasurement = JsonConvert.DeserializeObject<BodyWeightDto>(patchContent);

            // Assert
            Assert.NotNull(updatedWeightMeasurement);
            Assert.Equal(expectedStatus, updatedWeightMeasurement.Status);
            Assert.Equal(weight, updatedWeightMeasurement.Weight);
        }

        // PATCH weight measurement for  Measurement Status "Normal"
        [Theory]
        [InlineData(70.0f, "Normal")]
        [InlineData(71.0f, "Normal")]
        [InlineData(70.6f, "Normal")]
        public async Task TestPatchWeightMeasurementNormal(float weight, string expectedStatus)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Get the newest weight measurement ID
            var response = await client.GetAsync("/api/bodyweight/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var weightMeasurements = JsonConvert.DeserializeObject<List<BodyWeightDto>>(content);
            Assert.NotNull(weightMeasurements);
            var measurementToUpdate = weightMeasurements[0];
            var id = measurementToUpdate.ID;

            // Prepare the updated weight measurement
            var weightMeasurementToUpdate = new BodyWeightInputModel
            {
                Weight = weight,
            };

            // Act
            var patchResponse = await client.PatchAsJsonAsync($"/api/bodyweight/{id}", weightMeasurementToUpdate);
            patchResponse.EnsureSuccessStatusCode();
            var patchContent = await patchResponse.Content.ReadAsStringAsync();
            var updatedWeightMeasurement = JsonConvert.DeserializeObject<BodyWeightDto>(patchContent);

            // Assert
            Assert.NotNull(updatedWeightMeasurement);
            Assert.Equal(expectedStatus, updatedWeightMeasurement.Status);
            Assert.Equal(weight, updatedWeightMeasurement.Weight);
        }

        // Delete weight measurement
        [Fact]
        public async Task TestDeleteWeightMeasurement()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Create a new weight measurement
            var newMeasurement = new BodyWeightInputModel
            {
                Weight = 75.0f // Example weight
            };

            var createResponse = await client.PostAsJsonAsync("/api/bodyweight/8", newMeasurement);
            createResponse.EnsureSuccessStatusCode();
            var createContent = await createResponse.Content.ReadAsStringAsync();
            var createdMeasurement = JsonConvert.DeserializeObject<BodyWeightDto>(createContent);
            Assert.NotNull(createdMeasurement);
            var id = createdMeasurement.ID;

            // Ensure the measurement is less than 1 day old
            var createdAt = createdMeasurement.Date; // Assuming Date is part of BodyWeightDto
            Assert.NotNull(createdAt);
            Assert.True((DateTime.UtcNow - createdAt).TotalDays < 1, "The measurement is not less than 1 day old.");

            // Act
            var deleteResponse = await client.DeleteAsync($"/api/bodyweight/{id}");
            deleteResponse.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, deleteResponse.StatusCode);
        }

    }
}