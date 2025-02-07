
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using HomeVital.API;

namespace HomeVital.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void TestCalcAdd()
        {
            // Arrange
            var calc = new Calc();
            int a = 5;
            int b = 3;
            int expected = 8;

            // Act
            int result = calc.Add(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestUsersControllerRegister()
        {
            // Arrange
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject("testUser"), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/user/register", content);
            // console.log the response
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            
            System.Console.WriteLine(responseString);
            // Assert
            Assert.Equal("wohoow testUser", responseString);
        }
    }
}
