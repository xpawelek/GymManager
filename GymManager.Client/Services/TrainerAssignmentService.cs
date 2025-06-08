using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using GymManager.Shared.DTOs.Admin;
using GymManager.Shared.DTOs.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Client.Services
{
    using ACreateDto = GymManager.Shared.DTOs.Admin.CreateTrainerAssignmentDto;
    using AUpdateDto = GymManager.Shared.DTOs.Admin.UpdateTrainerAssignmentsDto;
    using MCreateDto = GymManager.Shared.DTOs.Member.CreateTrainerAssignmentDto;

    public class TrainerAssignmentService
    {
        private readonly HttpClient _http;

        public TrainerAssignmentService(HttpClient http)
        {
            _http = http;
        }

        // [GET] /api/trainer-assignments
        // Admin
        public async Task<List<ReadTrainerAssignmentDto>?> GetAllAdminAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadTrainerAssignmentDto>>("api/trainer-assignments");
        }

        // [GET] /api/trainer-assignments/self
        // Member
        public async Task<ReadSelfTrainerAssignmentDto?> GetMemberSelfAsync()
        {
            return await _http.GetFromJsonAsync<ReadSelfTrainerAssignmentDto>("api/trainer-assignments/self");
        }
        
        // [GET] /api/trainer-assignments/{id}
        // Admin
        public async Task<ReadTrainerAssignmentDto?> GetByMemberIdAdminAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadTrainerAssignmentDto>($"api/trainer-assignments/{id}");
        }

        // [get] api/trainer-assignments/ever-assigned
        // member
        public async Task<bool> HasEverBeenAssignedAsync()
        {
            return await _http.GetFromJsonAsync<bool>("api/trainer-assignments/ever-assigned");
        }

        // [get] api/trainer-assignments/has-active
        // member
        public async Task<bool> HasActiveAssignmentAsync()
        {
            return await _http.GetFromJsonAsync<bool>("api/trainer-assignments/has-active");
        }

        // [POST] /api/trainer-assignments
        // Admin
        public async Task<ReadTrainerAssignmentDto?> CreateAdminAsync(ACreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/trainer-assignments", dto);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ReadTrainerAssignmentDto>() 
                : null;
        }

        // [PATCH] /api/trainer-assignments/{id}
        // Admin
        public async Task<bool> PatchAdminAsync(int id, AUpdateDto dto)
        {
            var response = await _http.PatchAsJsonAsync($"api/trainer-assignments/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        // [DELETE] /api/trainer-assignments/{id}
        // Admin
        public async Task<bool> DeleteAdminAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/trainer-assignments/{id}");
            return response.IsSuccessStatusCode;
        }

        // [POST] /api/trainer-assignments/self
        // Member
        public async Task<int?> CreateSelfAsync(MCreateDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var response = await _http.PostAsync("api/trainer-assignments/self", new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
            return content?["id"];
        }

        // [GET] /api/trainer-assignments/me
        // Trainer
        public async Task<List<ReadTrainerAssignmentDto>?> GetAllTrainerAsync()
        {
            return await _http.GetFromJsonAsync<List<ReadTrainerAssignmentDto>>("api/trainer-assignments/me");
        }

        // [GET] /api/trainer-assignments/me/{id}
        // Trainer
        public async Task<ReadTrainerAssignmentDto?> GetByIdTrainerAsync(int id)
        {
            return await _http.GetFromJsonAsync<ReadTrainerAssignmentDto>($"api/trainer-assignments/me/{id}");
        }
    }
}
