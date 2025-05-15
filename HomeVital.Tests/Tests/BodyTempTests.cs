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
    public class BodyTempTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BodyTempTests(WebApplicationFactory<Program> factory)
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

        // get body temperature measurements by patient id
        [Fact] 
        public async Task TestBodyTemperatureMeasurementsByPatientId()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync("/api/bodytemperature/" + Constants.PatientId);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var bodyTemperatures = JsonConvert.DeserializeObject<List<BodyTemperatureDto>>(content);
            Assert.NotNull(bodyTemperatures);
        }

        // POST body temperature measurement for each Measurement Status "Normal", "Raised", "High"
        [Theory]
        [InlineData(35.5f, "Raised")]
        [InlineData(36f, "Normal")]
        [InlineData(37.5f, "Raised")]
        [InlineData(38f, "Raised")]
        [InlineData(39f, "High")]
        public async Task TestPostBodyTemperatureMeasurement(double temperature, string status)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var bodyTemperatureInput = new BodyTemperatureInputModel
            {
                Temperature = (float)temperature
            };

            // Act
            var response = await client.PostAsJsonAsync($"/api/bodytemperature/{Constants.PatientId}", bodyTemperatureInput);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var bodyTemperature = JsonConvert.DeserializeObject<BodyTemperatureDto>(content);
            Assert.NotNull(bodyTemperature);
            Assert.Equal(temperature, bodyTemperature.Temperature);
            Assert.Equal(status, bodyTemperature.Status);
        }

        // PATCH body temperature measurement for each Measurement Status "Normal", "Raised", "High"
        [Theory]
        [InlineData(35.5f, "Raised")]
        [InlineData(36f, "Normal")]
        [InlineData(37.5f, "Raised")]
        [InlineData(38f, "Raised")]
        [InlineData(39f, "High")]
        public async Task TestPatchBodyTemperatureMeasurement(double temperature, string status)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var Temp = new BodyTemperatureInputModel
            {
                Temperature = 37.0f
            };
            // create a new body temperature measurement
            var postResponse = await client.PostAsJsonAsync($"/api/bodytemperature/{Constants.PatientId}", Temp);
            postResponse.EnsureSuccessStatusCode();
            var postContent = await postResponse.Content.ReadAsStringAsync();
            var bodyTemperature_Mes = JsonConvert.DeserializeObject<BodyTemperatureDto>(postContent);
            Assert.NotNull(bodyTemperature_Mes);
            var measurementID = bodyTemperature_Mes.ID;
            Assert.True(measurementID > 0); 

            var bodyTemperatureInput = new BodyTemperatureInputModel
            {
                Temperature = (float)temperature
            };
            // Act
            var response = await client.PatchAsJsonAsync($"/api/bodytemperature/{measurementID}", bodyTemperatureInput);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var bodyTemperature = JsonConvert.DeserializeObject<BodyTemperatureDto>(content);
            Assert.NotNull(bodyTemperature);
            Assert.Equal(temperature, bodyTemperature.Temperature);
            Assert.Equal(status, bodyTemperature.Status);
        }

        // DELETE body temperature measurement
        [Fact]
        public async Task TestDeleteBodyTemperatureMeasurement()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var Temp = new BodyTemperatureInputModel
            {
                Temperature = 37.0f
            };
            // create a new body temperature measurement
            var postResponse = await client.PostAsJsonAsync($"/api/bodytemperature/{Constants.PatientId}", Temp);
            postResponse.EnsureSuccessStatusCode();
            var postContent = await postResponse.Content.ReadAsStringAsync();
            var bodyTemperature_Mes = JsonConvert.DeserializeObject<BodyTemperatureDto>(postContent);
            Assert.NotNull(bodyTemperature_Mes);
            var measurementID = bodyTemperature_Mes.ID;
            Assert.True(measurementID > 0); 

            // Act
            var response = await client.DeleteAsync($"/api/bodytemperature/{measurementID}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
