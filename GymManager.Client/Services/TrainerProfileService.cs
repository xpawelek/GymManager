using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Trainer;

namespace GymManager.Client.Services
{
    public class TrainerProfileService
    {
        private readonly HttpClient _http;

        public TrainerProfileService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/trainers/profile
        // Trainer
        public async Task<ReadTrainerDto?> GetMyProfileAsync()
        {
            return await _http.GetFromJsonAsync<ReadTrainerDto>("api/trainers/profile");
        }

        // [get]
        // member
        public async Task<ReadTrainerDto?> GetAssignedTrainerAsync()
        {
            return await _http.GetFromJsonAsync<ReadTrainerDto>("api/trainers/assigned-trainer");
        }

        // [get]
        // member
        public async Task<List<ReadTrainerDto>?> GetAllContactedAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadTrainerDto>>("api/trainers/contacted");
        }


        // [PATCH] /api/trainers/profile
        // Trainer
        public async Task<bool> UpdateMyProfileAsync(UpdateSelfTrainerDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync("api/trainers/profile", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
