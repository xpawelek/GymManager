using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
using GymManager.Shared.DTOs.Trainer;
using Microsoft.AspNetCore.Components.Forms;

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
        // Admin / Trainer
        public async Task<List<AReadDto>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<AReadDto>>("api/progress-photos");
        }

        // [GET] /api/progress-photos (member)
        public async Task<List<MReadDto>?> GetMineAsync()
        {
            return await _http.GetFromJsonAsync<List<MReadDto>>("api/progress-photos");
        }
        
        public async Task<List<Shared.DTOs.Admin.ReadProgressPhotoDto>?> GetAllAsAdminAsync()
        {
            return await _http.GetFromJsonAsync<List<Shared.DTOs.Admin.ReadProgressPhotoDto>>("api/progress-photos");
        }

        // [GET] /api/progress-photos/public
        // Public + wszystkie role
        public async Task<List<MReadDto>?> GetPublicAsync()
        {
            return await _http.GetFromJsonAsync<List<MReadDto>>("api/progress-photos/public");
        }
        
        // [get] dla trainera
        public async Task<List<MReadDto>?> GetAssignedMembersPhotosAsync()
        {
            return await _http.GetFromJsonAsync<List<MReadDto>>("api/progress-photos/assigned-members");
        }

        // [GET] /api/progress-photos/{id}
        public async Task<MReadDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<MReadDto>($"api/progress-photos/{id}");
        }

        // [POST] /api/progress-photos
        public async Task<MReadDto?> CreateAsMemberAsync(CreateProgressPhotoDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/progress-photos", dto);
            return await response.Content.ReadFromJsonAsync<MReadDto>();
        }

        // [PATCH] /api/progress-photos/{id} (admin)
        public async Task<bool> PatchAsAdminAsync(int id, AUpdateDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync($"api/progress-photos/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        // [PATCH] /api/progress-photos/{id} (member)
        public async Task<bool> PatchAsMemberAsync(int id, GymManager.Shared.DTOs.Member.UpdateProgressPhotoDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PatchAsync($"api/progress-photos/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/progress-photos/{id}
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/progress-photos/{id}");
            return response.IsSuccessStatusCode;
        }

        // [POST] /api/progress-photos/upload-photo
        public async Task<string?> UploadFileAsync(IBrowserFile file)
        {
            try
            {
                var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(file.OpenReadStream(10 * 1024 * 1024));
                content.Add(fileContent, "file", file.Name);

                var response = await _http.PostAsync("api/progress-photos/upload-photo", content);
                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return result?["path"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Upload error: {ex.Message}");
                return null;
            }
        }

        public async Task<MReadDto?> CreateFromUploadedAsync(CreateProgressPhotoDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/progress-photos", dto);
            return await response.Content.ReadFromJsonAsync<MReadDto>();
        }
    }
}
