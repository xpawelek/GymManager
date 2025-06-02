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
        // Admin / Member / Trainer
        public async Task<List<object>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<object>>("api/messages");
        }

        // [GET] /api/messages/{id}
        // Admin / Member / Trainer
        public async Task<object?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<object>($"api/messages/{id}");
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
