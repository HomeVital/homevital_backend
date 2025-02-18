using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using HomeVital.API;
using HomeVital.Models.Dtos;

namespace HomeVital.Tests
{
    public class UnitTestPatients : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UnitTestPatients(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        // test get patients 
        [Fact]
        public async Task TestGetPatients()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/patients");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var patients = JsonConvert.DeserializeObject<List<PatientDto>>(responseString);

            // Assert
            Assert.NotNull(patients);
            Assert.NotEmpty(patients);
            
        }
    }
}