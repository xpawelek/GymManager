using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Trainer;

using AReadDto = GymManager.Shared.DTOs.Admin.ReadWorkoutNoteDto;
using MReadDto = GymManager.Shared.DTOs.Member.ReadSelfWorkoutNoteDto;
using TReadDto = GymManager.Shared.DTOs.Trainer.ReadSelfWorkoutNoteDto;
using TUpdateDto = GymManager.Shared.DTOs.Trainer.UpdateSelfWorkoutNoteDto;


namespace GymManager.Client.Services
{
    public class WorkoutNoteService
    {
        private readonly HttpClient _http;

        public WorkoutNoteService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/workout-notes
        // Admin
        public async Task<List<AReadDto>?> GetAllAdminAsync()
        {
            return await _http.GetFromJsonAsync<List<AReadDto>>("api/workout-notes");
        }

        // [GET] /api/workout-notes/{id}
        // Admin
        public async Task<AReadDto?> GetByIdAdminAsync(int id)
        {
            return await _http.GetFromJsonAsync<AReadDto>($"api/workout-notes/{id}");
        }

        // [GET] /api/workout-notes/self
        // Member
        public async Task<List<MReadDto>?> GetAllMemberAsync()
        {
            return await _http.GetFromJsonAsync<List<MReadDto>>("api/workout-notes/self");
        }

        // [GET] /api/workout-notes/self/{id}
        // Member
        public async Task<MReadDto?> GetByIdMemberAsync(int id)
        {
            return await _http.GetFromJsonAsync<MReadDto>($"api/workout-notes/self/{id}");
        }

        // [GET] /api/workout-notes/me
        // Trainer
        public async Task<List<TReadDto>?> GetAllTrainerAsync()
        {
            return await _http.GetFromJsonAsync<List<TReadDto>>("api/workout-notes/me");
        }

        // [GET] /api/workout-notes/me/{id}
        // Trainer
        public async Task<TReadDto?> GetByIdTrainerAsync(int id)
        {
            return await _http.GetFromJsonAsync<TReadDto>($"api/workout-notes/me/{id}");
        }

        // [PATCH] /api/workout-notes/me/{id}
        // Trainer
        public async Task<bool> PatchTrainerAsync(int id, TUpdateDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync(
                $"api/workout-notes/me/{id}",
                new StringContent(json, Encoding.UTF8, "application/json")
            );
            return response.IsSuccessStatusCode;
        }
    }
}
