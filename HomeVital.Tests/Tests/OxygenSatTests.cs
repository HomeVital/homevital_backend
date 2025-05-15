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
    public class OxygenSatTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public OxygenSatTests(WebApplicationFactory<Program> factory)
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

        // get oxygen saturation measurements by patient id
        [Fact]
        public async Task TestOxygenSaturationMeasurementsByPatientId()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync("/api/oxygensaturation/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var measurements = JsonConvert.DeserializeObject<List<OxygenSaturationDto>>(content);
            Assert.NotNull(measurements);
        }

        // POST oxygen saturation measurement and get 3 different Statuses : "Normal", "Raised", "High"
        [Theory]
        [InlineData(96, "Normal")]
        [InlineData(93, "Raised")]
        [InlineData(89, "High")]
        public async Task TestPostOxygenSaturationMeasurement(int oxygenSaturation, string expectedStatus)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var measurement = new OxygenSaturationInputModel
            {
                OxygenSaturationValue = oxygenSaturation,
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/oxygensaturation/1", measurement);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OxygenSaturationDto>(content);
            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
        }

        // PATCH oxygen saturation measurement 3 different Statuses : "Normal", "Raised", "High"
        [Theory]
        [InlineData(90, "High")]
        [InlineData(93, "Raised")]
        [InlineData(89, "High")]
        public async Task TestPatchOxygenSaturationMeasurement(int oxygenSaturation, string expectedStatus)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var measurement = new OxygenSaturationInputModel
            {
                OxygenSaturationValue = oxygenSaturation,
            };

            // Patch uses the ID of the measurement to update get newest measurement 
            var response = await client.GetAsync("/api/oxygensaturation/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var measurements = JsonConvert.DeserializeObject<List<OxygenSaturationDto>>(content);
            Assert.NotNull(measurements);
            var measurementToUpdate = measurements[0];
            var id = measurementToUpdate.ID;
            var patchResponse = await client.PatchAsJsonAsync($"/api/oxygensaturation/{id}", measurement);




            // Assert
            patchResponse.EnsureSuccessStatusCode();
            var patchContent = await patchResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OxygenSaturationDto>(patchContent);
            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
            Assert.Equal(oxygenSaturation, result.OxygenSaturationValue);
        }

        // DELETE oxygen saturation measurement
        [Fact]
        public async Task TestDeleteOxygenSaturationMeasurement()
        {
            await WaitForDatabaseAsync();
            
            // for id get most recent measurement

            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var response = await client.GetAsync("/api/oxygensaturation/1");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var measurements = JsonConvert.DeserializeObject<List<OxygenSaturationDto>>(content);
            Assert.NotNull(measurements);
            var measurement = measurements[0];
            var id = measurement.ID;

            // Act
            var _response = await client.DeleteAsync($"/api/oxygensaturation/{id}");

            // Assert
            _response.EnsureSuccessStatusCode();

            // Check if the measurement is deleted
            var getResponse = await client.GetAsync($"/api/oxygensaturation/{id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);

        }

    }
}
        


