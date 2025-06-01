using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Trainer;

using MReadDto = GymManager.Shared.DTOs.Member.ReadProgressPhotoDto;
using AReadDto = GymManager.Shared.DTOs.Admin.ReadProgressPhotoDto;
using AUpdateDto = GymManager.Shared.DTOs.Admin.UpdateProgressPhotoDto;


namespace GymManager.Client.Services
{
    public class ProgressPhotoService
    {
        private readonly HttpClient _http;

        public ProgressPhotoService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/progress-photos
        // Admin / Member / Trainer
        public async Task<List<object>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<object>>("api/progress-photos");
        }

        // [GET] /api/progress-photos/public
        // Public + wszystkie role
        public async Task<List<object>?> GetPublicAsync()
        {
            return await _http.GetFromJsonAsync<List<object>>("api/progress-photos/public");
        }

        // [GET] /api/progress-photos/{id}
        // Admin / Member / Trainer
        public async Task<object?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<object>($"api/progress-photos/{id}");
        }

        // [POST] /api/progress-photos
        // Member
        public async Task<MReadDto?> CreateAsMemberAsync(CreateProgressPhotoDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/progress-photos", dto);
            return await response.Content.ReadFromJsonAsync<MReadDto>();
        }

        // [PATCH] /api/progress-photos/{id}
        // Admin
        public async Task<bool> PatchAsAdminAsync(int id, AUpdateDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync($"api/progress-photos/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        // [PATCH] /api/progress-photos/{id}
        // Member
        public async Task<bool> PatchAsMemberAsync(int id, GymManager.Shared.DTOs.Member.UpdateProgressPhotoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync($"api/progress-photos/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/progress-photos/{id}
        // Admin / Member
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/progress-photos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
