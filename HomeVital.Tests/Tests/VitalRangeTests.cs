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

// This test class is used to test the Patch Vital range endpoints in the HomeVital API

namespace HomeVital.Tests
{
    public class VitalTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public VitalTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }


        private async Task WaitForDatabaseAsync(int timeout = 100000, int pollingInterval = 1000)
        {
            var startTime = DateTime.UtcNow;
            // delay until the database is ready query patients until we get a response
            while ((DateTime.UtcNow - startTime).TotalMilliseconds < timeout)
            {
                try
                {
                    var client = _factory.CreateClient();
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

        // Test get Vital range for patient ID
        [Fact]
        public async Task TestGetVitalRange()
        {
            // Wait for the database to be ready
            await WaitForDatabaseAsync();

            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/vitalrange/1");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<VitalRangeDto>(content);

            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1.5, "Raised")]     // Below BloodSugarLowered
        [InlineData(3.5, "Normal")]    // Between BloodSugarLowered and BloodSugarGood
        [InlineData(5.5, "Raised")]   // Between BloodSugarGood and BloodSugarRaised
        [InlineData(9.0, "High")]    // Above BloodSugarHigh
        [InlineData(165.0, "High")]   // Above BloodSugarHigh
        public async Task TestBloodSugarRanges(float bloodSugarLevel, string expectedStatus)
        {
            // Wait for the database to be ready
            await WaitForDatabaseAsync();

            var client = _factory.CreateClient();
            var bloodSugarInputModel = new BloodsugarInputModel
            {
                BloodsugarLevel = bloodSugarLevel
            };

            var content = new StringContent(JsonConvert.SerializeObject(bloodSugarInputModel), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/bloodsugar/1", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BloodsugarDto>(responseString);

            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
        }

        [Theory]
        [InlineData(35.5f, "High")]            // Below TemperatureUnderAverage
        [InlineData(36.0f, "Normal")]         // Equal to TemperatureGood
        [InlineData(37.5f, "Raised")]        // Between TemperatureNotOk and TemperatureCritical
        [InlineData(38.5f, "High")]         // Above TemperatureCritical
        [InlineData(80.0f, "High")]         // abnormal input
        public async Task TestBodyTemperatureRanges(float temperature, string expectedStatus)
        {
            // Wait for the database to be ready
            await WaitForDatabaseAsync();

            var client = _factory.CreateClient();
            var bodyTemperatureInputModel = new BodyTemperatureInputModel
            {
                Temperature = temperature
            };
            var content = new StringContent(JsonConvert.SerializeObject(bodyTemperatureInputModel), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/bodytemperature/1", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BodyTemperatureDto>(responseString);

            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
        }

        [Theory]
        [InlineData(70.0f, "Normal")]         // Normal weight
        [InlineData(73.0f, "Normal")]        // Normal weight
        [InlineData(75.2f, "High")]         // Above normal weight
        [InlineData(77.0f, "High")]        // Above normal weight
        [InlineData(73.4f, "Normal")]  // Skoða þetta
        public async Task TestBodyWeightRanges(float newWeight, string expectedStatus)
        {
            // Wait for the database to be ready
            await WaitForDatabaseAsync();

            var client = _factory.CreateClient();
           
            // Update the patient's weight
            var weightInputModel = new BodyWeightInputModel
            {
                Weight = newWeight
            };

            var weightContent = new StringContent(JsonConvert.SerializeObject(weightInputModel), System.Text.Encoding.UTF8, "application/json");
            var weightResponse = await client.PostAsync($"/api/bodyweight/21", weightContent);
            weightResponse.EnsureSuccessStatusCode();

            var weightResponseString = await weightResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BodyWeightDto>(weightResponseString);

            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
        }

        // Test Oxygen saturation ranges
        [Theory]
        [InlineData(96, "Normal")]         // Above OxygenSaturationGood
        [InlineData(94, "Raised")]        // Between OxygenSaturationRaised and OxygenSaturationGood
        [InlineData(92, "Raised")]       // Between OxygenSaturationHigh and OxygenSaturationRaised
        [InlineData(90, "High")]        // Below OxygenSaturationHigh
        [InlineData(55, "High")]        // Below OxygenSaturationHigh
        public async Task TestOxygenSaturationRanges(int oxygenSaturation, string expectedStatus)
        {
            // Wait for the database to be ready
            await WaitForDatabaseAsync();

            var client = _factory.CreateClient();
            var oxygenInputModel = new OxygenSaturationInputModel
            {
                OxygenSaturationValue = oxygenSaturation
            };

            var content = new StringContent(JsonConvert.SerializeObject(oxygenInputModel), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/oxygensaturation/1", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OxygenSaturationDto>(responseString);

            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
        }

        [Theory]
        [InlineData(85, 55, "Raised")]          // Below SystolicLowered and DiastolicLowered
        [InlineData(115, 75, "Normal")]        // Within SystolicGood and DiastolicGood
        [InlineData(135, 91, "Raised")]       // Between SystolicRaised/DiastolicRaised and SystolicGood/DiastolicGood
        [InlineData(155, 95, "High")]        // Between SystolicHigh/DiastolicHigh and SystolicRaised/DiastolicRaised
        [InlineData(165, 105, "High")]      // Above SystolicHigh and DiastolicHigh
        [InlineData(110, 105, "High")]     // Between SystolicGood and DiastolicHigh
        public async Task TestBloodPressureRanges(int systolic, int diastolic, string expectedStatus)
        {
            // Wait for the database to be ready
            await WaitForDatabaseAsync();

            var client = _factory.CreateClient();
            var bloodPressureInputModel = new BloodPressureInputModel
            {
                Systolic = systolic,
                Diastolic = diastolic
            };

            var content = new StringContent(JsonConvert.SerializeObject(bloodPressureInputModel), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/bloodpressure/1", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BloodPressureDto>(responseString);

            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
        }
    }

}