using Frontend.Models;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http) => _http = http;

        public async Task<AuthResponseDTO?> RegisterAsync(string email, string password, string displayName, bool isArtist)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", new
            {
                email,
                password,
                displayName,
                isArtist
            });

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {response.StatusCode}, Body: {body}");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
        }

        public async Task<AuthResponseDTO?> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", new
            {
                email,
                password
            });

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
        }
    }
}
