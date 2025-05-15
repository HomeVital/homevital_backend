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
    public class PatientPlanTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PatientPlanTests(WebApplicationFactory<Program> factory)
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

        // Test an endpoint that returns a list of patient plans
        [Fact]
        public async Task TestPatientPlansControllerGet()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            // Debug: Ensure token is not null or empty
            Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);


            // Act /api/patientplans/patient/{patientId}
            var response = await client.GetAsync("/api/patientplans/patient/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PatientPlanDto>>(content);
            Assert.NotNull(result);
        }


        // POST a new patient plan
        [Fact]
        public async Task TestPatientPlansControllerPost()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            // Debug: Ensure token is not null or empty
            Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var newPlan = new PatientPlanInputModel
            {
                PatientID = 3,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Instructions = "Follow the instructions",
                WeightMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
                BloodSugarMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
                BloodPressureMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
                OxygenSaturationMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
                BodyTemperatureMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
                TeamID = 3 // Assuming you have a team with ID 1
            };
            var jsonContent = JsonConvert.SerializeObject(newPlan);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/patientplans", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var actualPlan = JsonConvert.DeserializeObject<PatientPlanDto>(responseString);
            Assert.NotNull(actualPlan);
        }

        // Test creating 2 new patient plans and checking if they are created correctly the newest one should be the one who has isActive = true
        // and the others one should be isActive = false
        // [Fact]
        // public async Task TestPatientPlansControllerPostMultiple()
        // {
        //     await WaitForDatabaseAsync();

        //     // Arrange
        //     var client = _factory.CreateClient();
        //     var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
        //     // Debug: Ensure token is not null or empty
        //     Assert.False(string.IsNullOrEmpty(authToken), "Auth token is null or empty");
        //     client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

        //     var newPlan1 = new PatientPlanInputModel
        //     {
        //         PatientID = 1,
        //         StartDate = DateTime.UtcNow.AddDays(-10),
        //         EndDate = DateTime.UtcNow.AddDays(10),
        //         Instructions = "Follow the instructions",
        //         WeightMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         BloodSugarMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         BloodPressureMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         OxygenSaturationMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         BodyTemperatureMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         TeamID = 1 // Assuming you have a team with ID 1
        //     };

        //     var newPlan2 = new PatientPlanInputModel
        //     {
        //         PatientID = 1,
        //         StartDate = DateTime.UtcNow.AddDays(-10),
        //         EndDate = DateTime.UtcNow.AddDays(20),
        //         Instructions = "Follow the instructions",
        //         WeightMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         BloodSugarMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         BloodPressureMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         OxygenSaturationMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         BodyTemperatureMeasurementDays = new int[7] { 1, 0, 0, 0, 0, 0, 0 },
        //         TeamID = 1 // Assuming you have a team with ID 1
        //     };

        //     var jsonContent1 = JsonConvert.SerializeObject(newPlan1);
        //     var content1 = new StringContent(jsonContent1, System.Text.Encoding.UTF8, "application/json");
        //     var jsonContent2 = JsonConvert.SerializeObject(newPlan2);
        //     var content2 = new StringContent(jsonContent2, System.Text.Encoding.UTF8, "application/json");

        //     // Act
        //     var response1 = await client.PostAsync("/api/patientplans", content1);
        //     var response2 = await client.PostAsync("/api/patientplans", content2);

        //     // Assert
        //     response1.EnsureSuccessStatusCode();
        //     var responseString1 = await response1.Content.ReadAsStringAsync();
        //     var actualPlan1 = JsonConvert.DeserializeObject<PatientPlanDto>(responseString1);
        //     Console.WriteLine($"First Plan IsActive: {actualPlan1.IsActive}");
        //     Assert.NotNull(actualPlan1);
        //     Assert.False(actualPlan1.IsActive, "The first plan should be inactive.");

        //     response2.EnsureSuccessStatusCode();
        //     var responseString2 = await response2.Content.ReadAsStringAsync();
        //     var actualPlan2 = JsonConvert.DeserializeObject<PatientPlanDto>(responseString2);
        //     Assert.NotNull(actualPlan2);
        //     Assert.True(actualPlan2.IsActive, "The second plan should be active.");
        //     Console.WriteLine($"Second Plan IsActive: {actualPlan2.IsActive}");

        //     Assert.Equal(actualPlan1.PatientID, actualPlan2.PatientID);
        //     Assert.Equal(actualPlan1.TeamID, actualPlan2.TeamID);
        // }
    }

}