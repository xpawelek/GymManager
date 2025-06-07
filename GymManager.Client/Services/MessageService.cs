using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Trainer;

using MReadDto = GymManager.Shared.DTOs.Member.ReadSelfMessageDto;
using TReadDto = GymManager.Shared.DTOs.Trainer.ReadSelfMessageDto;

namespace GymManager.Client.Services
{
    public class MessageService
    {
        private readonly HttpClient _http;

        public MessageService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/messages
        // Member
        public async Task<List<MReadDto>?> GetAllAsMemberAsync()
        {
            return await _http.GetFromJsonAsync<List<MReadDto>>("api/messages");
        }

        // [GET] /api/messages
        // Trainer
        public async Task<List<TReadDto>?> GetAllAsTrainerAsync()
        {
            return await _http.GetFromJsonAsync<List<TReadDto>>("api/messages");
        }

        // [GET] /api/messages/{id}
        // Member
        public async Task<MReadDto?> GetByIdAsMemberAsync(int id)
        {
            return await _http.GetFromJsonAsync<MReadDto>($"api/messages/{id}");
        }

        // [GET] /api/messages/{id}
        // Trainer
        public async Task<TReadDto?> GetByIdAsTrainerAsync(int id)
        {
            return await _http.GetFromJsonAsync<TReadDto>($"api/messages/{id}");
        }

        // [POST] /api/messages
        // Member
        public async Task<MReadDto?> CreateAsMemberAsync(CreateMessageDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PostAsync("api/messages", new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadFromJsonAsync<MReadDto>();
        }

        // [POST] /api/messages
        // Trainer
        public async Task<TReadDto?> CreateAsTrainerAsync(CreateSelfMessageDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PostAsync("api/messages", new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadFromJsonAsync<TReadDto>();
        }

        // [PATCH] /api/messages/{id}
        // Member
        public async Task<bool> PatchAsMemberAsync(int id, UpdateMessageDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync($"api/messages/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        // [PATCH] /api/messages/{id}
        // Trainer
        public async Task<bool> PatchAsTrainerAsync(int id, UpdateSelfMessageDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync($"api/messages/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
