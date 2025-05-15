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
using HomeVital.Models.Enums;

namespace HomeVital.Tests
{
    public class BloodPressureTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BloodPressureTests(WebApplicationFactory<Program> factory)
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

        // get blood pressure measurements by patient id
        [Fact]
        public async Task TestBloodPressureMeasurementsByPatientId()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync("/api/bloodpressure/13");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var bloodPressureMeasurements = JsonConvert.DeserializeObject<IEnumerable<BloodPressureDto>>(content);
            Assert.NotNull(bloodPressureMeasurements);
        }

        // POST blood pressure measurement for each Measurement Status "Normal", "Raised", "High"
        [Theory]
        [InlineData(115, 70, 70, "Normal")]
        [InlineData(126, 85, 75, "Raised")]
        [InlineData(160, 90, 80, "High")]
        public async Task PostBloodPressureMeasurement_ShouldReturnExpectedResult(
            int systolic, int diastolic, int pulse, string status)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);


            // Arrange
            var inputModel = new
            {
                MeasuredHand = MeasuredHand.Left, 
                BodyPosition = BodyPosition.Sitting, 
                Systolic = systolic,
                Diastolic = diastolic,
                Pulse = pulse
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(inputModel),
                System.Text.Encoding.UTF8,
                "application/json"
            );


            // Act
            var response = await client.PostAsync("/api/bloodpressure/13", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent);
            // Log the response content for debugging
            // assert that the response contains the expected status
            
            var bloodPressureMeasurement = JsonConvert.DeserializeObject<BloodPressureDto>(responseContent);
            Assert.NotNull(bloodPressureMeasurement);
            Assert.Equal(systolic, bloodPressureMeasurement.Systolic);
            Assert.Equal(diastolic, bloodPressureMeasurement.Diastolic);
            Assert.Equal(pulse, bloodPressureMeasurement.Pulse);
            Assert.Equal(status, bloodPressureMeasurement.Status);
        }

        // PATCH blood pressure measurement for each Measurement Status "Normal", "Raised", "High"
        [Theory]
        [InlineData(115, 70, 70, "Normal")]
        [InlineData(126, 85, 75, "Raised")]
        [InlineData(160, 90, 80, "High")]
        public async Task PatchBloodPressureMeasurement_ShouldReturnExpectedResult(
            int systolic, int diastolic, int pulse, string status)
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Step 1: Create a new blood pressure measurement
        
                // Arrange
            var inputModel = new
            {
                MeasuredHand = MeasuredHand.Left, 
                BodyPosition = BodyPosition.Sitting, 
                Systolic = 120,
                Diastolic = 80,
                Pulse = 72
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(inputModel),
                System.Text.Encoding.UTF8,
                "application/json"
            );



            var createResponse = await client.PostAsync("/api/bloodpressure/13", jsonContent);
            createResponse.EnsureSuccessStatusCode();
            var createResponseContent = await createResponse.Content.ReadAsStringAsync();
            var createdMeasurement = JsonConvert.DeserializeObject<BloodPressureDto>(createResponseContent);
            Assert.NotNull(createdMeasurement);
            var Patient_ID = createdMeasurement.PatientID;

            // Step 2: Retrieve the created measurement
            var getResponse = await client.GetAsync($"/api/bloodpressure/{Patient_ID}");


            getResponse.EnsureSuccessStatusCode();
            var getContent = await getResponse.Content.ReadAsStringAsync();
            // its a list of blood pressure measurements
            var bloodPressureMeasurements = JsonConvert.DeserializeObject<IEnumerable<BloodPressureDto>>(getContent);
            Assert.NotNull(bloodPressureMeasurements);


            // Step 3: Prepare the patch request
            var PatchinputModel = new BloodPressureInputModel
            {
                Systolic = systolic,
                Diastolic = diastolic,
                Pulse = pulse
            };
            var cjsonContent = new StringContent(
                JsonConvert.SerializeObject(PatchinputModel),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            // Get the ID of the first measurement in the list
            var measurementId = bloodPressureMeasurements.First().ID;

            // Act
            var response = await client.PatchAsync($"/api/bloodpressure/{measurementId}", cjsonContent);

            // Log the response
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(responseContent);

            var bloodPressureMeasurement_res = JsonConvert.DeserializeObject<BloodPressureDto>(responseContent);
            Assert.NotNull(bloodPressureMeasurement_res);
            Assert.Equal(systolic, bloodPressureMeasurement_res.Systolic);
            Assert.Equal(diastolic, bloodPressureMeasurement_res.Diastolic);
            Assert.Equal(pulse, bloodPressureMeasurement_res.Pulse);
            Assert.Equal(status, bloodPressureMeasurement_res.Status);
        }
        // DELETE blood pressure measurement
        [Fact]
        public async Task DeleteBloodPressureMeasurement_ShouldReturnSuccess()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Create a new blood pressure measurement
            var newMeasurement = new BloodPressureInputModel
            {
                Systolic = 120, // Example systolic value
                Diastolic = 80 // Example diastolic value
            };

            var createResponse = await client.PostAsJsonAsync("/api/bloodpressure/13", newMeasurement);
            createResponse.EnsureSuccessStatusCode();
            var createContent = await createResponse.Content.ReadAsStringAsync();
            var createdMeasurement = JsonConvert.DeserializeObject<BloodPressureDto>(createContent);
            Assert.NotNull(createdMeasurement);
            var id = createdMeasurement.ID;

            // Ensure the measurement is less than 1 day old
            var createdAt = createdMeasurement.Date; // Assuming Date is part of BloodPressureDto
            Assert.NotNull(createdAt);
            Assert.True((DateTime.UtcNow - createdAt).TotalDays < 1, "The measurement is not less than 1 day old.");

            // Act
            var deleteResponse = await client.DeleteAsync($"/api/bloodpressure/{id}");
            deleteResponse.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, deleteResponse.StatusCode);
        }
        
        


    }
}