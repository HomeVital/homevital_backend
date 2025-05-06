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

namespace HomeVital.Tests
{
    public class PatientTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PatientTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public class ApiResponse<T>
        {
            public T Data { get; set; }
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

            

            

        // Test an endpoint that returns a list of patients
        [Fact]
        public async Task TestPatientsControllerGet()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.GetAsync("/api/patients");
            
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var actualPatients = JsonConvert.DeserializeObject<ApiResponse<List<Patient>>>(responseString);
            var patients = actualPatients.Data;
            // Assert
            Assert.NotNull(patients);
            Assert.IsType<List<Patient>>(patients);
            Assert.NotEmpty(patients);
        }

        // Test getting a single patient by ID (Testing getting patient with id 6)
        [Fact]
        public async Task TestPatientsControllerGetById()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.GetAsync("/api/patients/6");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualPatient = JsonConvert.DeserializeObject<Patient>(responseString);

            // Assert
            Assert.NotNull(actualPatient);
            Assert.IsType<Patient>(actualPatient);
            Assert.Equal(6, actualPatient.ID);
        }

        // Test creating a new patient
        [Fact]
        public async Task TestPatientsControllerPost()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var newPatient = new Patient { Name = "Test Patient", Phone = "123456789", Status = "Active", Address = "123 Main St", TeamID = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(newPatient), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/patients", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualPatient = JsonConvert.DeserializeObject<Patient>(responseString);

            // Assert
            Assert.NotNull(actualPatient);
            Assert.IsType<Patient>(actualPatient);
            Assert.Equal("Test Patient", actualPatient.Name);
        }

        // Test updating a patient (patch)
        [Fact]
        public async Task TestPatientsControllerPatch()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            var updatedPatient = new Patient { Name = "Updated Patient", Phone = "123456789", Status = "Active", Address = "123 Main St", TeamID = 1 };
            var content = new StringContent(JsonConvert.SerializeObject(updatedPatient), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PatchAsync("/api/patients/5", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualPatient = JsonConvert.DeserializeObject<Patient>(responseString);

            // Assert
            Assert.NotNull(actualPatient);
            Assert.IsType<Patient>(actualPatient);
            Assert.Equal("Updated Patient", actualPatient.Name);
        }

        // Test deleting a patient check if patient with id 6 
        [Fact]
        public async Task TestPatientsControllerDelete()
        {
            await WaitForDatabaseAsync();

            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.DeleteAsync("/api/patients/6");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualPatient = JsonConvert.DeserializeObject<Patient>(responseString);

            // Assert
            Assert.NotNull(actualPatient);
            Assert.IsType<Patient>(actualPatient);
            Assert.Equal(6, actualPatient.ID);

            // check if patient with id 6 is deleted
            response = await client.GetAsync("/api/patients/6");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}