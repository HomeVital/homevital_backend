// using System.Collections.Generic;
// using System.Net.Http;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Newtonsoft.Json;
// using Xunit;
// using HomeVital.API;
// using HomeVital.Models.Entities;

// namespace HomeVital.Tests
// {
//     public class WorkerTest : IClassFixture<WebApplicationFactory<Program>>
//     {
//         private readonly WebApplicationFactory<Program> _factory;

//         public WorkerTest(WebApplicationFactory<Program> factory)
//         {
//             _factory = factory;
//         }
//         private async Task WaitForConditionAsync(Func<Task<bool>> condition, int timeout = 10000, int pollingInterval = 500)
//         {
//             var startTime = DateTime.UtcNow;
//             while ((DateTime.UtcNow - startTime).TotalMilliseconds < timeout)
//             {
//                 if (await condition())
//                 {
//                     return;
//                 }
//                 await Task.Delay(pollingInterval);
//             }
//             throw new TimeoutException("Condition was not met within the timeout period.");
//         }


//         /// Test getting a single worker by ID (Testing getting worker with id 6)
        
        
//         [Fact]
//         public async Task TestWorkersControllerGetById()
//         {
//             await Task.Delay(5000);
            
//             // Arrange
//             var client = _factory.CreateClient();
            
//             // Act
//             var response = await client.GetAsync("/api/healthcareworkers/2");
//             response.EnsureSuccessStatusCode();

//             var responseString = await response.Content.ReadAsStringAsync();
//             var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

//             // Assert
//             Assert.NotNull(actualWorker);
//             Assert.IsType<HealthcareWorker>(actualWorker);
//             Assert.Equal(2, actualWorker.ID);
//         }

//         // Test an endpoint that returns a list of workers
//         [Fact]
//         public async Task TestWorkersControllerGet()
//         {
//             await Task.Delay(5000);
            
//             // Arrange
//             var client = _factory.CreateClient();
            
//             // Act
//             var response = await client.GetAsync("/api/healthcareworkers");
//             response.EnsureSuccessStatusCode();

//             var responseString = await response.Content.ReadAsStringAsync();
//             var actualWorkers = JsonConvert.DeserializeObject<List<HealthcareWorker>>(responseString);

//             // Assert
//             Assert.NotNull(actualWorkers);
//             Assert.IsType<List<HealthcareWorker>>(actualWorkers);
//             Assert.NotEmpty(actualWorkers);

//         }

//         // Test creating a new worker
//         [Fact]
//         public async Task TestWorkersControllerPost()
//         {
//             await Task.Delay(5000);
//             // Arrange
//             var client = _factory.CreateClient();
//             var newWorker = new HealthcareWorker
//             {
//                 Name = "Test Worker",
//                 Phone = "123456789",
//                 Status = "Active",
//                 TeamID = 1
//             };
//             var content = new StringContent(JsonConvert.SerializeObject(newWorker), System.Text.Encoding.UTF8, "application/json");

//             // Act
//             var response = await client.PostAsync("/api/healthcareworkers", content);
//             response.EnsureSuccessStatusCode();

//             var responseString = await response.Content.ReadAsStringAsync();
//             var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

//             // Assert
//             Assert.NotNull(actualWorker);
//             Assert.IsType<HealthcareWorker>(actualWorker);
//             Assert.Equal("Test Worker", actualWorker.Name);
//         }

//         // Test updating a worker
//         [Fact]
//         public async Task TestWorkersControllerPatch()
//         {
//             await Task.Delay(5000);
            
//             // get worker with id 2
//             var client = _factory.CreateClient();
//             var response = await client.GetAsync("/api/healthcareworkers/2");
//             response.EnsureSuccessStatusCode();

//             var responseString = await response.Content.ReadAsStringAsync();
//             var worker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

//             Assert.NotNull(worker);
//             Assert.IsType<HealthcareWorker>(worker);
//             Assert.Equal("Jane Smith", worker.Name);

//             // Arrange
//             var updatedWorker = new HealthcareWorker
//             {
//                 Name = "Updated Worker",
//                 Phone = "987654321",
//                 Status = "Inactive",
//                 TeamID = 2
//             };
//             var content = new StringContent(JsonConvert.SerializeObject(updatedWorker), System.Text.Encoding.UTF8, "application/json");

//             // Act
//             response = await client.PatchAsync("/api/healthcareworkers/2", content);
//             response.EnsureSuccessStatusCode();

