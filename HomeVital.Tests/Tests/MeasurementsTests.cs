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
    public class MeasurementsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MeasurementsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public class ApiResponse<T>
        {
            public T Data { get; set; }
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

        // Endpoints for theses tests
        // GET 
        // /api/measurements/{patientId}/bodytemperature/latest/{count}
        // GET
        // /api/measurements/{patientId}
        // GET
        // /api/measurements/warnings
        // GET
        // /api/measurements/patient/{patientId}/warnings
        // POST
        // {
        //     "measurementType": "string",
        //     "measurementID": 0,
        //     "workerID": 0,
        //     "resolutionNotes": "string"
        // }   
        // /api/measurements/acknowledge
        // POST
        // {
        // "measurementType": "string",
        // "measurementID": 0
        // }
        // /api/measurements/saga

        // GET /api/measurements/{patientId}/bodytemperature/latest/{count}
        [Fact]
        public async Task TestGetLatestBodyTemperatureMeasurements()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync($"/api/measurements/{Constants.PatientId}/latest/5");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<BodyTemperatureDto>>(content);
            Assert.NotNull(result);
        }

        // GET /api/measurements/{patientId} 
        // has page size and page number 
        [Fact]
        public async Task TestGetBodyTemperatureMeasurementsByPatientId()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.PatientKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync($"/api/measurements/{Constants.PatientId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<List<MeasurementDto>>>(content);
            Assert.NotNull(result);
        }

        // GET /api/measurements/warnings
        // Get all warnings
        // Has page size and page number and teamIDs array[integer]
        // Test page size = 35 and page number = 1
        [Fact]
        public async Task TestGetAllWarnings()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync($"/api/measurements/warnings?pageSize=35&pageNumber=1");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<List<MeasurementDto>>>(content);
            Assert.NotNull(result);
            // assert that the page size is 35
            Assert.Equal(35, result.Data.Count);
           
        }

        // GET /api/measurements/warnings
        // Get all warnings
        // Has page size and page number and teamIDs array[integer]
        // Test page size = 35 and page number = 1 and search by teamID
        [Fact]
        public async Task TestGetAllWarningsByTeamId()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync($"/api/measurements/warnings?pageSize=35&pageNumber=1&teamIDs=2");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<List<MeasurementDto>>>(content);
            Assert.NotNull(result);
            // assert that the page size is 35
            Assert.Equal(35, result.Data.Count);
            // assert that the teamID is 1 for all the measurements
            foreach (var dto in result.Data)
            {
                Assert.All(dto.Measurements, measurement => Assert.Equal(1, measurement.TeamID));
            }
        }

        // GET /api/measurements/patient/{patientId}/warnings
        // Get all warnings by patient id
        // check if its the correct patient id
        [Fact]
        public async Task TestGetAllWarningsByPatientId()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var authToken = await AuthSetup.GetAuthTokenAsync(client, Constants.WorkerKennitala);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            // Act
            var response = await client.GetAsync($"/api/measurements/patient/{Constants.PatientId}/warnings");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<MeasurementDto>>(content); // Deserialize as a list
            Assert.NotNull(result);

            // Assert that the patient ID is the same as the one in the URL
            foreach (var dto in result)
            {
                Assert.All(dto.Measurements, measurement => Assert.Equal(Constants.PatientId, measurement.PatientID));
            }
            
        }


    }
}
