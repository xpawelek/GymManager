using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using GymManager.Shared.DTOs.Auth;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Shared.DTOs.Receptionist;

namespace GymManager.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        

        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        // [POST] /api/auth/login
        // Public
        public async Task<bool> LoginAsync(LoginDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", dto);
            var json = JsonSerializer.Serialize(dto);
            Console.WriteLine($"POST to /api/auth/login: {json}");
            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result?.Token is null) return false;
            
            await _localStorage.SetItemAsync("authToken", result.Token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            

            return true;
        }
        
        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _http.DefaultRequestHeaders.Authorization = null;
        }

        // [POST] /api/auth/register-member
        // Public
        public async Task<bool> RegisterMemberAsync(RegisterMemberDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register-member", dto);
            return response.IsSuccessStatusCode;
        }

        // [POST] /api/auth/admin/register-member
        // Admin
        public async Task<bool> RegisterMemberAsAdminAsync(RegisterMemberDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/admin/register-member", dto);
            return response.IsSuccessStatusCode;
        }

        // [POST] /api/auth/receptionist/register-member
        // Receptionist
        public async Task<bool> RegisterMemberAsReceptionistAsync(RegisterMemberDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/receptionist/register-member", dto);
            return response.IsSuccessStatusCode;
        }

        // [POST] /api/auth/admin/register-trainer
        // Admin
        public async Task<bool> RegisterTrainerAsync(RegisterTrainerDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/admin/register-trainer", dto);
            return response.IsSuccessStatusCode;
        }

        // [POST] /api/auth/admin/register-receptionist
        // Admin
        public async Task<bool> RegisterReceptionistAsync(RegisterReceptionistDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/admin/register-receptionist", dto);
            return response.IsSuccessStatusCode;
        }

        // [GET] /api/auth/me
        // Any authenticated user
        public async Task<UserInfo?> GetCurrentUserAsync()
        {
            return await _http.GetFromJsonAsync<UserInfo>("api/auth/me");
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
    }

    public class UserInfo
    {
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
    }
    
    
}