//             responseString = await response.Content.ReadAsStringAsync();
//             var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

//             // Assert
//             Assert.NotNull(actualWorker);
//             Assert.IsType<HealthcareWorker>(actualWorker);
//             Assert.Equal("Updated Worker", actualWorker.Name);

//             // get the updated worker to check if the changes were saved
//             response = await client.GetAsync("/api/healthcareworkers/2");
//             response.EnsureSuccessStatusCode();

//             responseString = await response.Content.ReadAsStringAsync();
//             actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

//             Assert.NotNull(actualWorker);
//             Assert.IsType<HealthcareWorker>(actualWorker);
//             // not equal to the original name
//             Assert.NotEqual("Jane Smith", actualWorker.Name);
//         }

//         // Test deleting a worker
//         [Fact]
//         public async Task TestWorkersControllerDelete()
//         {
//             await Task.Delay(5000);
            
//             // Arrange
//             var client = _factory.CreateClient();
            
//             // Act
//             var response = await client.DeleteAsync("/api/healthcareworkers/1");
//             response.EnsureSuccessStatusCode();

//             var responseString = await response.Content.ReadAsStringAsync();
//             var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

//             // Assert
//             Assert.NotNull(actualWorker);
//             Assert.IsType<HealthcareWorker>(actualWorker);
//             Assert.Equal("John Doe", actualWorker.Name);

//             // get the deleted worker to check if the worker was deleted
//             response = await client.GetAsync("/api/healthcareworkers/1");
//             responseString = await response.Content.ReadAsStringAsync();
//             actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

//             Assert.Null(actualWorker);
//     }
//     }
// }

        




using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using HomeVital.API;
using HomeVital.Models.Entities;

namespace HomeVital.Tests
{
    public class WorkerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public WorkerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private async Task WaitForDatabaseAsync(int timeout = 10000, int pollingInterval = 500)
        {
            var startTime = DateTime.UtcNow;
            while ((DateTime.UtcNow - startTime).TotalMilliseconds < timeout)
            {
                try
                {
                    var client = _factory.CreateClient();
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
            
            // Act
            var response = await client.GetAsync("/api/healthcareworkers");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualWorkers = JsonConvert.DeserializeObject<List<HealthcareWorker>>(responseString);

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
            var newWorker = new HealthcareWorker
            {
                Name = "Test Worker",
                Phone = "123456789",
                Status = "Active",
                TeamID = 1
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
            var response = await client.GetAsync("/api/healthcareworkers/2");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var worker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            Assert.NotNull(worker);
            Assert.IsType<HealthcareWorker>(worker);
            Assert.Equal("Jane Smith", worker.Name);

            // Arrange
            var updatedWorker = new HealthcareWorker
            {
                Name = "Updated Worker",
                Phone = "987654321",
                Status = "Inactive",
                TeamID = 2
            };
            var content = new StringContent(JsonConvert.SerializeObject(updatedWorker), System.Text.Encoding.UTF8, "application/json");

            // Act
            response = await client.PatchAsync("/api/healthcareworkers/2", content);
            response.EnsureSuccessStatusCode();

            responseString = await response.Content.ReadAsStringAsync();
            var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            // Assert
            Assert.NotNull(actualWorker);
            Assert.IsType<HealthcareWorker>(actualWorker);
            Assert.Equal("Updated Worker", actualWorker.Name);

            // get the updated worker to check if the changes were saved
            response = await client.GetAsync("/api/healthcareworkers/2");
            response.EnsureSuccessStatusCode();

            responseString = await response.Content.ReadAsStringAsync();
            actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            Assert.NotNull(actualWorker);
            Assert.IsType<HealthcareWorker>(actualWorker);
            // not equal to the original name
            Assert.NotEqual("Jane Smith", actualWorker.Name);
        }

        [Fact]
        public async Task TestWorkersControllerDelete()
        {
            await WaitForDatabaseAsync();
            
            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.DeleteAsync("/api/healthcareworkers/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            // Assert
            Assert.NotNull(actualWorker);
            Assert.IsType<HealthcareWorker>(actualWorker);
            Assert.Equal("John Doe", actualWorker.Name);

            // get the deleted worker to check if the worker was deleted
            response = await client.GetAsync("/api/healthcareworkers/1");
            responseString = await response.Content.ReadAsStringAsync();
            actualWorker = JsonConvert.DeserializeObject<HealthcareWorker>(responseString);

            Assert.Null(actualWorker);
        }
    }
}        