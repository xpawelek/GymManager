using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using Microsoft.AspNetCore.Components.Forms;

namespace GymManager.Client.Services
{
    public class EquipmentService
    {
        private readonly HttpClient _http;

        public EquipmentService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/equipment/public
        // Dostęp publiczny
        public async Task<List<ReadEquipmentDto>?> GetPublicAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadEquipmentDto>>("api/equipment/public");
        }

        // [GET] /api/equipment
        // Admin
        public async Task<List<ReadEquipmentDto>?> GetAllAdminAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadEquipmentDto>>("api/equipment");
        }

        // [GET] /api/equipment/{id}
        // Admin
        public async Task<ReadEquipmentDto?> GetByIdAdminAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadEquipmentDto>($"api/equipment/{id}");
        }

        // [POST] /api/equipment
        // Admin / Receptionist
        public async Task<ReadEquipmentDto?> CreateAsync(CreateEquipmentDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/equipment", dto);
            return await response.Content.ReadFromJsonAsync<ReadEquipmentDto>();
        }

        // [PATCH] /api/equipment/{id}
        // Admin
        public async Task<bool> PatchAsync(int id, UpdateEquipmentDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/equipment/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/equipment/{id}
        // Admin
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/equipment/{id}");
            return response.IsSuccessStatusCode;
        }

        // [GET] /api/equipment/self
        // Member
        public async Task<List<ReadEquipmentDto>?> GetAllMemberAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadEquipmentDto>>("api/equipment/self");
        }

        // [GET] /api/equipment/self/{id}
        // Member
        public async Task<ReadEquipmentDto?> GetByIdMemberAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadEquipmentDto>($"api/equipment/self/{id}");
        }

        // [GET] /api/equipment/me
        // Trainer
        public async Task<List<ReadEquipmentDto>?> GetAllTrainerAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadEquipmentDto>>("api/equipment/me");
        }

        // [GET] /api/equipment/me/{id}
        // Trainer
        public async Task<ReadEquipmentDto?> GetByIdTrainerAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadEquipmentDto>($"api/equipment/me/{id}");
        }

        public async Task<bool> uploadPhotoAsync(int id, IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024));
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
    
            content.Add(fileContent, "file", file.Name);
    
            var response = await _http.PostAsync("api/equipment/{id}/upload-photo", content);
            return response.IsSuccessStatusCode;
        }

        
    }
}
