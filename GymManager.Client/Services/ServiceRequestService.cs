using System.Net.Http;
using System.Net.Http.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Trainer;

using AReadDto = GymManager.Shared.DTOs.Admin.ReadServiceRequestDto;
using AUpdateDto = GymManager.Shared.DTOs.Admin.UpdateServiceRequestDto;
using ACreateDto = GymManager.Shared.DTOs.Admin.CreateServiceRequestDto;
using MCreateDto = GymManager.Shared.DTOs.Member.CreateServiceRequestDto;
using TCreateDto = GymManager.Shared.DTOs.Trainer.CreateServiceRequestDto;

namespace GymManager.Client.Services
{
    public class ServiceRequestService
    {
        private readonly HttpClient _http;

        public ServiceRequestService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AReadDto>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<AReadDto>>("api/service-requests");
        }

        public async Task<AReadDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<AReadDto>($"api/service-requests/{id}");
        }

        public async Task<AReadDto?> CreateAsAdminAsync(ACreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/service-requests", dto);
            return await response.Content.ReadFromJsonAsync<AReadDto>();
        }

        public async Task<bool> CreateAsMemberAsync(MCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/service-requests/member", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateAsTrainerAsync(TCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/service-requests/trainer", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PatchAsync(int id, AUpdateDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/service-requests/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ToggleResolvedAsync(int id)
        {
            var response = await _http.PatchAsync($"api/service-requests/{id}/toggle", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/service-requests/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
