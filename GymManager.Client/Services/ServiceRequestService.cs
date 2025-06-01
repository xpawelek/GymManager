using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Trainer;

using AReadDto = GymManager.Shared.DTOs.Admin.ReadServiceRequestDto;
using AUpdateDto = GymManager.Shared.DTOs.Admin.UpdateServiceRequestDto;
using ACreateDto = GymManager.Shared.DTOs.Admin.CreateServiceRequestDto;

namespace GymManager.Client.Services
{
    public class ServiceRequestService
    {
        private readonly HttpClient _http;

        public ServiceRequestService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/service-requests
        // Admin
        public async Task<List<AReadDto>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<AReadDto>>("api/service-requests");
        }

        // [GET] /api/service-requests/{id}
        // Admin
        public async Task<AReadDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<AReadDto>($"api/service-requests/{id}");
        }

        // [POST] /api/service-requests
        // Admin
        public async Task<AReadDto?> CreateAsAdminAsync(ACreateDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PostAsync("api/service-requests", new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadFromJsonAsync<AReadDto>();
        }

        // [POST] /api/service-requests
        // Member
        public async Task<bool> CreateAsMemberAsync(GymManager.Shared.DTOs.Member.CreateServiceRequestDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PostAsync("api/service-requests", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        // [POST] /api/service-requests
        // Trainer
        public async Task<bool> CreateAsTrainerAsync(GymManager.Shared.DTOs.Trainer.CreateServiceRequestDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PostAsync("api/service-requests", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        // [PATCH] /api/service-requests/{id}
        // Admin
        public async Task<bool> PatchAsync(int id, AUpdateDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/service-requests/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/service-requests/{id}
        // Admin
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/service-requests/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
