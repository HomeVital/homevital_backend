using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeVital.Tests.Utils
{
    public static class AuthSetup
    {
        
        public static async Task<string> GetAuthTokenAsync(HttpClient client, string Kennitala)
        {
            var loginData = new
            {
                Kennitala = Kennitala
            };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Send the request
            var response = await client.PostAsync("/api/user/generate-token", content);

            // Debug: Log response if not successful
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to fetch auth token. Status code: {response.StatusCode}, Content: {errorContent}");
            }

            
            // Parse the response
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

            // Debug: Ensure token is not null or empty
            if (!tokenResponse.TryGetValue("token", out var token) || string.IsNullOrEmpty(token))
            {
                throw new Exception("Auth token is null or empty. Response content: " + responseContent);
            }

            return token;
        }
    }
}