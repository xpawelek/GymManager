using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Shared.DTOs.Member;

using AReadDto = GymManager.Shared.DTOs.Admin.ReadTrainerDto;
using TReadDto = GymManager.Shared.DTOs.Trainer.ReadTrainerDto;
using MReadDto = GymManager.Shared.DTOs.Member.ReadTrainerDto;
using Microsoft.AspNetCore.Components.Forms;

namespace GymManager.Client.Services
{
    public class TrainerService
    {
        private readonly HttpClient _http;

        public TrainerService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/trainers
        // Admin
        public async Task<List<AReadDto>?> GetAllAdminAsync()
        {
            return await _http.GetFromJsonAsync<List<AReadDto>>("api/trainers");
        }

        // [GET] /api/trainers/{id}
        // Admin
        public async Task<AReadDto?> GetByIdAdminAsync(int id)
        {
            return await _http.GetFromJsonAsync<AReadDto>($"api/trainers/{id}");
        }
        
        // [POST] /api/auth/admin/register-trainer
        // Admin
        public async Task<string?> RegisterTrainerAsync(RegisterTrainerDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/admin/register-trainer", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync(); 
        }

        // [POST] /api/trainers/{id}/upload-photo
        // Admin + trainer
        public async Task<string?> UploadPhotoAdminAsync(int trainerId, IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var stream = new StreamContent(file.OpenReadStream(10 * 1024 * 1024)); // max 10MB ???
            stream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
            content.Add(stream, "file", file.Name);

            var response = await _http.PostAsync($"api/trainers/{trainerId}/upload-photo", content);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync();
        }

        // [PATCH] /api/trainers/{id}
        // Admin
        public async Task<bool> UpdateAdminAsync(int id, UpdateTrainerDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/trainers/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/trainers/{id}
        // Admin
        public async Task<bool> DeleteAdminAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/trainers/{id}");
            return response.IsSuccessStatusCode;
        }

        // [GET] /api/trainers/public
        // Member
        public async Task<List<MReadDto>?> GetAllMemberAsync()
        {
            return await _http.GetFromJsonAsync<List<MReadDto>>("api/trainers/public");
        }

        // [GET] /api/trainers/public/{id}
        // Member
        public async Task<MReadDto?> GetByIdMemberAsync(int id)
        {
            return await _http.GetFromJsonAsync<MReadDto>($"api/trainers/public/{id}");
        }

        // [GET] /api/trainers/me
        // Trainer
        public async Task<TReadDto?> GetMyProfileAsync()
        {
            return await _http.GetFromJsonAsync<TReadDto>("api/trainers/me");
        }

        // [PATCH] /api/trainers/me
        // Trainer
        public async Task<bool> UpdateMyProfileAsync(UpdateSelfTrainerDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync("api/trainers/me", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
